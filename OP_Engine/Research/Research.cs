﻿using System;
using System.Collections.Generic;
using System.Linq;

using OP_Engine.Utility;

namespace OP_Engine.Research
{
    public class Research : Something
    {
        #region Variables

        public TimeSpan TimeElapsed;
        public TimeSpan TimeToComplete;
        public bool Started;
        public bool Completed;
        public bool Unlocked;

        //Resources required to complete this research (name, amount)
        public Dictionary<string, int> Cost = new Dictionary<string, int>();

        //Resources paid toward completing this research (name, amount)
        public Dictionary<string, int> Payments = new Dictionary<string, int>();

        //IDs of other research required to be Completed for this research to be Unlocked
        public List<long> Prerequisites = new List<long>();

        //IDs of other research this will contribute to Unlocking when this is Completed
        public List<long> Unlocks = new List<long>();

        #endregion

        #region Events

        public event EventHandler OnStart;
        public event EventHandler OnComplete;
        public event EventHandler OnUnlock;

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

        public virtual void Update()
        {
            if (Cost.Any())
            {
                int costsPaid = 0;

                foreach (var cost in Cost)
                {
                    bool paid = false;
                    bool found = false;

                    foreach (var payment in Payments)
                    {
                        if (payment.Key == cost.Key)
                        {
                            found = true;

                            if (payment.Value >= cost.Value)
                            {
                                paid = true;
                            }

                            break;
                        }

                        if (found &&
                            paid)
                        {
                            costsPaid++;
                        }
                    }
                }

                if (costsPaid == Cost.Count)
                {
                    Complete();
                }
            }
            else
            {
                Complete();
            }
        }

        public virtual void Update(TimeSpan add_time)
        {
            if (Started &&
                !Completed)
            {
                if (TimeElapsed >= TimeToComplete)
                {
                    Complete();
                }
                else
                {
                    TimeElapsed.Add(add_time);
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

        public virtual void Start()
        {
            Started = true;
            OnStart?.Invoke(this, EventArgs.Empty);
        }

        public virtual void Complete()
        {
            Completed = true;
            OnComplete?.Invoke(this, EventArgs.Empty);
        }

        public virtual void Unlock()
        {
            Unlocked = true;
            OnUnlock?.Invoke(this, EventArgs.Empty);
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion
    }
}
