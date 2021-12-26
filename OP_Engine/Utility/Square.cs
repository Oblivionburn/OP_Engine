using System;

namespace OP_Engine.Utility
{
    public class Square : IDisposable
    {
        #region Variables

        public int X;
        public int Y;
        public int Width;
        public int Height;

        #endregion

        #region Constructors

        public Square()
        {

        }

        public Square (int x, int y, int width, int height)
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
