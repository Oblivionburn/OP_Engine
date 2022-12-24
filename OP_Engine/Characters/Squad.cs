using System.Collections.Generic;

using Microsoft.Xna.Framework;

using OP_Engine.Utility;

namespace OP_Engine.Characters
{
    public class Squad : Something
    {
        #region Variables

        public List<Character> Characters = new List<Character>();
        public long Leader_ID;

        #endregion

        #region Constructor

        public Squad()
        {
            
        }

        #endregion

        #region Methods

        public virtual Character GetCharacter(long id)
        {
            int count = Characters.Count;
            for (int i = 0; i < count; i++)
            {
                Character existing = Characters[i];
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

        public virtual Character GetCharacter(string name)
        {
            int count = Characters.Count;
            for (int i = 0; i < count; i++)
            {
                Character existing = Characters[i];
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

        public virtual Character GetCharacter (Vector2 formation)
        {
            int count = Characters.Count;
            for (int i = 0; i < count; i++)
            {
                Character existing = Characters[i];
                if (existing != null)
                {
                    if (existing.Formation.X == formation.X &&
                        existing.Formation.Y == formation.Y)
                    {
                        return existing;
                    }
                }
            }

            return null;
        }

        public virtual Character GetLeader()
        {
            int count = Characters.Count;
            for (int i = 0; i < count; i++)
            {
                Character existing = Characters[i];
                if (existing != null)
                {
                    if (existing.ID == Leader_ID)
                    {
                        return existing;
                    }
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
