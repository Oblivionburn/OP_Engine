using OP_Engine.Controls;
using OP_Engine.Utility;

namespace OP_Engine.Jobs
{
    public class Task : Something
    {
        #region Variables

        public int StartTime;
        public int StepTime;
        public int EndTime;
        public bool Started;
        public bool Completed;
        public bool Keep_On_Completed;

        public ProgressBar TaskBar;
        
        #endregion

        #region Constructor

        public Task()
        {
            TaskBar = new ProgressBar();
        }

        #endregion

        #region Methods

        public virtual void Update(int current_time)
        {
            if (!Completed)
            {
                if (!InProgress(current_time))
                {
                    Completed = true;
                }
            }
        }

        public virtual void Update(int current_time, int step_time)
        {
            if (Started &&
                !Completed &&
                current_time >= StepTime)
            {
                StepTime = current_time + step_time;

                if (TaskBar.Increment > 0 &&
                    TaskBar.Value < TaskBar.Max_Value)
                {
                    TaskBar.Step();
                }

                if (!InProgress(current_time))
                {
                    if (EndTime == 0)
                    {
                        EndTime = current_time;
                    }
                    
                    Completed = true;
                }
            }
        }

        public virtual void Start(int current_time)
        {
            Started = true;
            StartTime = current_time;
        }

        public virtual void Start(int current_time, int next_step_time)
        {
            Started = true;
            StartTime = current_time;
            StepTime = current_time + next_step_time;
        }

        public virtual bool InProgress(int current_time)
        {
            if (Started &&
                StartTime > 0)
            {
                if (current_time >= StartTime &&
                    current_time < EndTime)
                {
                    return true;
                }
                else if (TaskBar.Value > 0 &&
                         TaskBar.Increment > 0 &&
                         TaskBar.Value < TaskBar.Max_Value)
                {
                    return true;
                }
                else if (current_time >= StartTime &&
                         EndTime == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public override void Dispose()
        {
            if (TaskBar != null)
            {
                TaskBar.Dispose();
            }

            base.Dispose();
        }

        #endregion
    }
}
