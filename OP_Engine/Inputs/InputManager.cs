using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace OP_Engine.Inputs
{
    public class InputManager : GameComponent
    {
        #region Variables

        public static bool MouseEnabled;
        public static MouseHandler Mouse;

        public static bool KeyboardEnabled;
        public static KeyboardHandler Keyboard;

        public static bool GamepadsEnabled;
        public static GamepadHandler Player1;
        public static GamepadHandler Player2;
        public static GamepadHandler Player3;
        public static GamepadHandler Player4;

        #endregion

        #region Properties

        public static bool Mouse_Moved
        {
            get { return MouseEnabled && Mouse.Moved; }
        }

        public static bool Mouse_LB_Pressed
        {
            get { return MouseEnabled && Mouse.LB_Pressed; }
        }

        public static bool Mouse_LB_Pressed_Flush
        {
            get { return MouseEnabled && Mouse.LB_Pressed_Flush; }
        }

        public static bool Mouse_LB_Held
        {
            get { return MouseEnabled && Mouse.LB_Held; }
        }

        public static bool Mouse_RB_Pressed
        {
            get { return MouseEnabled && Mouse.RB_Pressed; }
        }

        public static bool Mouse_RB_Pressed_Flush
        {
            get { return MouseEnabled && Mouse.RB_Pressed_Flush; }
        }

        public static bool Mouse_RB_Held
        {
            get { return MouseEnabled && Mouse.RB_Held; }
        }

        public static bool Mouse_ScrolledUp
        {
            get { return MouseEnabled && Mouse.ScrolledUp; }
        }

        public static bool Mouse_ScrolledDown
        {
            get { return MouseEnabled && Mouse.ScrolledDown; }
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
                Keyboard.Update();
            }

            if (MouseEnabled)
            {
                Mouse.Update();
            }

            if (GamepadsEnabled)
            {
                Player1.Update();
                Player2.Update();
                Player3.Update();
                Player4.Update();
            }
        }

        public static Keys GetKey(string value)
        {
            Array keys = Enum.GetValues(typeof(Keys));

            int count = keys.Length;
            for (int i = 0; i < count; i++)
            {
                Keys key = (Keys)keys.GetValue(i);
                if (key.ToString() == value)
                {
                    return key;
                }

            }

            return 0;
        }

        public static Buttons GetButton(string value)
        {
            Array buttons = Enum.GetValues(typeof(Buttons));

            int count = buttons.Length;
            for (int i = 0; i < count; i++)
            {
                Buttons button = (Buttons)buttons.GetValue(i);
                if (button.ToString() == value)
                {
                    return button;
                }

            }

            return 0;
        }

        public static GamepadHandler GetGamepad(PlayerIndex player)
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
            if (Keyboard.KeysMapped.Count > 0)
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
            return KeyboardEnabled && Keyboard.keyboardState.IsKeyUp(key) && Keyboard.lastKeyboardState.IsKeyDown(key);
        }

        public static bool KeyPressed(string name)
        {
            return KeyboardEnabled && Keyboard.keyboardState.IsKeyUp(GetMappedKey(name)) && Keyboard.lastKeyboardState.IsKeyDown(GetMappedKey(name));
        }

        public static bool KeyDown(Keys key)
        {
            return KeyboardEnabled && Keyboard.keyboardState.IsKeyDown(key);
        }

        public static bool KeyDown(string name)
        {
            return KeyboardEnabled && Keyboard.keyboardState.IsKeyDown(GetMappedKey(name));
        }

        public static Buttons GetMappedButton(PlayerIndex player, string value)
        {
            if (GetGamepad(player).ButtonsMapped.Count > 0)
            {
                return GetGamepad(player).ButtonsMapped[value];
            }
            else
            {
                throw new Exception("InputManager.Player" + ((int)player + 1).ToString() + ".ButtonsMapped is empty!");
            }
        }

        public static bool ButtonPressed(PlayerIndex player, Buttons button)
        {
            return GamepadsEnabled && 
                GetGamepad(player).gamePadState.IsButtonUp(button) && 
                GetGamepad(player).lastGamePadState.IsButtonDown(button);
        }

        public static bool ButtonPressed(PlayerIndex player, string name)
        {
            return GamepadsEnabled && 
                GetGamepad(player).gamePadState.IsButtonUp(GetMappedButton(player, name)) && 
                GetGamepad(player).lastGamePadState.IsButtonDown(GetMappedButton(player, name));
        }

        public static bool ButtonDown(PlayerIndex player, Buttons button)
        {
            return GamepadsEnabled && GetGamepad(player).gamePadState.IsButtonDown(button);
        }

        public static bool ButtonDown(PlayerIndex player, string name)
        {
            return GamepadsEnabled && GetGamepad(player).gamePadState.IsButtonDown(GetMappedButton(player, name));
        }

        public static bool MouseWithin(Rectangle region)
        {
            if (MouseEnabled)
            {
                if (Mouse.mouseState.X >= region.X &&
                    Mouse.mouseState.X < region.X + region.Width &&
                    Mouse.mouseState.Y >= region.Y &&
                    Mouse.mouseState.Y < region.Y + region.Height)
                {
                    return true;
                }
            }
            
            return false;
        }

        private void Game_Exiting(object sender, EventArgs e)
        {
            //This is to stop all the timers when exiting game
            if (Mouse != null)
            {
                Mouse.Dispose();
            }
            
            if (Keyboard != null)
            {
                Keyboard.Dispose();
            }
            
            if (Player1 != null)
            {
                Player1.Dispose();
            }

            if (Player2 != null)
            {
                Player2.Dispose();
            }

            if (Player3 != null)
            {
                Player3.Dispose();
            }

            if (Player4 != null)
            {
                Player4.Dispose();
            }
        }

        #endregion
    }
}
