using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class UserRoleDAL : BaseQuery
    {
        public int AddUserRole(UserRole entity)
        {
            return NonExecute(db =>
            {
                db.AddObject("UserRole", entity);
            });
        }

        public int UpdateUserRole(UserRole entity)
        {
            return NonExecute(db =>
            {
                var key = db.CreateEntityKey("UserRole", entity);
                Object Emptyobject = null;
                if (db.TryGetObjectByKey(key, out Emptyobject))
                {
                    
                    db.ApplyCurrentValues(key.EntitySetName, entity);
                    db.SaveChanges();
                }
            });
        }

        public int DeleteUserRole(string id)
        {
            return NonExecute(db => {
                var ojb = db.UserRole.FirstOrDefault(item=>item.adname==id);
                if (ojb != null)
                {
                    db.DeleteObject(ojb);
                    db.SaveChanges();
                }
            });
        }

        public bool IsExets(string name)
        {
            var en = NonExecute<UserRole>(db => {
                return db.UserRole.FirstOrDefault(x=>x.adname==name);
            });
            if (en == null)
                return false;
            else
                return true;
        }

        public List<UserRole> ReturnUserRoleList(int pagesize, int pageIndex, string adname, string roles, out int count)
        {
            List<UserRole> list = NonExecute<List<UserRole>>(db =>
            {
                return db.UserRole.ToList();
            });

            if (!string.IsNullOrEmpty(adname))
                list = list.Where(item => item.adname == adname).ToList();
            if (!string.IsNullOrEmpty(roles))
                list = list.Where(item => item.roles.Contains(roles)).ToList();
            count = list.Count;
            list = list.OrderBy(item => item.adname).Skip((pageIndex - 1) * pagesize).Take(pagesize).ToList();
            return list;
        }

        /// <summary>
        /// 根据ad域名账号，获取当前用户权限
        /// </summary>
        /// <param name="adname">ad账号</param>
        /// <returns></returns>
        public UserRole GetUserRole(string adname)
        {
            UserRole role = null;
            if (!string.IsNullOrEmpty(adname))
            {
                role = NonExecute<UserRole>(db =>
                {
                    return db.UserRole.FirstOrDefault(item => item.adname == adname);
                });
            }
            return role;
        }

    }
}
