using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace OP_Engine.Inventories
{
    public class InventoryManager : GameComponent
    {
        #region Variables

        public static List<Inventory> Inventories = new List<Inventory>();

        #endregion

        #region Constructor

        public InventoryManager(Game game) : base(game)
        {
            game.Exiting += Game_Exiting;
        }

        #endregion

        #region Methods

        public static Inventory GetInventory(long id)
        {
            int count = Inventories.Count;
            for (int i = 0; i < count; i++)
            {
                Inventory existing = Inventories[i];
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

        public static Inventory GetInventory(string name)
        {
            int count = Inventories.Count;
            for (int i = 0; i < count; i++)
            {
                Inventory existing = Inventories[i];
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

        private void Game_Exiting(object sender, EventArgs e)
        {
            foreach (Inventory inventory in Inventories)
            {
                inventory.Dispose();
            }
        }

        #endregion
    }
}
