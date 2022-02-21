using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Input;

namespace OP_Engine.Inputs
{
    public class KeyEventArgs : EventArgs
    {
        public List<Keys> KeysPressed { get; private set; }
        public List<Keys> KeysDown { get; private set; }

        public KeyEventArgs(List<Keys> keysPressed, List<Keys> keysDown)
        {
            KeysPressed = keysPressed;
            KeysDown = keysDown;
        }
    }
}
