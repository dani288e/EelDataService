﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Ringsted1Entities111 : DbContext
    {
        public Ringsted1Entities111()
            : base("name=Ringsted1Entities111")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bassin> Bassins { get; set; }
        public virtual DbSet<Hall> Halls { get; set; }
        public virtual DbSet<Sensor> Sensors { get; set; }
        public virtual DbSet<SensorData> SensorDatas { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Warning> Warnings { get; set; }
        public virtual DbSet<Silo> Silos { get; set; }
        public virtual DbSet<Trigger> Triggers { get; set; }
    }
}
