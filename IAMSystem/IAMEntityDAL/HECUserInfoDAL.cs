using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class HECUserInfoDAL : BaseFind<HEC_User_Info>
    {
        /// <summary>
        /// 添加 HEC用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddHECUserInfo(HEC_User_Info entity)
        {
            var mo = NonExecute<HEC_User_Info>(db =>
            {
                return db.HEC_User_Info.FirstOrDefault(item => item.USER_NAME == entity.USER_NAME && item.ROLE_CODE == entity.ROLE_CODE && item.DESCRIPTION == entity.DESCRIPTION&&item.COMPANY_CODE==entity.COMPANY_CODE);
            });
            if (mo == null)
                return Add(entity);
            else
                return 1;


            
        }

        /// <summary>
        /// 更新 HEC用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateHECUserInfo(HEC_User_Info entity)
        {

            using (IAMEntities db = new IAMEntities())
            {
                db.HEC_User_Info.Attach(entity);
                db.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Modified);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 删除 HEC用户信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int DeleteHECUserInfo(Guid id)
        {


            using (IAMEntities db = new IAMEntities())
            {
                HEC_User_Info module = db.HEC_User_Info.FirstOrDefault(item => item.ID == id);
                db.HEC_User_Info.DeleteObject(module);
                db.ObjectStateManager.ChangeObjectState(module, System.Data.EntityState.Deleted);
                return db.SaveChanges();
            }

        }

        /// <summary>
        /// 根据用户账号返回用户详细
        /// </summary>
        /// <param name="User_Name"></param>
        /// <returns></returns>
        public HEC_User_Info GetOneHEC_User_Info(string User_Name)
        {
            return NonExecute<HEC_User_Info>(db =>
            {
                return db.HEC_User_Info.FirstOrDefault(item => item.USER_NAME == User_Name);
            });
        }


        /// <summary>
        /// 获取HEC 用户信息
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<HEC_User_Info> GetHecUserInfo(int PageSize, int PageIndex, string UserName, string Descrption, out int count)
        {
            List<HEC_User_Info> listhec = NonExecute<List<HEC_User_Info>>(db =>
            {
                return db.HEC_User_Info.ToList();
            });
            if (!string.IsNullOrEmpty(UserName))
                listhec = listhec.Where(item => item.USER_NAME == UserName).ToList();
            if (!string.IsNullOrEmpty(Descrption))
                listhec = listhec.Where(item => item.DESCRIPTION.Contains(Descrption)).ToList();

            count = listhec.Count;
            return listhec.OrderByDescending(item => item.START_DATE).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }

        public List<HEC_User_Info> GetHecUserInfoByUser_CD(string userCD)
        {
            return NonExecute<List<HEC_User_Info>>(item =>
            {
                return item.HEC_User_Info.Where(itm => itm.USER_NAME.Trim() == userCD).ToList();
            });
        }

        public void Delete(Guid id)
        {
            using (IAMEntities db = new IAMEntities())
            {
                var tmp = db.HEC_User_Info.FirstOrDefault(item => item.ID == id);
                if (tmp != null)
                {
                    tmp.isdr = 1;
                    UpdateHECUserInfo(tmp);
                }
                
            }
        }

    

        public bool AddRole(List<V_HECUSER_Role> UserList, HEC_User entity)
        {
            try
            {
                List<HEC_User_Info> All = NonExecute<List<HEC_User_Info>>(db => db.HEC_User_Info.ToList());
                List<V_HECUSER_Role> deletelist = UserList.Where(item => item.isdr == 1).ToList();
                foreach (var item in deletelist)
                {
                    //Delete(item.uID);
                }

                foreach (var item in UserList.Where(item => item.isdr == 0))
                {
                    HEC_User_Info en = All.FirstOrDefault(i => i.ID == item.uID);
                    if (en != null)
                    {
                            en.ROLE_START_DATE = item.uROLESTARTDATE;
                            en.ROLE_END_DATE = item.uROLEENDDATE;
                            en.IsSync = 0;
                            en.ERROR_MSG = "未同步";
                            UpdateHECUserInfo(en);
                    }
                }

                foreach (var item in UserList.Where(item => item.isdr == 2))
                {
                    HEC_User_Info en = All.FirstOrDefault(i => i.ID == item.uID);
                    if (en == null)
                    {
                        en = new HEC_User_Info()
                        {
                            ID = item.uID,
                            ROLE_CODE = item.rROLECODE,
                            COMPANY_CODE = item.cCOMPANYCODE,
                            USER_NAME = entity.User_CD,
                            DESCRIPTION = entity.DESCRIPTION,
                            EMPLOYEE_CODE = entity.USER_CODE,
                            EMPLOYEE_NAME = entity.USER_NAME,
                            ROLE_START_DATE = item.uROLESTARTDATE,
                            ROLE_END_DATE = item.uROLEENDDATE,
                            IsSync = 0,
                            ERROR_MSG = "未同步",
                            STATUS = "",
                            START_DATE = entity.START_DATE == null ? "" : Convert.ToDateTime(entity.START_DATE).ToShortDateString(),
                            END_DATE = entity.END_DATE == null ? "" : Convert.ToDateTime(entity.END_DATE).ToShortDateString(),
                            isdr = 0

                        };
                        AddHECUserInfo(en);
                    }
                }

                foreach (var item in UserList.Where(item => item.isdr == 1))
                {
                    HEC_User_Info en = All.FirstOrDefault(i => i.ID == item.uID);
                    if (en != null)
                        Delete(item.uID);
                }

                return true;
            }
            catch (Exception ex)
            {
                new LogDAL().AddsysErrorLog(ex.ToString());
                return false;
            }
        }


        /// <summary>
        /// 同步任务
        /// </summary>
        /// <param name="entity"></param>
        public void SyncHECUserInfo(List<HEC_User_Info> entitylist)
        {
            try
            {
                var listold = NonExecute<List<HEC_User_Info>>(db => {
                    return db.HEC_User_Info.Where(item => item.ROLE_CODE != "RPL_PRESENTER").ToList();
                });
                DeferencesSlution.UserRoleAlignmentValue userroles=new DeferencesSlution.UserRoleAlignmentValue();
                entitylist = entitylist.Where(item => item.ROLE_CODE != "RPL_PRESENTER").ToList();
                foreach (var item in entitylist)
                {
                  if (item.ROLE_CODE != "RPL_PRESENTER")
                   {
                      
                        if (base.IsFirstTime)
                        {
                            AddHECUserInfo(item);
                        }
                        else
                        {
                            var enti = listold.FirstOrDefault(itm=>itm.ROLE_CODE==item.ROLE_CODE&&itm.USER_NAME==item.USER_NAME&&item.COMPANY_CODE==itm.COMPANY_CODE);
                            if (enti == null)
                            {
                                if (new Sys_UserName_ConflictResolutionDAL().GetOne(item.USER_NAME, "源系统中有该角色", "iam无该角色", "HEC", item.ROLE_CODE + "/" + item.COMPANY_CODE) == null)
                                {
                                    userroles.IsAddNewNotInIAM<HEC_User_Info>(item, item.USER_NAME, Unitity.SystemType.HEC, "HEC_User_Info", item.ROLE_CODE + "/" + item.COMPANY_CODE, "iam无该角色", "源系统中有该角色");
                                }
                            }
                            else
                            {
                                if (enti.ROLE_END_DATE == null)
                                    enti.ROLE_END_DATE = "";
                                if (item.ROLE_END_DATE == null)
                                    item.ROLE_END_DATE = "";
                                if (enti.ROLE_START_DATE == null)
                                    enti.ROLE_START_DATE = "";
                                if (item.ROLE_START_DATE == null)
                                    enti.ROLE_START_DATE = "";

                                //dhzhang add begin
                                if (!enti.ROLE_END_DATE.Equals(item.ROLE_END_DATE))
                                {
                                    if (new Sys_UserName_ConflictResolutionDAL().GetOne(item.USER_NAME, item.ROLE_END_DATE, enti.ROLE_END_DATE, "HEC", "HEC角色过期时间/" + enti.ROLE_CODE + "/" + item.COMPANY_CODE) == null)
                                    {
                                        userroles.AddRoleChayi(item.USER_NAME, Unitity.SystemType.HEC, "HEC_User_Info", "HEC角色过期时间/" + enti.ROLE_CODE + "/" + item.COMPANY_CODE, enti.ROLE_END_DATE, item.ROLE_END_DATE,
                                            string.Format("UPDATE HEC_User_Info SET ROLE_END_DATE='{0}' WHERE USER_NAME='{1}' AND ROLE_CODE='{2}' AND COMPANY_CODE='{3}'", item.ROLE_END_DATE, item.USER_NAME, item.ROLE_CODE,item.COMPANY_CODE));
                                    }
                                }
                                if (!enti.ROLE_START_DATE.Equals(item.ROLE_START_DATE))
                                {
                                    if (new Sys_UserName_ConflictResolutionDAL().GetOne(item.USER_NAME, item.ROLE_END_DATE, enti.ROLE_END_DATE, "HEC", "HEC角色开始时间/" + item.ROLE_CODE+"/"+item.COMPANY_CODE) == null)
                                    {
                                        userroles.AddRoleChayi(item.USER_NAME, Unitity.SystemType.HEC, "HEC_User_Info", "HEC角色开始时间/" + item.ROLE_CODE + "/" + item.COMPANY_CODE, string.IsNullOrEmpty(enti.ROLE_START_DATE) ? "" : enti.ROLE_START_DATE, string.IsNullOrEmpty(item.ROLE_START_DATE) ? "" : item.ROLE_START_DATE,
                                            string.Format("UPDATE HEC_User_Info SET ROLE_START_DATE='{0}' WHERE USER_NAME='{1}' AND ROLE_CODE='{2}'AND COMPANY_CODE='{3}'", item.ROLE_START_DATE, item.USER_NAME, item.ROLE_CODE,item.COMPANY_CODE));
                                    }
                                }
                                //dhzhang add end
                                
                            }
                          
                        }
                   }

                }

                foreach (var item in listold)
                {
                    if (item.ROLE_CODE != "RPL_PRESENTER")
                    {
                        var enti = entitylist.FirstOrDefault(it => it.ROLE_CODE != "RPL_PRESENTER" && it.ROLE_CODE.Trim() == item.ROLE_CODE.Trim() && it.USER_NAME.Trim() == item.USER_NAME.Trim() && item.COMPANY_CODE == it.COMPANY_CODE);
                        if (enti == null)
                            userroles.IsAddNewNotInIAM<HEC_User_Info>(item, item.USER_NAME, Unitity.SystemType.HEC, "HEC_User_Info", item.ROLE_CODE + "/" + item.COMPANY_CODE, "iam拥有该角色", "源系统无该角色");
                    }
                }
                
            }
            catch (Exception ex)
            {
                new LogDAL().AddsysErrorLog(ex.ToString());
                throw ex;
            }
        }

    }
}
