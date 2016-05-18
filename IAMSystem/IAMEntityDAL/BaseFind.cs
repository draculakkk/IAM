using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAMEntityDAL
{
   public class BaseFind<T>:BaseQuery where T:class,new()
    {

       public bool IsFirstTime = System.Web.Configuration.WebConfigurationManager.AppSettings["isFirstSync"] == "1" ? true : false;

       public void AddDeference(T classa, T cassb, Unitity.SystemType systemname,string usercollname,string uservalue,string UserName)
       {
           new DeferencesSlution.Alignment_Value_Fun().AddConflic(classa,cassb,systemname,usercollname,uservalue,UserName);
       }

       public int Add(T entity)
       {
           return NonExecute(db => {
               if (entity != null)
               {
                   db.AddObject(entity.GetType().Name,entity);
               }
           });
       }


       public int Update(T entity)
       {
           return NonExecute(db => {
               var key = db.CreateEntityKey(entity.GetType().Name,entity);
               object _obj = null;
               if (db.TryGetObjectByKey(key, out _obj))
               {
                   db.ApplyCurrentValues(entity.GetType().Name,entity);
                   db.SaveChanges();
               }
           });
       }

       public int Delete(T entity)
       {
           return NonExecute(db => {
               var key = db.CreateEntityKey(entity.GetType().Name,entity);
               object _obj = null;
               if (db.TryGetObjectByKey(key, out _obj))
               {
                   db.DeleteObject(entity);
                   db.SaveChanges();
               }
           });
       }


    }
}
