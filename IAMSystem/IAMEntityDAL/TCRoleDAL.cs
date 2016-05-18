using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class TCRoleDAL : BaseFind<TC_Role>
    {
        /// <summary>
        /// 添加Tc_Role信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddTCRole(TC_Role entity)
        {
            return Add(entity);
        }


        /// <summary>
        /// 更新Tc_Role 信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateTCRole(TC_Role entity)
        {
            using (IAMEntities db = new IAMEntities())
            {
                db.TC_Role.Attach(entity);
                db.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Modified);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 获取Tc_Role 信息
        /// </summary>
        /// <param name="Count">总数量</param>
        /// <returns></returns>
        public List<TC_Role> GetRoleList(out int Count)
        {
            List<TC_Role> list = NonExecute<List<TC_Role>>(db =>
            {
                return db.TC_Role.ToList();
            });
            Count = list.Count;
            return list;
        }

        /// <summary>
        /// 同步Tc 用户信息，根据用户所在组信息，获取对应的RoleId
        /// </summary>
        /// <param name="UserGroups"></param>
        /// <returns></returns>
        public List<string> GetRoleIdByRoleName(string UserGroups)
        {
            string[] RoleGroups = UserGroups.Split(';');
            List<string> roleIdlist = new List<string>();
            int count = 0;
            List<TC_Role> list = GetRoleList(out count);
            foreach (var item in RoleGroups)
            {
                string role = item.Replace("|",".");
                TC_Role entity = list.FirstOrDefault(it => it.RoleName == role);
                if (entity != null)
                    roleIdlist.Add(entity.RoleID);
            }
            return roleIdlist;
        }

        public void SyncRole(TC_Role entity)
        {
            try
            {
                TC_Role module = NonExecute<TC_Role>(db =>
                {
                    return db.TC_Role.FirstOrDefault(item => item.RoleID == entity.RoleID);

                });
                if (module != null)
                {
                    UpdateTCRole(entity);
                }
                else
                    AddTCRole(entity);
            }
            catch (Exception ex)
            {
#if DEBUG

                throw ex;
#else
                new LogDAL().AddsysErrorLog(ex.ToString());
              
#endif
            }

        }


    }
}
