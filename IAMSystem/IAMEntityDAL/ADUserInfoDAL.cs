using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
   public class ADUserInfoDAL:BaseFind<AD_UserInfo>
    {
       /// <summary>
       /// 添加AD_UserInfo 信息
       /// </summary>
       /// <param name="entity"></param>
       /// <returns></returns>
       public int AddAdUserInfo(AD_UserInfo entity)
       {
           var mo = NonExecute<AD_UserInfo>(db => {
               return db.AD_UserInfo.FirstOrDefault(item=>item.Accountname==entity.Accountname&&item.Id==entity.Id);
           });
           if (mo == null)
               return Add(entity);
           else
               return 1;
       }

       /// <summary>
       /// 更新AD_UserInfo信息
       /// </summary>
       /// <param name="entity"></param>
       /// <returns></returns>
       public int UpdateUserInfo(AD_UserInfo entity)
       {
           using (IAMEntities db = new IAMEntities())
           {
               db.AD_UserInfo.Attach(entity);
               db.ObjectStateManager.ChangeObjectState(entity,System.Data.EntityState.Modified);
               return db.SaveChanges();
           }
       }

       public AD_UserInfo GetOne(string accountName)
       {
           return NonExecute<AD_UserInfo>(db => {
               return db.AD_UserInfo.FirstOrDefault(item=>item.Accountname==accountName);
           });
       }

       /// <summary>
       /// 获取AD_UserInfo 信息集合
       /// </summary>
       /// <param name="count">返回集合总数目</param>
       /// <returns></returns>
       public List<AD_UserInfo> GetADUserInfo(string userCode,string department,string username, out int count)
       {
           List<AD_UserInfo> listUserInfo = NonExecute<List<AD_UserInfo>>(db => {
               return db.AD_UserInfo.ToList();
           });
           if (!string.IsNullOrEmpty(userCode))
               listUserInfo = listUserInfo.Where(item => item.UserID == userCode || item.Id == userCode).ToList();
           if (!string.IsNullOrEmpty(department))
               listUserInfo = listUserInfo.Where(item => item.Department == department).ToList();
           if (!string.IsNullOrEmpty(username))
               listUserInfo = listUserInfo.Where(item => item.NAME == username).ToList();
           count = listUserInfo.Count;
           return listUserInfo.OrderByDescending(item => item.SyncDate).ToList();
       }

       /// <summary>
       /// 同步AD_UserInfo 任务
       /// </summary>
       /// <param name="entity">AD_UserInfo实例</param>
       public int CreateOrUpdate(AD_UserInfo entity,List<AD_Computer> computerlist)
       {
           try
           {
               AD_UserInfo model = NonExecute<AD_UserInfo>(db =>
               {
                   return db.AD_UserInfo.FirstOrDefault(item =>item.UserID==entity.UserID);
               });
               int ok = 0;
               if (model != null)
                  ok= UpdateUserInfo(entity);
               else
                  ok= AddAdUserInfo(entity);
               if (ok > 0)
               {
                   return new ADComputerDAL().CreateOrUpdate(computerlist);
               }
               else
               {
                   return 0;
               }
           }
           catch (Exception ex)
           {
#if DEBUG
               new LogDAL().AddsysErrorLog(ex.ToString());
               return 0;
               
#else
               new LogDAL().AddsysErrorLog(ex.ToString());
               return 0;
#endif

           }
       }



       /// <summary>
       /// 同步AD_UserInfo 任务
       /// </summary>
       /// <param name="entity">AD_UserInfo实例</param>
       public void SyncADUserInfo(List<AD_UserInfo> entity)
       {
           try
           {
               var listold = NonExecute < List<AD_UserInfo>>(db =>
               {
                   return db.AD_UserInfo.ToList();
               });
               
               DeferencesSlution.UserRoleAlignmentValue userrols = new DeferencesSlution.UserRoleAlignmentValue();
               System.Text.StringBuilder stb = new StringBuilder();
               foreach (var item in entity)
               {
                   var model = listold.FirstOrDefault(i=>i.Accountname==item.Accountname);

                   if (model != null)
                   {
                       if (item.ENABLE == false)
                       {
                           if (model.ENABLE == true)
                           {
                               var tSys_UserName_ConflictResolution = new Sys_UserName_ConflictResolutionDAL();
                               var aa = new Sys_UserName_ConflictResolution() { 
                                SysType=Unitity.SystemType.AD.ToString(),
                                CreateTime=DateTime.Now,
                                STATE=1,
                                TableName = "AD_UserInfo",
                                CollName = "是否可用",
                                CollSysValue=item.ENABLE.ToString(),
                                CollIAMValue=model.ENABLE.ToString(),
                                UserCollName = "Accountname",
                                UserValue=model.Accountname,
                                P1 = "ENABLE",
                                P2 = "user",
                                UserName=model.Accountname
                               };
                               tSys_UserName_ConflictResolution.Add(aa);
                           }
                       }
                       else
                       {
                           AddDeference(model, item, Unitity.SystemType.AD, "Accountname", model.Accountname, model.Accountname);
                       }
                       
                   }
                   else
                   {
                       if (base.IsFirstTime)
                           AddAdUserInfo(item);
                       else
                       {
                           if (new Sys_UserName_ConflictResolutionDAL().GetOneUser(item.Accountname, "AD") == null)
                               userrols.IsAddNewNotInIAM<AD_UserInfo>(item, item.Accountname, Unitity.SystemType.AD, "AD_UserInfo", "", "IAM系统无该账号", "源系统新增账号");


                       }
                   }
                   stb.Append(string.Format("UPDATE dbo.AD_UserInfo SET ToPostsDate='{0}',ADTel='{1}',ADMobile='{2}' WHERE Accountname='{3}'", item.ToPostsDate,item.ADTel,item.ADMobile, item.Accountname));
               }

               foreach (var item in listold)
               {
                   var model = entity.FirstOrDefault(ite=>ite.Accountname.Trim()==item.Accountname.Trim());
                   if(model==null)
                       userrols.IsAddNewNotInIAM<AD_UserInfo>(item, item.Accountname, Unitity.SystemType.AD, "AD_UserInfo", "", "IAM系统有该账号", "源系统中无该账号");
               }
               if (!string.IsNullOrEmpty(stb.ToString()))
               {
                   using (IAMEntities db = new IAMEntities())
                   {
                       db.ExecuteStoreCommand(stb.ToString());
                       db.SaveChanges();
                   }
               }
             
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
