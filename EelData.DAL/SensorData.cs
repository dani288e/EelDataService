//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EelData.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class SensorData
    {
        public int SensorID { get; set; }
        public Nullable<byte> AmbientTemperature { get; set; }
        public Nullable<byte> WaterLevel { get; set; }
        public Nullable<byte> WindSpeed { get; set; }
        public Nullable<byte> WaterTemperature { get; set; }
        public int ID { get; set; }
    
        public virtual Sensor Sensor { get; set; }
    }
}
