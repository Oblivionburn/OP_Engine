using System;

namespace OP_Engine.Logging
{
    public class Log
    {
        #region Variables

        public DateTime TimeStamp;
        public string Source;
        public string Message;
        public string StackTrace;

        #endregion

        #region Constructors

        public Log()
        {

        }

        public Log(string source, string message, string stack_trace)
        {
            TimeStamp = DateTime.Now;
            Source = source;
            Message = message;
            StackTrace = stack_trace;
        }

        #endregion

        #region Methods



        #endregion
    }
}
