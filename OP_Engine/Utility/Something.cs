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
        public int Level;
        public int XP;
        public Dictionary<int, int> XP_Needed_ForLevels = new Dictionary<int, int>();
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
        public Vector2 Coordinates; //X, Y
        public Vector3 Location; //X, Y, Z
        public Dimension3 Dimensions; //Width, Height, Depth
        public Texture2D Texture;
        public Region Region; //Where to draw this to the screen/window; X, Y, Width, Height
        public Rectangle Image; //Sub-region of Texture
        public Color DrawColor;

        #endregion

        #region Events

        public event EventHandler OnValueChange;
        public event EventHandler OnMinValueChange;
        public event EventHandler OnMaxValueChange;
        public event EventHandler OnGainLevel;

        #endregion

        #region Constructors

        public Something()
        {

        }

        public Something(long id, string name, int value, int max_value)
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
            OnMaxValueChange?.Invoke(this, EventArgs.Empty);

            CheckMinMax();
        }

        public virtual void IncreaseMinValue(float amount)
        {
            Min_Value += amount;
            OnMinValueChange?.Invoke(this, EventArgs.Empty);

            CheckMinMax();
        }

        public virtual void IncreaseValue(float amount)
        {
            Value += amount;
            OnValueChange?.Invoke(this, EventArgs.Empty);

            CheckMinMax();
        }

        public virtual void IncreaseValueByRate()
        {
            Value += Rate;
            OnValueChange?.Invoke(this, EventArgs.Empty);

            CheckMinMax();
        }

        public virtual void DecreaseMaxValue(float amount)
        {
            Max_Value -= amount;
            OnMaxValueChange?.Invoke(this, EventArgs.Empty);

            CheckMinMax();
        }

        public virtual void DecreaseMinValue(float amount)
        {
            Min_Value -= amount;
            OnMinValueChange?.Invoke(this, EventArgs.Empty);

            CheckMinMax();
        }

        public virtual void DecreaseValue(float amount)
        {
            Value -= amount;
            OnValueChange?.Invoke(this, EventArgs.Empty);

            CheckMinMax();
        }

        public virtual void DecreaseValueByRate()
        {
            Value -= Rate;
            OnValueChange?.Invoke(this, EventArgs.Empty);

            CheckMinMax();
        }

        public virtual void SetMaxValue(float amount)
        {
            Max_Value = amount;
            OnMaxValueChange?.Invoke(this, EventArgs.Empty);

            CheckMinMax();
        }

        public virtual void SetMinValue(float amount)
        {
            Min_Value = amount;
            OnMinValueChange?.Invoke(this, EventArgs.Empty);

            CheckMinMax();
        }

        public virtual void SetValue(float amount)
        {
            Value = amount;
            OnValueChange?.Invoke(this, EventArgs.Empty);

            CheckMinMax();
        }

        public virtual void CheckMinMax()
        {
            if (Value > Max_Value)
            {
                Value = Max_Value;
                OnValueChange?.Invoke(this, EventArgs.Empty);
            }
            else if (Value < Min_Value)
            {
                Value = Min_Value;
                OnValueChange?.Invoke(this, EventArgs.Empty);
            }
            else if (Value < 0)
            {
                Value = 0;
                OnValueChange?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual void AddXP(int amount)
        {
            XP += amount;

            foreach (var level in XP_Needed_ForLevels)
            {
                if (level.Key == Level + 1)
                {
                    if (XP >= level.Value)
                    {
                        XP -= level.Value;
                        if (XP < 0)
                        {
                            XP = 0;
                        }

                        Level++;
                        OnGainLevel?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public virtual float Get_BuyPrice_Markup(float percent)
        {
            if (percent >= 1)
            {
                return Buy_Price + (Buy_Price * (percent / 100));
            }
            else
            {
                return Buy_Price + (Buy_Price * percent);
            }
        }

        public virtual float Get_BuyPrice_Discount(float percent)
        {
            if (percent >= 1)
            {
                return Buy_Price - (Buy_Price * (percent / 100));
            }
            else
            {
                return Buy_Price - (Buy_Price * percent);
            }
        }

        public virtual float Get_SellPrice_Markup(float percent)
        {
            if (percent >= 1)
            {
                return Sell_Price + (Sell_Price * (percent / 100));
            }
            else
            {
                return Sell_Price + (Sell_Price * percent);
            }
        }

        public virtual float Get_SellPrice_Discount(float percent)
        {
            if (percent >= 1)
            {
                return Sell_Price - (Sell_Price * (percent / 100));
            }
            else
            {
                return Sell_Price - (Sell_Price * percent);
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