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
            int count = ResearchNodes.Count;
            for (int i = 0; i < count; i++)
            {
                Research research = ResearchNodes[i];
                if (research != null)
                {
                    research.Update(add_time);

                    if (research.Completed)
                    {
                        int unlockCount = research.Unlocks.Count;
                        for (int u = 0; u < unlockCount; u++)
                        {
                            long unlock = research.Unlocks[u];

                            //Check if unlocked research has all prerequisites completed
                            Research unlocked_research = GetResearch(unlock);
                            if (unlocked_research != null)
                            {
                                //Don't bother checking if it's already unlocked
                                if (!unlocked_research.Unlocked)
                                {
                                    bool unlocked = false;
                                    int prerequisites = 0;

                                    int prerequisiteCount = unlocked_research.Prerequisites.Count;
                                    for (int p = 0; p < unlockCount; p++)
                                    {
                                        long prerequisite = unlocked_research.Prerequisites[p];

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

            int prerequisiteCount = prerequisites.Count;
            for (int p = 0; p < prerequisiteCount; p++)
            {
                research.Prerequisites.Add(prerequisites[p]);
            }

            int unlockCount = unlocks.Count;
            for (int u = 0; u < unlockCount; u++)
            {
                research.Unlocks.Add(unlocks[u]);
            }

            ResearchNodes.Add(research);
        }

        public virtual Research GetResearch(string name)
        {
            int count = ResearchNodes.Count;
            for (int i = 0; i < count; i++)
            {
                Research existing = ResearchNodes[i];
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

        public virtual Research GetResearch(long id)
        {
            int count = ResearchNodes.Count;
            for (int i = 0; i < count; i++)
            {
                Research existing = ResearchNodes[i];
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
