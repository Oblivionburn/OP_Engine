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
            game.Exiting += Game_Exiting;
        }

        #endregion

        #region Methods

        public static void Update(TimeHandler current_time)
        {
            int count = Jobs.Count;
            for (int i = 0; i < count; i++)
            {
                Jobs[i]?.Update(current_time);
            }
        }

        public static void Update(TimeHandler current_time, TimeSpan time_span)
        {
            int count = Jobs.Count;
            for (int i = 0; i < count; i++)
            {
                Jobs[i]?.Update(current_time, time_span);
            }
        }

        public static Job GetJob(long id)
        {
            int count = Jobs.Count;
            for (int i = 0; i < count; i++)
            {
                Job existing = Jobs[i];
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

        public static Job GetJob(string name)
        {
            int count = Jobs.Count;
            for (int i = 0; i < count; i++)
            {
                Job existing = Jobs[i];
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

        public static Job GetJob(string name, long owner_id)
        {
            int count = Jobs.Count;
            for (int i = 0; i < count; i++)
            {
                Job existing = Jobs[i];
                if (existing != null)
                {
                    if (existing.Name == name &&
                        existing.OwnerIDs.Contains(owner_id))
                    {
                        return existing;
                    }
                }
            }

            return null;
        }

        public static List<Job> GetJobs(string name)
        {
            List<Job> jobs = new List<Job>();

            int count = Jobs.Count;
            for (int i = 0; i < count; i++)
            {
                Job existing = Jobs[i];
                if (existing != null)
                {
                    if (existing.Name == name)
                    {
                        jobs.Add(existing);
                    }
                }
            }

            return jobs;
        }

        public static List<Job> GetJobs(long owner_id)
        {
            List<Job> jobs = new List<Job>();

            int count = Jobs.Count;
            for (int i = 0; i < count; i++)
            {
                Job existing = Jobs[i];
                if (existing != null)
                {
                    if (existing.OwnerIDs.Contains(owner_id))
                    {
                        jobs.Add(existing);
                    }
                }
            }

            return jobs;
        }

        public static List<Job> GetJobs(string name, long owner_id)
        {
            List<Job> jobs = new List<Job>();

            int count = Jobs.Count;
            for (int i = 0; i < count; i++)
            {
                Job existing = Jobs[i];
                if (existing != null)
                {
                    if (existing.Name == name &&
                        existing.OwnerIDs.Contains(owner_id))
                    {
                        jobs.Add(existing);
                    }
                }
            }

            return jobs;
        }

        private void Game_Exiting(object sender, EventArgs e)
        {
            foreach (Job job in Jobs)
            {
                job.Dispose();
            }
        }

        #endregion
    }
}
