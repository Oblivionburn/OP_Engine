using Microsoft.Xna.Framework.Input;

namespace OP_Engine.Inputs
{
    public class ButtonEventArgs(List<Buttons> buttonsPressed, List<Buttons> buttonsDown) : EventArgs
    {
        public List<Buttons> ButtonsPressed { get; private set; } = buttonsPressed;
        public List<Buttons> ButtonsDown { get; private set; } = buttonsDown;
    }
}
