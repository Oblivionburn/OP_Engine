﻿using System;
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
                Particle particle = Particles[i];

                particle.Update();

                if (particle.Lifetime <= 0)
                {
                    particle.Dispose();
                    Particles.Remove(particle);
                    i--;
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Particles.Count; i++)
            {
                Particle particle = Particles[i];
                particle.Draw(spriteBatch);
            }
        }

        public virtual Particle GetParticle(string type, Point region, Vector2 velocity, float angle, Color color, float opaque, float size, int lifetime, int waver_min_x, int waver_max_x, int waver_min_y, int waver_max_y)
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

            CryptoRandom random = new CryptoRandom();
            Vector2 location = new Vector2(random.Next(-region.X, region.X), random.Next(-region.Y, region.Y));

            Color new_color = color * opaque;

            return new Particle(texture, location, velocity, angle, new_color, size, lifetime, waver_min_x, waver_max_x, waver_min_y, waver_max_y);
        }

        public virtual Particle GetParticle(string type, Point region, Vector2 velocity, float angle, Color color, float opaque, float size, int lifetime, bool scatter)
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

            Vector2 location;
            CryptoRandom random = new CryptoRandom();

            if (scatter)
            {
                location = new Vector2(random.Next(0, region.X), random.Next(-(texture.Height * 2), region.Y));
            }
            else
            {
                location = new Vector2(region.X, region.Y);
            }

            Color new_color = color * opaque;

            return new Particle(texture, location, velocity, angle, new_color, size, lifetime);
        }

        public void Dispose()
        {
            foreach (Particle particle in Particles)
            {
                particle.Dispose();
            }

            Textures.Clear();
        }

        #endregion
    }
}
