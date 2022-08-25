using System;
using Microsoft.Xna.Framework;

namespace OP_Engine.Utility
{
    public class Region : IDisposable
    {
        #region Variables

        public float X;
        public float Y;
        public float Width;
        public float Height;

        #endregion

        #region Properties

        public Rectangle ToRectangle
        {
            get { return new Rectangle((int)X, (int)Y, (int)Width, (int)Height); }
        }

        #endregion

        #region Constructors

        public Region()
        {

        }

        public Region (float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        #endregion

        #region Methods

        public virtual void Dispose()
        {

        }

        #endregion
    }
}
