using System;
using Microsoft.Xna.Framework;

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

        #region Properties

        public Vector2 ToVector2
        {
            get { return new Vector2(X, Y); }
        }

        public Vector3 ToVector3
        {
            get { return new Vector3(X, Y, Z); }
        }

        #endregion

        #region Methods

        public virtual void Dispose()
        {

        }

        #endregion
    }
}
