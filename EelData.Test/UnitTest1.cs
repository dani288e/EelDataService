using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EelData.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AmbientTemperatureTest()
        {
            SensorData sensorData = new SensorData();
            sensorData.AmbientTemperature = 20;
            Assert.AreEqual(sensorData.AmbientTemperature, 20);
        }
    }
}
