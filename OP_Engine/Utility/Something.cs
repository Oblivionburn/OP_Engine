using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OP_Engine.Utility
{
    public class Something : IDisposable
    {
        #region Variables

        public long ID;
        public long OwnerID;
        public string Name;
        public string Type;
        public string Assignment;
        public string Description;
        public float Value;
        public float Min_Value;
        public float Max_Value;
        public float Cost;
        public float Sell_Price;
        public float Buy_Price;
        public float Rate;
        public float Weight;
        public float Rarity;
        public bool Active;
        public bool InSight;
        public bool IsLightSource;
        public bool IsLit;
        public bool Visible;
        public Direction Direction;
        public Vector3 Location; //X, Y, Z
        public Vector3 Dimensions; //Width, Height, Depth
        public Texture2D Texture;
        public Rectangle Region; //Screen render space
        public Rectangle Image; //Sub-region of Texture
        public Color DrawColor;

        #endregion

        #region Constructor

        public Something()
        {

        }

        public Something(int id, string name, int value, int max_value)
        {
            ID = id;
            Name = name;
            Value = value;
            Max_Value = max_value;
        }

        #endregion

        #region Methods

        public virtual void IncreaseMaxValue(float amount)
        {
            Max_Value += amount;
            CheckMinMax();
        }

        public virtual void IncreaseMinValue(float amount)
        {
            Min_Value += amount;
            CheckMinMax();
        }

        public virtual void IncreaseValue(float amount)
        {
            Value += amount;
            CheckMinMax();
        }

        public virtual void IncreaseValueByRate()
        {
            Value += Rate;
            CheckMinMax();
        }

        public virtual void DecreaseMaxValue(float amount)
        {
            Max_Value -= amount;
            CheckMinMax();
        }

        public virtual void DecreaseMinValue(float amount)
        {
            Min_Value -= amount;
            CheckMinMax();
        }

        public virtual void DecreaseValue(float amount)
        {
            Value -= amount;
            CheckMinMax();
        }

        public virtual void DecreaseValueByRate()
        {
            Value -= Rate;
            CheckMinMax();
        }

        public virtual void SetMaxValue(float amount)
        {
            Max_Value = amount;
            CheckMinMax();
        }

        public virtual void SetMinValue(float amount)
        {
            Min_Value = amount;
            CheckMinMax();
        }

        public virtual void SetValue(float amount)
        {
            Value = amount;
            CheckMinMax();
        }

        public virtual void CheckMinMax()
        {
            if (Value > Max_Value)
            {
                Value = Max_Value;
            }
            else if (Value < Min_Value)
            {
                Value = Min_Value;
            }
            else if (Value < 0)
            {
                Value = 0;
            }
        }

        public virtual void Dispose()
        {
            Texture = null;
        }

        #endregion
    }
}