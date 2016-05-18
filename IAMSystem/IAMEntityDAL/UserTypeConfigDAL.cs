using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
   public class UserTypeConfigDAL:BaseQuery
    {
       BaseFind<UserTypeConfig> _DMLServerices = new BaseFind<UserTypeConfig>();
       public int AddUserTypeConfig(UserTypeConfig entity)
       {
           return _DMLServerices.Add(entity);
       }

       public int UpdateUserTypeConfig(UserTypeConfig entity)
       {
           return _DMLServerices.Update(entity);
       }

       public List<UserTypeConfig> UserTypeConfigList(int PageSize, int PageIndex, out int count)
       {
           var list = NonExecute<List<UserTypeConfig>>(db => {
               return db.UserTypeConfig.ToList();
           });
           count = list.Count;
           return list.OrderByDescending(item=>item.type).Skip((PageIndex-1)*PageSize).Take(PageSize).ToList();
       }
    }
}
