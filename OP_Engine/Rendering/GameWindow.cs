using System;
using System.Windows.Forms;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Inputs;
using OP_Engine.Logging;
using OP_Engine.Menus;
using OP_Engine.Scenes;
using OP_Engine.Sounds;
using OP_Engine.Time;
using OP_Engine.Enums;
using OP_Engine.Weathers;

using Menu = OP_Engine.Menus.Menu;

namespace OP_Engine.Rendering
{
    public class OP_Game
    {
        #region Variables

        public Game Game;
        public Form Form;
        public GameWindow Window;
        public SpriteBatch SpriteBatch;
        public GraphicsDeviceManager GraphicsManager;
        public ContentManager Content;
        public Logger Logger;
        public ScreenType ScreenType;
        
        public float Zoom = 1;
        public int BaseSize = 32;
        public bool Quit = false;
        public bool GameStarted;
        public bool Debugging;

        public float TileSize_X;
        public float TileSize_Y;

        public float MenuSize_X;
        public float MenuSize_Y;

        //Default resolution to desktop width/height
        public int ScreenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public int ScreenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        public FormWindowState LastWindowState = FormWindowState.Minimized;
        public bool IsResizeTickEnabled;
        public System.Timers.Timer ResizeTickTimer;

        #endregion

        #region Properties

        public Vector2 TileSize
        {
            get { return new Vector2(TileSize_X, TileSize_Y); }
        }

        public Vector2 MenuSize
        {
            get { return new Vector2(MenuSize_X, MenuSize_Y); }
        }

        public Point Resolution
        {
            get { return new Point(ScreenWidth, ScreenHeight); }
        }

        #endregion

        #region Constructors

        public OP_Game()
        {
            
        }

        #endregion

        #region Events

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            if (!IsResizeTickEnabled) { return; }

            //This fires after the window has been manually resized
            //and on release of title bar being clicked and held
            if (Form.WindowState == LastWindowState)
            {
                ResetScreen();
            }
        }

        private void GameForm_ResizeBegin(object sender, EventArgs e)
        {
            IsResizeTickEnabled = true;
            ResizeTickTimer.Enabled = true;
        }

        private void OnResizeTick(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!IsResizeTickEnabled)
            {
                return;
            }

            ResizeScenes();
            ResizeMenus();
            Game.Tick();

