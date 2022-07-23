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

        public List<string> Categories = new List<string>();
        public List<string> Materials = new List<string>();
        public bool Equipped;

        //Inventory for items that are containers of other items
        public Inventory Inventory;

        public List<Something> Properties = new List<Something>();
        public List<Item> Attachments = new List<Item>();
        public List<Spell> Spells = new List<Spell>();

        public string Task;

        public bool Icon_Visible;
        public Texture2D Icon;
        public Region Icon_Region;
        public Rectangle Icon_Image;
        public Color Icon_DrawColor;

        #endregion

        #region Constructor

        public Item()
        {
            Inventory = new Inventory();

            Region = new Region();
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
                if (Icon != null)
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
                if (Texture != null)
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
                if (Icon != null)
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

        public virtual Something GetProperty(long id)
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

        public virtual Item GetAttachment(long id)
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

        public virtual Spell GetSpell(long id)
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
            foreach (Something property in Properties)
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

            if (Icon != null)
            {
                Icon.Dispose();
            }

            base.Dispose();
        }

        #endregion
    }
}
