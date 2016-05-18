using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class SAPUserRoleDAL : BaseFind<SAP_User_Role>
    {
        public int AddSapUserRole(SAP_User_Role entity)
        {
            return Add(entity);
        }

        public int UpdateUserRole(SAP_User_Role entity)
        {
            using (IAMEntities db = new IAMEntities())
            {
                db.SAP_User_Role.AddObject(entity);
                db.ObjectStateManager.ChangeObjectState(entity,System.Data.EntityState.Modified);
                return db.SaveChanges();
            }
        }

        public int DeleteUserRole(Guid id)
        {
            return NonExecute(db =>
            {
                var mo = db.SAP_User_Role.FirstOrDefault(item => item.ID == id);
                if (mo != null)
                    db.DeleteObject(mo);
            });
        }

        public void CreateOrUpdate(List<SAP_User_Role> list, string username)
        {
            foreach (SAP_User_Role item in list)
            {
                SAP_User_Role module = NonExecute<SAP_User_Role>(db =>
                {
                    return db.SAP_User_Role.FirstOrDefault(f => f.ROLEID == item.ROLEID && f.ROLENAME == item.ROLENAME && f.BAPIBNAME == username);
                });
                if (module != null)
                {
                    module.ROLEID = item.ROLEID;
                    module.ROLENAME = item.ROLENAME;
                    module.START_DATE = item.START_DATE;
                    module.END_DATE = item.END_DATE;
                    module.BAPIBNAME = username;
                    if (module.isdr==2)
                    {
                        module.isdr = 0;
                        UpdateUserRole(module);
                    }
                    else if (module.isdr==1)
                    {
                        DeleteUserRole(module.ID);
                    }
                    else
                    {

                        UpdateUserRole(module);
                    }
                }
                else
                {
                    module = new SAP_User_Role();
                    module.ROLEID = item.ROLEID;
                    module.ROLENAME = item.ROLENAME;
                    module.START_DATE = item.START_DATE;
                    module.END_DATE = item.END_DATE;
                    module.BAPIBNAME = username;
                    module.ID = Guid.NewGuid();
                    AddSapUserRole(module);
                }
            }
        }

        /// <summary>
        /// 添加或更新 Sap_User_Role 信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int CreateOrUpdate(List<SAP_User_Role> list)
        {
            try
            {
                List<SAP_User_Role> listold = NonExecute<List<SAP_User_Role>>(db => {
                    return db.SAP_User_Role.ToList();
                });
                DeferencesSlution.UserRoleAlignmentValue userroles = new DeferencesSlution.UserRoleAlignmentValue();
                foreach (var item in list)
                {
                    var module = listold.FirstOrDefault(a => a.BAPIBNAME.Trim() == item.BAPIBNAME.Trim() && a.ROLEID.Trim() == item.ROLEID.Trim());

                    if (module == null&&base.IsFirstTime)
                    {
                        item.ID = Guid.NewGuid();
                        AddSapUserRole(item);
                    }
                    else
                    {
                        if (module == null)
                        {
                            if (new Sys_UserName_ConflictResolutionDAL().GetOne(item.BAPIBNAME, "源系统新增角色", "无该角色", "SAP", item.ROLENAME) == null)
                            {
                                userroles.IsAddNewNotInIAM<SAP_User_Role>(item, item.BAPIBNAME, Unitity.SystemType.SAP, "SAP_User_Role", item.ROLENAME, "无该角色", "源系统新增角色");
                            }
                        }
                        else if (module != null)
                        {
                            if (string.IsNullOrEmpty(module.START_DATE))
                            {
                                module.START_DATE = "";
                            }
                            if (string.IsNullOrEmpty(item.START_DATE))
                            {
                                item.START_DATE = "";
                            }
                            if (string.IsNullOrEmpty(module.END_DATE))
                            {
                                module.END_DATE = "";
                            }
                            if (string.IsNullOrEmpty(item.END_DATE))
                            {
                                item.END_DATE = "";
                            }

                            //dhzhang add begin
                            if (!module.START_DATE.Trim().Equals(item.START_DATE.Trim()))
                            {
                                if (new Sys_UserName_ConflictResolutionDAL().GetOne(item.BAPIBNAME, item.START_DATE, module.START_DATE, "SAP", "SAP角色开始时间/" + item.ROLENAME) == null)
                                {
                                    userroles.AddRoleChayi(item.BAPIBNAME, Unitity.SystemType.SAP, "SAP_User_Role", "SAP角色开始时间/"+item.ROLENAME, string.IsNullOrEmpty(module.START_DATE) ? "" : module.START_DATE, string.IsNullOrEmpty(item.START_DATE) ? "" : item.START_DATE,
                                        string.Format("UPDATE SAP_User_Role SET START_DATE='{0}' WHERE BAPIBNAME='{1}' AND ROLENAME='{2}'", item.START_DATE, item.BAPIBNAME, item.ROLENAME));
                                }
                            }
                            if (!module.END_DATE.Trim().Equals(item.END_DATE.Trim()))
                            {
                                if (new Sys_UserName_ConflictResolutionDAL().GetOne(item.BAPIBNAME, item.END_DATE, module.END_DATE, "SAP", "SAP角色过期时间/" + item.ROLENAME) == null)
                                {
                                    userroles.AddRoleChayi(item.BAPIBNAME, Unitity.SystemType.SAP, "SAP_User_Role", "SAP角色过期时间/"+item.ROLENAME, module.END_DATE, item.END_DATE,
                                        string.Format("UPDATE SAP_User_Role SET START_DATE='{0}' WHERE BAPIBNAME='{1}' AND ROLENAME='{2}'", item.END_DATE, item.BAPIBNAME, item.ROLENAME));
                                }
                            }
                            //dhzhang add end

                        }
                    }
                }

                //查找iam系统角色在同步接口是否存在
                foreach (var item in listold)
                {
                    var entity = list.FirstOrDefault(i=>i.BAPIBNAME.Trim()==item.BAPIBNAME.Trim()&&i.ROLEID.Trim()==item.ROLEID.Trim());
                    if (entity == null)
                    {
                        if (new Sys_UserName_ConflictResolutionDAL().GetOne(item.BAPIBNAME, "源系统无该角色", "iam系统中存在该角色", "SAP",item.ROLENAME) == null)
                        {
                            userroles.IsAddNewNotInIAM<SAP_User_Role>(entity, item.BAPIBNAME, Unitity.SystemType.SAP, "SAP_User_Role", item.ROLENAME, "iam系统中存在该角色", "源系统无该角色");
                        }
                    }
                    
                }

                return 1;
            }
            catch
            {
                return 0;
            }
        }


        public List<SAP_User_Role> GetUserRole(int PageSize, int PageIndex, out int count)
        {
            List<SAP_User_Role> listuserrole = NonExecute<List<SAP_User_Role>>(db =>
            {
                return db.SAP_User_Role.ToList();
            });
            count = listuserrole.Count;
            return listuserrole.OrderByDescending(item => item.START_DATE).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
    }
}
