using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAMEntityDAL;
using BaseDataAccess;

namespace IAM.BLL
{
    public class ADSyncServices
    {
        ADComputerDAL _adServices = new ADComputerDAL();
        ADUserInfoDAL _adUserServices = new ADUserInfoDAL();
        ADUserWorkGroupDAL _adworkServices = new ADUserWorkGroupDAL();
        WebReference3.Service1 _autoServices = new WebReference3.Service1();
        public ADSyncServices()
        {

        }
        /// <summary>
        /// 同步ADComputer任务
        /// </summary>
        /// <param name="AllCount"></param>
        /// <param name="OkCount"></param>
        public void SyncComputer(out int AllCount, out int OkCount)
        {


            var xxf = _autoServices.QueryAllComputer().ToList();
            AllCount = xxf.Count;
            OkCount = 0;
            List<AD_Computer> listc = new List<AD_Computer>();
            foreach (var item in xxf)
            {

                AD_Computer entity = new AD_Computer();
                List<AD_Computer_WorkGroups> computerlist = new List<AD_Computer_WorkGroups>();
                entity.ID = Guid.NewGuid();
                entity.DESCRIPTION = item.Description;
                entity.NAME = item.Name;
                entity.ExpiryDate = string.IsNullOrEmpty(item.ExpiryDate) ? DateTime.Now : Convert.ToDateTime(item.ExpiryDate);
                entity.IsDelete = false;
                entity.IsSync = false;
                entity.ENABLE = item.Enabled == false ? 0 : 1;
                entity.p1 = item.LastLoginTime.ToString();
                listc.Add(entity);
                if (item.WorkGroups.Length > 0)
                {
                    foreach (var itm in item.WorkGroups)
                    {
                        AD_Computer_WorkGroups mo = new AD_Computer_WorkGroups();
                        mo.ComputerName = item.Name;
                        mo.WorkGroup = itm;
                        mo.Id = Guid.NewGuid();
                        computerlist.Add(mo);
                    }
                }
                try
                {
                    new ADComputerDAL().SyncADComputer(entity);
                    new AD_Computer_WorkGroupsDAL().SyncComputerWorkGroup(computerlist);
                    OkCount++;
                }
                catch (Exception ex)
                {
#if DEBUG
                    new LogDAL().AddsysErrorLog("ADComputer同步" + ex.ToString());
                    throw ex;
#else

                    continue;
#endif
                }
            }

            try
            {
                int count;
                var listold = new ADComputerDAL().GetAdComputerList(out count);
                foreach (var item in listold)
                {
                    var tmp = listc.FirstOrDefault(it => it.NAME.Trim().ToUpper() == item.NAME.Trim().ToUpper());
                    if (tmp == null)
                        new IAMEntityDAL.DeferencesSlution.UserRoleAlignmentValue().IsAddNewNotInIAM<AD_Computer>(item, item.NAME, Unitity.SystemType.ADComputer, "AD_Computer", "", "IAM系统中有该账号", "源系统中无该账号");
                }

                //发送账号冲突邮件
                //BLL.Sys_UserName_ConflictResolutionMail.SendMail(IAMEntityDAL.Unitity.SystemType.ADComputer);
            }
            catch (Exception ex)
            {
                new LogDAL().AddsysErrorLog("ADComputer同步 fa" + ex.ToString());
            }
        }

