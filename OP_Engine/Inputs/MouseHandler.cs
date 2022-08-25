using System;
using System.Timers;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace OP_Engine.Inputs
{
    public class MouseHandler : IDisposable
    {
        #region Variables
        /*
            Set to false if you need the state changes happening independent of
                game speed for triggering mouse events as soon as they happen
                to not miss button presses
            Warning: this can cause state changes to happen faster than your game 
                is able to detect them without subscribing to the events!
        */
        public bool Updated_By_Game = true;

        public MouseState mouseState = new MouseState();
        public MouseState lastMouseState = new MouseState();

        private Timer mouseListener = new Timer(1);

        #endregion

        #region Properties

        public int X
        {
            get { return mouseState.X; }
        }

        public int Y
        {
            get { return mouseState.Y; }
        }

        public bool Moved
        {
            get { return mouseState.X != lastMouseState.X || mouseState.Y != lastMouseState.Y; }
        }

        public bool LB_Pressed
        {
            get { return mouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed; }
        }

        public bool LB_Pressed_Flush
        {
            get
            {
                if (mouseState.LeftButton == ButtonState.Released &&
                    lastMouseState.LeftButton == ButtonState.Pressed)
                {
                    Flush();
                    return true;
                }

                return false;
            }
        }

        public bool LB_Held
        {
            get { return mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Pressed; }
        }

        public bool RB_Pressed
        {
            get { return mouseState.RightButton == ButtonState.Released && lastMouseState.RightButton == ButtonState.Pressed; }
        }

        public bool RB_Pressed_Flush
        {
            get
            {
                if (mouseState.RightButton == ButtonState.Released &&
                    lastMouseState.RightButton == ButtonState.Pressed)
                {
                    Flush();
                    return true;
                }

                return false;
            }
        }

        public bool RB_Held
        {
            get { return mouseState.RightButton == ButtonState.Pressed && lastMouseState.RightButton == ButtonState.Pressed; }
        }

        public bool ScrolledUp
        {
            get { return lastMouseState.ScrollWheelValue < mouseState.ScrollWheelValue; }
        }

        public bool ScrolledDown
        {
            get { return lastMouseState.ScrollWheelValue > mouseState.ScrollWheelValue; }
        }

        public int Wheel
        {
            get { return mouseState.ScrollWheelValue - lastMouseState.ScrollWheelValue; }
        }

        #endregion

        #region Events

        public event EventHandler StateChanged;
        public event EventHandler LeftButtonPressed;
        public event EventHandler LeftButtonHeld;
        public event EventHandler RightButtonPressed;
        public event EventHandler RightButtonHeld;
        public event EventHandler WheelUp;
        public event EventHandler WheelDown;
        public event EventHandler MouseMoved;

        #endregion

        #region Constructors

        public MouseHandler(Game game)
        {
            mouseListener.Elapsed += Check_MouseState;

            StateChanged += Check_LeftButton;
            StateChanged += Check_RightButton;
            StateChanged += Check_MouseWheel;
            StateChanged += Check_MouseMoved;

            game.Activated += Game_Activated;
            game.Deactivated += Game_Deactivated;

            mouseListener.Start();
        }

        #endregion

        #region Methods

        public virtual void Update()
        {
            if (Updated_By_Game)
            {
                lastMouseState = mouseState;
                mouseState = Mouse.GetState();

                StateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual void Check_MouseState(object sender, ElapsedEventArgs e)
        {
            if (!Updated_By_Game)
            {
                lastMouseState = mouseState;
                mouseState = Mouse.GetState();

                StateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual void Check_LeftButton(object sender, EventArgs e)
        {
            if (mouseState.LeftButton == ButtonState.Released &&
                lastMouseState.LeftButton == ButtonState.Pressed)
            {
                LeftButtonPressed?.Invoke(this, EventArgs.Empty);
            }
            else if (mouseState.LeftButton == ButtonState.Pressed)
            {
                LeftButtonHeld?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual void Check_RightButton(object sender, EventArgs e)
        {
            if (mouseState.RightButton == ButtonState.Released &&
                lastMouseState.RightButton == ButtonState.Pressed)
            {
                RightButtonPressed?.Invoke(this, EventArgs.Empty);
            }
            else if (mouseState.RightButton == ButtonState.Pressed)
            {
                RightButtonHeld?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual void Check_MouseWheel(object sender, EventArgs e)
        {
            if (lastMouseState.ScrollWheelValue < mouseState.ScrollWheelValue)
            {
                WheelUp?.Invoke(this, EventArgs.Empty);
            }
            else if (lastMouseState.ScrollWheelValue > mouseState.ScrollWheelValue)
            {
                WheelDown?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual void Check_MouseMoved(object sender, EventArgs e)
        {
            if (mouseState.X != lastMouseState.X || 
                mouseState.Y != lastMouseState.Y)
            {
                MouseMoved?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual void Flush()
        {
            lastMouseState = Mouse.GetState();
            mouseState = Mouse.GetState();
        }

        public virtual void Game_Activated(object sender, EventArgs e)
        {
            mouseListener.Start();
        }

        public virtual void Game_Deactivated(object sender, EventArgs e)
        {
            mouseListener.Stop();
        }

        public virtual void Dispose()
        {
            mouseListener.Stop();
            mouseListener.Dispose();
        }

        #endregion
    }
}
