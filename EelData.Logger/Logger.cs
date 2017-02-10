using System;
using System.Collections.Generic;

namespace EelData.Logger
{
    public class Logger
    {
        private List<ILogObserver> _logList = new List<ILogObserver>();


        public void Attach(ILogObserver observer)
        {
            _logList.Add(observer);
        }

        public void Log(string message)
        {
            foreach (ILogObserver obs in _logList)
            {
                obs.OnLog(message);
            }
        }

        public void Log(string message, Exception ex)
        {
            foreach (ILogObserver obs in _logList)
            {
                obs.OnLog(message, ex);
            }
        }
    }
}
