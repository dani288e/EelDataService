using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EelData.Logger
{
    public class LoggerSingleton
    {
        private static readonly Lazy<Logger> lazy;

        static LoggerSingleton()
        {
            lazy = new Lazy<Logger>(() => new Logger());
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
