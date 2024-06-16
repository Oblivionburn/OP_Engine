using System;
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
        public List<Something> Traits = new List<Something>();
        public List<Something> StatusEffects = new List<Something>();
        public List<BodyPart> BodyParts = new List<BodyPart>();
        public List<Memory> Memories = new List<Memory>();
        public List<ALocation> Path = new List<ALocation>();
        public Dictionary<long, string> Relationships = new Dictionary<long, string>();

        public ProgressBar HealthBar;
        public ProgressBar ManaBar;
        public ProgressBar StaminaBar;

        public Animator Animator;
        public Spellbook Spellbook;
        public Inventory Inventory;
        public Job Job;

        public bool InCombat;
        public bool CombatTurn;
        public int CombatStep;
        public long Target_ID;

        public Vector2 Formation;
        public Location Destination;
        
        public bool Moving;
        public float Moved;
        public float Move_TotalDistance;
        public float Speed;

        #endregion

        #region Events

        public event EventHandler OnMove;
        public event EventHandler OnMovementFinish;
        public event EventHandler OnKill;
        public event EventHandler<ReactionEventArgs> OnHearSomething;
        public event EventHandler<ReactionEventArgs> OnSeeSomething;
        public event EventHandler<ReactionEventArgs> OnSmellSomething;
        public event EventHandler<ReactionEventArgs> OnTasteSomething;
        public event EventHandler<ReactionEventArgs> OnFeelSomething;

        #endregion

        #region Constructor

        public Character()
        {
            Formation = default;
            Location = default;
            Destination = default;

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

        public virtual Something GetStat(string name)
        {
            int count = Stats.Count;
            for (int i = 0; i < count; i++)
            {
                Something existing = Stats[i];
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

        public virtual Something GetSkill(string name)
        {
            int count = Skills.Count;
            for (int i = 0; i < count; i++)
            {
                Something existing = Skills[i];
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

        public virtual Something GetStatusEffect(string name)
        {
            int count = StatusEffects.Count;
            for (int i = 0; i < count; i++)
            {
                Something existing = StatusEffects[i];
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

        public virtual BodyPart GetBodyPart(string name)
        {
            int count = BodyParts.Count;
            for (int i = 0; i < count; i++)
            {
                BodyPart existing = BodyParts[i];
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

        public virtual void Kill()
        {
            Dead = true;
            OnKill?.Invoke(this, EventArgs.Empty);
        }

        public virtual void HearSomething(Direction direction, int distance, string adjective, int strength, int scale)
        {
            OnHearSomething?.Invoke(this, new ReactionEventArgs(direction, distance, adjective, strength, scale));
        }

        public virtual void SeeSomething(Direction direction, int distance, string adjective, float light_level, int scale)
        {
            OnSeeSomething?.Invoke(this, new ReactionEventArgs(direction, distance, adjective, light_level, scale));
        }

        public virtual void SmellSomething(Direction direction, string adjective, int strength, int scale)
        {
            OnSmellSomething?.Invoke(this, new ReactionEventArgs(direction, adjective, strength, scale));
        }

        public virtual void TasteSomething(string adjective, int strength, int scale)
        {
            OnTasteSomething?.Invoke(this, new ReactionEventArgs(adjective, strength, scale));
        }

        public virtual void FeelSomething(string adjective, int strength, int scale, BodyPart body_part)
        {
            OnFeelSomething?.Invoke(this, new ReactionEventArgs(adjective, strength, scale, body_part));
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

            foreach (ALocation location in Path)
            {
                location.Dispose();
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
