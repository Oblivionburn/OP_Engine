using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace OP_Engine.Spells
{
    public class SpellbookManager : GameComponent
    {
        #region Variables

        public static List<Spellbook> Spellbooks;

        #endregion

        #region Constructor

        public SpellbookManager(Game game) : base(game)
        {
            Spellbooks = new List<Spellbook>();

            game.Exiting += Game_Exiting;
        }

        #endregion

        #region Methods

        public static Spellbook GetSpellbook(long id)
        {
            int count = Spellbooks.Count;
            for (int i = 0; i < count; i++)
            {
                Spellbook existing = Spellbooks[i];
                if (existing == null)
                {
                    if (existing.ID == id)
                    {
                        return existing;
                    }
                }
            }

            return null;
        }

        public static Spellbook GetSpellbook(string name)
        {
            int count = Spellbooks.Count;
            for (int i = 0; i < count; i++)
            {
                Spellbook existing = Spellbooks[i];
                if (existing == null)
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
            foreach (Spellbook spellbook in Spellbooks)
            {
                spellbook.Dispose();
            }
        }

        #endregion
    }
}
