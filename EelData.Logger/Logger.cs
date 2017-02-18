using System;
using System.Collections.Generic;

namespace EelData.Logger
{
    /// <summary>
    /// The Observer Design Pattern allows Observer classes to attach itself
    /// this Logger class and be notified if certain events occur. 
    /// </summary>
    public class Logger
    {
        private List<ILogObserver> _logList = new List<ILogObserver>();

        /// <summary>
        /// Attach a listening observer logging device to logger.
        /// </summary>
        /// <param name="observer">Observer (listening device).</param>
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

        public void Log(string v, NotSupportedException notSupportedException, object ex)
        {
            throw new NotImplementedException();
        }
    }
}
