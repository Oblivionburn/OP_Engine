using System.Collections.Generic;

using Microsoft.Xna.Framework;

using OP_Engine.Utility;

namespace OP_Engine.Characters
{
    public class Squad : Something
    {
        #region Variables

        public List<Character> Characters = new List<Character>();
        public int Leader_ID;

        #endregion

        #region Constructor

        public Squad()
        {
            
        }

        #endregion

        #region Methods

        public virtual Character GetCharacter(int id)
        {
            foreach (Character character in Characters)
            {
                if (character.ID == id)
                {
                    return character;
                }
            }

            return null;
        }

        public virtual Character GetCharacter(string name)
        {
            foreach (Character character in Characters)
            {
                if (character.Name == name)
                {
                    return character;
                }
            }

            return null;
        }

        public virtual Character GetCharacter (Vector2 formation)
        {
            foreach (Character character in Characters)
            {
                if (character.Formation.X == formation.X &&
                    character.Formation.Y == formation.Y)
                {
                    return character;
                }
            }

            return null;
        }

        public virtual Character GetLeader()
        {
            foreach (Character character in Characters)
            {
                if (character.ID == Leader_ID)
                {
                    return character;
                }
            }

            return null;
        }

        public override void Dispose()
        {
            foreach (Character character in Characters)
            {
                character.Dispose();
            }

            base.Dispose();
        }

        #endregion
    }
}
