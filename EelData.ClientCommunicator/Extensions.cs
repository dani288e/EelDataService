using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EelData.ClientCommunicator
{
    public static class Extensions
    {
        public static bool StartsWith(this string str, string[] equals)
        {
            foreach (var s in equals)
            {
                if (str.StartsWith(s))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
