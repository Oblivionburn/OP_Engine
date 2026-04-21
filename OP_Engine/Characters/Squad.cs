using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OP_Engine.Enums;
using OP_Engine.Utility;

namespace OP_Engine.Characters
{
    public class Squad : IDisposable
    {
        #region Variables

        public long ID;
        public string Name;
        public string Type;

        public Region Region;
        public Texture2D Texture;
        public Rectangle Image;
        public bool Visible;
        public Color DrawColor;

        public Direction Direction;
        public Location Location;
        public Location Destination;

        public Army Army;

        public List<Character> Characters;
        public List<ALocation> Path;

        public long Leader_ID;
        public bool Moving;
        public float Moved;
        public float Move_TotalDistance;
        public float Speed;
        public int Frames;

        #endregion

        #region Events

        public event EventHandler OnMove;
        public event EventHandler OnMovementFinish;

        #endregion

        #region Constructor

        public Squad()
        {
            Characters = new List<Character>();
            Path = new List<ALocation>();
        }

        #endregion

        #region Methods

        public virtual void Update()
        {
            if (Moving &&
                Region != null)
            {
                if (Destination.X > Location.X)
                {
                    MoveTo(new Vector2(Region.X + Speed, Region.Y));

                    if (Moved == Move_TotalDistance)
                    {
                        Location.X++;
                        FinishMove();
                    }
                    else if (Moved > Move_TotalDistance)
                    {
                        MoveTo(new Vector2(Region.X - Speed, Region.Y));
                        Location.X = Destination.X;
                        FinishMove();
                    }
                    else
                    {
                        for (int i = 1; i <= Frames; i++)
                        {
                            if (Moved == (Move_TotalDistance / Frames) * i)
                            {
                                Animate();
                                break;
                            }
                        }
                    }
                }
                else if (Destination.X < Location.X)
                {
                    MoveTo(new Vector2(Region.X - Speed, Region.Y));

                    if (Moved == Move_TotalDistance)
                    {
                        Location.X--;
                        FinishMove();
                    }
                    else if (Moved > Move_TotalDistance)
                    {
                        MoveTo(new Vector2(Region.X + Speed, Region.Y));
                        Location.X = Destination.X;
                        FinishMove();
                    }
                    else
                    {
                        for (int i = 1; i <= Frames; i++)
                        {
                            if (Moved == (Move_TotalDistance / Frames) * i)
                            {
                                Animate();
                                break;
                            }
                        }
                    }
                }
                else
                {
                    if (Destination.Y > Location.Y)
                    {
                        MoveTo(new Vector2(Region.X, Region.Y + Speed));

                        if (Moved == Move_TotalDistance)
                        {
                            Location.Y++;
                            FinishMove();
                        }
                        else if (Moved > Move_TotalDistance)
                        {
                            MoveTo(new Vector2(Region.X, Region.Y - Speed));
                            Location.Y = Destination.Y;
                            FinishMove();
                        }
                        else
                        {
                            for (int i = 1; i <= Frames; i++)
                            {
                                if (Moved == (Move_TotalDistance / Frames) * i)
                                {
                                    Animate();
                                    break;
                                }
                            }
                        }
                    }
                    else if (Destination.Y < Location.Y)
                    {
                        MoveTo(new Vector2(Region.X, Region.Y - Speed));

                        if (Moved == Move_TotalDistance)
                        {
                            Location.Y--;
                            FinishMove();
                        }
                        else if (Moved > Move_TotalDistance)
                        {
                            MoveTo(new Vector2(Region.X, Region.Y + Speed));
                            Location.Y = Destination.Y;
                            FinishMove();
                        }
                        else
                        {
                            for (int i = 1; i <= Frames; i++)
                            {
                                if (Moved == (Move_TotalDistance / Frames) * i)
                                {
                                    Animate();
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        FinishMove();
                    }
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution)
        {
            if (Visible &&
                Texture != null &&
                Region != null)
            {
                if (Region.X >= (Texture.Width * -2) && Region.X < resolution.X + (Texture.Width * 2))
                {
                    if (Region.Y >= (Texture.Height * -2) && Region.Y < resolution.Y + (Texture.Height * 2))
                    {
                        if (DrawColor != new Color(0, 0, 0, 0))
                        {
                            spriteBatch.Draw(Texture, Region.ToRectangle, Image, DrawColor);
                        }
                        else
                        {
                            spriteBatch.Draw(Texture, Region.ToRectangle, Image, Color.White);
                        }
                    }
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution, Color color)
        {
            if (Visible &&
                Texture != null &&
                Region != null)
            {
                if (Region.X >= (Texture.Width * -2) && Region.X < resolution.X + (Texture.Width * 2))
                {
                    if (Region.Y >= (Texture.Height * -2) && Region.Y < resolution.Y + (Texture.Height * 2))
                    {
                        if (DrawColor != new Color(0, 0, 0, 0))
                        {
                            spriteBatch.Draw(Texture, Region.ToRectangle, Image, DrawColor);
                        }
                        else
                        {
                            spriteBatch.Draw(Texture, Region.ToRectangle, Image, color);
                        }
                    }
                }
            }
        }

        public virtual void MoveTo(Vector2 region)
        {
            if (Region != null)
            {
                Region.X = region.X;
                Region.Y = region.Y;
                Moved += Speed;

                OnMove?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual void FinishMove()
        {
            Moved = 0;
            Moving = false;
            OnMovementFinish?.Invoke(this, EventArgs.Empty);
            ResetAnimation();
        }

        public virtual void Animate()
        {
            int X = Image.X + Image.Width;
            if (X >= Texture.Width)
            {
                X = 0;
            }

            Image = new Rectangle(X, Image.Y, Image.Width, Image.Height);
        }

        public virtual void ResetAnimation()
        {
            Image = new Rectangle(0, Image.Y, Image.Width, Image.Height);
        }

        public virtual void AddCharacter(Character character)
        {
            character.Army = Army;
            character.Squad = this;
            Characters.Add(character);
        }

        public virtual Character GetCharacter(long id)
        {
            int count = Characters.Count;
            for (int i = 0; i < count; i++)
            {
                Character existing = Characters[i];
                if (existing != null)
                {
                    if (existing.ID == id)
                    {
                        return existing;
                    }
                }
            }

            return null;
        }

        public virtual Character GetCharacter(string name)
        {
            int count = Characters.Count;
            for (int i = 0; i < count; i++)
            {
                Character existing = Characters[i];
                if (existing != null)
                {
                    if (existing.Name == name)
                    {
                        return existing;
                    }
                }
            }

            return null;
        }

        public virtual Character GetCharacter (Vector2 formation)
        {
            int count = Characters.Count;
            for (int i = 0; i < count; i++)
            {
                Character existing = Characters[i];
                if (existing != null)
                {
                    if (existing.Formation.X == formation.X &&
                        existing.Formation.Y == formation.Y)
                    {
                        return existing;
                    }
                }
            }

            return null;
        }

        public virtual Character GetLeader()
        {
            int count = Characters.Count;
            for (int i = 0; i < count; i++)
            {
                Character existing = Characters[i];
                if (existing != null)
                {
                    if (existing.ID == Leader_ID)
                    {
                        return existing;
                    }
                }
            }

            return null;
        }

        public virtual void Dispose()
        {
            foreach (Character character in Characters)
            {
                character.Dispose();
            }

            foreach (ALocation location in Path)
            {
                location.Dispose();
            }
        }

        #endregion
    }
}
