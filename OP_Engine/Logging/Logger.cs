using System.Text;
using Microsoft.Xna.Framework;

namespace OP_Engine.Logging
{
    public class Logger(Game game) : GameComponent(game)
    {
        #region Variables

        public static string? LogFile;
        public static List<Log> Logs = [];

        #endregion
        #region Constructors

        #endregion

        #region Methods

        public static void ClearLog()
        {
            Logs.Clear();

            if (!string.IsNullOrEmpty(LogFile))
            {
                File.WriteAllText(LogFile, "");
            }
        }

        public static void AddLog(string source, string message, string stack_trace)
        {
            Logs.Add(new Log(source, message, stack_trace));
        }

        public static void WriteLog()
        {
            if (!string.IsNullOrEmpty(LogFile))
            {
                StringBuilder sb = new();

                for (int i = 0; i < Logs.Count; i++)
                {
                    Log log = Logs[i];
                    sb.AppendLine("TimeStamp: " + log.TimeStamp.ToString());
                    sb.AppendLine("Source: " + log.Source);
                    sb.AppendLine("Message: " + log.Message);
                    sb.AppendLine("Stack Trace: " + log.StackTrace);
                    sb.AppendLine();
                }

                File.AppendAllText(LogFile, sb.ToString());
            }
        }

        #endregion
    }
}
