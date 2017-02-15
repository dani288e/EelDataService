using System;
using System.Linq;

namespace EelData.DAL
{
    public class DALManager
    {
        public void SaveBassin(Model.Bassin record)
        {
            using (EelDataEntities context = new EelDataEntities())
            {
                Bassin query = (from o in context.Bassins
                                where o.HallID == record.ID
                                select o).FirstOrDefault();

                if (query == null)
                {
                    Bassin bassinE = new Bassin();
                    //bassinE.
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

        public void SaveSensorData()
        {
            throw new NotImplementedException();
        }

        public void SaveSensor()
        {
            throw new NotImplementedException();
        }

        public void SaveTrigger()
        {
            throw new NotImplementedException();
        }

        public void SaveWarning()
        {
            throw new NotImplementedException();
        }
    }
}
