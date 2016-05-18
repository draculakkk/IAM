using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
   public class HRCompanyDAL:BaseFind<HRCompany>
    {
       public int AddHRCompany(HRCompany entity)
       {
           using (var context = new IAMEntities())
           {
               var tmp = context.HRCompany.FirstOrDefault(x=>x.Pk_CORP==entity.Pk_CORP);
               if (tmp == null)
                   return Add(entity);
               else
                   return 0;
           }
           
       }

       public int UpdateHRCompany(HRCompany entity)
       {
           using (IAMEntities db = new IAMEntities())
           {
               db.HRCompany.Attach(entity);
               db.ObjectStateManager.ChangeObjectState(entity,System.Data.EntityState.Modified);
               return db.SaveChanges();
           }
       }

       public List<HRCompany> GetHrCompany(out int Count)
       {
           List<HRCompany> list = NonExecute<List<HRCompany>>(db => {
               return db.HRCompany.ToList();
           });
           Count = list.Count;
           return list;
       }


    }
}
