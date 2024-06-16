using System;

namespace OP_Engine.Utility
{
    public class Location : IDisposable
    {
        #region Variables

        public float X;
        public float Y;
        public float Z;

        #endregion

        #region Constructors

        public Location()
        {

        }

        public Location(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        #endregion

        #region Methods

        public virtual void Dispose()
        {

        }

        #endregion
    }
}
