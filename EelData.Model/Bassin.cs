namespace EelData.Model
{
    public class Bassin
    {
        public int ID { get; set; }
        public Sensor BassinSensor { get; set; }
        public Silo BassinSilo { get; set; }
        public Trigger BassinTrigger { get; set; }
    }
}
