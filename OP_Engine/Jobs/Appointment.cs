using OP_Engine.Time;
using System;

namespace OP_Engine.Jobs
{
    public class Appointment : IDisposable
    {
        public string Name;
        public TimeHandler StartTime;
        public TimeHandler EndTime;

        public Appointment()
        {

        }

        public virtual void Dispose()
        {
            StartTime?.Dispose();
            EndTime?.Dispose();
        }
    }
}
