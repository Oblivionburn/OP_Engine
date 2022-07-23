using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using OP_Engine.Utility;

namespace OP_Engine.Inventories
{
    public class Inventory : Something
    {
        #region Variables

        public List<Item> Items = new List<Item>();

        #endregion

        #region Constructor

        public Inventory()
        {
            
        }

        #endregion

        #region Methods

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution)
        {
            foreach (Item item in Items)
            {
                item.Draw(spriteBatch, resolution);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution, Color color)
        {
            foreach (Item item in Items)
            {
                item.Draw(spriteBatch, resolution, color);
            }
        }

        public virtual Item GetItem(string name)
        {
            foreach (Item item in Items)
            {
                if (item.Name == name)
                {
                    return item;
                }
            }

            return null;
        }

        public virtual Item GetItem(long id)
        {
            foreach (Item item in Items)
            {
                if (item.ID == id)
                {
                    return item;
                }
            }

            return null;
        }

        public virtual Item GetItem(Vector2 location)
        {
            foreach (Item item in Items)
            {
                if (item.Location.X == location.X &&
                    item.Location.Y == location.Y)
                {
                    return item;
                }
            }

            return null;
        }

        public virtual List<Item> GetItems(string type, List<string> categories, List<string> materials)
        {
            List<Item> items = new List<Item>();

            foreach (Item item in Items)
            {
                bool type_found = true;
                if (!string.IsNullOrEmpty(type) &&
                    item.Type != type)
                {
                    type_found = false;
                }

                bool categories_found = true;
                if (categories != null)
                {
                    foreach (string category in categories)
                    {
                        if (!item.Categories.Contains(category))
                        {
                            categories_found = false;
                            break;
                        }
                    }
                }

                bool materials_found = true;
                if (materials != null)
                {
                    foreach (string material in materials)
                    {
                        if (!item.Materials.Contains(material))
                        {
                            materials_found = false;
                            break;
                        }
                    }
                }

                if (type_found &&
                    categories_found &&
                    materials_found)
                {
                    items.Add(item);
                }
            }

            return items;
        }

        public override void Dispose()
        {
            foreach (Item item in Items)
            {
                item.Dispose();
            }

            base.Dispose();
        }

        #endregion
    }
}
