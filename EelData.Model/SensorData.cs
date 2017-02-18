namespace EelData.Model
{
    public class SensorData
    {
        public int SensorID { get; set; }
        public byte AmbientTemperature { get; set; }
        public byte WaterTemperature { get; set; }
        public byte WaterLevel { get; set; }
        public byte WindSpeed { get; set; }
    }
}
