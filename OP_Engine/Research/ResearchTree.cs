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

        #region Properties

        public bool Completed
        {
            get
            {
                return IsCompleted();
            }
        }

        #endregion

        #region Events

        public event EventHandler OnComplete;

        #endregion

        #region Constructors

        public ResearchTree()
        {
            
        }

        #endregion

        #region Methods

        public virtual void Update()
        {
            int count = ResearchNodes.Count;
            for (int i = 0; i < count; i++)
            {
                Research research = ResearchNodes[i];
                if (research != null)
                {
                    research.Update();

                    if (research.Completed)
                    {
                        CheckUnlocks(research);
                    }
                }
            }
        }

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
                        CheckUnlocks(research);
                    }
                }
            }
        }

        public virtual void AddResearch(long id, string name, string description, Dictionary<string, int> cost)
        {
            Research research = new Research
            {
                ID = id,
                Name = name,
                Description = description
            };

            foreach (var item in cost)
            {
                if (!research.Cost.ContainsKey(item.Key))
                {
                    research.Cost.Add(item.Key, item.Value);
                }
                else
                {
                    research.Cost[item.Key] += item.Value;
                }
            }

            ResearchNodes.Add(research);
        }

        public virtual void AddResearch(long id, string name, string description, Dictionary<string, int> cost, List<long> prerequisites, List<long> unlocks)
        {
            Research research = new Research
            {
                ID = id,
                Name = name,
                Description = description,
            };

            foreach (var item in cost)
            {
                if (!research.Cost.ContainsKey(item.Key))
                {
                    research.Cost.Add(item.Key, item.Value);
                }
                else
                {
                    research.Cost[item.Key] += item.Value;
                }
            }

            int prerequisiteCount = prerequisites.Count;
            for (int p = 0; p < prerequisiteCount; p++)
            {
                long prerequisiteId = prerequisites[p];
                if (!research.Prerequisites.Contains(prerequisiteId))
                {
                    research.Prerequisites.Add(prerequisiteId);
                }
            }

            int unlockCount = unlocks.Count;
            for (int u = 0; u < unlockCount; u++)
            {
                long unlockId = unlocks[u];
                if (!research.Unlocks.Contains(unlockId))
                {
                    research.Unlocks.Add(unlockId);
                }
            }

            ResearchNodes.Add(research);
        }

        public virtual void AddResearch(long id, string name, string description, TimeSpan time_to_complete)
        {
            Research research = new Research
            {
                ID = id,
                Name = name,
                Description = description,
                TimeToComplete = TimeSpan.FromMilliseconds(time_to_complete.TotalMilliseconds)
            };

            ResearchNodes.Add(research);
        }

        public virtual void AddResearch(long id, string name, string description, TimeSpan time_to_complete, List<long> prerequisites, List<long> unlocks)
        {
            Research research = new Research
            {
                ID = id,
                Name = name,
                Description = description,
                TimeToComplete = TimeSpan.FromMilliseconds(time_to_complete.TotalMilliseconds)
            };

            int prerequisiteCount = prerequisites.Count;
            for (int p = 0; p < prerequisiteCount; p++)
            {
                long prerequisiteId = prerequisites[p];
                if (!research.Prerequisites.Contains(prerequisiteId))
                {
                    research.Prerequisites.Add(prerequisiteId);
                }
            }

            int unlockCount = unlocks.Count;
            for (int u = 0; u < unlockCount; u++)
            {
                long unlockId = unlocks[u];
                if (!research.Unlocks.Contains(unlockId))
                {
                    research.Unlocks.Add(unlockId);
                }
            }

            ResearchNodes.Add(research);
        }

        public virtual void AddResearch(long id, string name, string description, TimeSpan time_to_complete, Dictionary<string, int> cost)
        {
            Research research = new Research
            {
                ID = id,
                Name = name,
                Description = description,
                TimeToComplete = TimeSpan.FromMilliseconds(time_to_complete.TotalMilliseconds)
            };

            foreach (var item in cost)
            {
                if (!research.Cost.ContainsKey(item.Key))
                {
                    research.Cost.Add(item.Key, item.Value);
                }
                else
                {
                    research.Cost[item.Key] += item.Value;
                }
            }

            ResearchNodes.Add(research);
        }

        public virtual void AddResearch(long id, string name, string description, TimeSpan time_to_complete, Dictionary<string, int> cost, List<long> prerequisites, List<long> unlocks)
        {
            Research research = new Research
            {
                ID = id,
                Name = name,
                Description = description,
                TimeToComplete = TimeSpan.FromMilliseconds(time_to_complete.TotalMilliseconds)
            };

            foreach (var item in cost)
            {
                if (!research.Cost.ContainsKey(item.Key))
                {
                    research.Cost.Add(item.Key, item.Value);
                }
                else
                {
                    research.Cost[item.Key] += item.Value;
                }
            }

            int prerequisiteCount = prerequisites.Count;
            for (int p = 0; p < prerequisiteCount; p++)
            {
                long prerequisiteId = prerequisites[p];
                if (!research.Prerequisites.Contains(prerequisiteId))
                {
                    research.Prerequisites.Add(prerequisiteId);
                }
            }

            int unlockCount = unlocks.Count;
            for (int u = 0; u < unlockCount; u++)
            {
                long unlockId = unlocks[u];
                if (!research.Unlocks.Contains(unlockId))
                {
                    research.Unlocks.Add(unlockId);
                }
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

        public bool IsCompleted()
        {
            int completed = 0;

            int count = ResearchNodes.Count;
            for (int i = 0; i < count; i++)
            {
                Research research = ResearchNodes[i];
                if (research != null)
                {
                    if (research.Completed)
                    {
                        completed++;
                    }
                }
            }

            if (completed >= count)
            {
                OnComplete?.Invoke(this, EventArgs.Empty);
                return true;
            }

            return false;
        }

        public void CheckUnlocks(Research research)
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
                        for (int p = 0; p < prerequisiteCount; p++)
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

                        if (unlocked)
                        {
                            unlocked_research.Unlock();
                        }
                    }
                }
            }
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
