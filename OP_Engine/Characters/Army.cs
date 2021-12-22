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

        public virtual Squad GetSquad(int id)
        {
            foreach (Squad squad in Squads)
            {
                if (squad.ID == id)
                {
                    return squad;
                }
            }

            return null;
        }

        public virtual Squad GetSquad(string name)
        {
            foreach (Squad squad in Squads)
            {
                if (squad.Name == name)
                {
                    return squad;
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
