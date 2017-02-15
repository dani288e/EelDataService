namespace EelData.Model
{
    public class Hall
    {
        public string Name { get; set; }
        public Sensor Sensor { get; set; }
        public SensorData SensorData { get; set; }
        public Silo Silo { get; set; }

        public Hall()
        {
            Sensor = new Sensor();
            SensorData = new SensorData();
            Silo = new Silo();
        }
    }
}
