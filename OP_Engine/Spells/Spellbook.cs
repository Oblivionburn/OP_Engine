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
