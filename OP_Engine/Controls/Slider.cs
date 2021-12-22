using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OP_Engine.Controls
{
    public class Slider : Control
    {
        #region Variables

        public int Min_Value;

        public Texture2D Selection;
        public Rectangle Selection_Region;

        public Texture2D Slidebar;
        public Rectangle Slidebar_Region;

        #endregion

        #region Constructor

        public Slider(int max) : base()
        {
            Max_Value = max;
        }

        #endregion

        #region Methods

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                if (Enabled &&
                    Value > 0)
                {
                    if (Slidebar != null &&
                        Selection != null)
                    {
                        spriteBatch.Draw(Slidebar, Slidebar_Region, DrawColor);
                        spriteBatch.Draw(Selection, Selection_Region, DrawColor);
                    }
                }
                else
                {
                    if (Slidebar != null)
                    {
                        spriteBatch.Draw(Slidebar, Slidebar_Region, DrawColor);
                    }
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (Visible)
            {
                if (Enabled &&
                    Value > 0)
                {
                    if (Slidebar != null &&
                        Selection != null)
                    {
                        spriteBatch.Draw(Slidebar, Slidebar_Region, color);
                        spriteBatch.Draw(Selection, Selection_Region, Color.White);
                    }
                }
                else
                {
                    if (Slidebar != null)
                    {
                        spriteBatch.Draw(Slidebar, Slidebar_Region, color);
                    }
                }
            }
        }

        public virtual void SetValue()
        {
            int increment = Slidebar_Region.Width / Max_Value;

            for (int i = Slidebar_Region.X; i < Slidebar_Region.X + Slidebar_Region.Width; i += increment)
            {
                if (Selection_Region.X >= i &&
                    Selection_Region.X < i + increment)
                {
                    Value = ((i - Slidebar_Region.X) / increment) + 1;
                    break;
                }
            }
        }

        public override void Dispose()
        {
            if (Selection != null)
            {
                Selection = null;
            }

            if (Slidebar != null)
            {
                Slidebar = null;
            }

            base.Dispose();
        }

        #endregion
    }
}