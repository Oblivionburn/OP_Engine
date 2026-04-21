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
    public class Character : IDisposable
    {
        #region Variables

        public long ID;
        public string Name;
        public string Type;

        public int Level;
        public int XP;
        public Dictionary<int, int> XP_Needed_ForLevels;

        public Region Region;
        public Texture2D Texture;
        public Rectangle Image;
        public bool Visible;
        public Color DrawColor;

        public Direction Direction;
        public Location Location;
        public Location Destination;

        public Army Army;
        public Squad Squad;

        public Map Map;
        public Layer Layer;

        public string Gender;
        public bool Interacting;
        public bool Dead;
        public bool Unconscious;
        public bool InSight;

        public List<Property> Stats;
        public List<Property> Skills;
        public List<Property> Traits;
        public List<Property> StatusEffects;
        public List<BodyPart> BodyParts;
        public List<Memory> Memories;
        public List<ALocation> Path;
        public Dictionary<long, string> Relationships;

        public ProgressBar HealthBar;
        public ProgressBar ManaBar;
        public ProgressBar StaminaBar;

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
        public int Frames;

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
            XP_Needed_ForLevels = new Dictionary<int, int>();

            Stats = new List<Property>();
            Skills = new List<Property>();
            Traits = new List<Property>();
            StatusEffects = new List<Property>();
            BodyParts = new List<BodyPart>();
            Memories = new List<Memory>();
            Path = new List<ALocation>();
            Relationships = new Dictionary<long, string>();

            HealthBar = new ProgressBar();
            ManaBar = new ProgressBar();
            StaminaBar = new ProgressBar();

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

        public virtual void FaceNorth()
        {
            if (Texture != null)
            {
                Image = new Rectangle(Image.X, (Texture.Height / 4) * 3, Texture.Width / Frames, Texture.Height / 4);
                Direction = Direction.Up;
            }
        }

        public virtual void FaceEast()
        {
            if (Texture != null)
            {
                Image = new Rectangle(Image.X, (Texture.Height / 4) * 2, Texture.Width / Frames, Texture.Height / 4);
                Direction = Direction.Right;
            }
        }

        public virtual void FaceSouth()
        {
            if (Texture != null)
            {
                Image = new Rectangle(Image.X, 0, Texture.Width / Frames, Texture.Height / 4);
                Direction = Direction.Down;
            }
        }

        public virtual void FaceWest()
        {
            if (Texture != null)
            {
                Image = new Rectangle(Image.X, (Texture.Height / 4) * 1, Texture.Width / Frames, Texture.Height / 4);
                Direction = Direction.Left;
            }
        }

        public virtual Property GetStat(string name)
        {
            Property[] stats = Stats.ToArray();
            int count = stats.Length;
            for (int i = 0; i < count; i++)
            {
                Property existing = stats[i];
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

        public virtual Property GetSkill(string name)
        {
            Property[] skills = Skills.ToArray();
            int count = skills.Length;
            for (int i = 0; i < count; i++)
            {
                Property existing = skills[i];
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

        public virtual Property GetStatusEffect(string name)
        {
            Property[] statusEffects = StatusEffects.ToArray();
            int count = statusEffects.Length;
            for (int i = 0; i < count; i++)
            {
                Property existing = statusEffects[i];
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

        public virtual void Dispose()
        {
            foreach (Property stat in Stats)
            {
                stat.Dispose();
            }

            foreach (Property skill in Skills)
            {
                skill.Dispose();
            }

            foreach (Property trait in Traits)
            {
                trait.Dispose();
            }

            foreach (Property statusEffect in StatusEffects)
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
            Spellbook.Dispose();
            Inventory.Dispose();
            Job.Dispose();

            Shader?.Dispose();
        }

        #endregion
    }
}
