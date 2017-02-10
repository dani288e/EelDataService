using System.Net;

namespace EelData.Model
{
    public class SensorData
    {
        public IPAddress IP { get; set; }
        public string AmbientTemperature { get; set; }
        public string WaterTemperature { get; set; }
        public string FoodStock { get; set; }
        public string WaterLevel { get; set; }
        public string WindSpeed { get; set; }
        public string Message { get; set; }
        public int Priority { get; set; }
    }
}
