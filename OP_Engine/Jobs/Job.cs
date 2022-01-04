using System.Collections.Generic;

using OP_Engine.Utility;

namespace OP_Engine.Jobs
{
    public class Job : Something
    {
        #region Variables

        public List<Task> Tasks = new List<Task>();

        #endregion

        #region Constructor

        public Job()
        {

        }

        #endregion

        #region Methods

        public virtual void Update(int current_time, int task_step_time)
        {
            if (Tasks.Count > 0)
            {
                Task current_task = Tasks[0];
                if (current_task != null)
                {
                    if (current_task.Completed &&
                        !current_task.Keep_On_Completed)
                    {
                        Tasks.RemoveAt(0);

                        if (Tasks.Count > 0)
                        {
                            current_task = Tasks[0];
                            if (current_task != null)
                            {
                                current_task.Start(current_time, task_step_time);
                            }
                        }
                    }
                    else
                    {
                        current_task.Update(current_time, task_step_time);
                    }
                }
            }
        }

        public virtual Task GetTask(int id)
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

        public virtual Task CurrentTask()
        {
            if (Tasks.Count > 0)
            {
                return Tasks[0];
            }

            return null;
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
