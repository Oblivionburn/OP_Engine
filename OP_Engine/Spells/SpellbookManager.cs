using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace OP_Engine.Spells
{
    public class SpellbookManager : GameComponent
    {
        #region Variables

        public static List<Spellbook> Spellbooks = new List<Spellbook>();

        #endregion

        #region Constructor

        public SpellbookManager(Game game) : base(game)
        {
            
        }

        #endregion

        #region Methods

        public static Spellbook GetSpellbook(long id)
        {
            foreach (Spellbook existing in Spellbooks)
            {
                if (existing.ID == id)
                {
                    return existing;
                }
            }

            return null;
        }

        public static Spellbook GetSpellbook(string name)
        {
            foreach (Spellbook existing in Spellbooks)
            {
                if (existing.Name == name)
                {
                    return existing;
                }
            }

            return null;
        }

        private void Game_Exiting(object sender, EventArgs e)
        {
            foreach (Spellbook spellbook in Spellbooks)
            {
                spellbook.Dispose();
            }
        }

        #endregion
    }
}
