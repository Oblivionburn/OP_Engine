using System;

using OP_Engine.Controls;
using OP_Engine.Time;
using OP_Engine.Utility;

namespace OP_Engine.Jobs
{
    public class Task : Something
    {
        #region Variables

        public TimeHandler StartTime;
        public TimeHandler StepTime;
        public TimeHandler EndTime;
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

        public virtual void Update(TimeHandler current_time)
        {
            if (Started &&
                !Completed)
            {
                if (TaskBar.Rate > 0 &&
                    TaskBar.Value < TaskBar.Max_Value)
                {
                    TaskBar.Step();
                }

                if (!InProgress(current_time))
                {
                    if (EndTime == null)
                    {
                        EndTime = current_time;
                    }

                    Completed = true;
                }
            }
        }

        public virtual void Update(TimeHandler current_time, TimeSpan time_span)
        {
            if (Started &&
                !Completed)
            {
                if (current_time != null)
                {
                    if (StepTime == null)
                    {
                        StepTime = new TimeHandler(current_time, time_span);
                    }

                    if (current_time.TotalMilliseconds >= StepTime.TotalMilliseconds)
                    {
                        StepTime.CopyTime(current_time);
                        StepTime.AddTimeSpan(time_span);

                        if (TaskBar.Rate > 0 &&
                            TaskBar.Value < TaskBar.Max_Value)
                        {
                            TaskBar.Step();
                        }

                        if (!InProgress(current_time))
                        {
                            if (EndTime == null)
                            {
                                EndTime = current_time;
                            }

                            Completed = true;
                        }
                    }
                }
            }
        }

        public virtual void Start(TimeHandler current_time)
        {
            Started = true;
            StartTime = new TimeHandler(current_time);
        }

        public virtual void Start(TimeHandler current_time, TimeSpan time_span)
        {
            Started = true;
            StartTime = new TimeHandler(current_time);
            StepTime = new TimeHandler(current_time, time_span);
        }

        public virtual bool InProgress(TimeHandler current_time)
        {
            if (Started &&
                StartTime != null)
            {
                if (current_time != null &&
                    EndTime != null)
                {
                    if (current_time.TotalMilliseconds >= StartTime.TotalMilliseconds &&
                        current_time.TotalMilliseconds < EndTime.TotalMilliseconds)
                    {
                        return true;
                    }
                }
                else if (TaskBar.Value > 0 &&
                         TaskBar.Rate > 0 &&
                         TaskBar.Value < TaskBar.Max_Value)
                {
                    return true;
                }
                else if (current_time != null)
                {
                    if (current_time.TotalMilliseconds >= StartTime.TotalMilliseconds)
                    {
                        return true;
                    }
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
