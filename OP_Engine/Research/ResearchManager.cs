using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace OP_Engine.Research
{
    public class ResearchManager : GameComponent
    {
        #region Variables

        public static List<Research> ResearchTree = new List<Research>();

        #endregion

        #region Constructors

        public ResearchManager(Game game) : base(game)
        {

        }

        #endregion

        #region Methods

        public static void Update(TimeSpan add_time)
        {
            foreach (Research research in ResearchTree)
            {
                research.Update(add_time);

                if (research.Completed)
                {
                    foreach (long unlock in research.Unlocks)
                    {
                        //Check if unlocked research has all prerequisites completed
                        Research unlocked_research = GetResearch(unlock);
                        if (unlocked_research != null)
                        {
                            //Don't bother checking if it's already unlocked
                            if (!unlocked_research.Unlocked)
                            {
                                bool unlocked = false;
                                int prerequisites = 0;

                                foreach (long prerequisite in unlocked_research.Prerequisites)
                                {
                                    Research prerequisite_research = GetResearch(prerequisite);
                                    if (prerequisite_research != null)
                                    {
                                        if (prerequisite_research.Completed)
                                        {
                                            prerequisites++;
                                        }
                                    }
                                }

                                if (prerequisites >= unlocked_research.Prerequisites.Count)
                                {
                                    unlocked = true;
                                }

                                unlocked_research.Unlocked = unlocked;
                            }
                        }
                    }
                }
            }
        }

        public static void AddResearch(long id, string name, string description, TimeSpan max_time, List<long> prerequisites, List<long> unlocks)
        {
            Research research = new Research();
            research.ID = id;
            research.Name = name;
            research.Description = description;
            research.Max_Time = TimeSpan.FromMilliseconds(max_time.TotalMilliseconds);

            foreach (long prerequisite in prerequisites)
            {
                research.Prerequisites.Add(prerequisite);
            }

            foreach (long unlock in unlocks)
            {
                research.Unlocks.Add(unlock);
            }

            ResearchTree.Add(research);
        }

        public static Research GetResearch(string name)
        {
            foreach (Research research in ResearchTree)
            {
                if (research.Name == name)
                {
                    return research;
                }
            }

            return null;
        }

        public static Research GetResearch(long id)
        {
            foreach (Research research in ResearchTree)
            {
                if (research.ID == id)
                {
                    return research;
                }
            }

            return null;
        }

        private void Game_Exiting(object sender, EventArgs e)
        {
            foreach (Research research in ResearchTree)
            {
                research.Dispose();
            }
        }

        #endregion
    }
}
