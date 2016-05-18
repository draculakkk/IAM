using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
   public class HRSm_userDAL:BaseFind<HRSm_user>
    {
       BaseFind<HRSm_user> _DMLServices = new BaseFind<HRSm_user>();
       /// <summary>
       /// 添加HRsm_user信息
       /// </summary>
       /// <param name="entity"></param>
       /// <returns></returns>
       public int AddHRsmUser(HRSm_user entity)
       {
           return _DMLServices.Add(entity);
       }

       /// <summary>
       /// 更新HRsm_user信息
       /// </summary>
       /// <param name="entity"></param>
       /// <returns></returns>
       public int UpdateHRsmUser(HRSm_user entity)
       {
           using (IAMEntities db = new IAMEntities())
           {
               db.HRSm_user.Attach(entity);
               db.ObjectStateManager.ChangeObjectState(entity,System.Data.EntityState.Modified);
               return db.SaveChanges();
           }
       }

       public HRSm_user GetOne(string currid)
       {
           return NonExecute<HRSm_user>(db =>
           {
               return db.HRSm_user.FirstOrDefault(item => item.Cuserid == currid);
           });
       }

       /// <summary>
       /// 自行判断，当前HRsm_role实体是添加还是更新
       /// </summary>
       /// <param name="entity"></param>
       /// <returns></returns>
       public int AddOrUpdate(HRSm_user entity)
       {
           HRSm_user module = NonExecute<HRSm_user>(db => {
               return db.HRSm_user.FirstOrDefault(item=>item.Cuserid==entity.Cuserid);
           });
           if (module != null)
              return UpdateHRsmUser(entity);
           else
             return  AddHRsmUser(entity);
       }

       /// <summary>
       /// 批量更新 hrsm_user表
       /// </summary>
       public void UpdateHRsmUser()
       {
           List<HRSm_user> list = NonExecute<List<HRSm_user>>(db => {
               return db.HRSm_user.ToList();
           });

           foreach (var item in list)
           {
               item.isSync = false;
               UpdateHRsmUser(item);
           }
       }

       public List<HRSm_user> HrSmUserList(int PageSize, int PageIndex,string EmployeeCode,string EmployeeNumber,string EmployeeName,string LockedTag, out int count)
       {
           var list = NonExecute<List<HRSm_user>>(db => {
               return db.HRSm_user.ToList();
           });
           //if (!string.IsNullOrEmpty(EmployeeCode)) //工号查询，目前没有找到工号对应的字段
           //    list = list.Where(item=>item.User_code==EmployeeCode).ToList();
           if (!string.IsNullOrEmpty(EmployeeName))
               list = list.Where(item=>item.USER_name==EmployeeName).ToList();
           if (!string.IsNullOrEmpty(EmployeeNumber))
               list = list.Where(item => item.User_code==EmployeeNumber).ToList();
           if (!string.IsNullOrEmpty(LockedTag))
               list = list.Where(item => item.Locked_tag == LockedTag).ToList() ;
           count = list.Count;
           return list.OrderByDescending(item=>item.Able_time).Skip((PageIndex-1)*PageSize).Take(PageSize).ToList();
       }

       public List<HRSm_user> HrSmUserList()
       {
           var list = NonExecute<List<HRSm_user>>(db =>
           {
               return db.HRSm_user.ToList();
           });
           return list;
       }

       //2014-11-12  haiboax   修改用户在以后同步中，iam系统不存在的用户将以冲突的方式，存储，不直接写入数据库中
       public void SyncHRsmUser(HRSm_user entity)
       {
           try
           {
               HRSm_user newentity = NonExecute<HRSm_user>(db =>
               {
                   return db.HRSm_user.FirstOrDefault(b => b.User_code == entity.User_code);
               });

               if (newentity != null)
               {
                   if (newentity.Cuserid.Trim() != entity.Cuserid.Trim())
                   {
                       string sql = @"UPDATE dbo.HRsm_user_role SET Cuserid='" + entity.Cuserid + "' WHERE Cuserid='" + newentity.Cuserid + "' UPDATE dbo.HRSm_user SET p1='" + entity.Cuserid + "' ,Cuserid='"+entity.Cuserid+"' WHERE Cuserid='" + newentity.Cuserid + "'";
                       IAMEntities db = new IAMEntities();
                       db.ExecuteStoreCommand(sql);
                       db.SaveChanges();
                   }
                   new DeferencesSlution.Alignment_Value_Fun().AddConflic<HRSm_user>(newentity, entity, Unitity.SystemType.HR, "Cuserid", newentity.Cuserid, newentity.User_code);
                   
               }
               else
               {
                   if (base.IsFirstTime)
                   {
                       AddHRsmUser(entity);
                   }
                   else
                   {
                      if(new Sys_UserName_ConflictResolutionDAL().GetOneUser(entity.User_code,"HR")==null) 
                       new DeferencesSlution.UserRoleAlignmentValue().IsAddNewNotInIAM<HRSm_user>(entity, entity.User_code, Unitity.SystemType.HR, "HRSm_user", "", "", "源系统新增账号");
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
