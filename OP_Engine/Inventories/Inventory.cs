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
                if (item.Category == category &&
                    item.Type == type)
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
                if (item.Category == category &&
                    item.Type == type &&
                    item.Material == material)
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
