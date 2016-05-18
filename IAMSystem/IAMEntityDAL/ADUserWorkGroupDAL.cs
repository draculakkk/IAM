using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
   public class ADUserWorkGroupDAL:BaseFind<AD_UserWorkGroup>
    {
       /// <summary>
        /// 添加AD_UserWorkGroup信息
       /// </summary>
       /// <param name="entity"></param>
       /// <returns></returns>
       public int AddADUserWorkGroup(AD_UserWorkGroup entity)
       {
           try
           {
               var mo = NonExecute<AD_UserWorkGroup>(db => {
                   return db.AD_UserWorkGroup.FirstOrDefault(item=>item.GroupName==entity.GroupName&&item.Uid==entity.Uid);
               });
               if (mo == null)
                   return Add(entity);
               else
                   return 1;
           }
           catch(Exception ex)
           {
               new LogDAL().AddsysErrorLog(ex.ToString());
               return 1;
           }
       }

       /// <summary>
       /// <summary>
       /// 更新AD_UserWorkGroup信息
       /// </summary>
       /// <param name="entity"></param>
       /// <returns></returns>
       /// </summary>
       /// <param name="entity"></param>
       /// <returns></returns>
       public int UpdateADUserWorkGroup(AD_UserWorkGroup entity)
       {
           using (IAMEntities db = new IAMEntities())
           {
               db.AD_UserWorkGroup.Attach(entity);
               db.ObjectStateManager.ChangeObjectState(entity,System.Data.EntityState.Modified);
               return db.SaveChanges();
           }
       }


       public int DeleteAdUserWorkGroup(Guid id)
       {
           using (IAMEntities db = new IAMEntities())
           {
               var module = db.AD_UserWorkGroup.FirstOrDefault(item => item.ID == id);
               if (module != null)
               {
                   db.AD_UserWorkGroup.DeleteObject(module);
                   db.ObjectStateManager.ChangeObjectState(module, System.Data.EntityState.Deleted);
                   return db.SaveChanges();
               }
               else
               {
                   return 0;
               }
           }
       }

       /// <summary>
       /// 返回AD_UserWorkGroup信息
       /// </summary>
       /// <param name="entity">返回AD_UserWorkGroup集合数目</param>
       /// <returns></returns>
       /// </summary>
       /// <param name="Count"></param>
       /// <returns></returns>
       public List<AD_UserWorkGroup> GetADUserWorkGroupList(out int Count)
       {
           List<AD_UserWorkGroup> listGroup = NonExecute<List<AD_UserWorkGroup>>(db => {
               return db.AD_UserWorkGroup.ToList();
           });

           Count = listGroup.Count;
           return listGroup.OrderByDescending(item => item.Uid).ToList();
       }
        

       /// <summary>
       /// 同步Ad_UserWorkGroup 信息
       /// </summary>
       /// <param name="entity">Ad_UserWorkGroup 实体类</param>
       public void SyncUserWorkGroup(List<AD_UserWorkGroup> list)
       {
           try
           {
               int count;
               var listold = GetADUserWorkGroupList(out count).OrderBy(x=>x.Uid).ToList();

               var listkekongzu = new ADWorkGroupDAL().GetADWorkGroupList(out count);
               var listmorenzu = new AD_DefaultWorkGroupDAL().GetList();
               var listzhijizu = new AD_Zhiji_WorkGroupDAL().GetList();
               var listbumenzu = new AD_Department_WorkGroupDAL().GetList();

               foreach (var entity in list)
               {
                   if (base.IsFirstTime)
                      AddADUserWorkGroup(entity);
                   else
                   {
                       
                           var tmp = listold.FirstOrDefault(item => item.GroupName == entity.GroupName && item.Uid == entity.Uid && item.isdr != 1);

                           try
                           {
                               if (tmp.Uid == "")
                               {
                               }
                           }
                           catch
                           {
                               try
                               {
                                   var tmpkekong = listkekongzu.FirstOrDefault(x => x.NAME == entity.GroupName);
                                   var tmpzhiji = listzhijizu.FirstOrDefault(x => x.WorkGroup == entity.GroupName);
                                   var tmpbumen = listbumenzu.FirstOrDefault(x => x.AdDepartment == entity.GroupName);
                                   var tmpmoren = listmorenzu.FirstOrDefault(x => x.NAME == entity.GroupName);
                                   if (tmpkekong == null && tmpzhiji == null && tmpbumen == null && tmpmoren == null)
                                   {
                                       AddADUserWorkGroup(entity);
                                   }
                                   else
                                   {
                                       new DeferencesSlution.UserRoleAlignmentValue().IsAddNewNotInIAM<AD_UserWorkGroup>(entity, entity.Uid, Unitity.SystemType.AD, "AD_UserWorkGroup", entity.GroupName, "IAM系统无该组权限", "源系统中有该组权限");
                                   }
                               }
                               catch
                               {

                               }

                           }
                       
                   }
               }
                
               foreach (var entity in listold)
               {
                   if (entity.isdr == 1)
                       continue;
                   var tmp = list.FirstOrDefault(item=>item.Uid==entity.Uid&&item.GroupName==entity.GroupName&&item.isdr!=1);
                   if (tmp == null)
                   {
                       if (entity.Uid == "wangjinxin1")
                       {
                           string aa = entity.Uid;
                       }

                       var tmpkekong = listkekongzu.FirstOrDefault(x => x.NAME == entity.GroupName);
                       var tmpzhiji = listzhijizu.FirstOrDefault(x => x.WorkGroup == entity.GroupName);
                       var tmpbumen = listbumenzu.FirstOrDefault(x => x.AdDepartment == entity.GroupName);
                       var tmpmoren = listmorenzu.FirstOrDefault(x => x.NAME == entity.GroupName);
                       if (tmpkekong == null && tmpzhiji == null && tmpbumen == null && tmpmoren == null)
                       {
                           continue;
                       }
                       else
                       {
                           new DeferencesSlution.UserRoleAlignmentValue().IsAddNewNotInIAM<AD_UserWorkGroup>(entity, entity.Uid, Unitity.SystemType.AD, "AD_UserWorkGroup", entity.GroupName, "IAM系统有该组权限", "源系统中无该组权限");
                       }
                   }
               }
           }
           catch (Exception ex)
           {
#if DEBUG
               new LogDAL().AddsysErrorLog("ad 工作组同步，报错了；"+ex.ToString());
               throw ex;
#else
                 new LogDAL().AddsysErrorLog(ex.ToString());
#endif
           }
       }

    }
}
