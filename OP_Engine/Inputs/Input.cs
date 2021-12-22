using System;

using Microsoft.Xna.Framework.Input;

namespace OP_Engine.Inputs
{
    public class Input : IDisposable
    {
        #region Variables

        public string Name;
        public Keys Key;
        public Buttons Button;

        #endregion

        #region Constructor

        public Input()
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
