using System;
using System.IO;

namespace EelData.Logger
{
    public class LoggerSingleton
    {
        private static readonly System.Lazy<Logger> lazy;

        static LoggerSingleton()
        {
            lazy = new System.Lazy<Logger>(() => new Logger());
        }

        public static Logger Instance
        {
            get
            {
                return lazy.Value;
            }
        }
    }
}
