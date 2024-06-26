﻿using System;

namespace OP_Engine.Utility
{
    public class ALocation : IDisposable
    {
        #region Variables

        public int X;
        public int Y;
        public float Priority;
        public int Distance_ToStart;
        public int Distance_ToDestination;
        public ALocation Parent;

        #endregion

        #region Constructors

        public ALocation()
        {
            
        }

        public ALocation(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Methods

        public virtual void Dispose()
        {

        }

        #endregion
    }
}