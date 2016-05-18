using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
   public class SAPRoleDAL:BaseFind<SAP_Role>
    {
       public int AddSapRole(SAP_Role entity)
       {
           var tmp = NonExecute<SAP_Role>(db => {
               return db.SAP_Role.FirstOrDefault(item=>item.ROLEID==entity.ROLEID);
           });
           if (tmp == null)
               return Add(entity);
           else
               return 1;
       }

       public int UpdateSapRole(SAP_Role entity)
       {
           return Update(entity);
       }

       public int DeleteSapRole(string id)
       {
           return NonExecute(db => {
               var module = db.SAP_Role.FirstOrDefault(item => item.ROLEID == id);
               if (module != null)
                   db.DeleteObject(module);
           });
       }



       public List<SAP_Role> GetSapRole(int PageSize, int PageIndex, out int count)
       {
           List<SAP_Role> listrole = NonExecute<List<SAP_Role>>(db=>db.SAP_Role.ToList());
           count = listrole.Count;
           return listrole.OrderByDescending(item => item.STARTDATE).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
       }
    }
}
