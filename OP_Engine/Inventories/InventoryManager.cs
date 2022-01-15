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

        #endregion
    }
}
