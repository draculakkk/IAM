using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
   public class KeyValueDAL:BaseFind<KeyValue>
    {
       public int NewAdd(KeyValue entity)
       {
           var tmp = NonExecute<KeyValue>(db => {
               return db.KeyValue.FirstOrDefault(x=>x.KEY==entity.KEY);
           });
           if (tmp != null)
               throw new Exception(string.Format("添加名称{0},已存在，请核实", entity.KEY));
           else
               return Add(entity);
       }

       public int NewUpdate(KeyValue entity)
       {
           using (IAMEntities db = new IAMEntities())
           {
               db.KeyValue.Attach(entity);
               db.ObjectStateManager.ChangeObjectState(entity,System.Data.EntityState.Modified);
               return db.SaveChanges();
           }
       }
       public int NewUpdate(Guid id,string value)
       {
           using (IAMEntities db = new IAMEntities())
           {
               var tmp = db.KeyValue.FirstOrDefault(x=>x.ID==id);
               tmp.VALUE = value;              
               db.ObjectStateManager.ChangeObjectState(tmp, System.Data.EntityState.Modified);
               return db.SaveChanges();
           }
       }

       public KeyValue GetOne(Guid id)
       {
           return NonExecute<KeyValue>(db => {
               return db.KeyValue.FirstOrDefault(x=>x.ID==id);
           });
       }

       public KeyValue GetOne(string key)
       {
           return NonExecute<KeyValue>(db => {
               return db.KeyValue.FirstOrDefault(x=>x.KEY==key);
           });
       }

       public List<KeyValue> GetList()
       {
           return NonExecute<List<KeyValue>>(db => {
               return db.KeyValue.ToList();
           });
       }
    }
}
