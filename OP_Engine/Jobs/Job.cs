using System;
using System.Collections.Generic;
using OP_Engine.Time;

namespace OP_Engine.Jobs
{
    public class Job : IDisposable
    {
        #region Variables

        public long ID;
        public string Name;
        public long OwnerID;

        public List<Appointment> Schedule;
        public List<Task> Tasks;
        public List<Task> TasksCompleted;
        public List<Task> TasksAborted;

        #endregion

        #region Properties

        public Task CurrentTask
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
            Schedule = new List<Appointment>();
            Tasks = new List<Task>();
            TasksCompleted = new List<Task>();
            TasksAborted = new List<Task>();
        }

        #endregion

        #region Methods

        public virtual void Update(TimeHandler current_time)
        {
            Task current = Get_CurrentTask();
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
            Task current = Get_CurrentTask();
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

                        if (CurrentTask != null)
                        {
                            CurrentTask.Start(current_time, time_span);
                        }
                    }
                }
            }
        }

        public virtual Task GetTask(long id)
        {
            int count = Tasks.Count;
            for (int i = 0; i < count; i++)
            {
                Task existing = Tasks[i];
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

        public virtual Task GetTask(string name)
        {
            int count = Tasks.Count;
            for (int i = 0; i < count; i++)
            {
                Task existing = Tasks[i];
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

        public virtual Task Get_CurrentTask()
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
                        Task temp = Tasks[j + 1];
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
                Task task = Tasks[i];
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
            Schedule = null;

            foreach (Task task in Tasks)
            {
                task.Dispose();
            }
            Tasks = null;

            foreach (Task task in TasksCompleted)
            {
                task.Dispose();
            }
            TasksCompleted = null;

            foreach (Task task in TasksAborted)
            {
                task.Dispose();
            }
            TasksAborted = null;
        }

        #endregion
    }
}
