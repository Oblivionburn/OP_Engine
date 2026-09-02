using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OP_Engine.Menus;
using OP_Engine.Scenes;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;

namespace OP_Engine.Rendering
{
    public class RenderingManager : GameComponent
    {
        #region Variables

        public bool UsingDefaults;
        public static Lighting? Lighting;
        public Renderer? LightingRenderer;
        public Renderer? AddLightingRenderer;
        public Renderer? BufferRenderer;
        public Renderer? FinalRenderer;

        public List<Renderer> Renderers = [];

        #endregion

        #region Constructors

        public RenderingManager(Game game) : base(game)
        {
            game.Exiting += Game_Exiting;
        }

        #endregion

        #region Methods

        public virtual void InitDefaults(GraphicsDeviceManager graphicsManager, Point resolution)
        {
            Lighting = new Lighting();

            LightingRenderer = new Renderer
            {
                Name = "Lighting",
                SetRenderTarget_BeforeDraw = true,
                ClearGraphics_BeforeDraw = true,
                ClearRenderTarget_AfterDraw = true,
                BlendState = BlendState.Additive
            };
            LightingRenderer.Init(graphicsManager, resolution);

            AddLightingRenderer = new Renderer
            {
                Name = "Add Lighting to World",
                RenderTarget = LightingRenderer.RenderTarget,
                DrawToRenderTarget = true,
                BlendState = new BlendState
                {
                    AlphaBlendFunction = BlendFunction.Add,
                    AlphaSourceBlend = Blend.One,
                    AlphaDestinationBlend = Blend.One,
                    ColorBlendFunction = BlendFunction.Add,
                    ColorSourceBlend = Blend.DestinationColor,
                    ColorDestinationBlend = Blend.Zero
                }
            };

            UsingDefaults = true;
        }

        public virtual void Update()
        {
            if (UsingDefaults)
            {
                LightingRenderer?.Update();
            }

            int count = Renderers.Count;
            for (int i = 0; i < count; i++)
            {
                Renderers[i].Update();
            }
        }

        public virtual void Draw(GameWindow? window, GraphicsDeviceManager? graphicsManager, SpriteBatch? spriteBatch, Point resolution)
        {
            if (window == null ||
                spriteBatch == null ||
                graphicsManager == null ||
                LightingRenderer == null ||
                Lighting == null ||
                AddLightingRenderer == null ||
                BufferRenderer?.RenderTarget == null ||
                FinalRenderer?.RenderTarget == null)
            {
                return;
            }

            //Don't bother drawing if the window is minimized
            if (window.ClientBounds.Width == 0 ||
                window.ClientBounds.Height == 0)
            {
                return;
            }

            if (UsingDefaults)
            {
                /*
                    This is a very basic setup for rendering whatever's currently visible in SceneManager,
                        applying deferred lighting on the scene, and then rendering menus ontop of that.

                    Can just not run InitDefaults() if you want to use your own custom rendering.
                */

                //Set ambient light in case the color changed
                LightingRenderer.GraphicsClearColor = Lighting.DrawColor;

                //Render lighting
                LightingRenderer.Draw(spriteBatch, resolution);

                //=================================
                // Draw world to Buffer
                //---------------------------------
                graphicsManager.GraphicsDevice.SetRenderTarget(BufferRenderer.RenderTarget);
                graphicsManager.GraphicsDevice.Clear(Color.Black);

                //Render world
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
                SceneManager.Draw_WorldsOnly(spriteBatch, resolution, Color.White);
                spriteBatch.End();

                //Add lighting to world
                AddLightingRenderer.Draw(spriteBatch, resolution);

                //Render world with no lighting applied
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
                SceneManager.Draw_WorldsOnly(spriteBatch, resolution); 
                spriteBatch.End();
                //---------------------------------
                // End of drawing to Buffer
                //=================================

                //Apply shaders
                ApplyShaders(BufferRenderer.RenderTarget);

                //Draw Buffer to Final RenderTarget
                graphicsManager.GraphicsDevice.SetRenderTarget(FinalRenderer.RenderTarget);
                graphicsManager.GraphicsDevice.Clear(Color.Black);

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque);
                spriteBatch.Draw(BufferRenderer.RenderTarget, new Microsoft.Xna.Framework.Rectangle(0, 0, resolution.X, resolution.Y), Color.White);
                spriteBatch.End();

                //Draw Final RenderTarget to screen
                graphicsManager.GraphicsDevice.SetRenderTarget(null);
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque);
                spriteBatch.Draw(FinalRenderer.RenderTarget, new Microsoft.Xna.Framework.Rectangle(0, 0, resolution.X, resolution.Y), Color.White);
                spriteBatch.End();

                //=================================
                // Draw menus
                //---------------------------------
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);

                //Render scene specific menus
                SceneManager.Draw_MenusOnly(spriteBatch);

                //Render standalone menus
                MenuManager.Draw(spriteBatch);

                spriteBatch.End();
            }

            int count = Renderers.Count;
            for (int i = 0; i < count; i++)
            {
                Renderers[i].Draw(spriteBatch, resolution);
            }
        }

        public virtual void ApplyShaders(RenderTarget2D renderTarget)
        {

        }

        private void Game_Exiting(object? sender, EventArgs e)
        {
            Lighting?.Dispose();
            LightingRenderer?.Dispose();
            AddLightingRenderer?.Dispose();

            foreach (Renderer renderer in Renderers)
            {
                renderer.Dispose();
            }
        }

        #endregion
    }
}
