using System;
using System.Linq;
using System.Timers;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using OP_Engine.Utility;

namespace OP_Engine.Inputs
{
    public class GamepadHandler : IDisposable
    {
        #region Variables
        /*
            Set to false if you need the state changes happening independent of
                game speed for triggering gamepad events as soon as they happen
                to not miss button presses
            Warning: this can cause state changes to happen faster than your game 
                is able to detect them without subscribing to the events!
        */
        public bool Updated_By_Game = true;

        /*
            Keep a store of bound buttons for friendly name lookup
                and faster iteration (looping only through buttons your game is using)
        */
        public Dictionary<string, Buttons> ButtonsMapped = new Dictionary<string, Buttons>();

        public bool Connected;
        public PlayerIndex Player;
        public GamePadState gamePadState = new GamePadState();
        public GamePadState lastGamePadState = new GamePadState();
        public Direction LeftStick;
        public Direction RightStick;
        public Direction DPad;

        private Timer gamepadListener = new Timer(1);

        #endregion

        #region Properties



        #endregion

        #region Events

        public event EventHandler OnStateChange;
        public event EventHandler<ButtonEventArgs> OnButtonStateChange;

        #endregion

        #region Constructors

        public GamepadHandler(Game game, PlayerIndex player)
        {
            Player = player;

            gamepadListener.Elapsed += Check_GamepadState;

            OnStateChange += Check_ButtonState;
            OnStateChange += Check_LeftStickState;
            OnStateChange += Check_RightStickState;
            OnStateChange += Check_DPadState;

            game.Activated += Game_Activated;
            game.Deactivated += Game_Deactivated;

            gamepadListener.Start();
        }

        #endregion

        #region Methods

