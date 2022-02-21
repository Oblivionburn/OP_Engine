using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Input;

namespace OP_Engine.Inputs
{
    public class ButtonEventArgs : EventArgs
    {
        public List<Buttons> ButtonsPressed { get; private set; }
        public List<Buttons> ButtonsDown { get; private set; }

        public ButtonEventArgs(List<Buttons> buttonsPressed, List<Buttons> buttonsDown)
        {
            ButtonsPressed = buttonsPressed;
            ButtonsDown = buttonsDown;
        }
    }
}
