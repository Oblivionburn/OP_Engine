using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Utility;
using OP_Engine.Inputs;

namespace OP_Engine.Controls
{
    public class ProgressBar : Picture
    {
        #region Variables

        public Texture2D Base_Texture;
        public Region Base_Region = new Region();

        public Texture2D Bar_Texture;
        public Region Bar_Region = new Region();
        public Rectangle Bar_Image = new Rectangle();

        #endregion

        #region Events

        public event EventHandler OnStep;
        public event EventHandler OnValueIncrease;
        public event EventHandler OnValueDecrease;

        #endregion

        #region Constructor

        public ProgressBar() : base()
        {
            
        }

        #endregion

        #region Methods

        public override void Update()
        {
            if (Base_Region != null)
            {
                float CurrentVal = (Bar_Texture.Width / Max_Value) * Value;
                Bar_Image = new Rectangle(Bar_Image.X, Bar_Image.Y, (int)CurrentVal, Bar_Image.Height);

                CurrentVal = (Base_Region.Width / Max_Value) * Value;
                Bar_Region = new Region(Base_Region.X, Base_Region.Y, (int)CurrentVal, Base_Region.Height);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Base_Texture != null &&
                Bar_Texture != null &&
                Base_Region != null &&
                Bar_Region != null)
            {
                if (Visible)
                {
                    spriteBatch.Draw(Base_Texture, Base_Region.ToRectangle, Color.White * Opacity);
                    spriteBatch.Draw(Bar_Texture, Bar_Region.ToRectangle, Bar_Image, DrawColor * Opacity);
                }
            }
        }

        public virtual void Step()
        {
            IncreaseValueByRate();
            Update();

            OnStep?.Invoke(this, EventArgs.Empty);
        }

        public virtual void IncreaseValue(int value)
        {
            base.IncreaseValue(value);
            Update();

            OnValueIncrease?.Invoke(this, EventArgs.Empty);
        }

        public virtual void DecreaseValue(int value)
        {
            base.DecreaseValue(value);
            Update();

            OnValueDecrease?.Invoke(this, EventArgs.Empty);
        }

        public virtual void SetValue(int value)
        {
            base.SetValue(value);
            Update();
        }

        public virtual void SetValue_FromMouse()
        {
            if (Base_Region != null &&
                Bar_Region != null &&
                Bar_Texture != null)
            {
                Bar_Region.Width = InputManager.Mouse.X - Base_Region.X;
                float value = ((Bar_Region.Width * Max_Value) / Base_Region.Width) + 1;

                float CurrentVal = ((value - 1) * Bar_Texture.Width) / Max_Value;
                Bar_Image.Width = (int)CurrentVal;

                Value = (int)value;
            }
        }

        public virtual void Reset()
        {
            Value = 0;
        }

        public override void Dispose()
        {
            Base_Region = null;
            Bar_Region = null;

            if (Base_Texture != null)
            {
                Base_Texture.Dispose();
            }

            if (Bar_Texture != null)
            {
                Bar_Texture.Dispose();
            }

            base.Dispose();
        }

        #endregion
    }
}