using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OP_Engine.Utility;

namespace OP_Engine.Spells
{
    public class Spell : IDisposable
    {
        #region Variables

        public long ID;
        public string Name;
        public string Description;
        public string Type;
        public float Value;
        public float Min_Value;
        public float Max_Value;
        public float Rate;
        public float Duration;
        public TimeSpan Time;
        public bool IsLightSource;

        public Region Region;
        public Texture2D Texture;
        public Rectangle Image;
        public bool Visible;
        public Color DrawColor;

        public List<Property> Properties;

        #endregion

        #region Constructor

        public Spell()
        {
            Region = new Region();
            Properties = new List<Property>();
        }

        #endregion

        #region Methods

        public virtual Property GetProperty(string name)
        {
            int count = Properties.Count;
            for (int i = 0; i < count; i++)
            {
                Property existing = Properties[i];
                if (existing == null)
                {
                    if (existing.Name == name)
                    {
                        return existing;
                    }
                }
            }

            return null;
        }

        public virtual void Dispose()
        {
            foreach (Property something in Properties)
            {
                something.Dispose();
            }

            Region?.Dispose();
        }

        #endregion
    }
}