        public virtual void Update()
        {
            if (Updated_By_Game)
            {
                lastGamePadState = gamePadState;
                gamePadState = GamePad.GetState(Player);

                Connected = gamePadState.IsConnected;

                OnStateChange?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual void Check_GamepadState(object sender, ElapsedEventArgs e)
        {
            if (!Updated_By_Game)
            {
                lastGamePadState = gamePadState;
                gamePadState = GamePad.GetState(Player);

                Connected = gamePadState.IsConnected;

                OnStateChange?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual void Check_ButtonState(object sender, EventArgs e)
        {
            if (Connected)
            {
                try
                {
                    List<Buttons> ButtonsPressed = new List<Buttons>();
                    List<Buttons> ButtonsDown = new List<Buttons>();

                    int count = ButtonsMapped.Count;
                    for (int i = 0; i < count; i++)
                    {
                        var button = ButtonsMapped.ElementAt(i).Value;

                        if (gamePadState.IsButtonUp(button) &&
                            lastGamePadState.IsButtonDown(button))
                        {
                            if (!ButtonsPressed.Contains(button))
                            {
                                ButtonsPressed.Add(button);
                            }
                        }
                        else if (gamePadState.IsButtonDown(button))
                        {
                            if (!ButtonsDown.Contains(button))
                            {
                                ButtonsDown.Add(button);
                            }
                        }
                    }

                    if (ButtonsPressed.Count > 0 ||
                        ButtonsDown.Count > 0)
                    {
                        OnButtonStateChange?.Invoke(this, new ButtonEventArgs(ButtonsPressed, ButtonsDown));
                    }
                }
                catch (Exception)
                {
                    //This probably happened from the collection being modified when you were adding new buttons... it's safe to ignore
                }
            }
        }

        public virtual void Check_LeftStickState(object sender, EventArgs e)
        {
            if (Connected)
            {
                if (gamePadState.IsButtonDown(Buttons.LeftThumbstickRight))
                {
                    if (gamePadState.IsButtonDown(Buttons.LeftThumbstickDown))
                    {
                        LeftStick = Direction.DownRight;
                    }
                    else if (gamePadState.IsButtonDown(Buttons.LeftThumbstickUp))
                    {
                        LeftStick = Direction.UpRight;
                    }
                    else
                    {
                        LeftStick = Direction.Right;
                    }
                }
                else if (gamePadState.IsButtonDown(Buttons.LeftThumbstickLeft))
                {
                    if (gamePadState.IsButtonDown(Buttons.LeftThumbstickDown))
                    {
                        LeftStick = Direction.DownLeft;
                    }
                    else if (gamePadState.IsButtonDown(Buttons.LeftThumbstickUp))
                    {
                        LeftStick = Direction.UpLeft;
                    }
                    else
                    {
                        LeftStick = Direction.Left;
                    }
                }
                else if (gamePadState.IsButtonDown(Buttons.LeftThumbstickDown))
                {
                    LeftStick = Direction.Down;
                }
                else if (gamePadState.IsButtonDown(Buttons.LeftThumbstickUp))
                {
                    LeftStick = Direction.Up;
                }
            } 
        }

        public virtual void Check_RightStickState(object sender, EventArgs e)
        {
            if (Connected)
            {
                if (gamePadState.IsButtonDown(Buttons.RightThumbstickRight))
                {
                    if (gamePadState.IsButtonDown(Buttons.RightThumbstickDown))
                    {
                        RightStick = Direction.DownRight;
                    }
                    else if (gamePadState.IsButtonDown(Buttons.RightThumbstickUp))
                    {
                        RightStick = Direction.UpRight;
                    }
                    else
                    {
                        RightStick = Direction.Right;
                    }
                }
                else if (gamePadState.IsButtonDown(Buttons.RightThumbstickLeft))
                {
                    if (gamePadState.IsButtonDown(Buttons.RightThumbstickDown))
                    {
                        RightStick = Direction.DownLeft;
                    }
                    else if (gamePadState.IsButtonDown(Buttons.RightThumbstickUp))
                    {
                        RightStick = Direction.UpLeft;
                    }
                    else
                    {
                        RightStick = Direction.Left;
                    }
                }
                else if (gamePadState.IsButtonDown(Buttons.RightThumbstickDown))
                {
                    RightStick = Direction.Down;
                }
                else if (gamePadState.IsButtonDown(Buttons.RightThumbstickUp))
                {
                    RightStick = Direction.Up;
                }
            }
        }

        public virtual void Check_DPadState(object sender, EventArgs e)
        {
            if (Connected)
            {
                if (gamePadState.IsButtonDown(Buttons.DPadRight))
                {
                    if (gamePadState.IsButtonDown(Buttons.DPadDown))
                    {
                        DPad = Direction.DownRight;
                    }
                    else if (gamePadState.IsButtonDown(Buttons.DPadUp))
                    {
                        DPad = Direction.UpRight;
                    }
                    else
                    {
                        DPad = Direction.Right;
                    }
                }
                else if (gamePadState.IsButtonDown(Buttons.DPadLeft))
                {
                    if (gamePadState.IsButtonDown(Buttons.DPadDown))
                    {
                        DPad = Direction.DownLeft;
                    }
                    else if (gamePadState.IsButtonDown(Buttons.DPadUp))
                    {
                        DPad = Direction.UpLeft;
                    }
                    else
                    {
                        DPad = Direction.Left;
                    }
                }
                else if (gamePadState.IsButtonDown(Buttons.DPadDown))
                {
                    DPad = Direction.Down;
                }
                else if (gamePadState.IsButtonDown(Buttons.DPadUp))
                {
                    DPad = Direction.Up;
                }
            }
        }

        public virtual void Game_Activated(object sender, EventArgs e)
        {
            gamepadListener.Start();
        }

        public virtual void Game_Deactivated(object sender, EventArgs e)
        {
            gamepadListener.Stop();
        }

        public virtual void Dispose()
        {
            gamepadListener.Stop();
            gamepadListener.Dispose();
        }

        #endregion
    }
}
