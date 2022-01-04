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

        #endregion
    }
}
