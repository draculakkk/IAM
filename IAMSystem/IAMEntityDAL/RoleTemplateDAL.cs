using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class RoleTemplateDAL : BaseFind<RoleTemplate>
    {
        private int AddRoleTemplate(RoleTemplate entity)
        {
            return Add(entity);
        }

        public int UpdateRoleTemplate(RoleTemplate entity)
        {
            using (IAMEntities db = new IAMEntities())
            {
                db.RoleTemplate.Attach(entity);
                db.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Modified);
                return db.SaveChanges();
            }
        }

        /// <param name="isnewuser">True 为添加一个新用户  False 为添加一个已有的账号</param>
        public int CreateRoleTemplate(RoleTemplate entity, List<RoleTemplateInfo> list, bool isnewuser)
        {
            if (AddRoleTemplate(entity) > 0)
                return new RoleTemplateInfoDAL().CreateRoleTemplateInfo(list, entity.ID, isnewuser);
            else
                return 0;

        }

        public bool Exeits(string rolename)
        {
            var en = NonExecute<RoleTemplate>(db => {
                return db.RoleTemplate.FirstOrDefault(item=>item.TemplateName==rolename);
            });
            if (en == null)
                return true;
            else
                return false;
        }

    }

    public class RoleTemplateInfoDAL : BaseFind<RoleTemplateInfo>
    {
        private void AddRoleTemplateInfo(RoleTemplateInfo entity)
        {
            Add(entity);
        }

        public int DeleteRoleTemplateInfo(RoleTemplateInfo entity)
        {
            using (IAMEntities db = new IAMEntities())
            {

                db.DeleteObject(db.RoleTemplateInfo.FirstOrDefault(itme=>itme.ID==entity.ID));
                return db.SaveChanges();
            }
        }

        private void UpdateRoleTemplageInfo(RoleTemplateInfo entity)
        {
            using (IAMEntities db = new IAMEntities())
            {
                db.RoleTemplateInfo.Attach(entity);
                db.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Modified);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="TemplateID"></param>
        /// <param name="isnewuser">True 为添加一个新用户  False 为添加一个已有的账号</param>
        /// <returns></returns>
        public int CreateRoleTemplateInfo(List<RoleTemplateInfo> list,Guid TemplateID,bool isnewuser)
        {
            try {
                IAMEntities db = new IAMEntities();
                var listt = db.RoleTemplateInfo.Where(item=>item.TemplateID==TemplateID).ToList();
                foreach (var item in list)
                {
                    item.TemplateID = TemplateID;
                    if (item.p1 != "员工"&&isnewuser==false)
                    {
                        var enti = listt.FirstOrDefault(i => i.p2 == item.p2 && i.SystemName == item.SystemName);
                        if (enti == null)
                            throw new Exception("用户名不存在");
                    }

                    var entit = listt.FirstOrDefault(a=>a.TemplateID==TemplateID&&a.SystemName==item.SystemName&&a.RoleName==item.RoleName);
                    if (entit == null)
                    {
                        AddRoleTemplateInfo(item);
                    }                  
                    
                }
                return 1;
            }
            catch (Exception ex)
            { 
#if DEBUG
                throw ex;
#else
                 return 0;
#endif
            }
           
        }
    }

}
