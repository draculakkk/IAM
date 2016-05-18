using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAM.BLL
{
    public class SyncTaskBase
    {
        /// <summary>
        /// HR 所有同步任务
        /// </summary>
        public class HRSyncTask
        {
            public static void HRSyncTaskList()
            {
                //HR员工基本信息同步任务1
                IAMEntityDAL.SyncTask.AddTask("HR员工基本信息同步任务",  (m) =>
                {
                    int countall, okcount;
                   
                    IAMEntityDAL.EHRDAL.SyncEHR.SyncHREmployee(out countall, out okcount);
                    using (BaseDataAccess.IAMEntities db = new BaseDataAccess.IAMEntities())
                    {
                        db.ExecuteStoreCommand(@"INSERT INTO dbo.HRChayiLog
        ( ID ,
          Pk_psndoc ,
          code ,
          posts ,
          dept ,
          name ,
          moblePhone ,
          toPostsDate ,
          leavePostsDate ,
          userScope ,
          isSync ,
          syncDate ,
          p1 ,
          p2 ,
          p3 ,
          p4 ,
          p5 ,
          p6
        )
SELECT NEWID(),
          Pk_psndoc ,
          code ,
          posts ,
          dept ,
          name ,
          moblePhone ,
          toPostsDate ,
          leavePostsDate ,
          userScope ,
          isSync ,
          GETDATE() ,
          p1 ,
          '未处理' ,
          p3 ,
          p4 ,
          p5 ,
          p6 
		  FROM dbo.HREmployee WHERE isSync=0");
                        db.SaveChanges();
                    }
                    
                    m.AllCount = countall;
                    m.OkCount = okcount;

                });

                //HR公司信息同步任务2
                IAMEntityDAL.SyncTask.AddTask("HR公司信息同步任务", (m) =>
                {
                    int countall, okcount;
                    IAMEntityDAL.EHRDAL.SyncEHR.SyncHRCompanyInfo(out countall, out okcount);
                    m.AllCount = countall;
                    m.OkCount = okcount;
                });


                //HR操作员信息同步任务3
                IAMEntityDAL.SyncTask.AddTask("HR操作员信息同步任务",  (m) =>
                {

                    int countall, okcount;
                    new IAMEntityDAL.Sys_UserName_ConflictResolutionDAL().DeleteBySync(IAMEntityDAL.Unitity.SystemType.HR);
                    IAMEntityDAL.EHRDAL.SyncEHR.SyncSmUser(out countall, out okcount);
                    
                    m.AllCount = countall;
                    m.OkCount = okcount;
                });

               //HR角色更新同步任务4
                IAMEntityDAL.SyncTask.AddTask("HR角色更新同步任务",  (m) =>
                {
                    int countall, okcount;
                    IAMEntityDAL.EHRDAL.SyncEHR.SyncHzRole(out countall, out okcount);
                    m.AllCount = countall;
                    m.OkCount = okcount;

                });

                //HR角色公司关系更新同步任务5
                IAMEntityDAL.SyncTask.AddTask("HR角色公司关系更新同步任务",  (m) =>
                {
                    int countall, okcount;
                    IAMEntityDAL.EHRDAL.SyncEHR.SyncHrsmRoleCorpAlloc(out countall, out okcount);
                    m.AllCount = countall;
                    m.OkCount = okcount;
                });

                //HR用户角色关系更新同步任务6
                IAMEntityDAL.SyncTask.AddTask("HR用户角色关系更新同步任务",  (m) =>
                {
                    int countall, okcount;
                    IAMEntityDAL.EHRDAL.SyncEHR.SyncHrsmUserRole(out countall, out okcount);
                    //发送账号冲突邮件
                    //BLL.Sys_UserName_ConflictResolutionMail.SendMail(IAMEntityDAL.Unitity.SystemType.HR);
                    m.AllCount = countall;
                    m.OkCount = okcount;
                });

                //HR部门关系更新同步任务7
                IAMEntityDAL.SyncTask.AddTask("HR部门关系更新同步任务",  (m) =>
                {
                    int countall, okcount;
                    IAMEntityDAL.EHRDAL.SyncEHR.SyncHrDepartMent(out countall, out okcount);
                    m.AllCount = countall;
                    m.OkCount = okcount;
                });
            }
        }

        public class ADSyncTask
        {
            
            public static void SyncComputer()
            {
                //AD计算机信息同步任务8
                IAMEntityDAL.SyncTask.AddTask("AD计算机信息同步任务",  (m) =>
                {
                    int countall, okcount;
                    new IAMEntityDAL.Sys_UserName_ConflictResolutionDAL().DeleteBySync(IAMEntityDAL.Unitity.SystemType.ADComputer);
                    new BLL.ADSyncServices().SyncComputer(out countall, out okcount);
                    m.AllCount = countall;
                    m.OkCount = okcount;

                });

                //AD人员信息同步任务9
                IAMEntityDAL.SyncTask.AddTask("AD人员信息同步任务",  (m) =>
                {
                    int countall, okcount;
                    new IAMEntityDAL.Sys_UserName_ConflictResolutionDAL().DeleteBySync(IAMEntityDAL.Unitity.SystemType.AD);
                    new BLL.ADSyncServices().SyncUserInfo(out countall, out okcount);
                    m.AllCount = countall;
                    m.OkCount = okcount;

                });

                //AD人员工作组信息同步任务10
                IAMEntityDAL.SyncTask.AddTask("AD人员工作组信息同步任务",  (m) =>
                {
                    int countall, okcount;
                    new BLL.ADSyncServices().SyncUserWorkGroup(out countall, out okcount);
                    m.AllCount = countall;
                    m.OkCount = okcount;

                });

                //AD工作组信息同步任务11
                IAMEntityDAL.SyncTask.AddTask("AD工作组信息同步任务",  (m) =>
                {
                    int countall, okcount;
                    new BLL.ADSyncServices().SyncWorkGroup(out countall, out okcount);
                    m.AllCount = countall;
                    m.OkCount = okcount;

                });
            }

        }

        public class TCSyncTask
        {
            public static void SyncTCInfo()
            {
                //TC用户信息同步任务12
                IAMEntityDAL.SyncTask.AddTask("TC用户信息同步任务",  (m) => {
                    int allcount, okcount;
                    new IAMEntityDAL.Sys_UserName_ConflictResolutionDAL().DeleteBySync(IAMEntityDAL.Unitity.SystemType.TC);
                    new BLL.TcSyncServices().SyncUserInfo(out allcount,out okcount);
                    m.AllCount = allcount;
                    m.OkCount = okcount;
                });

                //TC角色信息同步任务13
                IAMEntityDAL.SyncTask.AddTask("TC角色信息同步任务", (m) =>
                {
                    int allcount, okcount;
                    new BLL.TcSyncServices().SyncRole(out allcount, out okcount);
                    m.AllCount = allcount;
                    m.OkCount = okcount;
                });
            }
        }

        public class HECSyncTask
        {
            //HEC用户角色信息同步任务14
            public static void SyncHECInfo()
            {
                IAMEntityDAL.SyncTask.AddTask("HEC用户角色信息同步任务",  (m) =>
                {
                    int allcount, okcount;
                    new IAMEntityDAL.Sys_UserName_ConflictResolutionDAL().DeleteBySync(IAMEntityDAL.Unitity.SystemType.HEC);
                    using (BaseDataAccess. IAMEntities db =new BaseDataAccess.IAMEntities())
                    {
                        db.ExecuteStoreCommand("UPDATE dbo.HEC_User SET END_DATE='2099-12-31' WHERE END_DATE IS NULL UPDATE dbo.HEC_User_Info SET END_DATE='2099-12-31' WHERE END_DATE IS NULL OR END_DATE=''");
                        db.SaveChanges();
                    }
                    new BLL.HECSyncServices().HECSyncUserRole(out allcount, out okcount);
                    m.AllCount = allcount;
                    m.OkCount = okcount;
                });

                //HEC角色信息同步任务15
                IAMEntityDAL.SyncTask.AddTask("HEC角色信息同步任务",  (m) =>
                {
                    int allcount, okcount;
                    new BLL.HECSyncServices().HECSyncRole(out allcount, out okcount);
                    m.AllCount = allcount;
                    m.OkCount = okcount;
                });

                //HEC公司信息同步任务16
                IAMEntityDAL.SyncTask.AddTask("HEC公司信息同步任务",  (m) =>
                {
                    int allcount, okcount;
                    new BLL.HECSyncServices().HECSyncCoompany(out allcount, out okcount);
                    m.AllCount = allcount;
                    m.OkCount = okcount;
                });

                //HEC部门信息同步
                IAMEntityDAL.SyncTask.AddTask("HEC部门信息同步任务", (x) =>
                {
                    int allcount, okcount;
                    new BLL.HECSyncServices().HECSyncDepartMent(out allcount, out okcount);
                    x.AllCount = allcount;
                    x.OkCount = okcount;
                });

                //HEC岗位信息同步
                IAMEntityDAL.SyncTask.AddTask("HEC岗位信息同步任务", (x) =>
                {
                    int allcount, okcount;
                    new BLL.HECSyncServices().HECSyncGangwei(out allcount, out okcount);
                    x.AllCount = allcount;
                    x.OkCount = okcount;
                });

                //HEC账号岗位信息同步
                IAMEntityDAL.SyncTask.AddTask("HEC账号岗位信息同步任务", (x) =>
                {
                    int allcount, okcount;
                    new BLL.HECSyncServices().HECSyncUserGangwei(out allcount, out okcount);
                    x.AllCount = allcount;
                    x.OkCount = okcount;
                });

            }
        }

        public class SapSyncTask
        {
            //sap用户信息,sap用户角色信息同步任务
            public static void SyncSap()
            {
                //sap用户信息同步任务17
                IAMEntityDAL.SyncTask.AddTask("sap用户信息同步任务",  (m) =>
                {
                    int allcount, okcount;
                    new IAMEntityDAL.Sys_UserName_ConflictResolutionDAL().DeleteBySync(IAMEntityDAL.Unitity.SystemType.SAP);
                    new BLL.SAPSyncServices().SyncSapUserInfo(out allcount, out okcount);
                    m.AllCount = allcount;
                    m.OkCount = okcount;
                });

                //sap用户角色信息同步任务18
                IAMEntityDAL.SyncTask.AddTask("sap用户角色信息同步任务",  (m) =>
                {
                    int allcount, okcount;
                    new BLL.SAPSyncServices().SyncSapRolesnew(out allcount, out okcount);
                    
                    m.AllCount = allcount;
                    m.OkCount = okcount;
                });
            }
        }

        public class EmailServicesTask
        {
            public static void SendMail()
            {
                IAMEntityDAL.SyncTask.AddTask("发送邮件", (m) =>
                {
                    int allcount, okcount;

                    var SystemNames = new IAMEntityDAL.Sys_UserName_ConflictResolutionDAL().GetSendSystemName();
                    allcount=okcount = SystemNames.Count;
                    foreach (var x in SystemNames)
                    {
                        BLL.Sys_UserName_ConflictResolutionMail.SendMail((IAMEntityDAL.Unitity.SystemType)Enum.Parse(typeof(IAMEntityDAL.Unitity.SystemType), x,true));
                    }
                    //BLL.Sys_UserName_ConflictResolutionMail.SendMail(IAMEntityDAL.Unitity.SystemType.ADComputer);                   
                    //BLL.Sys_UserName_ConflictResolutionMail.SendMail(IAMEntityDAL.Unitity.SystemType.AD);
                    //BLL.Sys_UserName_ConflictResolutionMail.SendMail(IAMEntityDAL.Unitity.SystemType.TC); 
                    //BLL.Sys_UserName_ConflictResolutionMail.SendMail(IAMEntityDAL.Unitity.SystemType.SAP);
                    //BLL.Sys_UserName_ConflictResolutionMail.SendMail(IAMEntityDAL.Unitity.SystemType.HEC);
                    m.AllCount = allcount;
                    m.OkCount = okcount;
                });
            }
        }
    }
}