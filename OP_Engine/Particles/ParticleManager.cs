using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Utility;

namespace OP_Engine.Particles
{
    public class ParticleManager : IDisposable
    {
        #region Variables

        public List<Particle> Particles = new List<Particle>();
        public List<Texture2D> Textures = new List<Texture2D>();

        private CryptoRandom random = new CryptoRandom();

        #endregion

        #region Constructor

        public ParticleManager()
        {
            
        }

        #endregion

        #region Methods

        public virtual void Update()
        {
            for (int i = 0; i < Particles.Count; i++)
            {
                Particles[i].Update();

                if (Particles[i].Lifetime <= 0)
                {
                    Particle particle = Particles[i];
                    particle.Dispose();
                    Particles.Remove(particle);
                    i--;
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particle particle in Particles)
            {
                particle.Draw(spriteBatch);
            }
        }

        public virtual Particle AddParticle(string type, Point region, Vector2 velocity, float angle, Color color, float opaque, float size, int lifetime, bool waver, bool scatter)
        {
            Texture2D texture = null;

            foreach (Texture2D existing in Textures)
            {
                if (existing.Name == type)
                {
                    texture = existing;
                    break;
                }
            }

            Vector2 position = new Vector2(0, 0);
            if (waver)
            {
                position = new Vector2(random.Next(-region.X, region.X), random.Next(-region.Y, region.Y));
            }
            else if (scatter)
            {
                position = new Vector2(random.Next(0, region.X), random.Next(-(texture.Height * 2), region.Y));
            }
            else
            {
                position = new Vector2(region.X, region.Y);
            }

            Color new_color = color * opaque;

            return new Particle(texture, position, velocity, angle, new_color, size, lifetime);
        }

        public void Dispose()
        {
            foreach (Particle particle in Particles)
            {
                particle.Dispose();
            }

            foreach (Texture2D texture in Textures)
            {
                texture.Dispose();
            }
        }

        #endregion
    }
}
