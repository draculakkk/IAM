using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
   public  class HRDepartmentDAL:BaseQuery
    {
       public int ADDHrDepartment(HRDepartment department)
       {
           return NonExecute(db => {
               db.AddObject("HRDepartment", department);
           });
       }


       /// <summary>
       /// 批量更新部门同步类型
       /// </summary>
       /// <param name="department"></param>
       /// <returns></returns>
       public int UpdateHrDepartMent(HRDepartment department)
       {
           return NonExecute(db => {
               var key = db.CreateEntityKey("HRDepartment",department);
               object _obj = null;
               if (db.TryGetObjectByKey(key, out _obj))
               {
                   db.ApplyCurrentValues("HRDepartment",department);
                   db.SaveChanges();
               }
           });
       }

       /// <summary>
       /// 部门信息，同步任务
       /// </summary>
       /// <param name="module"></param>
       public void SyncHrDepartMent(HRDepartment module)
       {
           HRDepartment tmpEntity = NonExecute<HRDepartment>(db=>db.HRDepartment.FirstOrDefault(i=>i.dept==module.dept));
           if (tmpEntity != null)
               UpdateHrDepartMent(module);
           else
               ADDHrDepartment(module);
       }


       public void UpdateHrDepartMent()
       {
           List<HRDepartment> listdepartment = NonExecute<List<HRDepartment>>(db => {
               return db.HRDepartment.ToList();
           });

           foreach (var tmp in listdepartment)
           {
               tmp.isSync = false;
               UpdateHrDepartMent(tmp);
           }
       }

       public List<HRDepartment> DepartMentList(int PageSize, int PageIndex, out int count)
       {
           var list = NonExecute<List<HRDepartment>>(db => {
               return db.HRDepartment.ToList();
           });

           count = list.Count;
           return list.OrderByDescending(item => item.syncDate).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
       }


       /// <summary>
       /// 以同步时间倒叙排序， HrDepartment 视图
       /// </summary>
       /// <param name="PageSize"></param>
       /// <param name="PageIndex"></param>
       /// <param name="count"></param>
       /// <returns></returns>
       public List<V_HrDepartment> VDepartmentList(int PageSize, int PageIndex, out int count)
       {
           var list = NonExecute<List<V_HrDepartment>>(db=>db.V_HrDepartment.ToList());
           count = list.Count;

           return list.OrderByDescending(item => item.syncDate).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
       }
       
    }
}
