using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Controls;
using OP_Engine.Inventories;
using OP_Engine.Jobs;
using OP_Engine.Spells;
using OP_Engine.Utility;

namespace OP_Engine.Characters
{
    public class Character : Something
    {
        #region Variables

        public long MapID;
        public long LayerID;
        public long SquadID;
        public string Gender;
        public bool Interacting;
        public bool Dead;
        public bool Unconscious;

        public List<Something> Stats = new List<Something>();
        public List<Something> Skills = new List<Something>();
        public List<Something> StatusEffects = new List<Something>();
        public List<BodyPart> BodyParts = new List<BodyPart>();

        public ProgressBar HealthBar;
        public ProgressBar ManaBar;

        public Animator Animator;
        public Spellbook Spellbook;
        public Inventory Inventory;
        public Job Job;
        public Pathing Pathing;

        public bool InCombat;
        public bool CombatTurn;
        public int CombatStep;
        public long Target_ID;

        public Vector2 Formation;
        public Vector3 Destination;
        
        public bool Travelling;
        public float Travelled;
        public float Travel_TotalDistance;
        public float Speed;

        #endregion

        #region Constructor

        public Character()
        {
            Formation = default;
            Location = default;
            Destination = default;

            Pathing = new Pathing();
            Job = new Job();

            Region = new Region();
            Image = default;

            HealthBar = new ProgressBar();
            ManaBar = new ProgressBar();

            Inventory = new Inventory();
            Spellbook = new Spellbook();

            Animator = new Animator();
        }

        #endregion

        #region Methods

