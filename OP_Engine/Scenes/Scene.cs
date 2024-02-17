using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Controls;
using OP_Engine.Menus;
using OP_Engine.Tiles;
using OP_Engine.Utility;

namespace OP_Engine.Scenes
{
    public class Scene : Something
    {
        #region Variables

        public Menu Menu;
        public World World;

        public Picture TextFrame;
        public float TextFrame_OpaqueLevel;
        public List<Button> Messages = new List<Button>();
        public bool BlockMessages;

        #endregion

        #region Events

        public event EventHandler OnLoad;
        public event EventHandler OnResize;

        #endregion

        #region Constructor

        public Scene()
        {
            World = new World();
            Menu = new Menu();

            TextFrame = new Picture();
            TextFrame_OpaqueLevel = 1;
        }

        #endregion

        #region Methods

        public virtual void Update()
        {
            if (Visible)
            {
                Menu.Update();
            }
        }

        public virtual void Update(Game gameRef, ContentManager content)
        {
            if (Visible)
            {
                Menu.Update(gameRef, content);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution)
        {
            if (Visible)
            {
                World.Draw(spriteBatch, resolution);
                Menu.Draw(spriteBatch);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution, Color color)
        {
            if (Visible)
            {
                World.Draw(spriteBatch, resolution, color);
                Menu.Draw(spriteBatch);
            }
        }

        public virtual void DrawWorld(SpriteBatch spriteBatch, Point resolution)
        {
            if (Visible)
            {
                World.Draw(spriteBatch, resolution);
            }
        }

        public virtual void DrawWorld(SpriteBatch spriteBatch, Point resolution, Color color)
        {
            if (Visible)
            {
                World.Draw(spriteBatch, resolution, color);
            }
        }

        public virtual void DrawMenu(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                Menu.Draw(spriteBatch);
            }
        }

        public virtual void Load()
        {
            OnLoad?.Invoke(this, EventArgs.Empty);
        }

        public virtual void Load(ContentManager content)
        {
            OnLoad?.Invoke(this, EventArgs.Empty);
        }

        public virtual void Resize(Point point)
        {
            OnResize?.Invoke(this, EventArgs.Empty);
        }

        public virtual void PrintMessage(SpriteFont font, string text, Point resolution)
        {
            if (!BlockMessages)
            {
                int height = resolution.Y / 32;
                int num = 56;
                Button label = new Button();
                label.Visible = true;

                bool repeat = false;
                string text2 = "";

                if (text.Length > num)
                {
                    int startIndex = 0;
                    for (int i = num; i >= 0; i--)
                    {
                        if (text[i] == ' ')
                        {
                            startIndex = i;
                            break;
                        }
                    }

                    text2 = text.Substring(startIndex);
                    text = text.Remove(startIndex);
                    repeat = true;
                }

                label.Text = text;
                label.TextColor = Color.White;
                label.Font = font;

                if (Messages.Count > 0 &&
                    Messages.Count < 5)
                {
                    Vector2 vector;
                    vector.X = Messages[Messages.Count - 1].Region.X;
                    vector.Y = Messages[Messages.Count - 1].Region.Y;

                    Vector2 vector2;
                    vector2.X = vector.X;
                    vector2.Y = vector.Y + (resolution.Y / 32);

                    int width = (int)TextFrame.Region.Width - 32 - (num - label.Text.Length) * 10;
                    int x = (int)TextFrame.Region.X + 16 + (num - label.Text.Length) * 5;

                    label.Region = new Region(x, (int)vector2.Y, width, height);
                }
                else
                {
                    if (Messages.Count >= 5)
                    {
                        Messages.RemoveAt(0);
                        for (int j = 0; j < Messages.Count; j++)
                        {
                            Messages[j].Region = new Region(Messages[j].Region.X, Messages[j].Region.Y - resolution.Y / 32, TextFrame.Region.Width, height);
                        }

                        Vector2 vector;
                        vector.X = Messages[Messages.Count - 1].Region.X;
                        vector.Y = Messages[Messages.Count - 1].Region.Y;

                        Vector2 vector2;
                        vector2.X = vector.X;
                        vector2.Y = vector.Y + (resolution.Y / 32);

                        int width2 = (int)TextFrame.Region.Width - 32 - (num - label.Text.Length) * 10;
                        int x2 = (int)TextFrame.Region.X + 16 + (num - label.Text.Length) * 5;
                        label.Region = new Region(x2, (int)vector2.Y, width2, height);
                    }
                    else
                    {
                        Vector2 vector2;
                        vector2.X = TextFrame.Region.X;
                        vector2.Y = TextFrame.Region.Y;

                        int width3 = (int)TextFrame.Region.Width - 32 - (num - label.Text.Length) * 10;
                        int x3 = (int)TextFrame.Region.X + 16 + (num - label.Text.Length) * 5;

                        label.Region = new Region(x3, (int)vector2.Y, width3, height);
                    }
                }

                Messages.Add(label);

                if (repeat)
                {
                    PrintMessage(font, text2, resolution);
                }
            }
        }

        public override void Dispose()
        {
            Menu.Dispose();
            World.Dispose();
            TextFrame.Dispose();

            foreach (Button label in Messages)
            {
                label.Dispose();
            }

            base.Dispose();
        }

        #endregion
    }
}
