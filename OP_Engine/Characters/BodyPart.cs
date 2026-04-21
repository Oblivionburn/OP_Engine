using System;
using System.Collections.Generic;
using OP_Engine.Utility;

namespace OP_Engine.Characters
{
    public class BodyPart : IDisposable
    {
        #region Variables

        public string Name;
        public string Description;

        public List<Property> Stats;
        public List<Property> StatusEffects;
        public List<Wound> Wounds;

        #endregion

        #region Contructors

        public BodyPart()
        {
            Stats = new List<Property>();
            StatusEffects = new List<Property>();
            Wounds = new List<Wound>();
        }

        #endregion

        #region Methods

        public virtual Property GetStat(string name)
        {
            int count = Stats.Count;
            for (int i = 0; i < count; i++)
            {
                Property existing = Stats[i];
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

        public virtual Property GetStatusEffect(string name)
        {
            int count = StatusEffects.Count;
            for (int i = 0; i < count; i++)
            {
                Property existing = StatusEffects[i];
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

        public virtual List<Wound> GetWounds(string name)
        {
            List<Wound> results = new List<Wound>();

            int count = Wounds.Count;
            for (int i = 0; i < count; i++)
            {
                Wound existing = Wounds[i];
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

        public virtual void Dispose()
        {
            foreach (Property stat in Stats)
            {
                stat.Dispose();
            }

            foreach (Property statusEffect in StatusEffects)
            {
                statusEffect.Dispose();
            }

            foreach (Wound wound in Wounds)
            {
                wound.Dispose();
            }
        }

        #endregion
    }
}
