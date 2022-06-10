using System;
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
            Something result = null;

            foreach (Something existing in Stats)
            {
                if (existing.Name == name)
                {
                    result = existing;
                    break;
                }
            }

            return result;
        }

        public virtual Something GetStatusEffect(string name)
        {
            Something result = null;

            foreach (Something existing in StatusEffects)
            {
                if (existing.Name == name)
                {
                    result = existing;
                    break;
                }
            }

            return result;
        }

        public virtual List<Something> GetWounds(string name)
        {
            List<Something> results = new List<Something>();

            foreach (Something existing in Wounds)
            {
                if (existing.Name == name)
                {
                    results.Add(existing);
                    break;
                }
            }

            return results;
        }

        #endregion
    }
}
