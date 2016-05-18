using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
   public class TaskEmailDAL:BaseFind<TaskEmail>
    {
       public int AddTaskEmail(TaskEmail module)
       {
           return Add(module);
       }

       public int UpdateTaskEmail(TaskEmail module)
       {
           using (IAMEntities db = new IAMEntities())
           {
               db.TaskEmail.Attach(module);
               db.ObjectStateManager.ChangeObjectState(module,System.Data.EntityState.Modified);
               return db.SaveChanges();
           }
       }

       public int DeleteTaskEmail(TaskEmail module)
       {
           using (IAMEntities db = new IAMEntities())
           {
               db.TaskEmail.Detach(module);
               db.ObjectStateManager.ChangeObjectState(module, System.Data.EntityState.Deleted);
               return db.SaveChanges();
           }
       }

       public TaskEmail GetOne(string systemname)
       {
           return NonExecute<TaskEmail>(db => {
               return db.TaskEmail.FirstOrDefault(item=>item.SystemName.Trim()==systemname);
           });
       }

       public List<TaskEmail> GetList()
       {
           return NonExecute<List<TaskEmail>>(db => {
               return db.TaskEmail.ToList();
           });
       }
    }
}
