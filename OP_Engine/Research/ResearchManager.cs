using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace OP_Engine.Research
{
    public class ResearchManager : GameComponent
    {
        #region Variables

        public static TimeSpan Rate;
        public static List<ResearchTree> ResearchTrees = new List<ResearchTree>();

        #endregion

        #region Constructors

        public ResearchManager(Game game) : base(game)
        {
            game.Exiting += Game_Exiting;
        }

        #endregion

        #region Methods

        public static void Update()
        {
            int count = ResearchTrees.Count;
            for (int i = 0; i < count; i++)
            {
                ResearchTree tree = ResearchTrees[i];
                if (!tree.Completed)
                {
                    if (Rate != null)
                    {
                        tree.Update(Rate);
                    }
                    else
                    {
                        tree.Update();
                    }
                }
            }
        }

        public static ResearchTree GetResearchTree(string name)
        {
            int count = ResearchTrees.Count;
            for (int i = 0; i < count; i++)
            {
                ResearchTree existing = ResearchTrees[i];
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

        public static ResearchTree GetResearchTree(long id)
        {
            int count = ResearchTrees.Count;
            for (int i = 0; i < count; i++)
            {
                ResearchTree existing = ResearchTrees[i];
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

        private void Game_Exiting(object sender, EventArgs e)
        {
            foreach (ResearchTree tree in ResearchTrees)
            {
                tree.Dispose();
            }
        }

        #endregion
    }
}
