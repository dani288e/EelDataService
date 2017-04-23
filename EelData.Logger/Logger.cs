using System;
using System.Collections.Generic;

namespace EelData.Logger
{
    /// <summary>
    /// logger class and be notified if certain events occur. 
    /// </summary>
    public class Logger
    {
        private List<ILogObserver> _logList = new List<ILogObserver>();

        /// <summary>
        /// attach a listening observer to the logger.
        /// </summary>
        /// <param name="observer">observer - the listening device.</param>
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
