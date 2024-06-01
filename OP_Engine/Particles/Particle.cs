﻿using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Utility;

namespace OP_Engine.Particles
{
    public class Particle : IDisposable
    {
        #region Variables

        private CryptoRandom random;

        public Texture2D Texture;
        public Vector2 Location;
        public Vector2 Velocity;
        public float Angle;
        public Color Color;
        public float Size;
        public int Lifetime;

        public bool Wavering;
        public int Waver_Min_X;
        public int Waver_Max_X;
        public int Waver_Min_Y;
        public int Waver_Max_Y;

        #endregion

        #region Constructors

        public Particle(Texture2D texture, Vector2 location, Vector2 velocity, float angle, Color color, float size, int lifetime)
        {
            Texture = texture;
            Location = location;
            Velocity = velocity;
            Angle = angle;
            Color = color;
            Size = size;
            Lifetime = lifetime;
        }

        public Particle(Texture2D texture, Vector2 location, Vector2 velocity, float angle, Color color, float size, int lifetime,
            int waver_min_x, int waver_max_x, int waver_min_y, int waver_max_y)
        {
            Texture = texture;
            Location = location;
            Velocity = velocity;
            Angle = angle;
            Color = color;
            Size = size;
            Lifetime = lifetime;

            Wavering = true;
            Waver_Min_X = waver_min_x;
            Waver_Max_X = waver_max_x;
            Waver_Min_Y = waver_min_y;
            Waver_Max_Y = waver_max_y;
        }

        #endregion

        #region Methods

        public virtual void Update()
        {
            Lifetime--;

            if (Wavering)
            {
                random = new CryptoRandom();
                int chance = random.Next(0, 3);
                if (chance == 0)
                {
                    float x = Velocity.X;
                    float y = Velocity.X;

                    Vector2 Waver_Velocity = new Vector2(Velocity.X, Velocity.Y);

                    if (Velocity.X != 0)
                    {
                        y = random.Next(Waver_Min_Y, Waver_Max_Y + 1);
                    }
                    if (Velocity.Y != 0)
                    {
                        x = random.Next(Waver_Min_X, Waver_Max_X + 1);
                    }

                    Waver_Velocity.X += x;
                    Waver_Velocity.Y += y;

                    Location += Waver_Velocity;
                }
                else
                {
                    Location += Velocity;
                }
            }
            else
            {
                Location += Velocity;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Texture != null)
            {
                Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
                Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

                spriteBatch.Draw(Texture, Location, sourceRectangle, Color, Angle, origin, Size, SpriteEffects.None, 0f);
            }
        }

        public void Dispose()
        {

        }

        #endregion
    }
}
