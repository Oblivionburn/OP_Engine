using System;
using OP_Engine.Controls;
using OP_Engine.Enums;
using OP_Engine.Time;
using OP_Engine.Utility;

namespace OP_Engine.Jobs
{
    public class Task : IDisposable
    {
        #region Variables

        public long ID;
        public string Name;
        public string Type;
        public string Description;
        public string Assignment;
        public long OwnerID;

        public Direction Direction;
        public Location Location;

        public TimeHandler StartTime;
        public TimeSpan StepTime;
        public TimeHandler EndTime;

        public bool Started;
        public bool Completed;

        /// <summary>
        /// If this value is true, then Job will not automatically start the next queued task when this one has completed.
        /// </summary>
        public bool Keep_On_Completed;

        /// <summary>
        /// Alternative way to check if task is in progress (TaskBar.Value < TaskBar.Max_Value) if EndTime is null and TaskBar.Rate > 0, 
        /// or for visual display of task progress.
        /// </summary>
        public ProgressBar TaskBar;

        #endregion

        #region Events

        public event EventHandler OnStart;
        public event EventHandler OnStep;
        public event EventHandler OnComplete;

        #endregion

        #region Constructor

        public Task()
        {
            Location = new Location();
            TaskBar = new ProgressBar();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Runs the task if it is started and is not yet completed.
        /// </summary>
        /// <param name="current_time">
        /// Passed to InProgress() to check if the task should be completed.
        /// </param>
        public virtual void Update(TimeHandler current_time)
        {
            if (Started &&
                !Completed)
            {
                if (current_time != null)
                {
                    if (TaskBar.Rate > 0 &&
                        TaskBar.Value < TaskBar.Max_Value)
                    {
                        TaskBar.Step();
                    }

                    Action();

                    if (!InProgress(current_time))
                    {
                        Complete(current_time);
                    }
                    else
                    {
                        OnStep?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// Runs the task if it is started and is not yet completed.
        /// </summary>
        /// <param name="current_time">
        /// Used for checking if the task has reached its next StepTime, and passed to InProgress() to check if the task should be completed.
        /// </param>
        /// <param name="step_time">
        /// Used for setting the next StepTime interval when current_time is greater than or equal to the current StepTime.
        /// </param>
        public virtual void Update(TimeHandler current_time, TimeSpan step_time)
        {
            if (Started &&
                !Completed)
            {
                if (current_time != null)
                {
                    if (StepTime == null)
                    {
                        StepTime = new TimeSpan(current_time.Days, current_time.Hours, current_time.Minutes, current_time.Seconds,
                            current_time.Milliseconds);
                        StepTime.Add(step_time);
                    }

                    if (current_time.TotalMilliseconds >= StepTime.TotalMilliseconds)
                    {
                        StepTime = new TimeSpan(current_time.Days, current_time.Hours, current_time.Minutes, current_time.Seconds,
                            current_time.Milliseconds);
                        StepTime.Add(step_time);

                        if (TaskBar.Rate > 0 &&
                            TaskBar.Value < TaskBar.Max_Value)
                        {
                            TaskBar.Step();
                        }

                        Action();

                        if (!InProgress(current_time))
                        {
                            if (EndTime == null)
                            {
                                EndTime = current_time;
                            }

                            Completed = true;
                            OnComplete?.Invoke(this, EventArgs.Empty);
                        }
                        else
                        {
                            OnStep?.Invoke(this, EventArgs.Empty);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Starts the task: sets Started to true, runs Action_Start(), and sets StartTime to current_time.
        /// </summary>
        /// <param name="current_time">
        /// StartTime gets set to this value.
        /// </param>
        public virtual void Start(TimeHandler current_time)
        {
            Started = true;
            StartTime = new TimeHandler(current_time);

            Action_Start();

            OnStart?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Starts the task: sets Started to true, runs Action_Start(), sets StartTime to current_time, and 
        /// sets StepTime to current_time + step_time for the next interval of progress.
        /// </summary>
        /// <param name="current_time">
        /// StartTime gets set to this value.
        /// </param>
        /// <param name="step_time">
        /// StepTime gets set to current_time + this value.
        /// </param>
        public virtual void Start(TimeHandler current_time, TimeSpan step_time)
        {
            Started = true;
            StartTime = new TimeHandler(current_time);
            StepTime = new TimeSpan(current_time.Days, current_time.Hours, current_time.Minutes, current_time.Seconds,
                current_time.Milliseconds + step_time.Milliseconds);

            Action_Start();

            OnStart?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Completes the task: sets Completed to true, and sets EndTime to current_time if EndTime is null.
        /// </summary>
        /// <param name="current_time">
        /// The value EndTime will be set to if EndTime is null.
        /// </param>
        public virtual void Complete(TimeHandler current_time)
        {
            Completed = true;

            if (EndTime == null)
            {
                EndTime.CopyTime(current_time);
            }

            Action_End();

            OnComplete?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// For checking if the task is ready to be completed.
        /// </summary>
        /// <param name="current_time">
        /// If StartTime and EndTime are not null, then will be used to check if it's greater than or equal to StartTime and less than EndTime.
        /// Else if StartTime is not null, then will be used to check if it's greater than or equal to StartTime.
        /// </param>
        /// <returns>
        /// <para>If StartTime and EndTime are not null, then this will return true if current_time is greater than or equal to StartTime and less than EndTime.</para>
        /// <para>Else if TaskBar.Value and TaskBar.Rate are greater than 0, then this will return true if TaskBar.Value is less than TaskBar.Max_Value.</para>
        /// <para>Else if StartTime is not null, then this will return true if current_time is greater than or equal to StartTime.</para>
        /// <para>Returns false if none of the above is true.</para>
        /// </returns>
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

        /// <summary>
        /// Override this to execute custom code when the task is started.
        /// </summary>
        public virtual void Action_Start()
        {
            
        }

        /// <summary>
        /// Override this to execute custom code while the task is running (every time Update is called).
        /// </summary>
        public virtual void Action()
        {
            
        }

        /// <summary>
        /// Override this to execute custom code when the task is completed.
        /// </summary>
        public virtual void Action_End()
        {
            
        }

        public virtual void Dispose()
        {
            TaskBar.Dispose();

            StartTime?.Dispose();
            EndTime?.Dispose();
        }

        #endregion
    }
}
