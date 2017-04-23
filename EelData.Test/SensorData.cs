using System;

namespace EelData.Test
{
    internal class SensorData
    {
        private double _ambientTemperature;

        public double AmbientTemperature
        {
            get { return _ambientTemperature; }
            set { _ambientTemperature = value; }
        }

    }
}