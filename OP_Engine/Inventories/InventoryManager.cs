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
            foreach (Inventory inventory in Inventories)
            {
                if (inventory.ID == id)
                {
                    return inventory;
                }
            }

            return null;
        }

        public static Inventory GetInventory(string name)
        {
            foreach (Inventory inventory in Inventories)
            {
                if (inventory.Name == name)
                {
                    return inventory;
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
