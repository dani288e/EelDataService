﻿using System;
using System.Linq;
using EelData.Logger;

namespace EelData.DAL
{
    public class DALManager
    {
        public void SaveSilo(Model.Silo record, int bassinid)
        {
            using (EelDataEntities context = new EelDataEntities())
            {
                Silo query = (from o in context.Silos
                                where o.BassinID == record.BassinID
                                select o).FirstOrDefault();

                if (query == null)
                {
                    Silo siloE = new Silo();
                    siloE.BassinID = bassinid;
                    siloE.FoodAmount = record.FoodAmount;
                    context.Silos.Add(siloE);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        LoggerSingleton.Instance.Log("An exception occurred when attempting to save a new silo to the database", ex);
                    }
                }
            }
        }

        public void SaveBassin(Model.Bassin record, int hallid)
        {
            using (EelDataEntities context = new EelDataEntities())
            {
                Bassin query = (from o in context.Bassins
                                where o.HallID == record.HallID
                                select o).FirstOrDefault();

                if (query == null)
                {
                    Bassin bassinE = new Bassin();
                    bassinE.HallID = hallid;
                    context.Bassins.Add(bassinE);
                    context.SaveChanges();
                }
            }
        }

        public void SaveHall(Model.Hall record)
        {
            using (EelDataEntities context = new EelDataEntities())
            {
                Hall query = (from o in context.Halls
                                 where o.Name == record.Name
                                 select o).FirstOrDefault();

                //hall wasn't found in the database - create one
                if (query == null)
                {
                    Hall hallE = new Hall();
                    hallE.Name = record.Name;
                    context.Halls.Add(hallE);
                    context.SaveChanges();
                }
            }
        }

        public void SaveSensor(Model.Sensor record, int bassinid, string ip)
        {
            using (EelDataEntities context = new EelDataEntities())
            {
                Sensor query = (from o in context.Sensors
                                where o.BassinID == bassinid
                                select o).FirstOrDefault();

                if (query == null)
                {
                    Sensor sensorE = new Sensor();
                    sensorE.BassinID = bassinid; // modify this needs to obtain id from model
                    sensorE.IPAddress = ip; // modify this needs to obtain ip from model
                    context.Sensors.Add(sensorE);
                    context.SaveChanges();
                }
            }
        }

        public void SaveSensorData(Model.SensorData record, int sensorid)
        {
            using (EelDataEntities context = new EelDataEntities())
            {
                SensorData query = (from o in context.SensorDatas
                                    where o.SensorID == sensorid //record.SensorID
                                    select o).FirstOrDefault();

                if (query == null)
                {
                    SensorData sensorDataE = new SensorData();
                    sensorDataE.SensorID = sensorid;
                    sensorDataE.AmbientTemperature = null;
                    sensorDataE.WaterLevel = null;
                    sensorDataE.WindSpeed = null;
                    sensorDataE.WaterTemperature = null;
                    context.SensorDatas.Add(sensorDataE);
                    context.SaveChanges();
                }
            }
        }

        public void SaveTrigger(Model.Trigger record, int bassinid, int warningid)
        {
            using (EelDataEntities context = new EelDataEntities())
            {
                Trigger query = (from o in context.Triggers
                                 where o.BassinID == bassinid
                                 select o).FirstOrDefault();

                if (query != null)
                {
                    // fix this, this db insert exception
                    Trigger triggerE = new Trigger();
                    triggerE.DateTime = DateTime.Now;
                    triggerE.BassinID = bassinid;
                    triggerE.WarningID = warningid;
                    context.Triggers.Add(triggerE);
                    context.SaveChanges();
                }
                else
                {
                    // TODO - bassin id not found - create it
                    Trigger triggerE = new Trigger();
                    triggerE.DateTime = DateTime.Now;
                    triggerE.WarningID = warningid;
                    triggerE.BassinID = bassinid;
                    context.Triggers.Add(triggerE);
                    context.SaveChanges();
                }
            }
        }

        public void SaveWarning(Model.Warning record, int id, byte priority, string message)
        {
            using (EelDataEntities context = new EelDataEntities())
            {
                Warning query = (from o in context.Warnings
                                 where o.ID == id
                                 select o).FirstOrDefault();

                if (query == null)
                {
                    Warning warningE = new Warning();
                    warningE.Priority = priority;
                    warningE.Message = message;
                    context.Warnings.Add(warningE);
                    context.SaveChanges();
                }
            }
        }
    }
}