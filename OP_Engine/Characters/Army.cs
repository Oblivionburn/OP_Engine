using System.Collections.Generic;
using OP_Engine.Utility;

namespace OP_Engine.Characters
{
    public class Army : Something
    {
        #region Variables

        public List<Squad> Squads = new List<Squad>();

        #endregion

        #region Constructor

        public Army()
        {
            
        }

        #endregion

        #region Methods

        public virtual Squad GetSquad(long id)
        {
            int count = Squads.Count;
            for (int i = 0; i < count; i++)
            {
                Squad existing = Squads[i];
                if (existing != null)
                {
                    if (existing.ID == id)
                    {
                        return existing;
                    }
                }
            }

            return null;
        }

        public virtual Squad GetSquad(string name)
        {
            int count = Squads.Count;
            for (int i = 0; i < count; i++)
            {
                Squad existing = Squads[i];
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

        public override void Dispose()
        {
            foreach (Squad squad in Squads)
            {
                squad.Dispose();
            }

            base.Dispose();
        }

        #endregion
    }
}
