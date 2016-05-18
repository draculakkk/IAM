using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class TC_UserRoleDAL : BaseFind<TC_UserRole>
    {

        public enum UserType
        {
            客户 = 0,
            作者 = 1,
            其他 = 2
        }

        public enum UserStutus
        {
            启用 = 1,
            不启用 = 0
        }

        /// <summary>
        /// 添加TC_UserRole 信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddTcUserRole(TC_UserRole entity)
        {
            return Add(entity);
        }

        /// <summary>
        /// 更新TC_UserRole信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateTcUserRole(TC_UserRole entity)
        {
            using (IAMEntities db = new IAMEntities())
            {
                db.TC_UserRole.Attach(entity);
                db.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Modified);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 添加用户权限信息
        /// </summary>
        /// <param name="list">用户所拥有的权限</param>
        public void CreateOrUpdate(List<V_TCReport> list)
        {

            TC_UserGroupSettingService moduleService = new TC_UserGroupSettingService();
            List<TC_UserGroupSetting> moduleLst = moduleService.GetLst();
            List<V_TCReport> newlist = list.Where(item => item.isdr == 2).ToList();
            List<V_TCReport> oldlist = list.Where(item => item.isdr == 1).ToList();
            List<V_TCReport> normallist = list.Where(item=>item.isdr==0).ToList();

            foreach (var item in newlist)
            {
                TC_UserGroupSetting module = new TC_UserGroupSetting()
                {
                  ID=item.urid, isdr=0, p1=item.urp1,p2=item.urp2, UserID=item.uUserID, Memo=item.urMemo, GroupAdmin=0, GroupDefaultRole=0, GroupOut=0, GroupStatus=item.urGroupStatus
                };
                moduleService.Add(module);
            }

            StringBuilder stb = new StringBuilder();
            foreach (var item in oldlist)
            {
                stb.Append(" update TC_UserGroupSetting set isdr=1 where id='"+item.urid+"'");
            }
            foreach (var x in normallist)
            {
                stb.Append(" update TC_UserGroupSetting set GroupStatus="+x.urGroupStatus+" where id='" + x.urid + "'");
                using (IAMEntities db = new IAMEntities())
                {
                    db.ExecuteStoreCommand(stb.ToString());
                    db.SaveChanges();
                }
            }
        }


        /// <summary>
        /// 获取TC_UserRole信息
        /// </summary>
        /// <param name="count">集合总数</param>
        /// <returns></returns>
        public List<TC_UserRole> GetTcUserRole(out int count)
        {
            List<TC_UserRole> list = NonExecute<List<TC_UserRole>>(db =>
            {
                return db.TC_UserRole.ToList();
            });
            count = list.Count;
            return list;
        }

        /// <summary>
        /// 同步UserRole信息
        /// </summary>
        /// <param name="entity"></param>
        public void SyncUserRole(TC_UserRole entity)
        {
            try
            {
                TC_UserRole module = NonExecute<TC_UserRole>(db =>
                {
                    return db.TC_UserRole.FirstOrDefault(imte => imte.UserID == entity.UserID && imte.RoleID == entity.RoleID);
                });
                if (module != null)
                    UpdateTcUserRole(entity);
                else
                    AddTcUserRole(entity);
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
