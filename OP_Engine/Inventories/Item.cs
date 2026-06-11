using Microsoft.Xna.Framework.Graphics;
using OP_Engine.Spells;
using OP_Engine.Enums;
using OP_Engine.Utility;
using Point = Microsoft.Xna.Framework.Point;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Region = OP_Engine.Utility.Region;

namespace OP_Engine.Inventories
{
    public class Item : IDisposable
    {
        #region Variables

        public long ID;
        public string? Name;
        public string? Description;
        public string? Type;
        public string? Assignment;
        public int Amount;
        public int Max_Amount;
        public int Tier;
        public int Grade;
        public int Quality;
        public Rarity Rarity;
        public float Value;
        public float Min_Value;
        public float Max_Value;
        public float Sell_Price;
        public float Buy_Price;
        public float Weight;
        public float Duration;
        public float Durability;
        public bool IsLightSource;

        public int Level;
        public int XP;
        public Dictionary<int, int> XP_Needed_ForLevels = [];

        public Inventory? Parent;

        public List<string> Categories = [];
        public List<string> Materials = [];

        public bool Equipped;
        public bool Used;
        public bool OneTimeUse;

        public string? Sound;
        public int SoundRange;

        //Inventory for items that are containers of other items
        public Inventory Inventory = new();

        public List<Property> Properties = [];
        public List<Item> Attachments = [];
        public List<Spell> Spells = [];

        public string? Task;
        public Location? Location;

        public Region? Region;
        public Texture2D? Texture;
        public Rectangle Image;
        public bool Visible;
        public Color DrawColor;

        public bool Icon_Visible;
        public Texture2D? Icon;
        public Region Icon_Region = new();
        public Rectangle Icon_Image = new();
        public Color Icon_DrawColor;

        #endregion

        #region Events

        public event EventHandler? OnUsed;
        public event EventHandler? OnEquipped;

        #endregion

        #region Constructor

        public Item()
        {
            
        }

        #endregion

        #region Methods

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution)
        {
            if (Visible)
            {
                if (Texture != null &&
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

            if (Icon_Visible)
            {
                if (Icon != null &&
                    Icon_Region != null)
                {
                    if (Icon_DrawColor != new Color(0, 0, 0, 0))
                    {
                        spriteBatch.Draw(Icon, Icon_Region.ToRectangle, Icon_Image, Icon_DrawColor);
                    }
                    else
                    {
                        spriteBatch.Draw(Icon, Icon_Region.ToRectangle, Icon_Image, Color.White);
                    }
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution, Color color)
        {
            if (Visible)
            {
                if (Texture != null &&
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

            if (Icon_Visible)
            {
                if (Icon != null &&
                    Icon_Region != null)
                {
                    if (Icon_DrawColor != new Color(0, 0, 0, 0))
                    {
                        spriteBatch.Draw(Icon, Icon_Region.ToRectangle, Icon_Image, Icon_DrawColor);
                    }
                    else
                    {
                        spriteBatch.Draw(Icon, Icon_Region.ToRectangle, Icon_Image, color);
                    }
                }
            }
        }

        public virtual Property? GetProperty(string name)
        {
            int count = Properties.Count;
            for (int i = 0; i < count; i++)
            {
                Property existing = Properties[i];
                if (existing?.Name == name)
                {
                    return existing;
                }
            }

            return null;
        }

        public virtual Item? GetAttachment(long id)
        {
            if (Attachments != null)
            {
                int count = Attachments.Count;
                for (int i = 0; i < count; i++)
                {
                    Item existing = Attachments[i];
                    if (existing?.ID == id)
                    {
                        return existing;
                    }
                }
            }

            return null;
        }

        public virtual Item? GetAttachment(string name)
        {
            if (Attachments != null)
            {
                int count = Attachments.Count;
                for (int i = 0; i < count; i++)
                {
                    Item existing = Attachments[i];
                    if (existing?.Name == name)
                    {
                        return existing;
                    }
                }
            }

            return null;
        }

        public virtual Spell? GetSpell(long id)
        {
            if (Spells != null)
            {
                int count = Spells.Count;
                for (int i = 0; i < count; i++)
                {
                    Spell existing = Spells[i];
                    if (existing?.ID == id)
                    {
                        return existing;
                    }
                }
            }

            return null;
        }

        public virtual Spell? GetSpell(string name)
        {
            if (Spells != null)
            {
                int count = Spells.Count;
                for (int i = 0; i < count; i++)
                {
                    Spell existing = Spells[i];
                    if (existing?.Name == name)
                    {
                        return existing;
                    }
                }
            }

            return null;
        }

        public virtual void Equip()
        {
            Equipped = true;
            OnEquipped?.Invoke(this, EventArgs.Empty);
        }

        public virtual void Use()
        {
            Used = true;
            OnUsed?.Invoke(this, EventArgs.Empty);
        }

        public virtual void Dispose()
        {
            Inventory?.Dispose();

            foreach (Property property in Properties)
            {
                property.Dispose();
            }

            foreach (Item item in Attachments)
            {
                item.Dispose();
            }

            foreach (Spell spell in Spells)
            {
                spell.Dispose();
            }

            Icon = null;
            Icon_Region?.Dispose();
        }

        #endregion
    }
}