        public virtual void Update()
        {
            if (Travelling &&
                Region != null)
            {
                if (Destination.X > Location.X)
                {
                    Animator.FaceEast(this);
                    MoveTo(new Vector2(Region.X + Speed, Region.Y));

                    if (Travelled == Travel_TotalDistance)
                    {
                        Location.X++;
                        Travelled = 0;
                        Travelling = false;
                        Animator.Reset(this);
                    }
                    else if (Travelled > Travel_TotalDistance)
                    {
                        MoveTo(new Vector2(Region.X - Speed, Region.Y));
                        Location.X = Destination.X;
                        Travelled = 0;
                        Travelling = false;
                        Animator.Reset(this);
                    }
                    else
                    {
                        for (int i = 1; i <= Animator.Frames; i++)
                        {
                            if (Travelled == (Travel_TotalDistance / Animator.Frames) * i)
                            {
                                Animator.Animate(this);
                                break;
                            }
                        }
                    }
                }
                else if (Destination.X < Location.X)
                {
                    Animator.FaceWest(this);
                    MoveTo(new Vector2(Region.X - Speed, Region.Y));

                    if (Travelled == Travel_TotalDistance)
                    {
                        Location.X--;
                        Travelled = 0;
                        Travelling = false;
                        Animator.Reset(this);
                    }
                    else if (Travelled > Travel_TotalDistance)
                    {
                        MoveTo(new Vector2(Region.X + Speed, Region.Y));
                        Location.X = Destination.X;
                        Travelled = 0;
                        Travelling = false;
                        Animator.Reset(this);
                    }
                    else
                    {
                        for (int i = 1; i <= Animator.Frames; i++)
                        {
                            if (Travelled == (Travel_TotalDistance / Animator.Frames) * i)
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
                        Animator.FaceSouth(this);
                        MoveTo(new Vector2(Region.X, Region.Y + Speed));

                        if (Travelled == Travel_TotalDistance)
                        {
                            Location.Y++;
                            Travelled = 0;
                            Travelling = false;
                            Animator.Reset(this);
                        }
                        else if (Travelled > Travel_TotalDistance)
                        {
                            MoveTo(new Vector2(Region.X, Region.Y - Speed));
                            Location.Y = Destination.Y;
                            Travelled = 0;
                            Travelling = false;
                            Animator.Reset(this);
                        }
                        else
                        {
                            for (int i = 1; i <= Animator.Frames; i++)
                            {
                                if (Travelled == (Travel_TotalDistance / Animator.Frames) * i)
                                {
                                    Animator.Animate(this);
                                    break;
                                }
                            }
                        }
                    }
                    else if (Destination.Y < Location.Y)
                    {
                        Animator.FaceNorth(this);
                        MoveTo(new Vector2(Region.X, Region.Y - Speed));

                        if (Travelled == Travel_TotalDistance)
                        {
                            Location.Y--;
                            Travelled = 0;
                            Travelling = false;
                            Animator.Reset(this);
                        }
                        else if (Travelled > Travel_TotalDistance)
                        {
                            MoveTo(new Vector2(Region.X, Region.Y + Speed));
                            Location.Y = Destination.Y;
                            Travelled = 0;
                            Travelling = false;
                            Animator.Reset(this);
                        }
                        else
                        {
                            for (int i = 1; i <= Animator.Frames; i++)
                            {
                                if (Travelled == (Travel_TotalDistance / Animator.Frames) * i)
                                {
                                    Animator.Animate(this);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        Travelled = 0;
                        Travelling = false;
                        Animator.Reset(this);
                    }
                }
            }

            Animator.Update(this);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution)
        {
            if (Visible &&
                Region != null)
            {
                if (Region.X >= (Texture.Width * -2) && Region.X < resolution.X + (Texture.Width * 2))
                {
                    if (Region.Y >= (Texture.Height * -2) && Region.Y < resolution.Y + (Texture.Height * 2))
                    {
                        if (DrawColor != new Color(0, 0, 0, 0))
                        {
                            spriteBatch.Draw(Texture, Region.ToRectangle, Image, DrawColor);
                            Inventory.Draw(spriteBatch, resolution, DrawColor);
                        }
                        else
                        {
                            spriteBatch.Draw(Texture, Region.ToRectangle, Image, Color.White);
                            Inventory.Draw(spriteBatch, resolution, Color.White);
                        }
                    }
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution, Color color)
        {
            if (Visible &&
                Region != null)
            {
                if (Region.X >= (Texture.Width * -2) && Region.X < resolution.X + (Texture.Width * 2))
                {
                    if (Region.Y >= (Texture.Height * -2) && Region.Y < resolution.Y + (Texture.Height * 2))
                    {
                        if (DrawColor != new Color(0, 0, 0, 0))
                        {
                            spriteBatch.Draw(Texture, Region.ToRectangle, Image, DrawColor);
                            Inventory.Draw(spriteBatch, resolution, DrawColor);
                        }
                        else
                        {
                            spriteBatch.Draw(Texture, Region.ToRectangle, Image, color);
                            Inventory.Draw(spriteBatch, resolution, color);
                        }
                    }
                }
            }
        }

        public virtual void MoveTo(Vector2 location)
        {
            if (Region != null)
            {
                Region.X = (int)location.X;
                Region.Y = (int)location.Y;
                Travelled += Speed;
            }
        }

        public virtual Something GetStat(string name)
        {
            Something result = null;

            foreach (Something existing in Stats)
            {
                if (existing.Name == name)
                {
                    result = existing;
                    break;
                }
            }

            return result;
        }

        public virtual Something GetSkill(string name)
        {
            Something result = null;

            foreach (Something existing in Skills)
            {
                if (existing.Name == name)
                {
                    result = existing;
                    break;
                }
            }

            return result;
        }

        public virtual Something GetStatusEffect(string name)
        {
            Something result = null;

            foreach (Something existing in StatusEffects)
            {
                if (existing.Name == name)
                {
                    result = existing;
                    break;
                }
            }

            return result;
        }

        public virtual Something GetBodyPart(string name)
        {
            Something result = null;

            foreach (Something existing in BodyParts)
            {
                if (existing.Name == name)
                {
                    result = existing;
                    break;
                }
            }

            return result;
        }

        public override void Dispose()
        {
            if (HealthBar != null)
            {
                HealthBar.Dispose();
            }
            
            if (ManaBar != null)
            {
                ManaBar.Dispose();
            }

            if (Inventory != null)
            {
                Inventory.Dispose();
            }

            if (Spellbook != null)
            {
                Spellbook.Dispose();
            }

            if (Region != null)
            {
                Region.Dispose();
            }

            if (Job != null)
            {
                Job.Dispose();
            }

            if (Pathing != null)
            {
                Pathing.Dispose();
            }

            foreach (Something stat in Stats)
            {
                stat.Dispose();
            }

            foreach (Something skill in Skills)
            {
                skill.Dispose();
            }

            base.Dispose();
        }

        #endregion
    }
}
