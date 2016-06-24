using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class TC_UserInfoDAL : BaseFind<TC_UserInfo>
    {
        /// <summary>
        /// 添加Tc_userInfo 信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddTCUserInfo(TC_UserInfo entity)
        {
            return Add(entity);
        }

        /// <summary>
        /// 更新Tc_UserInfo 信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateUserInfo(TC_UserInfo entity)
        {
            return Update(entity);
        }

        public TC_UserInfo GetOneTCUser(string userid)
        {
            return NonExecute<TC_UserInfo>(db =>
            {
                return db.TC_UserInfo.FirstOrDefault(item => item.UserID.Trim() == userid);
            });
        }

        /// <summary>
        /// 添加或更新用户信息 及用户权限
        /// </summary>
        /// <param name="entity">用户信息</param>
        /// <param name="ListReport">用户所拥有的权限</param>
        /// <returns></returns>
        public int CreateOrUpdate(TC_UserInfo entity, List<V_TCReport> ListReport)
        {
            TC_UserInfo module = NonExecute<TC_UserInfo>(db =>
            {
                return db.TC_UserInfo.FirstOrDefault(item => item.UserID == entity.UserID);
            });
            try
            {
                if (module != null)
                {
                  
                    module.UserName = entity.UserName;
                    module.SystemName = entity.SystemName;
                    module.mailAddress = entity.mailAddress;
                   
                    module.LicenseLevel = entity.LicenseLevel;
                    module.UserStatus = entity.UserStatus;
                    UpdateUserInfo(module);
                    new TC_UserRoleDAL().CreateOrUpdate(ListReport);
                }
                else
                {
                    AddTCUserInfo(entity);
                    new TC_UserRoleDAL().CreateOrUpdate(ListReport);
                }
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        /// <summary>
        /// 获取Tc_UserInfo 信息
        /// </summary>
        /// <param name="Count"></param>
        /// <returns></returns>
        public List<TC_UserInfo> GetTCUserInfolist(out int Count)
        {
            List<TC_UserInfo> list = NonExecute<List<TC_UserInfo>>(db =>
            {
                return db.TC_UserInfo.ToList();
            });
            Count = list.Count;
            return list;
        }

        /// <summary>
        /// 同步TC_UserInfo 信息,并更新用户权限对应关系
        /// 2014-11-12 haiboax tc用户在第一次同步时直接添加进数据库，以后需要进入冲突解决方案中；
        /// </summary>
        /// <param name="entity"></param>
        public void SyncUserInfo(TC_UserInfo entity, string UserGroups,List<TC_UserGroupSetting> list)
        {
            try
            {
                TC_UserInfo module = NonExecute<TC_UserInfo>(db =>
                {
                    return db.TC_UserInfo.FirstOrDefault(e => e.UserID.ToUpper() == entity.UserID.ToUpper());
                });
                if (module != null)
                {
                    //module.UserID = module.UserID.ToUpper();
                    entity.UserID = entity.UserID.ToUpper();
                    AddDeference(module, entity, Unitity.SystemType.TC, "UserID", module.UserID.ToUpper(), module.UserID.ToUpper());
                }
                else
                {
                    if (base.IsFirstTime)
                    {
                        AddTCUserInfo(entity);
                    }
                    else
                    {
                        if (new Sys_UserName_ConflictResolutionDAL().GetOneUser(entity.UserID, "TC") == null)
                        {
                            new DeferencesSlution.UserRoleAlignmentValue().IsAddNewNotInIAM<TC_UserInfo>(entity, entity.UserID, Unitity.SystemType.TC, "TC_UserInfo", "", "无", "源系统新增账号");

                        }
                    }
                    //}
                }
                //同步用户组
                new TC_UserGroupSettingDAL().SyncTCGroupSettings(list, entity.UserID);//同步用户组配置信息
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
