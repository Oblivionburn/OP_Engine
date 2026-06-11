using Microsoft.Xna.Framework;
using OP_Engine.Time;
using Color = Microsoft.Xna.Framework.Color;

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
            return TimeManager.Now?.Hours switch
            {
                //12 AM
                0 => new(25, 25, 40),

                //1 AM
                1 => new(25, 25, 40),

                //2 AM
                2 => new(25, 25, 40),

                //3 AM
                3 => new(25, 25, 40),

                //4 AM
                4 => new(30, 30, 40),

                //5 AM
                5 => new(50, 50, 40),

                //6 AM
                6 => new(75, 75, 75),

                //7 AM
                7 => new(100, 100, 100),

                //8 AM
                8 => new(150, 150, 150),

                //9 AM
                9 => new(200, 200, 200),

                //10 AM
                10 => new(220, 220, 220),

                //11 AM
                11 => new(240, 240, 240),

                //12 PM
                12 => new(250, 250, 250),

                //1 PM
                13 => new(240, 240, 240),

                //2 PM
                14 => new(230, 230, 230),

                //3 PM
                15 => new(220, 220, 220),

                //4 PM
                16 => new(210, 210, 210),

                //5 PM
                17 => new(200, 200, 200),

                //6 PM
                18 => new(175, 175, 175),

                //7 PM
                19 => new(150, 150, 150),

                //8 PM
                20 => new(150, 100, 100),

                //9 PM
                21 => new(100, 75, 100),

                //10 PM
                22 => new(50, 50, 75),

                //11 PM
                23 => new(25, 25, 50),

                //default
                _ => new(0, 0, 0),
            };
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

            Vector3 result = new()
            {
                X = (((float)DrawColor.R * 100) / 255) / 100,
                Y = (((float)DrawColor.G * 100) / 255) / 100,
                Z = (((float)DrawColor.B * 100) / 255) / 100
            };

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
