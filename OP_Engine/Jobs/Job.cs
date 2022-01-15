using System.Collections.Generic;

using OP_Engine.Utility;

namespace OP_Engine.Jobs
{
    public class Job : Something
    {
        #region Variables

        public Task CurrentTask;
        public List<Task> PreviousTasks = new List<Task>();
        public List<Task> Tasks = new List<Task>();

        #endregion

        #region Constructor

        public Job()
        {

        }

        #endregion

        #region Methods

        public virtual void Update(int current_time)
        {
            CurrentTask = Get_CurrentTask();
            if (CurrentTask != null)
            {
                if (!CurrentTask.Started &&
                    !CurrentTask.Completed)
                {
                    CurrentTask.Start(current_time);
                }

                if (CurrentTask.Started &&
                    !CurrentTask.Completed)
                {
                    CurrentTask.Update(current_time);
                }

                if (CurrentTask.Completed)
                {
                    if (!CurrentTask.Keep_On_Completed)
                    {
                        PreviousTasks.Add(Tasks[0]);
                        Tasks.RemoveAt(0);

                        CurrentTask = Get_CurrentTask();
                        if (CurrentTask != null)
                        {
                            CurrentTask.Start(current_time);
                        }
                    }
                }
            }
        }

        public virtual void Update(int current_time, int task_step_time)
        {
            CurrentTask = Get_CurrentTask();
            if (CurrentTask != null)
            {
                if (!CurrentTask.Started &&
                    !CurrentTask.Completed)
                {
                    CurrentTask.Start(current_time, task_step_time);
                }

                if (CurrentTask.Started &&
                    !CurrentTask.Completed)
                {
                    CurrentTask.Update(current_time, task_step_time);
                }

                if (CurrentTask.Completed)
                {
                    if (!CurrentTask.Keep_On_Completed)
                    {
                        PreviousTasks.Add(Tasks[0]);
                        Tasks.RemoveAt(0);

                        CurrentTask = Get_CurrentTask();
                        if (CurrentTask != null)
                        {
                            CurrentTask.Start(current_time, task_step_time);
                        }
                    }
                }
            }
        }

        public virtual Task GetTask(long id)
        {
            foreach (Task task in Tasks)
            {
                if (task.ID == id)
                {
                    return task;
                }
            }

            return null;
        }

        public virtual Task GetTask(string name)
        {
            foreach (Task task in Tasks)
            {
                if (task.Name == name)
                {
                    return task;
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

        public virtual void Abort(int current_time)
        {
            for (int i = 0; i < Tasks.Count; i++)
            {
                Task task = Tasks[i];
                if (task.Started)
                {
                    task.EndTime = current_time;
                    PreviousTasks.Add(task);
                }
                
                Tasks.Remove(task);
                i--;
            }
        }

        public override void Dispose()
        {
            foreach (Task task in Tasks)
            {
                task.Dispose();
            }
            Tasks = null;

            base.Dispose();
        }

        #endregion
    }
}
