using OP_Engine.Time;

namespace OP_Engine.Jobs
{
    public class Job : IDisposable
    {
        #region Variables

        public long ID;
        public string? Name;
        public long OwnerID;

        public List<Appointment> Schedule = [];
        public List<JobTask> Tasks = [];
        public List<JobTask> TasksCompleted = [];
        public List<JobTask> TasksAborted = [];

        #endregion

        #region Properties

        public JobTask? CurrentTask
        {
            get
            {
                return Get_CurrentTask();
            }
        }

        #endregion

        #region Constructor

        public Job()
        {
            
        }

        #endregion

        #region Methods

        public virtual void Update(TimeHandler current_time)
        {
            JobTask? current = Get_CurrentTask();
            if (current != null)
            {
                if (!current.Completed)
                {
                    if (!current.Started)
                    {
                        current.Start(current_time);
                    }

                    if (!current.Completed)
                    {
                        current.Update(current_time);
                    }
                }

                if (current.Completed)
                {
                    if (!current.Keep_On_Completed &&
                        Tasks.Count > 0)
                    {
                        TasksCompleted.Add(Tasks[0]);
                        if (TasksCompleted.Count > 200)
                        {
                            TasksCompleted.RemoveAt(0);
                        }

                        Tasks.RemoveAt(0);

                        current = Get_CurrentTask();
                        current?.Start(current_time);
                    }
                }
            }
        }

        public virtual void Update(TimeHandler current_time, TimeSpan time_span)
        {
            JobTask? current = Get_CurrentTask();
            if (current != null)
            {
                if (!current.Completed)
                {
                    if (!current.Started)
                    {
                        current.Start(current_time, time_span);
                    }

                    current.Update(current_time, time_span);
                }

                if (current.Completed)
                {
                    if (!current.Keep_On_Completed)
                    {
                        TasksCompleted.Add(Tasks[0]);
                        if (TasksCompleted.Count > 200)
                        {
                            TasksCompleted.RemoveAt(0);
                        }

                        Tasks.RemoveAt(0);
                        CurrentTask?.Start(current_time, time_span);
                    }
                }
            }
        }

        public virtual Appointment? GetAppointment(TimeHandler current_time)
        {
            int count = Schedule.Count;
            for (int i = 0; i < count; i++)
            {
                Appointment appointment = Schedule[i];

                //Check for StartTime and EndTime not in the same day
                //Example: Starts at 22:00 and ends 06:00 the next morning
                if (appointment?.StartTime?.Hours > appointment?.EndTime?.Hours &&
                    appointment?.EndTime?.Hours >= 0)
                {
                    if (current_time.Hours >= appointment?.StartTime?.Hours &&
                        current_time.Minutes >= appointment?.StartTime?.Minutes &&
                        current_time.Seconds >= appointment?.StartTime?.Seconds &&
                        current_time.Milliseconds >= appointment?.StartTime?.Milliseconds)
                    {
                        return appointment;
                    }
                    else if (current_time.Hours < appointment?.EndTime?.Hours)
                    {
                        return appointment;
                    }
                }
                else if (current_time.Hours >= appointment?.StartTime?.Hours &&
                        current_time.Minutes >= appointment?.StartTime?.Minutes &&
                        current_time.Seconds >= appointment?.StartTime?.Seconds &&
                        current_time.Milliseconds >= appointment?.StartTime?.Milliseconds &&
                        current_time.Hours < appointment?.EndTime?.Hours)
                {
                    return appointment;
                }
            }

            return null;
        }

        public virtual JobTask? GetTask(long id)
        {
            int count = Tasks.Count;
            for (int i = 0; i < count; i++)
            {
                JobTask existing = Tasks[i];
                if (existing.ID == id)
                {
                    return existing;
                }
            }

            return null;
        }

        public virtual JobTask? GetTask(string name)
        {
            int count = Tasks.Count;
            for (int i = 0; i < count; i++)
            {
                JobTask existing = Tasks[i];
                if (existing.Name == name)
                {
                    return existing;
                }
            }

            return null;
        }

        public virtual JobTask? Get_CurrentTask()
        {
            if (Tasks.Count > 0)
            {
                return Tasks[0];
            }

            return null;
        }

        public virtual void Sort_ByPriority()
        {
            int count = Tasks.Count;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count - 1; j++)
                {
                    if (Tasks[j].Priority > Tasks[j + 1].Priority)
                    {
                        JobTask temp = Tasks[j + 1];
                        Tasks[j + 1] = Tasks[j];
                        Tasks[j] = temp;
                    }
                }
            }
        }

        public virtual void Abort(TimeHandler current_time)
        {
            for (int i = 0; i < Tasks.Count; i++)
            {
                JobTask task = Tasks[i];
                if (task.Started)
                {
                    task.EndTime = current_time;
                    TasksAborted.Add(task);
                }

                Tasks.Remove(task);
                i--;
            }
        }

        public virtual void Dispose()
        {
            foreach (JobTask task in Tasks)
            {
                task.Dispose();
            }

            foreach (JobTask task in TasksCompleted)
            {
                task.Dispose();
            }

            foreach (JobTask task in TasksAborted)
            {
                task.Dispose();
            }
        }

        #endregion
    }
}
