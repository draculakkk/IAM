using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
   public class hz_user_role_operationDAL:BaseQuery
    {
       BaseFind<hz_user_role_operation> _DMLServices = new BaseFind<hz_user_role_operation>();
       public int AddHzUserRoleOperation(hz_user_role_operation entity)
       {
           return _DMLServices.Add(entity);
       }

       public int UpdateHzUserRoleOperation(hz_user_role_operation entity)
       {
           return _DMLServices.Update(entity);
       }

       public List<hz_user_role_operation> HzUserRoleOperationList(int PageSize, int PageIndex, out int count)
       {
           var list = NonExecute<List<hz_user_role_operation>>(db => {
               return db.hz_user_role_operation.ToList();
           });
           count = list.Count;
           return list.OrderByDescending(item => item.Cuserid).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
       }
    }
}
