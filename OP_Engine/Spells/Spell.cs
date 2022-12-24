using System.Collections.Generic;

using OP_Engine.Utility;

namespace OP_Engine.Spells
{
    public class Spell : Something
    {
        #region Variables

        public List<Something> Properties = new List<Something>();

        #endregion

        #region Constructor

        public Spell()
        {
            
        }

        #endregion

        #region Methods

        public virtual Something GetProperty(long id)
        {
            int count = Properties.Count;
            for (int i = 0; i < count; i++)
            {
                Something existing = Properties[i];
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

        public virtual Something GetProperty(string name)
        {
            int count = Properties.Count;
            for (int i = 0; i < count; i++)
            {
                Something existing = Properties[i];
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

        public override void Dispose()
        {
            foreach (Something something in Properties)
            {
                something.Dispose();
            }

            base.Dispose();
        }

        #endregion
    }
}
