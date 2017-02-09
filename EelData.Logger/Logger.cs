using System;
using System.Collections.Generic;
using System.IO;

namespace EelData.Logger
{
    public class Logger
    {
        private List<ILogObserver> _logList = new List<ILogObserver>();

        public void Attach(ILogObserver observer)
        {
            _logList.Add(observer);
        }


        /// <summary>
        /// Error logger - for exceptions - Logger.WriteErrorLog(whatever);
        /// </summary>
        public void WriteErrorLog(Exception ex, string Message, params object[] args)
        {
            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.log", true))
            {
                sw.WriteLine(DateTime.Now.ToString("[dd-MM-yyyy HH:mm:ss]") + " " + ex.Source.ToString().Trim() + "; " + ex.Message);
            }
        }

        /// <summary>
        /// Basic logger for regular data - to use do Logger.WriteToLog(whatever);
        /// </summary>
        public void WriteToLog(string Message, params object[] args)
        {
            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.log", true))
            {
                sw.WriteLine(DateTime.Now.ToString("[dd-MM-yyyy HH:mm:ss]") + " " + Message);
            }
        }
    }
}
