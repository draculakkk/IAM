using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class HRsm_user_roleDAL : BaseFind<HRsm_user_role>
    {

        BaseFind<HRsm_user_role> _DMLServices = new BaseFind<HRsm_user_role>();
        /// <summary>
        /// 添加Hrsm_user_role信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddHRsmUserRole(HRsm_user_role entity)
        {
            return _DMLServices.Add(entity);
        }

        /// <summary>
        /// 更新Hrsm_user_role信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateHRsmUserRole(HRsm_user_role entity)
        {
            using (IAMEntities db = new IAMEntities())
            {
                db.HRsm_user_role.Attach(entity);
                db.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Modified);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 删除用户角色
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int DeleteHRsmUserRole(string id)
        {
            using (IAMEntities db = new IAMEntities())
            {
                var one = db.HRsm_user_role.FirstOrDefault(item=>item.Pk_user_role==id);
                if (one != null)
                {
                    db.DeleteObject(one);
                }
                return db.SaveChanges();
            }
        }

      
        /// <summary>
        /// 添加用户权限
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int AddHRsm_user_roleList(List<V_HRSm_User_Role_new> list)
        {
            try
            {
                List<V_HRSm_User_Role_new> listD = list.Where(item=>item.urDr==1).ToList();
                list = list.Where(item=>item.urDr!=1).ToList();
                
                foreach (var item in list)
                {
                    HRsm_user_role module = NonExecute<HRsm_user_role>(db =>
                    {
                        return db.HRsm_user_role.FirstOrDefault(a => a.Pk_user_role == item.urPk_user_role);
                    });
                    if (module != null)
                    {
                        //UpdateHRsmUserRole(GetModule(module, item));
                    }
                    else
                    {
                        module = new HRsm_user_role();
                        AddHRsmUserRole(GetModule(module, item));
                    }
                }

                if (listD != null)
                {
                    foreach (var item in listD)
                    {
                        HRsm_user_role module = NonExecute<HRsm_user_role>(db =>
                        {
                            return db.HRsm_user_role.FirstOrDefault(a => a.Pk_user_role == item.urPk_user_role);
                        });
                        if (module != null)
                        {
                            module.Dr = 1;
                            UpdateHRsmUserRole(module);//删除用户权限
                        }
                    }
                }

                return 1;
            }
            catch (Exception ex)
            { 
#if DEBUG
                throw ex;
#else
                new LogDAL().AddsysErrorLog(ex.ToString());
                return 0;
#endif
            }

        }

        HRsm_user_role GetModule(HRsm_user_role module, V_HRSm_User_Role_new roleModule)
        {
            module.Pk_user_role = roleModule.urPk_user_role;
            module.Pk_role = roleModule.rPk_role;
            module.Pk_corp = roleModule.cPk_corp;
            module.Cuserid = roleModule.urcuserid;
            module.Dr = 0; 
            module.isSync = false;
            return module;
        }

        /// <summary>
        /// 批量更新 HrsmUserRole表，是否同步设置为false
        /// </summary>
        public void UpdateHRsmUserRole()
        {
            List<HRsm_user_role> list = NonExecute<List<HRsm_user_role>>(db => db.HRsm_user_role.ToList());
            foreach (var tmp in list)
            {
                tmp.isSync = false;
                UpdateHRsmUserRole(tmp);
            }
        }

        /// <summary>
        /// HrsmUserRole 同步任务
        /// 2014-11-12 haiboax 修改同步时，用户角色关系，当账号拥有的角色在iam中不存在，不直接添加数据库
        /// </summary>
        /// <param name="entity"></param>
        public void SyncHRsmUserRole(HRsm_user_role entity, List<BaseDataAccess.EHREntities.view_sm_user> listsmuser)
        {
            HRsm_user_role module = NonExecute<HRsm_user_role>(db => db.HRsm_user_role.FirstOrDefault(item => item.Pk_role == entity.Pk_role&&item.Cuserid==entity.Cuserid&&item.Pk_corp==entity.Pk_corp));//根据角色id，操作员id,公司id 进行查询
            var userinfo = NonExecute<HRSm_user>(db => {
                return db.HRSm_user.FirstOrDefault(item=>item.Cuserid==entity.Cuserid);
            });

            var roleinfo = NonExecute<HRsm_role>(db => {
                return db.HRsm_role.FirstOrDefault(itme=>itme.Pk_role==entity.Pk_role);
            });
            DeferencesSlution.UserRoleAlignmentValue UserRoleDef = new DeferencesSlution.UserRoleAlignmentValue();

           

            if (module != null)
            {
                if (module.Pk_corp!=module.Pk_corp)
                {
                    
                    //AddDeference(module, entity, Unitity.SystemType.HR, "Pk_user_role",module.Pk_user_role, userinfo.User_code);
                    //module.Pk_corp = entity.Pk_corp;
                    //UpdateHRsmUserRole(module);
                }
                else
                {
                    module.Pk_corp = entity.Pk_corp;
                    UpdateHRsmUserRole(module);
                }
            }
            else
            {
                if (base.IsFirstTime)
                {
                    AddHRsmUserRole(entity);
                }
                else
                {
                    //if (userinfo == null)
                    //{
                        
                        var u = listsmuser.FirstOrDefault(item => item.Cuserid == entity.Cuserid);
                        if (u != null)
                        {
                            if (new Sys_UserName_ConflictResolutionDAL().GetOne(u.User_code, "源系统中新增", "IAM中无该角色", "HR", roleinfo.role_name + "/" + entity.Pk_corp) == null)
                            {
                                UserRoleDef.IsAddNewNotInIAM<HRsm_user_role>(entity, u.User_code, Unitity.SystemType.HR, "HRsm_user_role", roleinfo.role_name+"/"+entity.Pk_corp, "IAM中无该角色", "源系统中新增");
                            }
                        }
                        return;
                    //}
                    
                }
            }
        }

        public List<HRsm_user_role> HrsmUserRole(int PageSize, int PageIndex, out int count)
        {
            var list = NonExecute<List<HRsm_user_role>>(db =>
            {
                return db.HRsm_user_role.ToList();
            });
            count = list.Count;
            return list.OrderByDescending(item => item.Cuserid).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }

        public List<HRsm_user_role> HrsmUserRole()
        {
            var list = NonExecute<List<HRsm_user_role>>(db =>
            {
                return db.HRsm_user_role.ToList();
            });
            return list;
        }

        /// <summary>
        /// 获取用户角色关系 视图
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<V_HRSm_User_Role> VHRSmUserRole(int pageSize, int PageIndex, string companykey, string username, out int count)
        {
            List<V_HRSm_User_Role> list = NonExecute<List<V_HRSm_User_Role>>(db =>
            {
                return db.V_HRSm_User_Role.ToList();
            });
            if (!string.IsNullOrEmpty(companykey))
                list = list.Where(item => item.CompanyKey == companykey).ToList();
            if (!string.IsNullOrEmpty(username))
                list = list.Where(item => item.UserName == username).ToList();
            count = list.Count;
            return list.OrderBy(item => item.UserName).Skip((PageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

    }
}
