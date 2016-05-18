using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
   public class SyncConfigDAL:BaseQuery
    {
       public SyncConfig ReturnSyncConfigEntity(string asyncName)
       {
           return NonExecute(db => {
               return db.SyncConfig.FirstOrDefault(item=>item.asyncName==asyncName);
           });
       }

       public List<SyncConfig> ReturnSyncConfigList()
       {
           return NonExecute<List<SyncConfig>>(db => {
               return db.SyncConfig.ToList();
           });
       }

       public int UpdateSyncConfig(SyncConfig entity)
       {
           return NonExecute(db => {
               var key = db.CreateEntityKey("SyncConfig",entity);
               object obj = null;
               if (db.TryGetObjectByKey(key, out obj))
               {
                   db.ApplyCurrentValues(key.EntitySetName,entity);
                   db.SaveChanges();
               }
           });
       }
    }
}
