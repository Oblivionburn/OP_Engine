using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Utility;
using OP_Engine.Spells;
using System.Xml.Linq;

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

        public virtual Something GetProperty(long id)
        {
            int count = Properties.Count;
            for (int i = 0; i < count; i++)
            {
                Something existing = Properties[i];
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

        public virtual Something GetProperty(string name)
        {
            int count = Properties.Count;
            for (int i = 0; i < count; i++)
            {
                Something existing = Properties[i];
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

        public virtual Item GetAttachment(long id)
        {
            if (Attachments != null)
            {
                int count = Attachments.Count;
                for (int i = 0; i < count; i++)
                {
                    Item existing = Attachments[i];
                    if (existing != null)
                    {
                        if (existing.ID == id)
                        {
                            return existing;
                        }
                    }
                }
            }

            return null;
        }

        public virtual Item GetAttachment(string name)
        {
            if (Attachments != null)
            {
                int count = Attachments.Count;
                for (int i = 0; i < count; i++)
                {
                    Item existing = Attachments[i];
                    if (existing != null)
                    {
                        if (existing.Name == name)
                        {
                            return existing;
                        }
                    }
                }
            }

            return null;
        }

        public virtual Spell GetSpell(long id)
        {
            if (Spells != null)
            {
                int count = Spells.Count;
                for (int i = 0; i < count; i++)
                {
                    Spell existing = Spells[i];
                    if (existing != null)
                    {
                        if (existing.ID == id)
                        {
                            return existing;
                        }
                    }
                }
            }

            return null;
        }

        public virtual Spell GetSpell(string name)
        {
            if (Spells != null)
            {
                int count = Spells.Count;
                for (int i = 0; i < count; i++)
                {
                    Spell existing = Spells[i];
                    if (existing != null)
                    {
                        if (existing.Name == name)
                        {
                            return existing;
                        }
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
