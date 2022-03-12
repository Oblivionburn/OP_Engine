using Microsoft.Xna.Framework.Graphics;
using System;

namespace OP_Engine.Controls
{
    public class InputBox : Button
    {
        #region Variables

        public int MaxLength;
        public string Caret;
        public int CarrotDelay;

        public bool Active;

        private bool delayed;
        private int delay;

        #endregion

        #region Constructor

        public InputBox() : base()
        {
            
        }

        #endregion

        #region Methods

        public override void Update()
        {
            if (Font != null)
            {
                if (!string.IsNullOrEmpty(Text))
                {
                    Size = Font.MeasureString(Text);
                }
                else if (!string.IsNullOrEmpty(Caret))
                {
                    Size = Font.MeasureString(Caret);
                }

                float xScale = Region.Width / Size.X;
                float yScale = Region.Height / Size.Y;
                Scale = Math.Min(xScale, yScale);

                int strWidth = (int)Math.Round(Size.X * Scale);
                int strHeight = (int)Math.Round(Size.Y * Scale);

                Position.X = (Region.Width - strWidth) / 2 + Region.X;
                Position.Y = (Region.Height - strHeight) / 2 + Region.Y;
            }

            Caret = "";
            if (Active)
            {
                if (delayed)
                {
                    if (delay < CarrotDelay)
                    {
                        delay++;
                    }
                    else
                    {
                        delayed = false;
                    }
                }
                else
                {
                    if (delay > 0)
                    {
                        if (!string.IsNullOrEmpty(Text))
                        {
                            for (int i = 0; i < Text.Length; i++)
                            {
                                Caret += " ";
                            }
                        }
                        
                        Caret += "|";
                        delay--;
                    }
                    else
                    {
                        delayed = true;
                    }
                }
            }
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                base.Draw(spriteBatch);

                if (Active &&
                    !string.IsNullOrEmpty(Caret))
                {
                    spriteBatch.DrawString(Font, Caret, Position, TextColor, 0f, default, Scale, SpriteEffects.None, 0f);
                }
            }
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}