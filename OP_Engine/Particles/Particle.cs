using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace OP_Engine.Particles
{
    public class Particle : IDisposable
    {
        #region Variables

        public Texture2D Texture;
        public Vector2 Location;
        public Vector2 Velocity;
        public float Angle;
        public Color Color;
        public float Size;
        public int Lifetime;

        #endregion

        #region Constructor

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

        #endregion

        #region Methods

        public virtual void Update()
        {
            Lifetime--;
            Location += Velocity;
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
