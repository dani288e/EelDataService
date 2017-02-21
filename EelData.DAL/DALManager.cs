using System;
using System.Linq;
using EelData.Logger;

namespace EelData.DAL
{
    public class DALManager
    {
        public void SaveSilo(Model.Silo record, int bassinid)
        {
            using (Ringsted1Entities111 context = new Ringsted1Entities111())
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
                        LoggerSingleton.Instance.Log("An exception occurred when attempting to save a new silo to the database: ", ex);
                    }
                }
            }
        }

        public void SaveBassin(Model.Bassin record, int hallid)
        {
            using (Ringsted1Entities111 context = new Ringsted1Entities111())
            {
                Bassin query = (from o in context.Bassins
                                where o.HallID == record.HallID
                                select o).FirstOrDefault();

                if (query == null)
                {
                    Bassin bassinE = new Bassin();
                    bassinE.HallID = hallid;
                    context.Bassins.Add(bassinE);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        LoggerSingleton.Instance.Log("An exception occurred when attempting to save a new bassin to the database: ", ex);
                    }
                }
            }
        }

        public void SaveHall(Model.Hall record)
        {
            using (Ringsted1Entities111 context = new Ringsted1Entities111())
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
                    try
                    {
                        context.SaveChanges();

                    }
                    catch (Exception ex)
                    {
                        LoggerSingleton.Instance.Log("An exception occurred when attempting to save a new hall to the database: ", ex);
                    }
                }
            }
        }

        public void SaveSensor(Model.Sensor record)
        {
            using (Ringsted1Entities111 context = new Ringsted1Entities111())
            {
                Sensor query = (from o in context.Sensors
                                where o.BassinID == record.BassinID
                                select o).FirstOrDefault();

                if (query == null)
                {
                    Sensor sensorE = new Sensor();
                    sensorE.BassinID = record.BassinID; 
                    sensorE.IPAddress = record.IP; 
                    context.Sensors.Add(sensorE);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        LoggerSingleton.Instance.Log("An exception occurred when attempting to save a new sensor to the database: ", ex);
                    }
                }
            }
        }

        public void SaveSensorData(Model.SensorData record)
        {
            using (Ringsted1Entities111 context = new Ringsted1Entities111())
            {
                SensorData query = (from o in context.SensorDatas
                                    where o.SensorID == record.SensorID
                                    select o).FirstOrDefault();

                if (query != null)
                {
                    SensorData sensorDataE = new SensorData();
                    sensorDataE.SensorID = record.SensorID;
                    sensorDataE.AmbientTemperature = record.AmbientTemperature;
                    sensorDataE.WaterLevel = record.WaterLevel;
                    sensorDataE.WindSpeed = record.WindSpeed;
                    sensorDataE.WaterTemperature = record.WaterTemperature;
                    context.SensorDatas.Add(sensorDataE);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        LoggerSingleton.Instance.Log("An exception occurred when attempting to save sensordata to the database: ", ex);
                    }
                }
                else
                {
                    // test code - remember to remove this
                    SensorData sensorDataE = new SensorData();
                    sensorDataE.SensorID = record.SensorID;
                    sensorDataE.AmbientTemperature = record.AmbientTemperature;
                    sensorDataE.WaterLevel = record.WaterLevel;
                    sensorDataE.WindSpeed = record.WindSpeed;
                    sensorDataE.WaterTemperature = record.WaterTemperature;
                    context.SensorDatas.Add(sensorDataE);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        LoggerSingleton.Instance.Log("An exception occurred when attempting to save sensordata to the database: ", ex);
                    }
                }
            }
        }

        public void SaveTrigger(Model.Trigger record)
        {
            using (Ringsted1Entities111 context = new Ringsted1Entities111())
            {
                Trigger query = (from o in context.Triggers
                                 where o.BassinID == record.BassinID
                                 select o).FirstOrDefault();

                if (query != null)
                {
                    // fix this, this db insert exception
                    Trigger triggerE = new Trigger();
                    triggerE.DateTime = DateTime.Now;
                    triggerE.BassinID = record.BassinID;
                    triggerE.WarningID = record.WarningID;
                    context.Triggers.Add(triggerE);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        LoggerSingleton.Instance.Log("An exception occurred when attempting to save a new trigger to the database: ", ex);
                    }
                }
                //else
                //{
                //    // TODO - test this
                //    Trigger triggerE = new Trigger();
                //    triggerE.DateTime = DateTime.Now;
                //    triggerE.WarningID = record.WarningID;
                //    triggerE.BassinID = record.BassinID;
                //    context.Triggers.Add(triggerE);
                //    try
                //    {
                //        context.SaveChanges();
                //    }
                //    catch (Exception ex)
                //    {
                //        LoggerSingleton.Instance.Log("An exception occurred when attempting to save a trigger to the database: ", ex);
                //    }
                //}
            }
        }

        public void SaveWarning(Model.Warning record)
        {
            using (Ringsted1Entities111 context = new Ringsted1Entities111())
            {
                Warning query = (from o in context.Warnings
                                 where o.ID == 1
                                 select o).FirstOrDefault();

                if (query == null)
                {
                    Warning warningE = new Warning();
                    warningE.Priority = record.Priority;
                    warningE.Message = record.Message;
                    context.Warnings.Add(warningE);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        LoggerSingleton.Instance.Log("An exception occurred when attempting to save a new warning to the database: ", ex);
                    }
                }
            }
        }

        //public void SaveWarning(Model.Warning record, int id, byte priority, string message)
        //{
        //    using (Ringsted1Entities11 context = new Ringsted1Entities11())
        //    {
        //        Warning query = (from o in context.Warnings
        //                         where o.ID == id
        //                         select o).FirstOrDefault();

        //        if (query == null)
        //        {
        //            Warning warningE = new Warning();
        //            warningE.Priority = priority;
        //            warningE.Message = message;
        //            context.Warnings.Add(warningE);
        //            context.SaveChanges();
        //        }
        //    }
        //}
    }
}