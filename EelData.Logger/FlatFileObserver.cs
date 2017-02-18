using System;
using System.IO;

namespace EelData.Logger
{
    public class FlatFileObserver : ILogObserver
    {
        private void Log(string message)
        {
            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.log", true))
            {
                sw.WriteLine(DateTime.Now.ToString("[dd-MM-yyyy HH:mm:ss]") + " " + message);
            }
        }

        public void OnLog(string message)
        {
            Log(message);
        }

        public void OnLog(string message, Exception ex)
        {
            Log(ex.Source.ToString().Trim() + " " + message + "; " + ex.Message);
        }
    }
}