            ResizeTickTimer.Enabled = true;
        }

        private void GameForm_Resize(object sender, EventArgs e)
        {
            //This is a resize that occurs from using Maximize Window button
            //and does not fire from manual resizing of the window
            if (Form.WindowState != LastWindowState)
            {
                LastWindowState = Form.WindowState;

                if (Form.WindowState == FormWindowState.Maximized ||
                    Form.WindowState == FormWindowState.Normal)
                {
                    if (SceneManager.Scenes != null)
                    {
                        ResetScreen();
                    }
                }
            }
        }

        private void GameForm_ResizeEnd(object sender, EventArgs e)
        {
            IsResizeTickEnabled = false;
            ResizeTickTimer.Enabled = false;
        }

        #endregion

        #region Methods

        public virtual void Init(Game game, GameWindow window)
        {
            Logger.LogFile = Environment.CurrentDirectory + @"\CrashLog.txt";

            Game = game;
            Game.Exiting += OnExit;

            Application.EnableVisualStyles();

            GraphicsManager = new GraphicsDeviceManager(game)
            {
                PreferredBackBufferWidth = ScreenWidth,
                PreferredBackBufferHeight = ScreenHeight,
                GraphicsProfile = GraphicsProfile.HiDef
            };

            MenuSize_X = ScreenWidth / 32;
            MenuSize_Y = ScreenHeight / 32;

            Window = window;
            Window.Position = new Point(0, 0);
            Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);

            //Default windowed
            ScreenType = ScreenType.Windowed;
            Form.WindowState = FormWindowState.Maximized;

            ResizeTickTimer = new System.Timers.Timer(1) { SynchronizingObject = Form, AutoReset = false };
            ResizeTickTimer.Elapsed += OnResizeTick;
            Form.ResizeBegin += GameForm_ResizeBegin;
            Form.Resize += GameForm_Resize;
            Form.ResizeEnd += GameForm_ResizeEnd;
        }

        public virtual void Update(GameTime gameTime)
        {
            try
            {
                if (Window != null)
                {
                    if (Window.ClientBounds.Width > 0 &&
                        Window.ClientBounds.Height > 0)
                    {
                        if (!Form.Focused)
                        {
                            if (GameStarted &&
                                !TimeManager.Paused)
                            {
                                Menu main = MenuManager.GetMenu("Main");
                                if (main != null)
                                {
                                    main.Visible = true;
                                    main.Active = true;
                                }

                                TimeManager.Paused = true;
                            }

                            SoundManager.Paused = true;
                        }
                        else if (Form.Focused)
                        {
                            SoundManager.Paused = false;

                            InputManager.Update();
                            MenuManager.Update(Game, Content);
                            SceneManager.Update(Game, Content);
                            RenderingManager.Update();
                            WeatherManager.Update(Resolution, Color.White);
                        }
                    }
                    else
                    {
                        TimeManager.Paused = true;
                        SoundManager.Paused = true;
                    }

                    SoundManager.Update();
                }

                if (Quit)
                {
                    SoundManager.StopAll();
                    Game.Exit();
                }
            }
            catch (Exception e)
            {
                CrashHandler(e);
            }
        }

        public virtual void Draw()
        {
            RenderingManager.Draw(Window, GraphicsManager, SpriteBatch, new Point(ScreenWidth, ScreenHeight));
        }

        public virtual void ResetScreen()
        {
            if (Window.ClientBounds.Width > 0 &&
                Window.ClientBounds.Height > 0)
            {
                if (ScreenType == ScreenType.Fullscreen ||
                    ScreenType == ScreenType.BorderlessFullscreen)
                {
                    Window.Position = new Point(0, 0);

                    GraphicsManager.IsFullScreen = true;
                    GraphicsManager.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                    GraphicsManager.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

                    if (ScreenType == ScreenType.BorderlessFullscreen)
                    {
                        GraphicsManager.HardwareModeSwitch = false;
                        Form.WindowState = FormWindowState.Normal;
                        Window.AllowUserResizing = false;
                        Window.IsBorderless = true;
                    }
                    else
                    {
                        GraphicsManager.HardwareModeSwitch = true;
                    }
                }
                else if (ScreenType == ScreenType.Windowed)
                {
                    GraphicsManager.IsFullScreen = false;
                    GraphicsManager.PreferredBackBufferWidth = Window.ClientBounds.Width;
                    GraphicsManager.PreferredBackBufferHeight = Window.ClientBounds.Height;

                    Window.AllowUserResizing = true;
                    Window.IsBorderless = false;
                    GraphicsManager.HardwareModeSwitch = false;
                }

                ScreenWidth = GraphicsManager.PreferredBackBufferWidth;
                ScreenHeight = GraphicsManager.PreferredBackBufferHeight;

                GraphicsManager.ApplyChanges();

                if (RenderingManager.LightingRenderer != null)
                {
                    RenderingManager.LightingRenderer.RenderTarget = new RenderTarget2D(GraphicsManager.GraphicsDevice, ScreenWidth, ScreenHeight);
                    RenderingManager.AddLightingRenderer.RenderTarget = RenderingManager.LightingRenderer.RenderTarget;
                }

                ResolutionChange();

                if (!Form.Visible)
                {
                    Form.Visible = true;
                }
            }
        }

        public virtual void ResolutionChange()
        {
            ResetMenuSize();
            ResetTileSize();
            ResizeMenus();
            ResizeScenes();
        }

        public virtual void ResetMenuSize()
        {
            float width = ScreenWidth / 32;
            MenuSize_X = width;
            MenuSize_Y = width;
        }

        public virtual void ResetTileSize()
        {
            TileSize_X = BaseSize * Zoom;
            TileSize_Y = BaseSize * Zoom;
        }

        public virtual void ResizeMenus()
        {
            int count = MenuManager.Menus.Count;
            for (int i = 0; i < count; i++)
            {
                MenuManager.Menus[i].Resize(Resolution);
            }
        }

        public virtual void ResizeScenes()
        {
            int count = SceneManager.Scenes.Count;
            for (int i = 0; i < count; i++)
            {
                SceneManager.Scenes[i].Resize(Resolution);
            }
        }

        public virtual void CrashHandler(Exception e)
        {
            SoundManager.StopAll();
            Logger.Logs.Add(new Log(e.Source, e.Message, e.StackTrace));
            Game.Exit();
        }

        public virtual void OnExit(object sender, EventArgs e)
        {
            if (Logger.Logs.Count > 0)
            {
                Logger.WriteLog();
            }
        }

        #endregion
    }
}
