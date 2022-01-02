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

        public virtual Item GetItem(int id)
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

        public virtual Item GetItem(string category, string type)
        {
            foreach (Item item in Items)
            {
                if (item.Type == type &&
                    item.Categories.Contains(category))
                {
                    return item;
                }
            }

            return null;
        }

        public virtual Item GetItem(string category, string type, string material)
        {
            foreach (Item item in Items)
            {
                if (item.Type == type &&
                    item.Categories.Contains(category) &&
                    item.Materials.Contains(material))
                {
                    return item;
                }
            }

            return null;
        }

        public virtual Item GetItem(List<string> categories, string type)
        {
            foreach (Item item in Items)
            {
                if (item.Type == type)
                {
                    bool categories_found = true;
                    foreach (string category in categories)
                    {
                        if (!item.Categories.Contains(category))
                        {
                            categories_found = false;
                            break;
                        }
                    }

                    if (categories_found)
                    {
                        return item;
                    }
                }
            }

            return null;
        }

        public virtual Item GetItem(List<string> categories, string type, List<string> materials)
        {
            foreach (Item item in Items)
            {
                if (item.Type == type)
                {
                    bool categories_found = true;
                    foreach (string category in categories)
                    {
                        if (!item.Categories.Contains(category))
                        {
                            categories_found = false;
                            break;
                        }
                    }

                    if (categories_found)
                    {
                        bool materials_found = true;
                        foreach (string material in materials)
                        {
                            if (!item.Materials.Contains(material))
                            {
                                materials_found = false;
                                break;
                            }
                        }

                        if (materials_found)
                        {
                            return item;
                        }
                    }
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
