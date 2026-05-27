using System;
using OP_Engine.Time;

namespace OP_Engine.Jobs
{
    public class Appointment : IDisposable
    {
        #region Variables

        public string Name;
        public TimeHandler StartTime;
        public TimeHandler EndTime;

        #endregion

        #region Constructors

        public Appointment()
        {
            
        }

        #endregion

        #region Methods

        public virtual void Dispose()
        {
            StartTime?.Dispose();
            EndTime?.Dispose();
        }

        #endregion
    }
}
