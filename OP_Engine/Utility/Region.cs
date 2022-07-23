using System;
using Microsoft.Xna.Framework;

namespace OP_Engine.Utility
{
    public class Region : IDisposable
    {
        #region Variables

        public int X;
        public int Y;
        public int Width;
        public int Height;

        #endregion

        #region Properties

        public Rectangle ToRectangle
        {
            get { return new Rectangle(X, Y, Width, Height); }
        }

        #endregion

        #region Constructors

        public Region()
        {

        }

        public Region (int x, int y, int width, int height)
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
