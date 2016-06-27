using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class TC_UserGroupSettingDAL : BaseFind<TC_UserGroupSetting>
    {
        public int ADDTCUserGroupSetting(TC_UserGroupSetting entity)
        {
            return Add(entity);
        }

        public int UpdateTCUserGroup(TC_UserGroupSetting entity)
        {
            using (IAMEntities db = new IAMEntities())
            {
                db.TC_UserGroupSetting.Attach(entity);
                db.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Modified);
                return db.SaveChanges();
            }
        }

        public List<TC_UserGroupSetting> GetList()
        {
            return NonExecute<List<TC_UserGroupSetting>>(db =>
            {
                return db.TC_UserGroupSetting.ToList();
            });
        }

        /// <summary>
        /// 同步Tc用户组配置信息
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int SyncTCGroupSettings(List<TC_UserGroupSetting> list, string userid)
        {
            try
            {
                List<TC_UserGroupSetting> listold = new List<TC_UserGroupSetting>();
                //using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection("data source=.;initial catalog=IAM;persist security info=True;user id=sa;password=123;"))
                //{
                //    con.Open();
                //    System.Data.SqlClient.SqlCommand cmd = con.CreateCommand();
                //    cmd.CommandText = "SELECT * FROM dbo.TC_UserGroupSetting WHERE UserID='" + userid + "'";
                //    System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                   
                //    while (reader.Read())
                //    {
                //        TC_UserGroupSetting tmp = new TC_UserGroupSetting()
                //        {
                //            UserID = reader["UserID"].ToString(),
                //            ID = new Guid(reader["ID"].ToString()),
                //            isdr = Convert.ToInt32(reader["isdr"].ToString()),
                //            Memo = reader["Memo"].ToString(),
                //            p1 = reader["p1"].ToString(),
                //            p2 = reader["p2"].ToString(),
                //            GroupAdmin = short.Parse(reader["GroupAdmin"].ToString()),
                //             GroupDefaultRole=short.Parse(reader["GroupDefaultRole"].ToString()),
                //            GroupOut = short.Parse(reader["GroupOut"].ToString()),
                //            GroupStatus = short.Parse(reader["GroupStatus"].ToString())
                //        };
                //        listold.Add(tmp);
                //    }
                //}
                listold = new IAMEntities().TC_UserGroupSetting.Where(x=>x.UserID==userid).ToList();
                DeferencesSlution.UserRoleAlignmentValue userroles = new DeferencesSlution.UserRoleAlignmentValue();
                foreach (TC_UserGroupSetting item in list)
                {
                    if (base.IsFirstTime)
                    {
                        ADDTCUserGroupSetting(item);
                    }
                    else
                    {
                        var enti = listold.FirstOrDefault(it => it.UserID.Equals(userid, StringComparison.OrdinalIgnoreCase) && it.Memo.Equals(item.Memo, StringComparison.OrdinalIgnoreCase));
                        if (enti == null)
                        {
                            if (new Sys_UserName_ConflictResolutionDAL().GetOne(item.UserID, "源系统中存在该角色", "IAM系统无该角色", "TC", item.Memo) == null)
                            {
                                userroles.IsAddNewNotInIAM<TC_UserGroupSetting>(item, item.UserID.ToLower(), Unitity.SystemType.TC, "TC_UserGroupSetting", item.Memo, "IAM系统无该角色", "源系统中存在该角色");
                            }
                        }
                        else
                        {
                            //if (enti.isdr == 1 && (item.isdr == null || item.isdr == 0))
                            //{
                            //    userroles.IsAddNewNotInIAM<TC_UserGroupSetting>(enti, enti.UserID, Unitity.SystemType.TC, "TC_UserGroupSetting", enti.Memo, "IAM系统该角色标记为删除", "源系统中该角色不为删除");

                            //}

                            //old code
                            //enti.GroupStatus = item.GroupStatus;
                            //UpdateTCUserGroup(enti);

                            //dhzhang add begin
                            if (enti.GroupStatus != item.GroupStatus)
                            {
                                if (new Sys_UserName_ConflictResolutionDAL().GetOne(item.UserID,
                                    item.GroupStatus.HasValue ? enti.GroupStatus.Value.ToString() : "null",
                                    enti.GroupStatus.HasValue ? enti.GroupStatus.Value.ToString() : "null",
                                    "TC", "TC角色状态/" + enti.Memo) == null)
                                {
                                    userroles.AddRoleChayi(item.UserID, Unitity.SystemType.TC, "TC_UserGroupSetting", "TC角色状态/" + enti.Memo,
                                        enti.GroupStatus.HasValue ? enti.GroupStatus.Value.ToString() : "null",
                                        item.GroupStatus.HasValue ? item.GroupStatus.Value.ToString() : "null",
                                        string.Format("UPDATE TC_UserGroupSetting SET GroupStatus={0} WHERE UserID='{1}' AND Memo='{2}'",
                                        item.GroupStatus.HasValue ? "'" + item.GroupStatus+ "'" : "null",
                                        item.UserID, item.Memo));
                                }
                            }
                            //dhzhang add end

                        }
                    }
                }

                foreach (var item in listold)
                {
                    if (list == null)
                    {
                        userroles.IsAddNewNotInIAM<TC_UserGroupSetting>(item, item.UserID.ToLower(), Unitity.SystemType.TC, "TC_UserGroupSetting", item.Memo, "IAM系统存在该角色", "源系统中无该角色");
                    }
                    else
                    {
                        var enti = list.FirstOrDefault(it => it.UserID.Equals(item.UserID, StringComparison.OrdinalIgnoreCase) && it.Memo.Equals(item.Memo, StringComparison.OrdinalIgnoreCase));
                        if (enti == null)
                            userroles.IsAddNewNotInIAM<TC_UserGroupSetting>(item, item.UserID.ToLower(), Unitity.SystemType.TC, "TC_UserGroupSetting", item.Memo, "IAM系统存在该角色", "源系统中无该角色");
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                new LogDAL().AddsysErrorLog(ex.ToString());
                return 0;
            }
        }

    }
}