        /// <summary>
        /// 同步AD UserInfo 任务
        /// </summary>
        /// <param name="AllCount"></param>
        /// <param name="OkCount"></param>
        public void SyncUserInfo(out int AllCount, out int OkCount)
        {
            var xxf = _autoServices.QueryAllUser().ToList().OrderBy(item => item.Accountname).ToList();
            AllCount = xxf.Count;
            OkCount = 0;
            List<AD_UserInfo> list = new List<AD_UserInfo>();
            foreach (var item in xxf)
            {
                AD_UserInfo entity = new AD_UserInfo()
                {
                    Id = item.Id,
                    Accountname = item.Accountname,
                    ADMobile = item.Mobile,
                    ADTel = item.Tel,
                    CnName = item.Cn,
                    Department = item.Department,
                    DESCRIPTION = item.Description,
                    Email = item.Email,
                    Lync = item.Lync,
                    Group = item.Group,
                    Job = item.Job,
                    Posts = "",
                    UserID = item.Accountname,
                    ENABLE = item.Enabled,
                    IsSync = true,
                    SyncDate = DateTime.Now,
                    DisplayName = item.Displayname,
                    expiryDate = DateTime.Parse(item.AccountExpires) < new DateTime(1980, 12, 31) ? (DateTime?)null : DateTime.Parse(item.AccountExpires),
                    Drive = item.HomeDrive,
                    PATH = item.HomeDirectory,
                    ToPostsDate = item.LastLoginTime,
                    EnableDrive = true
                    //EmailDatabase=item.HomeMDB
                };
                if (item.HomeMDB != string.Empty)
                {
                    string[] db = item.HomeMDB.Split(',');
                    if (db.Length > 1)
                    {
                        entity.EmailDatabase = db[0].Replace("CN=", string.Empty);
                    }
                }
                list.Add(entity);
                OkCount++;
            }
            try
            {
                
                _adUserServices.SyncADUserInfo(list);
                //using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection("data source=.;Initial catalog=IAM;User ID=sa;Password=123;"))
                //{
                //    con.Open();
                //    System.Data.SqlClient.SqlBulkCopy sbk = new System.Data.SqlClient.SqlBulkCopy(con);
                //    sbk.DestinationTableName = "AD_UserInfoSystem";
                //    sbk.WriteToServer(list.ToDataTable());
                //}
            }
            catch (Exception ex)
            {
                new LogDAL().AddsysErrorLog(ex.ToString());
            }
        }


        public void SyncUserWorkGroup(out int AllCount, out int OkCount)
        {
            WebReference3.Service1 _autoServices = new WebReference3.Service1();
            var querylist = _autoServices.QueryAllUser().ToList();
            OkCount = 0;
            List<AD_UserWorkGroup> list = new List<AD_UserWorkGroup>();
            foreach (var item in querylist)
            {
                var xxf = _autoServices.QueryUserWorkGroup(item.Accountname).ToList();

                foreach (var it in xxf)
                {
                    AD_UserWorkGroup entity = new AD_UserWorkGroup();
                    entity.ID = Guid.NewGuid();
                    entity.Uid = item.Accountname;
                    entity.GroupName = it.Name;
                    entity.IsSync = true;
                    entity.SyncDate = DateTime.Now;
                    entity.isdr = 0;
                    entity.memo = "源数据";
                    list.Add(entity);
                }
            }

            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection("Data Source=10.124.87.172;Initial Catalog=IAM;User ID=sa;Password=Iam12345"))
            {
                con.Open();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("TRUNCATE TABLE dbo.AD_UserWorkGroupBAK DELETE dbo.AD_UserWorkGroup WHERE isdr=1 ",con);
                cmd.ExecuteNonQuery();
                System.Data.SqlClient.SqlBulkCopy sbc = new System.Data.SqlClient.SqlBulkCopy(con);
                sbc.DestinationTableName = "dbo.AD_UserWorkGroupBAK";
                System.Data.DataTable dt = list.Select(x => new
                {
                    x.Uid,
                    x.GroupName,
                    x.memo
                }).ToDataTable();
                sbc.WriteToServer(dt);
            }
            //AllCount = OkCount = list.Count;
            AllCount = list.Count;
            var grouplist = new ADUserWorkGroupDAL().GetADUserWorkGroupList(out OkCount);
            //OkCount = 0;
            new ADUserWorkGroupDAL().SyncUserWorkGroup(list);
            //发送账号冲突邮件
            //BLL.Sys_UserName_ConflictResolutionMail.SendMail(IAMEntityDAL.Unitity.SystemType.AD);
        }

        public void SyncWorkGroup(out int AllCount, out int OkCount)
        {
            var xxf = _autoServices.QueryAllWorkGroup().ToList();
            AllCount = xxf.Count;
            OkCount = 0;
            foreach (var item in xxf)
            {
                AD_workGroup workgroup = new AD_workGroup();
                workgroup.Id = Guid.NewGuid();
                workgroup.IsDelete = true;
                workgroup.IsSync = true;
                workgroup.SyncDate = DateTime.Now;
                workgroup.NAME = item.Name;
                workgroup.DESCRIPTION = item.Description;
                try
                {
                    new ADWorkGroupDAL().AddADWorkGroup(workgroup);
                    OkCount++;
                }
                catch (Exception ex)
                {

#if DEBUG
                    throw ex;
#else
                    continue;
#endif
                }
            }
        }
    }
}