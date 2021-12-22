using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OP_Engine.Controls
{
    public class ProgressBar : Picture
    {
        #region Variables

        public Texture2D Base_Texture;
        public Rectangle Base_Region;

        public Texture2D Bar_Texture;
        public Rectangle Bar_Region;
        public Rectangle Bar_Image;

        public float Increment;

        #endregion

        #region Constructor

        public ProgressBar() : base()
        {
            Base_Region = new Rectangle();
            Bar_Region = new Rectangle();
            Bar_Image = new Rectangle();

            Opacity = 1;
        }

        #endregion

        #region Methods

        public override void Update()
        {
            float CurrentVal = ((float)Bar_Texture.Width / Max_Value) * Value;
            Bar_Image = new Rectangle(Bar_Image.X, Bar_Image.Y, (int)CurrentVal, Bar_Image.Height);

            CurrentVal = ((float)Base_Region.Width / Max_Value) * Value;
            Bar_Region = new Rectangle(Base_Region.X, Base_Region.Y, (int)CurrentVal, Base_Region.Height);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Base_Texture != null &&
                Bar_Texture != null)
            {
                if (Visible)
                {
                    spriteBatch.Draw(Base_Texture, Base_Region, Color.White * Opacity);
                    spriteBatch.Draw(Bar_Texture, Bar_Region, Bar_Image, DrawColor * Opacity);
                }
            }
        }

        public virtual void Step()
        {
            Value += (int)Increment;

            if (Value > Max_Value)
            {
                Value = Max_Value;
            }
            else if (Value < 0)
            {
                Value = 0;
            }
        }

        public virtual void Increase(int value)
        {
            Value += value;

            if (Value > Max_Value)
            {
                Value = Max_Value;
            }
            else if (Value < 0)
            {
                Value = 0;
            }
        }

        public virtual void Decrease(int value)
        {
            Value -= value;

            if (Value > Max_Value)
            {
                Value = Max_Value;
            }
            else if (Value < 0)
            {
                Value = 0;
            }
        }

        public virtual void SetValue(int value)
        {
            Value = value;

            if (Value > Max_Value)
            {
                Value = Max_Value;
            }
            else if (Value < 0)
            {
                Value = 0;
            }
        }

        public virtual void Reset()
        {
            Value = 0;
        }

        public override void Dispose()
        {
            if (Base_Texture != null)
            {
                Base_Texture = null;
            }

            if (Bar_Texture != null)
            {
                Bar_Texture = null;
            }

            base.Dispose();
        }

        #endregion
    }
}