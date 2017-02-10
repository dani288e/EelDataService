using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
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
            Console.WriteLine("Standard debug::Logger: " + message);
        }
    }
}
