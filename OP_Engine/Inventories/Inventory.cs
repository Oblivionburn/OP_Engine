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
            int count = Items.Count;
            for (int i = 0; i < count; i++)
            {
                Items[i]?.Draw(spriteBatch, resolution);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, Point resolution, Color color)
        {
            int count = Items.Count;
            for (int i = 0; i < count; i++)
            {
                Items[i]?.Draw(spriteBatch, resolution, color);
            }
        }

        public virtual Item GetItem(string name)
        {
            int count = Items.Count;
            for (int i = 0; i < count; i++)
            {
                Item existing = Items[i];
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

        public virtual Item GetItem(long id)
        {
            int count = Items.Count;
            for (int i = 0; i < count; i++)
            {
                Item existing = Items[i];
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

        public virtual Item GetItem(Vector2 location)
        {
            int count = Items.Count;
            for (int i = 0; i < count; i++)
            {
                Item existing = Items[i];
                if (existing != null)
                {
                    if (existing.Location.X == location.X &&
                        existing.Location.Y == location.Y)
                    {
                        return existing;
                    }
                }
            }

            return null;
        }

        public virtual List<Item> GetItems(string type, List<string> categories, List<string> materials)
        {
            List<Item> items = new List<Item>();

            int itemCount = Items.Count;
            for (int i = 0; i < itemCount; i++)
            {
                Item item = Items[i];

                bool type_found = true;
                if (!string.IsNullOrEmpty(type) &&
                    item.Type != type)
                {
                    type_found = false;
                }

                bool categories_found = true;
                if (categories != null)
                {
                    int categoryCount = categories.Count;
                    for (int c = 0; c < categoryCount; c++)
                    {
                        string category = categories[c];
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
                    int materialCount = materials.Count;
                    for (int m = 0; m < materialCount; m++)
                    {
                        string material = materials[m];
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
