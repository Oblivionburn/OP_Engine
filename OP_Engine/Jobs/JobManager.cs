using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using OP_Engine.Time;

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

        public static void Update(TimeHandler current_time)
        {
            foreach (Job job in Jobs)
            {
                job.Update(current_time);
            }
        }

        public static void Update(TimeHandler current_time, TimeSpan time_span)
        {
            foreach (Job job in Jobs)
            {
                job.Update(current_time, time_span);
            }
        }

        public static Job GetJob(long id)
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

        public static Job GetJob(string name, long owner_id)
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

        public static List<Job> GetJobs(long owner_id)
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

        public static List<Job> GetJobs(string name, long owner_id)
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
