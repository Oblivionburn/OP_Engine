using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace OP_Engine.Inputs
{
    public class KeyEventArgs(List<Keys> keysPressed, List<Keys> keysDown) : EventArgs
    {
        public List<Keys> KeysPressed { get; private set; } = keysPressed;
        public List<Keys> KeysDown { get; private set; } = keysDown;
    }
}
