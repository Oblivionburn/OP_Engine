using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace OP_Engine.Research
{
    public class ResearchManager : GameComponent
    {
        #region Variables

        public static List<ResearchTree> ResearchTrees = new List<ResearchTree>();

        #endregion

        #region Constructors

        public ResearchManager(Game game) : base(game)
        {
            game.Exiting += Game_Exiting;
        }

        #endregion

        #region Methods

        public static ResearchTree GetResearchTree(string name)
        {
            foreach (ResearchTree tree in ResearchTrees)
            {
                if (tree.Name == name)
                {
                    return tree;
                }
            }

            return null;
        }

        public static ResearchTree GetResearchTree(long id)
        {
            foreach (ResearchTree tree in ResearchTrees)
            {
                if (tree.ID == id)
                {
                    return tree;
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
