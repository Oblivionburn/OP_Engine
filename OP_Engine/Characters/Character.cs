using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OP_Engine.Controls;
using OP_Engine.Inventories;
using OP_Engine.Jobs;
using OP_Engine.Spells;
using OP_Engine.Utility;
using OP_Engine.Tiles;
using OP_Engine.Enums;

namespace OP_Engine.Characters
{
    public class Character : Something
    {
        #region Variables

        public Army Army;
        public Squad Squad;

        public Map Map;
        public Layer Layer;

        public string Gender;
        public bool Interacting;
        public bool Dead;
        public bool Unconscious;

        public List<Something> Stats;
        public List<Something> Skills;
        public List<Something> Traits;
        public List<Something> StatusEffects;
        public List<BodyPart> BodyParts;
        public List<Memory> Memories;
        public List<ALocation> Path;
        public Dictionary<long, string> Relationships;

        public ProgressBar HealthBar;
        public ProgressBar ManaBar;
        public ProgressBar StaminaBar;

        public Animator Animator;
        public Spellbook Spellbook;
        public Inventory Inventory;
        public Job Job;

        public Effect Shader;

        public bool InCombat;
        public bool CombatTurn;
        public int CombatStep;
        public long Target_ID;

        public Vector2 Formation;
        
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

        public Character() : base()
        {
            Stats = new List<Something>();
            Skills = new List<Something>();
            Traits = new List<Something>();
            StatusEffects = new List<Something>();
            BodyParts = new List<BodyPart>();
            Memories = new List<Memory>();
            Path = new List<ALocation>();
            Relationships = new Dictionary<long, string>();

            HealthBar = new ProgressBar();
            ManaBar = new ProgressBar();
            StaminaBar = new ProgressBar();

            Animator = new Animator();
            Spellbook = new Spellbook();
            Inventory = new Inventory();
            Job = new Job();

            Formation = default;
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

                        if (Shader != null)
                        {
                            Shader.CurrentTechnique.Passes[0].Apply();
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

                        if (Shader != null)
                        {
                            Shader.CurrentTechnique.Passes[0].Apply();
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
            Something[] stats = Stats.ToArray();
            int count = stats.Length;
            for (int i = 0; i < count; i++)
            {
                Something existing = stats[i];
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
            Something[] skills = Skills.ToArray();
            int count = skills.Length;
            for (int i = 0; i < count; i++)
            {
                Something existing = skills[i];
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
            Something[] statusEffects = StatusEffects.ToArray();
            int count = statusEffects.Length;
            for (int i = 0; i < count; i++)
            {
                Something existing = statusEffects[i];
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
            BodyPart[] bodyParts = BodyParts.ToArray();
            int count = bodyParts.Length;
            for (int i = 0; i < count; i++)
            {
                BodyPart existing = bodyParts[i];
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
            foreach (Something stat in Stats)
            {
                stat.Dispose();
            }

            foreach (Something skill in Skills)
            {
                skill.Dispose();
            }

            foreach (Something trait in Traits)
            {
                trait.Dispose();
            }

            foreach (Something statusEffect in StatusEffects)
            {
                statusEffect.Dispose();
            }

            foreach (BodyPart bodyPart in BodyParts)
            {
                bodyPart.Dispose();
            }

            foreach (Memory memory in Memories)
            {
                memory.Dispose();
            }

            foreach (ALocation location in Path)
            {
                location.Dispose();
            }

            Relationships = null;

            HealthBar.Dispose();
            ManaBar.Dispose();
            StaminaBar.Dispose();
            Animator.Dispose();
            Spellbook.Dispose();
            Inventory.Dispose();
            Job.Dispose();

            Shader?.Dispose();

            base.Dispose();
        }

        #endregion
    }
}
