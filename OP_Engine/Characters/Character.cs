using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Controls;
using OP_Engine.Inventories;
using OP_Engine.Spells;
using OP_Engine.Utility;

namespace OP_Engine.Characters
{
    public class Character : Something
    {
        #region Variables

        public int MapID;
        public int LayerID;
        public int PartyID;
        public string Gender;

        public List<Something> Stats = new List<Something>();
        public List<Something> Skills = new List<Something>();
        public Spellbook Spellbook;
        public ProgressBar HealthBar;
        public ProgressBar ManaBar;

        public Inventory Inventory;
        public Animator Animator;

        public bool InCombat;
        public bool CombatTurn;
        public int CombatTick;
        public int Target_ID;

        public bool Interacting;
        public string Job;

        public string Task;
        public string Task_Type;
        public Vector3 Task_Location;
        public ProgressBar TaskBar;
        public int Task_StartTime;
        public int Patience;

        public Vector2 Formation;
        public Vector3 Destination;
        public Pathing Pathing;
        public bool Travelling;
        public int Travelled;
        public int Travel_TotalDistance;
        public int Speed;

        #endregion

        #region Constructor

        public Character()
        {
            Formation = default;
            Location = default;
            Destination = default;
            Pathing = new Pathing();

            Region = default;
            Image = default;

            HealthBar = new ProgressBar();
            ManaBar = new ProgressBar();

            Inventory = new Inventory();
            Spellbook = new Spellbook();

            Animator = new Animator();
            TaskBar = new ProgressBar();
        }

        #endregion

        #region Methods

        public virtual void Update()
        {
            if (Travelling)
            {
                if (Destination.X > Location.X)
                {
                    Animator.FaceEast(this);
                    MoveTo(new Rectangle(Region.X + Speed, Region.Y, Region.Width, Region.Height));

                    if (Travelled == Travel_TotalDistance)
                    {
                        Location.X++;
                        Travelled = 0;
                        Animator.Animate(this);
                    }
                    else if (Travelled == (Travel_TotalDistance / 4) * 3 ||
                             Travelled == (Travel_TotalDistance / 4) * 2 ||
                             Travelled == (Travel_TotalDistance / 4))
                    {
                        Animator.Animate(this);
                    }
                    else if (Travelled > Travel_TotalDistance)
                    {
                        MoveTo(new Rectangle(Region.X - Speed, Region.Y, Region.Width, Region.Height));
                        Location.X = Destination.X;
                        Travelled = 0;
                        Travelling = false;
                        Animator.Reset(this);
                    }
                }
                else if (Destination.X < Location.X)
                {
                    Animator.FaceWest(this);
                    MoveTo(new Rectangle(Region.X - Speed, Region.Y, Region.Width, Region.Height));

                    if (Travelled == Travel_TotalDistance)
                    {
                        Location.X--;
                        Travelled = 0;
                        Animator.Animate(this);
                    }
                    else if (Travelled == (Travel_TotalDistance / 4) * 3 ||
                             Travelled == (Travel_TotalDistance / 4) * 2 ||
                             Travelled == (Travel_TotalDistance / 4))
                    {
                        Animator.Animate(this);
                    }
                    else if (Travelled > Travel_TotalDistance)
                    {
                        MoveTo(new Rectangle(Region.X + Speed, Region.Y, Region.Width, Region.Height));
                        Location.X = Destination.X;
                        Travelled = 0;
                        Travelling = false;
                        Animator.Reset(this);
                    }
                }
                else
                {
                    if (Destination.Y > Location.Y)
                    {
                        Animator.FaceSouth(this);
                        MoveTo(new Rectangle(Region.X, Region.Y + Speed, Region.Width, Region.Height));

                        if (Travelled == Travel_TotalDistance)
                        {
                            Location.Y++;
                            Travelled = 0;
                            Animator.Animate(this);
                        }
                        else if (Travelled == (Travel_TotalDistance / 4) * 3 ||
                                 Travelled == (Travel_TotalDistance / 4) * 2 ||
                                 Travelled == (Travel_TotalDistance / 4))
                        {
                            Animator.Animate(this);
                        }
                        else if (Travelled > Travel_TotalDistance)
                        {
                            MoveTo(new Rectangle(Region.X, Region.Y - Speed, Region.Width, Region.Height));
                            Location.Y = Destination.Y;
                            Travelled = 0;
                            Travelling = false;
                            Animator.Reset(this);
                        }
                    }
                    else if (Destination.Y < Location.Y)
                    {
                        Animator.FaceNorth(this);
                        MoveTo(new Rectangle(Region.X, Region.Y - Speed, Region.Width, Region.Height));

                        if (Travelled == Travel_TotalDistance)
                        {
                            Location.Y--;
                            Travelled = 0;
                            Animator.Animate(this);
                        }
                        else if (Travelled == (Travel_TotalDistance / 4) * 3 ||
                                 Travelled == (Travel_TotalDistance / 4) * 2 ||
                                 Travelled == (Travel_TotalDistance / 4))
                        {
                            Animator.Animate(this);
                        }
                        else if (Travelled > Travel_TotalDistance)
                        {
                            MoveTo(new Rectangle(Region.X, Region.Y + Speed, Region.Width, Region.Height));
                            Location.Y = Destination.Y;
                            Travelled = 0;
                            Travelling = false;
                            Animator.Reset(this);
                        }
                    }
                    else
                    {
                        Travelling = false;
                        Travelled = 0;
                    }
                }
            }

            Animator.Update(this);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution)
        {
            if (Visible)
            {
                if (Region.X >= (Texture.Width * -2) && Region.X < resolution.X + (Texture.Width * 2))
                {
                    if (Region.Y >= (Texture.Height * -2) && Region.Y < resolution.Y + (Texture.Height * 2))
                    {
                        if (DrawColor != new Color(0, 0, 0, 0))
                        {
                            spriteBatch.Draw(Texture, Region, Image, DrawColor);
                            Inventory.Draw(spriteBatch, resolution, DrawColor);
                        }
                        else
                        {
                            spriteBatch.Draw(Texture, Region, Image, Color.White);
                            Inventory.Draw(spriteBatch, resolution, Color.White);
                        }
                    }
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution, Color color)
        {
            if (Visible)
            {
                if (Region.X >= (Texture.Width * -2) && Region.X < resolution.X + (Texture.Width * 2))
                {
                    if (Region.Y >= (Texture.Height * -2) && Region.Y < resolution.Y + (Texture.Height * 2))
                    {
                        if (DrawColor != new Color(0, 0, 0, 0))
                        {
                            spriteBatch.Draw(Texture, Region, Image, DrawColor);
                            Inventory.Draw(spriteBatch, resolution, DrawColor);
                        }
                        else
                        {
                            spriteBatch.Draw(Texture, Region, Image, color);
                            Inventory.Draw(spriteBatch, resolution, color);
                        }
                    }
                }
            }
        }

        public virtual void MoveTo(Rectangle region)
        {
            Region = region;
            Travelled += Speed;
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

        public override void Dispose()
        {
            if (Texture != null)
            {
                Texture = null;
            }

            if (HealthBar != null)
            {
                HealthBar.Dispose();
            }
            
            if (ManaBar != null)
            {
                ManaBar.Dispose();
            }

            if (TaskBar != null)
            {
                TaskBar.Dispose();
            }

            if (Inventory != null)
            {
                Inventory.Dispose();
            }

            if (Spellbook != null)
            {
                Spellbook.Dispose();
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
