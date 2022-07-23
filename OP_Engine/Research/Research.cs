using System;
using System.Collections.Generic;

using OP_Engine.Utility;

namespace OP_Engine.Research
{
    public class Research : Something
    {
        #region Variables

        public bool Started;
        public bool Completed;
        public bool Unlocked;

        //Resources required to start this one (name, amount)
        public Dictionary<string, int> Cost = new Dictionary<string, int>();

        //IDs of research required to unlock this one
        public List<long> Prerequisites = new List<long>();

        //IDs of research this contributes to unlocking when completed
        public List<long> Unlocks = new List<long>();

        #endregion

        #region Constructors

        public Research()
        {
            Time = default;
            Max_Time = default;
        }

        public Research(Dictionary<string, int> cost)
        {
            Time = default;
            Max_Time = default;

            foreach (var item in cost)
            {
                Cost.Add(item.Key, item.Value);
            }
        }

        #endregion

        #region Methods

        public virtual void Update(TimeSpan add_time)
        {
            if (Started &&
                !Completed)
            {
                if (Time >= Max_Time)
                {
                    Completed = true;
                }
                else
                {
                    Time.Add(add_time);
                }
            }
        }

        public virtual void AddCost(string name, int amount)
        {
            if (!Cost.ContainsKey(name))
            {
                Cost.Add(name, amount);
            }
            else
            {
                Cost[name] += amount;
            }
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
