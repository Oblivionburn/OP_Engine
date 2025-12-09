using System;
using System.Collections.Generic;
using OP_Engine.Time;
using OP_Engine.Utility;

namespace OP_Engine.Jobs
{
    public class Job : Something
    {
        #region Variables

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

        public Job() : base()
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

                    current.Update(current_time);
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
                            CurrentTask.Start(current_time);
                        }
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

        public override void Dispose()
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

            base.Dispose();
        }

        #endregion
    }
}
