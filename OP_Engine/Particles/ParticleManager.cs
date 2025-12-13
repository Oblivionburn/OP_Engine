using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OP_Engine.Utility;
using System.Threading.Tasks;

namespace OP_Engine.Particles
{
    public class ParticleManager : IDisposable
    {
        #region Variables

        public static long id;

        public List<Texture2D> Textures;

        public ConcurrentDictionary<long, Particle> Particles;
        public ConcurrentBag<Particle> ParticleBag;

        #endregion

        #region Constructor

        public ParticleManager()
        {
            Textures = new List<Texture2D>();
            Particles = new ConcurrentDictionary<long, Particle>();
            ParticleBag = new ConcurrentBag<Particle>();
        }

        #endregion

        #region Methods

        public static long GetID()
        {
            id++;
            return id;
        }

        public virtual void Update()
        {
            ParticleBag = new ConcurrentBag<Particle>();

            Parallel.ForEach(Particles, item =>
            {
                if (item.Value.Lifetime > 0)
                {
                    ParticleBag.Add(item.Value);
                }
                else
                {
                    Particles.TryRemove(item.Key, out Particle value);
                }
            });

            foreach (Particle particle in ParticleBag)
            {
                particle?.Update();
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particle particle in ParticleBag)
            {
                particle?.Draw(spriteBatch);
            }
        }

        public virtual Particle CreateParticle(string type, Point region, Vector2 velocity, float angle, Color color, float opaque, float size, int lifetime, int waver_min_x, int waver_max_x, int waver_min_y, int waver_max_y)
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

        public virtual Particle CreateParticle(string type, Point region, Vector2 velocity, float angle, Color color, float opaque, float size, int lifetime, bool scatter)
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
            Textures = null;
            Particles = null;
            ParticleBag = null;
        }

        #endregion
    }
}
