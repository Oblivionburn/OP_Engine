using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace OP_Engine.Inputs
{
    public class InputManager : GameComponent
    {
        #region Variables

        public static List<Input> Controls = new List<Input>();
        public static Input LastInput;

        public static KeyboardState keyboardState;
        public static KeyboardState lastKeyboardState;

        public static MouseState mouseState;
        public static MouseState lastMouseState;

        public static GamePadState Player1_GamePadState;
        public static GamePadState Player1_LastGamePadState;
        public static bool Player1_GamepadConnected;
        public static Input Player1_LastInput;

        public static GamePadState Player2_GamePadState;
        public static GamePadState Player2_LastGamePadState;
        public static bool Player2_GamepadConnected;
        public static Input Player2_LastInput;

        public static GamePadState Player3_GamePadState;
        public static GamePadState Player3_LastGamePadState;
        public static bool Player3_GamepadConnected;
        public static Input Player3_LastInput;

        public static GamePadState Player4_GamePadState;
        public static GamePadState Player4_LastGamePadState;
        public static bool Player4_GamepadConnected;
        public static Input Player4_LastInput;

        public static int Wheel;
        public static bool ScrollUp;
        public static bool ScrollDown;

        public static bool MouseMoved;
        public static bool MouseEnabled;
        public static bool Flushed;
        public static bool GamepadEnabled;

        #endregion

        #region Constructors

        public InputManager(Game game) : base(game)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            Player1_GamePadState = GamePad.GetState(PlayerIndex.One);
            Player1_GamepadConnected = GamePad.GetState(PlayerIndex.One).IsConnected;

            Player2_GamePadState = GamePad.GetState(PlayerIndex.Two);
            Player2_GamepadConnected = GamePad.GetState(PlayerIndex.Two).IsConnected;

            Player3_GamePadState = GamePad.GetState(PlayerIndex.Three);
            Player3_GamepadConnected = GamePad.GetState(PlayerIndex.Three).IsConnected;

            Player4_GamePadState = GamePad.GetState(PlayerIndex.Four);
            Player4_GamepadConnected = GamePad.GetState(PlayerIndex.Four).IsConnected;

            GamepadEnabled = false;
            MouseEnabled = true;
        }

        #endregion

        #region Methods

        public static void Update()
        {
            MouseMoved = false;
            ScrollDown = false;
            ScrollUp = false;
            Wheel = 0;

            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            if (MouseEnabled)
            {
                lastMouseState = mouseState;
                mouseState = Mouse.GetState();

                if (!Flushed)
                {
                    if (lastMouseState.ScrollWheelValue > mouseState.ScrollWheelValue)
                    {
                        ScrollDown = true;
                        Wheel = mouseState.ScrollWheelValue - lastMouseState.ScrollWheelValue;
                    }
                    else if (lastMouseState.ScrollWheelValue < mouseState.ScrollWheelValue)
                    {
                        ScrollUp = true;
                        Wheel = mouseState.ScrollWheelValue - lastMouseState.ScrollWheelValue;
                    }
                }

                if (mouseState.X != lastMouseState.X ||
                    mouseState.Y != lastMouseState.Y)
                {
                    MouseMoved = true;
                }
            }

            if (GamepadEnabled)
            {
                Player1_LastGamePadState = Player1_GamePadState;
                Player1_GamePadState = GamePad.GetState(PlayerIndex.One);
                Player1_GamepadConnected = GamePad.GetState(PlayerIndex.One).IsConnected;

                Player2_LastGamePadState = Player2_GamePadState;
                Player2_GamePadState = GamePad.GetState(PlayerIndex.Two);
                Player2_GamepadConnected = GamePad.GetState(PlayerIndex.Two).IsConnected;

                Player3_LastGamePadState = Player3_GamePadState;
                Player3_GamePadState = GamePad.GetState(PlayerIndex.Three);
                Player3_GamepadConnected = GamePad.GetState(PlayerIndex.Three).IsConnected;

                Player4_LastGamePadState = Player4_GamePadState;
                Player4_GamePadState = GamePad.GetState(PlayerIndex.Four);
                Player4_GamepadConnected = GamePad.GetState(PlayerIndex.Four).IsConnected;
            }
            else
            {
                Player1_GamepadConnected = false;
                Player2_GamepadConnected = false;
                Player3_GamepadConnected = false;
                Player4_GamepadConnected = false;
            }

            Flushed = false;
        }

        public static Keys GetKey(string value)
        {
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if (key.ToString() == value)
                {
                    return key;
                }
            }

            return 0;
        }

        public static Buttons GetButton(string value)
        {
            foreach (Buttons button in Enum.GetValues(typeof(Buttons)))
            {
                if (button.ToString() == value)
                {
                    return button;
                }
            }

            return 0;
        }

        public static Input GetControl(string name)
        {
            foreach (Input existing in Controls)
            {
                if (existing.Name == name)
                {
                    return existing;
                }
            }

            return null;
        }

        public static bool KeyPressed(Input input)
        {
            if (keyboardState.IsKeyUp(input.Key) &&
                lastKeyboardState.IsKeyDown(input.Key))
            {
                LastInput = input;
                Flush();
                return true;
            }

            return false;
        }

        public static bool KeyPressed_NoFlush(Input input)
        {
            if (keyboardState.IsKeyUp(input.Key) &&
                lastKeyboardState.IsKeyDown(input.Key))
            {
                LastInput = input;
                return true;
            }

            return false;
        }

        public static bool KeyDown(Input input)
        {
            if (keyboardState.IsKeyDown(input.Key))
            {
                return true;
            }

            return false;
        }

        public static bool ButtonPressed(Input input, PlayerIndex player)
        {
            if (GamepadEnabled)
            {
                if (player == PlayerIndex.One &&
                    Player1_GamepadConnected)
                {
                    if (Player1_GamePadState.IsButtonUp(input.Button) &&
                        Player1_LastGamePadState.IsButtonDown(input.Button))
                    {
                        Player1_LastInput = input;
                        Flush();

                        return true;
                    }
                }
                else if (player == PlayerIndex.Two &&
                         Player2_GamepadConnected)
                {
                    if (Player2_GamePadState.IsButtonUp(input.Button) &&
                        Player2_LastGamePadState.IsButtonDown(input.Button))
                    {
                        Player2_LastInput = input;
                        Flush();

                        return true;
                    }
                }
                else if (player == PlayerIndex.Three &&
                         Player3_GamepadConnected)
                {
                    if (Player3_GamePadState.IsButtonUp(input.Button) &&
                        Player3_LastGamePadState.IsButtonDown(input.Button))
                    {
                        Player3_LastInput = input;
                        Flush();

                        return true;
                    }
                }
                else if (player == PlayerIndex.Four &&
                         Player4_GamepadConnected)
                {
                    if (Player4_GamePadState.IsButtonUp(input.Button) &&
                        Player4_LastGamePadState.IsButtonDown(input.Button))
                    {
                        Player4_LastInput = input;
                        Flush();

                        return true;
                    }
                }
            }

            return false;
        }

        public static bool ButtonPressed_NoFlush(Input input, PlayerIndex player)
        {
            if (GamepadEnabled)
            {
                if (player == PlayerIndex.One &&
                    Player1_GamepadConnected)
                {
                    if (Player1_GamePadState.IsButtonUp(input.Button) &&
                        Player1_LastGamePadState.IsButtonDown(input.Button))
                    {
                        Player1_LastInput = input;
                        return true;
                    }
                }
                else if (player == PlayerIndex.Two &&
                         Player2_GamepadConnected)
                {
                    if (Player2_GamePadState.IsButtonUp(input.Button) &&
                        Player2_LastGamePadState.IsButtonDown(input.Button))
                    {
                        Player2_LastInput = input;
                        return true;
                    }
                }
                else if (player == PlayerIndex.Three &&
                         Player3_GamepadConnected)
                {
                    if (Player3_GamePadState.IsButtonUp(input.Button) &&
                        Player3_LastGamePadState.IsButtonDown(input.Button))
                    {
                        Player3_LastInput = input;
                        return true;
                    }
                }
                else if (player == PlayerIndex.Four &&
                         Player4_GamepadConnected)
                {
                    if (Player4_GamePadState.IsButtonUp(input.Button) &&
                        Player4_LastGamePadState.IsButtonDown(input.Button))
                    {
                        Player4_LastInput = input;
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool ButtonDown(Input input, PlayerIndex player)
        {
            if (GamepadEnabled)
            {
                if (player == PlayerIndex.One &&
                    Player1_GamepadConnected)
                {
                    if (Player1_GamePadState.IsButtonDown(input.Button))
                    {
                        Player1_LastInput = input;
                        return true;
                    }
                }
                else if (player == PlayerIndex.Two &&
                         Player2_GamepadConnected)
                {
                    if (Player2_GamePadState.IsButtonDown(input.Button))
                    {
                        Player2_LastInput = input;
                        return true;
                    }
                }
                else if (player == PlayerIndex.Three &&
                         Player3_GamepadConnected)
                {
                    if (Player3_GamePadState.IsButtonDown(input.Button))
                    {
                        Player3_LastInput = input;
                        return true;
                    }
                }
                else if (player == PlayerIndex.Four &&
                         Player4_GamepadConnected)
                {
                    if (Player4_GamePadState.IsButtonDown(input.Button))
                    {
                        Player4_LastInput = input;
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool Mouse_LB_Pressed()
        {
            if (mouseState.LeftButton == ButtonState.Released &&
                lastMouseState.LeftButton == ButtonState.Pressed)
            {
                Flush();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Mouse_LB_Pressed_NoFlush()
        {
            return mouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool Mouse_LB_Held()
        {
            return mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool Mouse_RB_Pressed()
        {
            if (mouseState.RightButton == ButtonState.Released &&
                lastMouseState.RightButton == ButtonState.Pressed)
            {
                Flush();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Mouse_RB_Pressed_NoFlush()
        {
            return mouseState.RightButton == ButtonState.Released && lastMouseState.RightButton == ButtonState.Pressed;
        }

        public static bool Mouse_RB_Held()
        {
            return mouseState.RightButton == ButtonState.Pressed && lastMouseState.RightButton == ButtonState.Pressed;
        }

        public static bool Mouse_ScrollUp()
        {
            return ScrollUp;
        }

        public static bool Mouse_ScrollDown()
        {
            return ScrollDown;
        }

        public static void Flush()
        {
            keyboardState = new KeyboardState();
            lastKeyboardState = new KeyboardState();

            if (MouseEnabled)
            {
                mouseState = new MouseState();
                lastMouseState = new MouseState();
                Wheel = 0;
            }

            if (GamepadEnabled)
            {
                Player1_GamePadState = new GamePadState();
                Player1_LastGamePadState = new GamePadState();

                Player2_GamePadState = new GamePadState();
                Player2_LastGamePadState = new GamePadState();

                Player3_GamePadState = new GamePadState();
                Player3_LastGamePadState = new GamePadState();

                Player4_GamePadState = new GamePadState();
                Player4_LastGamePadState = new GamePadState();
            }

            Flushed = true;
        }

        public static bool MouseWithin(Rectangle region)
        {
            if (mouseState.X >= region.X &&
                mouseState.X < region.X + region.Width &&
                mouseState.Y >= region.Y &&
                mouseState.Y < region.Y + region.Height)
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}
