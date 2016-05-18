using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
   public class AD_Zhiji_WorkGroupDAL:BaseFind<AD_Zhiji_WorkGroup>
    {
       public int AddAd_zhiji_WorkGroup(AD_Zhiji_WorkGroup entity)
       {
           return Add(entity);
       }

       public int UpdateAd_zhiji_WorkGroup(AD_Zhiji_WorkGroup entity)
       {
           using (IAMEntities db = new IAMEntities())
           {
               db.AD_Zhiji_WorkGroup.Attach(entity);
               db.ObjectStateManager.ChangeObjectState(entity,System.Data.EntityState.Modified);
              return db.SaveChanges();
           }
       }

       public int DeleteAd_zhiji_WorkGroup(string zhiji)
       {
           using (IAMEntities db = new IAMEntities())
           {
               var mo = db.AD_Zhiji_WorkGroup.FirstOrDefault(item=>item.Zhiji==zhiji);
               db.AD_Zhiji_WorkGroup.Detach(mo);
               db.ObjectStateManager.ChangeObjectState(mo,System.Data.EntityState.Deleted);
               return db.SaveChanges();
           }
       }

       public List<AD_Zhiji_WorkGroup> GetList()
       {
           return NonExecute<List<AD_Zhiji_WorkGroup>>(db => {
               return db.AD_Zhiji_WorkGroup.ToList();
           });
       }


    }
}
