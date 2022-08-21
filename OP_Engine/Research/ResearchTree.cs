using System;
using System.Collections.Generic;

using OP_Engine.Utility;

namespace OP_Engine.Research
{
    public class ResearchTree : Something
    {
        #region Variables

        public static List<Research> ResearchNodes = new List<Research>();

        #endregion

        #region Constructors

        public ResearchTree()
        {
            
        }

        #endregion

        #region Methods

        public virtual void Update(TimeSpan add_time)
        {
            foreach (Research research in ResearchNodes)
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

        public virtual void AddResearch(long id, string name, string description, TimeSpan max_time, List<long> prerequisites, List<long> unlocks)
        {
            Research research = new Research
            {
                ID = id,
                Name = name,
                Description = description,
                Max_Time = TimeSpan.FromMilliseconds(max_time.TotalMilliseconds)
            };

            foreach (long prerequisite in prerequisites)
            {
                research.Prerequisites.Add(prerequisite);
            }

            foreach (long unlock in unlocks)
            {
                research.Unlocks.Add(unlock);
            }

            ResearchNodes.Add(research);
        }

        public virtual Research GetResearch(string name)
        {
            foreach (Research research in ResearchNodes)
            {
                if (research.Name == name)
                {
                    return research;
                }
            }

            return null;
        }

        public virtual Research GetResearch(long id)
        {
            foreach (Research research in ResearchNodes)
            {
                if (research.ID == id)
                {
                    return research;
                }
            }

            return null;
        }

        public override void Dispose()
        {
            foreach (Research research in ResearchNodes)
            {
                research.Dispose();
            }
        }

        #endregion
    }
}
