﻿using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Particles;
using OP_Engine.Utility;

namespace OP_Engine.Weathers
{
    public class Weather : IDisposable
    {
        #region Variables

        public WeatherType Type;
        public int TransitionTime;
        public bool Visible;

        public ParticleManager ParticleManager;

        #endregion

        #region Constructor

        public Weather(List<Texture2D> Particles, WeatherType type, int transitionTime)
        {
            Type = type;
            TransitionTime = transitionTime;
            ParticleManager = new ParticleManager();

            if (Particles != null)
            {
                foreach (Texture2D existing in Particles)
                {
                    if (existing.Name == type.ToString())
                    {
                        ParticleManager.Textures.Add(existing);
                        break;
                    }
                }
            }
        }

        #endregion

        #region Methods

        public void Update(Point resolution)
        {
            if (Visible)
            {
                CryptoRandom random = new CryptoRandom();

                if (WeatherManager.Lightning)
                {
                    int flash = random.Next(0, 1000);
                    if (flash == 13)
                    {
                        WeatherManager.Flash = true;
                    }
                }
                else
                {
                    WeatherManager.Flash = false;
                }

                switch (Type)
                {
                    case WeatherType.Rain:
                        for (int i = 0; i < TransitionTime; i++)
                        {
                            float size = 0;

                            random = new CryptoRandom();
                            int result = random.Next(1, 5);
                            if (result == 1)
                            {
                                size = 0.25f;
                            }
                            else if (result == 2)
                            {
                                size = 0.5f;
                            }
                            else if (result == 3)
                            {
                                size = 0.75f;
                            }
                            else if (result == 4)
                            {
                                size = 1f;
                            }

                            ParticleManager.Particles.Add(ParticleManager.AddParticle(Type.ToString(), resolution, new Vector2(0, 8f), 0, Color.White, 0.5f, size, 16, true));
                        }
                        break;

                    case WeatherType.Snow:
                        for (int i = 0; i < TransitionTime; i++)
                        {
                            float size = 0;
                            Vector2 velocity = new Vector2(0, 0);

                            random = new CryptoRandom();
                            int result = random.Next(1, 5);
                            if (result == 1)
                            {
                                size = 0.25f;

                                random = new CryptoRandom();
                                velocity = new Vector2(random.Next(0, 1), 0.5f);
                            }
                            else if (result == 2)
                            {
                                size = 0.5f;

                                random = new CryptoRandom();
                                velocity = new Vector2(random.Next(0, 2), 1);
                            }
                            else if (result == 3)
                            {
                                size = 0.75f;

                                random = new CryptoRandom();
                                velocity = new Vector2(random.Next(0, 3), 2);
                            }
                            else if (result == 4)
                            {
                                size = 1f;

                                random = new CryptoRandom();
                                velocity = new Vector2(random.Next(0, 4), 4);
                            }

                            ParticleManager.Particles.Add(ParticleManager.AddParticle(Type.ToString(), resolution, velocity, 0, Color.White, 0.8f, size, 128, 0, 3, -1, 3));
                        }
                        break;

                    case WeatherType.Storm:
                        for (int i = 0; i < TransitionTime; i++)
                        {
                            float size = 0;
                            Vector2 velocity = new Vector2(0, 0);
                            int x = 0;
                            int y = 12;
                            float angle = 0;

                            int result = random.Next(1, 5);
                            if (result == 1)
                            {
                                size = 0.25f;
                                x = random.Next(0, 2);
                            }
                            else if (result == 2)
                            {
                                size = 0.5f;
                                x = random.Next(0, 3);
                            }
                            else if (result == 3)
                            {
                                size = 0.75f;
                                x = random.Next(0, 4);
                            }
                            else if (result == 4)
                            {
                                size = 1f;
                                x = random.Next(0, 5);
                            }

                            velocity = new Vector2(x, y);
                            angle = -(float)x / 20;

                            ParticleManager.Particles.Add(ParticleManager.AddParticle(Type.ToString(), resolution, velocity, angle, Color.White, 0.5f, size, 16, true));
                        }
                        break;

                    case WeatherType.Fog:
                        for (int i = 0; i < TransitionTime; i++)
                        {
                            float size = 0;

                            random = new CryptoRandom();
                            float opacity = random.Next(1, 11);
                            opacity = opacity / 100;

                            Vector2 velocity = new Vector2(0, 0);

                            random = new CryptoRandom();
                            int result = random.Next(1, 5);
                            if (result == 1)
                            {
                                size = 0.25f;

                                random = new CryptoRandom();
                                velocity = new Vector2(random.Next(-1, 2), random.Next(-1, 2));
                            }
                            else if (result == 2)
                            {
                                size = 0.5f;

                                random = new CryptoRandom();
                                velocity = new Vector2(random.Next(-2, 3), random.Next(-2, 3));
                            }
                            else if (result == 3)
                            {
                                size = 0.75f;

                                random = new CryptoRandom();
                                velocity = new Vector2(random.Next(-3, 4), random.Next(-3, 4));
                            }
                            else if (result == 4)
                            {
                                size = 1f;

                                random = new CryptoRandom();
                                velocity = new Vector2(random.Next(-4, 5), random.Next(-4, 5));
                            }

                            ParticleManager.Particles.Add(ParticleManager.AddParticle(Type.ToString(), resolution, velocity, 0, Color.White, opacity, size, 16, true));
                        }
                        break;
                }

                ParticleManager.Update();
                random.Dispose();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                ParticleManager.Draw(spriteBatch);
            }
        }

        public void Dispose()
        {
            ParticleManager.Dispose();
        }

        #endregion
    }
}
