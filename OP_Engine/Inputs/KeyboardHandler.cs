using System;
using System.Linq;
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

        public event EventHandler StateChanged;
        public event EventHandler<KeyEventArgs> KeyStateChanged;

        #endregion

        #region Constructor

        public KeyboardHandler(Game game)
        {
            keyboardListener.Elapsed += Check_KeyboardState;
            StateChanged += Check_KeyState;

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

                StateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual void Check_KeyboardState(object sender, ElapsedEventArgs e)
        {
            if (!Updated_By_Game)
            {
                lastKeyboardState = keyboardState;
                keyboardState = Keyboard.GetState();

                StateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual void Check_KeyState(object sender, EventArgs e)
        {
            try
            {
                List<Keys> KeysPressed = new List<Keys>();
                List<Keys> KeysDown = new List<Keys>();

                for (int i = 0; i < KeysMapped.Count; i++)
                {
                    Keys key = KeysMapped.ElementAt(i).Value;

                    if (keyboardState.IsKeyUp(key) &&
                        lastKeyboardState.IsKeyDown(key))
                    {
                        if (!KeysPressed.Contains(key))
                        {
                            KeysPressed.Add(key);
                        }
                    }
                    else if (keyboardState.IsKeyDown(key))
                    {
                        if (!KeysDown.Contains(key))
                        {
                            KeysDown.Add(key);
                        }
                    }
                }

                if (KeysPressed.Count > 0 ||
                    KeysDown.Count > 0)
                {
                    KeyStateChanged?.Invoke(this, new KeyEventArgs(KeysPressed, KeysDown));
                }
            }
            catch (Exception)
            {
                //This probably happened from the collection being modified when you were adding new keys... it's safe to ignore
            }
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
