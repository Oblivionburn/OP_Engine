using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Utility;

namespace OP_Engine.Characters
{
    public class Squad : Something
    {
        #region Variables

        public List<Character> Characters = new List<Character>();
        public long Leader_ID;

        public List<ALocation> Path = new List<ALocation>();
        public Location Destination = new Location();

        public Animator Animator = new Animator();

        public bool Moving;
        public float Moved;
        public float Move_TotalDistance;
        public float Speed;

        #endregion

        #region Events

        public event EventHandler OnMove;
        public event EventHandler OnMovementFinish;

        #endregion

        #region Constructor

        public Squad()
        {
            
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
                        for (int i = 1; i <= Animator.Frames; i++)
                        {
                            if (Moved == (Move_TotalDistance / Animator.Frames) * i)
                            {
                                Animator.Animate(this);
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
                        for (int i = 1; i <= Animator.Frames; i++)
                        {
                            if (Moved == (Move_TotalDistance / Animator.Frames) * i)
                            {
                                Animator.Animate(this);
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
                            for (int i = 1; i <= Animator.Frames; i++)
                            {
                                if (Moved == (Move_TotalDistance / Animator.Frames) * i)
                                {
                                    Animator.Animate(this);
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
                            for (int i = 1; i <= Animator.Frames; i++)
                            {
                                if (Moved == (Move_TotalDistance / Animator.Frames) * i)
                                {
                                    Animator.Animate(this);
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

            Animator.Update(this);
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
            Animator.Reset(this);
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

        public override void Dispose()
        {
            foreach (Character character in Characters)
            {
                character.Dispose();
            }

            base.Dispose();
        }

        #endregion
    }
}
