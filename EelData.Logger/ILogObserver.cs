using System;

namespace EelData.Logger
{
    public interface ILogObserver
    {
        void OnLog(string message);
        void OnLog(string message, Exception ex);
    }
}
