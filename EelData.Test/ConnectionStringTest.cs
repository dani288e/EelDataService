using EelData.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EelData.Test
{
    [TestClass]
    public class ConnectionStringTest
    {
        [TestMethod]
        public void GetConnectionString()
        {
            using (Ringsted1Entities111 context = new Ringsted1Entities111())
            {
                Debug.WriteLine(context.Database.Connection.ConnectionString);
            }
        }
    }
}
