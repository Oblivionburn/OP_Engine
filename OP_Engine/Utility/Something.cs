using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OP_Engine.Utility
{
    public class Something : IDisposable
    {
        #region Variables

        public long ID;
        public List<long> OwnerIDs = new List<long>();
        public string Name;
        public string Type;
        public string Assignment;
        public string Description;
        public List<string> Tags = new List<string>();
        public int Amount;
        public int Tier;
        public int Grade;
        public int Quality;
        public TimeSpan Time;
        public TimeSpan Min_Time;
        public TimeSpan Max_Time;
        public float Value;
        public float Min_Value;
        public float Max_Value;
        public float Sell_Price;
        public float Buy_Price;
        public float Rate;
        public float Weight;
        public float Duration;
        public float Durability;
        public bool Active;
        public bool InSight;
        public bool IsLightSource;
        public bool IsLit;
        public bool Visible;
        public Rarity Rarity;
        public Direction Direction;
        public Vector3 Location; //X, Y, Z
        public Dimension3 Dimensions; //Width, Height, Depth
        public Texture2D Texture;
        public Region Region; //Where to draw this to the screen/window; X, Y, Width, Height
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
            Region = null;

            if (Texture != null)
            {
                Texture.Dispose();
            }
        }

        #endregion
    }
}