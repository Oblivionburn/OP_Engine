using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace OP_Engine.Logging
{
    public class Logger : GameComponent
    {
        #region Variables

        public static string LogFile;
        public static List<Log> Logs = new List<Log>();

        #endregion

        #region Constructors

        public Logger(Game game) : base(game)
        {
            game.Exiting += Game_Exiting;
        }

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

        public static void WriteLog()
        {
            if (!string.IsNullOrEmpty(LogFile))
            {
                StringBuilder sb = new StringBuilder();

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

        private void Game_Exiting(object sender, EventArgs e)
        {
            if (Logs.Count > 0)
            {
                WriteLog();
            }
        }

        #endregion
    }
}
