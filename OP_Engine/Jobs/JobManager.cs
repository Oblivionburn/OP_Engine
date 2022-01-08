using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace OP_Engine.Jobs
{
    public class JobManager : GameComponent
    {
        #region Variables

        public static List<Job> Jobs = new List<Job>();

        #endregion

        #region Constructor

        public JobManager(Game game) : base(game)
        {

        }

        #endregion

        #region Methods

        public static void Update(int current_time)
        {
            foreach (Job job in Jobs)
            {
                job.Update(current_time);
            }
        }

        public static void Update(int current_time, int task_step_time)
        {
            foreach (Job job in Jobs)
            {
                job.Update(current_time, task_step_time);
            }
        }

        public static Job GetJob(int id)
        {
            foreach (Job job in Jobs)
            {
                if (job.ID == id)
                {
                    return job;
                }
            }

            return null;
        }

        public static Job GetJob(string name)
        {
            foreach (Job job in Jobs)
            {
                if (job.Name == name)
                {
                    return job;
                }
            }

            return null;
        }

        public static Job GetJob(string name, int owner_id)
        {
            foreach (Job job in Jobs)
            {
                if (job.Name == name &&
                    job.OwnerID == owner_id)
                {
                    return job;
                }
            }

            return null;
        }

        public static List<Job> GetJobs(string name)
        {
            List<Job> jobs = new List<Job>();

            foreach (Job job in Jobs)
            {
                if (job.Name == name)
                {
                    jobs.Add(job);
                }
            }

            return jobs;
        }

        public static List<Job> GetJobs(int owner_id)
        {
            List<Job> jobs = new List<Job>();

            foreach (Job job in Jobs)
            {
                if (job.OwnerID == owner_id)
                {
                    jobs.Add(job);
                }
            }

            return jobs;
        }

        public static List<Job> GetJobs(string name, int owner_id)
        {
            List<Job> jobs = new List<Job>();

            foreach (Job job in Jobs)
            {
                if (job.Name == name &&
                    job.OwnerID == owner_id)
                {
                    jobs.Add(job);
                }
            }

            return jobs;
        }

        #endregion
    }
}
