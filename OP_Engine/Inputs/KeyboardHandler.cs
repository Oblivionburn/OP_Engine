using System;
using System.Timers;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace OP_Engine.Inputs
{
    public class KeyboardHandler : IDisposable
    {
        #region Variables
        /*
            Set to false if you need the state changes happening independent of
                game speed for triggering keyboard events as soon as they happen
                to not miss key strokes
            Warning: this can cause state changes to happen faster than your game 
                is able to detect them without subscribing to the events!
        */
        public bool Updated_By_Game = true;

        /*
            Keep a store of bound keys for friendly name lookup
                and faster iteration (looping only through keys your game is using)
        */
        public Dictionary<string, Keys> KeysMapped = new Dictionary<string, Keys>();

        public KeyboardState keyboardState = new KeyboardState();
        public KeyboardState lastKeyboardState = new KeyboardState();

        private Timer keyboardListener = new Timer(1);

        #endregion

        #region Properties



        #endregion

        #region Events

        public event EventHandler OnStateChange;
        public event EventHandler<KeyEventArgs> OnKeyStateChange;

        #endregion

        #region Constructor

        public KeyboardHandler(Game game)
        {
            keyboardListener.Elapsed += Check_KeyboardState;
            OnStateChange += Check_KeyState;

            game.Activated += Game_Activated;
            game.Deactivated += Game_Deactivated;

            keyboardListener.Start();
        }

        #endregion

        #region Methods

        public virtual void Update()
        {
            if (Updated_By_Game)
            {
                lastKeyboardState = keyboardState;
                keyboardState = Keyboard.GetState();

                OnStateChange?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual void Check_KeyboardState(object sender, ElapsedEventArgs e)
        {
            if (!Updated_By_Game)
            {
                lastKeyboardState = keyboardState;
                keyboardState = Keyboard.GetState();

                OnStateChange?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual void Check_KeyState(object sender, EventArgs e)
        {
            try
            {
                List<Keys> KeysPressed = new List<Keys>();
                List<Keys> KeysDown = new List<Keys>();

                foreach (var key in KeysMapped)
                {
                    if (keyboardState.IsKeyUp(key.Value) &&
                        lastKeyboardState.IsKeyDown(key.Value))
                    {
                        if (!KeysPressed.Contains(key.Value))
                        {
                            KeysPressed.Add(key.Value);
                        }
                    }
                    else if (keyboardState.IsKeyDown(key.Value))
                    {
                        if (!KeysDown.Contains(key.Value))
                        {
                            KeysDown.Add(key.Value);
                        }
                    }
                }

                if (KeysPressed.Count > 0 ||
                    KeysDown.Count > 0)
                {
                    OnKeyStateChange?.Invoke(this, new KeyEventArgs(KeysPressed, KeysDown));
                }
            }
            catch (Exception)
            {
                //This probably happened from the collection being modified when you were adding new keys... it's safe to ignore
            }
        }

        public virtual void Flush()
        {
            lastKeyboardState = Keyboard.GetState();
            keyboardState = Keyboard.GetState();
        }

        public virtual void Game_Activated(object sender, EventArgs e)
        {
            keyboardListener.Start();
        }

        public virtual void Game_Deactivated(object sender, EventArgs e)
        {
            keyboardListener.Stop();
        }

        public virtual void Dispose()
        {
            keyboardListener.Stop();
            keyboardListener.Dispose();
        }

        #endregion
    }
}
