using System.Collections.Generic;

using OP_Engine.Utility;

namespace OP_Engine.Characters
{
    public class BodyPart : Something
    {
        #region Variables

        public List<Something> Stats = new List<Something>();
        public List<Something> StatusEffects = new List<Something>();
        public List<Something> Wounds = new List<Something>();

        #endregion

        #region Contructors

        public BodyPart()
        {

        }

        #endregion

        #region Methods

        public virtual Something GetStat(string name)
        {
            int count = Stats.Count;
            for (int i = 0; i < count; i++)
            {
                Something existing = Stats[i];
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

        public virtual Something GetStatusEffect(string name)
        {
            int count = StatusEffects.Count;
            for (int i = 0; i < count; i++)
            {
                Something existing = StatusEffects[i];
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

        public virtual List<Something> GetWounds(string name)
        {
            List<Something> results = new List<Something>();

            int count = Wounds.Count;
            for (int i = 0; i < count; i++)
            {
                Something existing = Wounds[i];
                if (existing != null)
                {
                    if (existing.Name == name)
                    {
                        results.Add(existing);
                    }
                }
            }

            return results;
        }

        #endregion
    }
}
