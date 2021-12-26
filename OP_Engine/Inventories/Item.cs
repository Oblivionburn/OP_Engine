using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Utility;
using OP_Engine.Spells;

namespace OP_Engine.Inventories
{
    public class Item : Something
    {
        #region Variables

        public string Category;
        public string Material;
        public int Amount;
        public bool Equipped;

        public Inventory Inventory;
        public List<Something> Properties = new List<Something>();
        public List<Item> Attachments = new List<Item>();
        public List<Spell> Spells = new List<Spell>();

        public string Task;
        public int TaskTime;

        /*
        Using this Square object for Region, instead of the Rectangle 
        struct, to enable referencing the same Region as characters
        for equipped items
        */
        public new Square Region;

        public bool Icon_Visible;
        public Texture2D Icon;
        public Rectangle Icon_Region;
        public Rectangle Icon_Image;
        public Color Icon_DrawColor;

        #endregion

        #region Constructor

        public Item()
        {
            Inventory = new Inventory();

            Region = new Square();
            Image = default;
        }

        #endregion

        #region Methods

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution)
        {
            if (Visible)
            {
                if (Texture != null)
                {
                    if (Region.X >= (Texture.Width * -2) && Region.X < resolution.X + (Texture.Width * 2))
                    {
                        if (Region.Y >= (Texture.Height * -2) && Region.Y < resolution.Y + (Texture.Height * 2))
                        {
                            Rectangle region = new Rectangle(Region.X, Region.Y, Region.Width, Region.Height);
                            if (DrawColor != new Color(0, 0, 0, 0))
                            {
                                spriteBatch.Draw(Texture, region, Image, DrawColor);
                            }
                            else
                            {
                                spriteBatch.Draw(Texture, region, Image, Color.White);
                            }
                        }
                    }
                }
            }

            if (Icon_Visible)
            {
                if (Icon != null)
                {
                    if (Icon_DrawColor != new Color(0, 0, 0, 0))
                    {
                        spriteBatch.Draw(Icon, Icon_Region, Icon_Image, Icon_DrawColor);
                    }
                    else
                    {
                        spriteBatch.Draw(Icon, Icon_Region, Icon_Image, Color.White);
                    }
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution, Color color)
        {
            if (Visible)
            {
                if (Texture != null)
                {
                    if (Region.X >= (Texture.Width * -2) && Region.X < resolution.X + (Texture.Width * 2))
                    {
                        if (Region.Y >= (Texture.Height * -2) && Region.Y < resolution.Y + (Texture.Height * 2))
                        {
                            Rectangle region = new Rectangle(Region.X, Region.Y, Region.Width, Region.Height);
                            if (DrawColor != new Color(0, 0, 0, 0))
                            {
                                spriteBatch.Draw(Texture, region, Image, DrawColor);
                            }
                            else
                            {
                                spriteBatch.Draw(Texture, region, Image, color);
                            }
                        }
                    }
                }
            }

            if (Icon_Visible)
            {
                if (Icon != null)
                {
                    if (Icon_DrawColor != new Color(0, 0, 0, 0))
                    {
                        spriteBatch.Draw(Icon, Icon_Region, Icon_Image, Icon_DrawColor);
                    }
                    else
                    {
                        spriteBatch.Draw(Icon, Icon_Region, Icon_Image, color);
                    }
                }
            }
        }

        public virtual Something GetProperty(int id)
        {
            foreach (Something something in Properties)
            {
                if (something.ID == id)
                {
                    return something;
                }
            }

            return null;
        }

        public virtual Something GetProperty(string name)
        {
            foreach (Something something in Properties)
            {
                if (something.Name == name)
                {
                    return something;
                }
            }

            return null;
        }

        public virtual Item GetAttachment(int id)
        {
            if (Attachments != null)
            {
                foreach (Item item in Attachments)
                {
                    if (item.ID == id)
                    {
                        return item;
                    }
                }
            }

            return null;
        }

        public virtual Item GetAttachment(string name)
        {
            if (Attachments != null)
            {
                foreach (Item item in Attachments)
                {
                    if (item.Name == name)
                    {
                        return item;
                    }
                }
            }

            return null;
        }

        public virtual Spell GetSpell(int id)
        {
            if (Spells != null)
            {
                foreach (Spell spell in Spells)
                {
                    if (spell.ID == id)
                    {
                        return spell;
                    }
                }
            }

            return null;
        }

        public virtual Spell GetSpell(string name)
        {
            if (Spells != null)
            {
                foreach (Spell spell in Spells)
                {
                    if (spell.Name == name)
                    {
                        return spell;
                    }
                }
            }

            return null;
        }

        public override void Dispose()
        {
            Icon = null;

            if (Region != null)
            {
                Region.Dispose();
            }

            foreach (Something property in Properties)
            {
                property.Dispose();
            }

            foreach (Spell spell in Spells)
            {
                spell.Dispose();
            }

            if (Attachments != null)
            {
                foreach (Item item in Attachments)
                {
                    item.Dispose();
                }
            }

            base.Dispose();
        }

        #endregion
    }
}
