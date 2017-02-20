using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EelData.Logger;

namespace EelData.Test
{
    [TestClass]
    public class LoggerTests
    {
        [TestMethod]
        public void WriteToLogTest()
        {
            LoggerSingleton.Instance.Log("Test log entry message");

            NotSupportedException ex = new NotSupportedException();
            LoggerSingleton.Instance.Log("Test log entry message with exception: ", ex);
        }
    }
}
