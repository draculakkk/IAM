using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
   public class HECUserDAL:BaseFind<HEC_User>
    {
       public int AddHECUser(HEC_User entity)
       {
           return Add(entity);
       }

       public void SyncUser(HEC_User entity)
       {
           HEC_User e = NonExecute<HEC_User>(db =>
           {
               return db.HEC_User.FirstOrDefault(item => item.User_CD.Trim() == entity.User_CD.Trim());

           });
           if (e == null)
           {
               if (base.IsFirstTime)
               {
                   e = entity;
                   AddHECUser(entity);
               }
               else
               {
                   if(new Sys_UserName_ConflictResolutionDAL().GetOneUser(entity.User_CD,"HEC")==null)
                   new DeferencesSlution.UserRoleAlignmentValue().IsAddNewNotInIAM<HEC_User>(entity, entity.User_CD, Unitity.SystemType.HEC, "HEC_User", "", "IAM系统中无该账号", "源系统新增账号");
               }
           }
           else
           {
               AddDeference(e,entity,Unitity.SystemType.HEC,"User_CD",e.User_CD,e.User_CD);

           }

       }

       public int UpdateHECUser(HEC_User entity)
       {
           HEC_User e = NonExecute<HEC_User>(db => {
               return db.HEC_User.FirstOrDefault(item=>item.User_CD.Trim()==entity.User_CD.Trim());

           });
           if (e != null)
           {
               e = entity;
           }

           using (IAMEntities db = new IAMEntities())
           {
               db.HEC_User.Attach(e);
               db.ObjectStateManager.ChangeObjectState(e, System.Data.EntityState.Modified);
              return db.SaveChanges();
           }
       }

       


       public HEC_User GetOneHECUser(string userCD)
       {
           return NonExecute<HEC_User>(db => {
               return db.HEC_User.FirstOrDefault(item=>item.User_CD.Trim()==userCD);
           });
       }


       public int UpdateOrCreate(HEC_User entity)
       {
           HEC_User module = GetOneHECUser(entity.User_CD);
           if (module != null)
             return  UpdateHECUser(entity);
           else
             return  AddHECUser(entity);
       }

       public List<HEC_User> GetHECUser(string userCD,string Description,int PageSize, int PageIndex, out int count)
       {
           List<HEC_User> listuser = NonExecute<List<HEC_User>>(db => {
               return db.HEC_User.ToList();
           });
           if (!string.IsNullOrEmpty(userCD))
               listuser = listuser.Where(item=>item.User_CD.Trim()==userCD).ToList();
           if (!string.IsNullOrEmpty(Description))
               listuser = listuser.Where(item=>item.DESCRIPTION.Trim()==Description).ToList();
           count = listuser.Count;
           return listuser.OrderByDescending(item => item.createTime).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
       }
    }
}
