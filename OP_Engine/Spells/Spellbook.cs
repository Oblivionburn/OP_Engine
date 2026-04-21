using System;
using System.Collections.Generic;

namespace OP_Engine.Spells
{
    public class Spellbook : IDisposable
    {
        #region Variables

        public long ID;
        public string Name;
        public string Description;
        public string Type;

        public List<Spell> Spells;

        #endregion

        #region Constructor

        public Spellbook()
        {
            Spells = new List<Spell>();
        }

        #endregion

        #region Methods

        public Spell GetSpell(long id)
        {
            int count = Spells.Count;
            for (int i = 0; i < count; i++)
            {
                Spell existing = Spells[i];
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

        public Spell GetSpell(string name)
        {
            int count = Spells.Count;
            for (int i = 0; i < count; i++)
            {
                Spell existing = Spells[i];
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

        public virtual void Dispose()
        {
            foreach (Spell spell in Spells)
            {
                spell.Dispose();
            }
        }

        #endregion
    }
}
