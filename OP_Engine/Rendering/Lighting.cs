using System;

using Microsoft.Xna.Framework;

using OP_Engine.Time;

namespace OP_Engine.Rendering
{
    public class Lighting : IDisposable
    {
        #region Variables

        public Color currentColor;
        public Color DrawColor;
        public float LerpAmount = 0;
        public bool FadingIn;
        public bool FadingOut;

        #endregion

        #region Constructors

        public Lighting()
        {

        }

        #endregion

        #region Methods

        public virtual void Update()
        {
            if (FadingIn)
            {
                FadeIn();
            }
            else if (FadingOut)
            {
                FadeOut();
            }
            else
            {
                DrawColor = Color.Lerp(currentColor, GetColor(), LerpAmount);
            }

            if (LerpAmount >= 1)
            {
                currentColor = new Color(DrawColor.R, DrawColor.G, DrawColor.B);
            }
        }

        public virtual Color GetColor()
        {
            if (TimeManager.Now.Hours == 0)
            {
                //12 AM
                return new Color(25, 25, 40);
            }
            else if (TimeManager.Now.Hours == 1)
            {
                //1 AM
                return new Color(25, 25, 40);
            }
            else if (TimeManager.Now.Hours == 2)
            {
                //2 AM
                return new Color(25, 25, 40);
            }
            else if (TimeManager.Now.Hours == 3)
            {
                //3 AM
                return new Color(25, 25, 40);
            }
            else if (TimeManager.Now.Hours == 4)
            {
                //4 AM
                return new Color(30, 30, 40);
            }
            else if (TimeManager.Now.Hours == 5)
            {
                //5 AM
                return new Color(50, 50, 40);
            }
            else if (TimeManager.Now.Hours == 6)
            {
                //6 AM
                return new Color(75, 75, 75);
            }
            else if (TimeManager.Now.Hours == 7)
            {
                //7 AM
                return new Color(100, 100, 100);
            }
            else if (TimeManager.Now.Hours == 8)
            {
                //8 AM
                return new Color(150, 150, 150);
            }
            else if (TimeManager.Now.Hours == 9)
            {
                //9 AM
                return new Color(200, 200, 200);
            }
            else if (TimeManager.Now.Hours == 10)
            {
                //10 AM
                return new Color(220, 220, 220);
            }
            else if (TimeManager.Now.Hours == 11)
            {
                //11 AM
                return new Color(240, 240, 240);
            }
            else if (TimeManager.Now.Hours == 12)
            {
                //12 PM
                return new Color(250, 250, 250);
            }
            else if (TimeManager.Now.Hours == 13)
            {
                //1 PM
                return new Color(240, 240, 240);
            }
            else if (TimeManager.Now.Hours == 14)
            {
                //2 PM
                return new Color(230, 230, 230);
            }
            else if (TimeManager.Now.Hours == 15)
            {
                //3 PM
                return new Color(220, 220, 220);
            }
            else if (TimeManager.Now.Hours == 16)
            {
                //4 PM
                return new Color(210, 210, 210);
            }
            else if (TimeManager.Now.Hours == 17)
            {
                //5 PM
                return new Color(200, 200, 200);
            }
            else if (TimeManager.Now.Hours == 18)
            {
                //6 PM
                return new Color(175, 175, 175);
            }
            else if (TimeManager.Now.Hours == 19)
            {
                //7 PM
                return new Color(150, 150, 150);
            }
            else if (TimeManager.Now.Hours == 20)
            {
                //8 PM
                return new Color(150, 100, 100);
            }
            else if (TimeManager.Now.Hours == 21)
            {
                //9 PM
                return new Color(100, 75, 100);
            }
            else if (TimeManager.Now.Hours == 22)
            {
                //10 PM
                return new Color(50, 50, 75);
            }
            else if (TimeManager.Now.Hours == 23)
            {
                //11 PM
                return new Color(25, 25, 50);
            }

            return new Color(0, 0, 0);
        }

        public virtual void FadeIn()
        {
            DrawColor = Color.Lerp(Color.Black, currentColor, LerpAmount);
        }

        public virtual void FadeOut()
        {
            DrawColor = Color.Lerp(currentColor, Color.Black, LerpAmount);
        }

        public virtual Vector3 ColorToVector3()
        {
            //Used for HLSL shader

            Vector3 result = new Vector3();

            result.X = (((float)DrawColor.R * 100) / 255) / 100;
            result.Y = (((float)DrawColor.G * 100) / 255) / 100;
            result.Z = (((float)DrawColor.B * 100) / 255) / 100;

            return result;
        }

        public virtual void Reset()
        {
            currentColor = GetColor();
            DrawColor = GetColor();
            LerpAmount = 0;
        }

        public virtual void Dispose()
        {

        }

        #endregion
    }
}
