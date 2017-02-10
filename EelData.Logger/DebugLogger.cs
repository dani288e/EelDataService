using System;
using System.IO;
using System.Text;

namespace EelData.Logger
{
    public class DebugLogger : AbstractLogger
    {
        public DebugLogger(int level)
        {
            this.level = level;
        }

        protected override void Write(string message)
        {
            //Console.WriteLine("File::Logger: " + message);
            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.log", true))
            {
                sw.WriteLine(DateTime.Now.ToString("[dd-MM-yyyy HH:mm:ss]") + " DebugLogger: " + message);
            }
        }
    }
}
