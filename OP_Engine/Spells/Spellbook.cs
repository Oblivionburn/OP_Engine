using System.Collections.Generic;

using OP_Engine.Utility;

namespace OP_Engine.Spells
{
    public class Spellbook : Something
    {
        #region Variables

        public List<Spell> Spells = new List<Spell>();

        #endregion

        #region Constructor

        public Spellbook()
        {
            
        }

        #endregion

        #region Methods

        public Spell GetSpell(long id)
        {
            foreach (Spell existing in Spells)
            {
                if (existing.ID == id)
                {
                    return existing;
                }
            }

            return null;
        }

        public Spell GetSpell(string name)
        {
            foreach (Spell existing in Spells)
            {
                if (existing.Name == name)
                {
                    return existing;
                }
            }

            return null;
        }

        public override void Dispose()
        {
            foreach (Spell spell in Spells)
            {
                spell.Dispose();
            }

            base.Dispose();
        }

        #endregion
    }
}
