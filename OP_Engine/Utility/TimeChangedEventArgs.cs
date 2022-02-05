using System;

namespace OP_Engine.Utility
{
    public class TimeChangedEventArgs : EventArgs
    {
        public long Amount { get; private set; }

        public TimeChangedEventArgs(long amount)
        {
            Amount = amount;
        }
    }
}
