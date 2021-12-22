using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OP_Engine.Utility
{
    public class Something : IDisposable
    {
        #region Variables

        public int ID;
        public int OwnerID;
        public string Name;
        public string Type;
        public string Assignment;
        public int Value;
        public int Max_Value;
        public float Rate;
        public float Weight;
        public int Rarity;
        public bool InSight;
        public bool Visible;
        public string Description;
        public Direction Direction;
        public Vector3 Location;
        public Texture2D Texture;
        public Rectangle Region;
        public Rectangle Image;
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

        public virtual void Dispose()
        {
            Texture = null;
        }

        #endregion
    }
}