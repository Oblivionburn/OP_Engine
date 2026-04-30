using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OP_Engine.Utility;

namespace OP_Engine.Characters
{
    public class Wound : IDisposable
    {
        #region Variables

        public long ID;
        public string Name;
        public string Description;
        public float Value;
        public float Max_Value;
        public float Duration;
        public float Rate;

        public Region Region;
        public Texture2D Texture;
        public Rectangle Image;
        public bool Visible;
        public Color DrawColor;

        #endregion

        #region Constructors

        public Wound()
        {

        }

        #endregion

        #region Methods

        public virtual void Dispose()
        {
            
        }

        #endregion
    }
}
