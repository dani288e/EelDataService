﻿using System.Linq;

namespace EelData.DAL
{
    public class DALManager
    {
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
    }
}
