namespace OP_Engine.Research
{
    public class Research : IDisposable
    {
        #region Variables

        public long ID;
        public string? Name;
        public string? Description;
        public TimeSpan Time = new();
        public TimeSpan Max_Time = new();
        public TimeSpan TimeElapsed;
        public TimeSpan TimeToComplete;
        public bool Started;
        public bool Completed;
        public bool Unlocked;

        //Resources required to complete this research (name, amount)
        public Dictionary<string, int> Cost = [];

        //Resources paid toward completing this research (name, amount)
        public Dictionary<string, int> Payments = [];

        //IDs of other research required to be Completed for this research to be Unlocked
        public List<Research> Prerequisites = [];

        //IDs of other research this will contribute to Unlocking when this is Completed
        public List<Research> Unlocks = [];

        #endregion

        #region Events

        public event EventHandler? OnStart;
        public event EventHandler? OnComplete;
        public event EventHandler? OnUnlock;

        #endregion

        #region Constructors

        public Research()
        {
            
        }

        public Research(Dictionary<string, int> cost) : this()
        {
            foreach (var item in cost)
            {
                Cost.Add(item.Key, item.Value);
            }
        }

        #endregion

        #region Methods

        public virtual void Update()
        {
            if (Cost.Count > 0)
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

        public virtual void Update(TimeSpan? add_time)
        {
            if (Started &&
                !Completed)
            {
                if (TimeElapsed >= TimeToComplete)
                {
                    Complete();
                }
                else if (add_time != null)
                {
                    TimeElapsed.Add((TimeSpan)add_time);
                }
            }
        }

        public virtual void AddCost(string name, int amount)
        {
            if (!Cost.TryAdd(name, amount))
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

        public virtual void Dispose()
        {
            
        }

        #endregion
    }
}
