using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace OP_Engine.Inputs
{
    public class InputManager : GameComponent
    {
        #region Variables

        public static bool MouseEnabled;
        public static MouseHandler? Mouse;

        public static bool KeyboardEnabled;
        public static KeyboardHandler? Keyboard;

        public static bool GamepadsEnabled;
        public static GamepadHandler? Player1;
        public static GamepadHandler? Player2;
        public static GamepadHandler? Player3;
        public static GamepadHandler? Player4;

        #endregion

        #region Properties

        public static bool Mouse_Moved
        {
            get
            {
                if (MouseEnabled &&
                    Mouse != null)
                {
                    return Mouse.Moved;
                }

                return false;
            }
        }

        public static bool Mouse_LB_Pressed
        {
            get
            {
                if (MouseEnabled &&
                    Mouse != null)
                {
                    return Mouse.LB_Pressed;
                }

                return false;
            }
        }

        public static bool Mouse_LB_Pressed_Flush
        {
            get
            {
                if (MouseEnabled &&
                    Mouse != null)
                {
                    return Mouse.LB_Pressed_Flush;
                }

                return false;
            }
        }

        public static bool Mouse_LB_Held
        {
            get
            {
                if (MouseEnabled &&
                    Mouse != null)
                {
                    return Mouse.LB_Held;
                }

                return false;
            }
        }

        public static bool Mouse_RB_Pressed
        {
            get
            {
                if (MouseEnabled &&
                    Mouse != null)
                {
                    return Mouse.RB_Pressed;
                }

                return false;
            }
        }

        public static bool Mouse_RB_Pressed_Flush
        {
            get
            {
                if (MouseEnabled &&
                    Mouse != null)
                {
                    return Mouse.RB_Pressed_Flush;
                }

                return false;
            }
        }

        public static bool Mouse_RB_Held
        {
            get
            {
                if (MouseEnabled &&
                    Mouse != null)
                {
                    return Mouse.RB_Held;
                }

                return false;
            }
        }

        public static bool Mouse_ScrolledUp
        {
            get
            {
                if (MouseEnabled &&
                    Mouse != null)
                {
                    return Mouse.ScrolledUp;
                }

                return false;
            }
        }

        public static bool Mouse_ScrolledDown
        {
            get
            {
                if (MouseEnabled &&
                    Mouse != null)
                {
                    return Mouse.ScrolledDown;
                }

                return false;
            }
        }

        #endregion

        #region Constructors

        public InputManager(Game game) : base(game)
        {
            game.Exiting += Game_Exiting;

            Keyboard = new KeyboardHandler(game);

            Mouse = new MouseHandler(game);

            Player1 = new GamepadHandler(game, PlayerIndex.One);
            Player2 = new GamepadHandler(game, PlayerIndex.Two);
            Player3 = new GamepadHandler(game, PlayerIndex.Three);
            Player4 = new GamepadHandler(game, PlayerIndex.Four);
        }

        #endregion

        #region Methods

        public static void Update()
        {
            if (KeyboardEnabled)
            {
                Keyboard?.Update();
            }

            if (MouseEnabled)
            {
                Mouse?.Update();
            }

            if (GamepadsEnabled)
            {
                Player1?.Update();
                Player2?.Update();
                Player3?.Update();
                Player4?.Update();
            }
        }

        public static Keys? GetKey(string value)
        {
            Array keys = Enum.GetValues(typeof(Keys));

            int count = keys.Length;
            for (int i = 0; i < count; i++)
            {
                Keys? key = (Keys?)keys.GetValue(i);
                if (key.ToString() == value)
                {
                    return key;
                }
            }

            return null;
        }

        public static Buttons? GetButton(string value)
        {
            Array buttons = Enum.GetValues(typeof(Buttons));

            int count = buttons.Length;
            for (int i = 0; i < count; i++)
            {
                Buttons? button = (Buttons?)buttons.GetValue(i);
                if (button.ToString() == value)
                {
                    return button;
                }

            }

            return null;
        }

        public static GamepadHandler? GetGamepad(PlayerIndex player)
        {
            if (player == PlayerIndex.One)
            {
                return Player1;
            }
            else if (player == PlayerIndex.Two)
            {
                return Player2;
            }
            else if (player == PlayerIndex.Three)
            {
                return Player3;
            }
            else if (player == PlayerIndex.Four)
            {
                return Player4;
            }

            return null;
        }

        public static Keys GetMappedKey(string name)
        {
            if (Keyboard?.KeysMapped.Count > 0)
            {
                return Keyboard.KeysMapped[name];
            }
            else
            {
                throw new Exception("InputManager.Keyboard.KeysMapped is empty!");
            }
        }

        public static bool KeyPressed(Keys key)
        {
            if (KeyboardEnabled &&
                Keyboard != null)
            {
                return Keyboard.keyboardState.IsKeyUp(key) && Keyboard.lastKeyboardState.IsKeyDown(key);
            }

            return false;
        }

        public static bool KeyPressed(string name)
        {
            if (KeyboardEnabled &&
                Keyboard != null)
            {
                return Keyboard.keyboardState.IsKeyUp(GetMappedKey(name)) && Keyboard.lastKeyboardState.IsKeyDown(GetMappedKey(name));
            }

            return false;
        }

        public static bool KeyDown(Keys key)
        {
            if (KeyboardEnabled &&
                Keyboard != null)
            {
                return Keyboard.keyboardState.IsKeyDown(key);
            }

            return false;
        }

        public static bool KeyDown(string name)
        {
            if (KeyboardEnabled &&
                Keyboard != null)
            {
                return Keyboard.keyboardState.IsKeyDown(GetMappedKey(name));
            }

            return false;
        }

        public static Buttons GetMappedButton(PlayerIndex player, string value)
        {
            GamepadHandler? handler = GetGamepad(player);
            if (handler?.ButtonsMapped.Count > 0)
            {
                return handler.ButtonsMapped[value];
            }
            else
            {
                throw new Exception("InputManager.Player" + ((int)player + 1).ToString() + ".ButtonsMapped is empty!");
            }
        }

        public static bool ButtonPressed(PlayerIndex player, Buttons button)
        {
            if (GamepadsEnabled)
            {
                GamepadHandler? handler = GetGamepad(player);
                if (handler != null)
                {
                    return handler.gamePadState.IsButtonUp(button) && handler.lastGamePadState.IsButtonDown(button);
                }
            }

            return false;
        }

        public static bool ButtonPressed(PlayerIndex player, string name)
        {
            if (GamepadsEnabled)
            {
                GamepadHandler? handler = GetGamepad(player);
                if (handler != null)
                {
                    Buttons button = GetMappedButton(player, name);
                    return handler.gamePadState.IsButtonUp(button) && handler.lastGamePadState.IsButtonDown(button);
                }
            }

            return false;
        }

        public static bool ButtonDown(PlayerIndex player, Buttons button)
        {
            if (GamepadsEnabled)
            {
                GamepadHandler? handler = GetGamepad(player);
                if (handler != null)
                {
                    return handler.gamePadState.IsButtonDown(button);
                }
            }

            return false;
        }

        public static bool ButtonDown(PlayerIndex player, string name)
        {
            if (GamepadsEnabled)
            {
                GamepadHandler? handler = GetGamepad(player);
                if (handler != null)
                {
                    Buttons button = GetMappedButton(player, name);
                    return handler.gamePadState.IsButtonDown(button);
                }
            }

            return false;
        }

        public static bool MouseWithin(Rectangle region)
        {
            if (MouseEnabled)
            {
                if (Mouse?.mouseState.X >= region.X &&
                    Mouse?.mouseState.X < region.X + region.Width &&
                    Mouse?.mouseState.Y >= region.Y &&
                    Mouse?.mouseState.Y < region.Y + region.Height)
                {
                    return true;
                }
            }

            return false;
        }

        private void Game_Exiting(object? sender, EventArgs e)
        {
            //This is to stop all the timers when exiting game
            Mouse?.Dispose();
            Keyboard?.Dispose();
            Player1?.Dispose();
            Player2?.Dispose();
            Player3?.Dispose();
            Player4?.Dispose();
        }

        #endregion
    }
}
