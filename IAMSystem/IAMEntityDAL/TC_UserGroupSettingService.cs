using BaseDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAMEntityDAL
{
    public class TC_UserGroupSettingService:BaseFind<TC_UserGroupSetting>
    {
        private readonly IAMEntities db;

        public TC_UserGroupSettingService()
        {
            db = new IAMEntities();
        }

        public List<TC_UserGroupSetting> GetLst()
        {
            return db.TC_UserGroupSetting.ToList();
        }

        public TC_UserGroupSetting GetOne(TC_UserGroupSetting entity)
        {
            return db.TC_UserGroupSetting.FirstOrDefault(o => o.ID == entity.ID);
        }

        public bool Insert(TC_UserGroupSetting entity)
        {
            if (entity != null)
            {
                db.TC_UserGroupSetting.AddObject(entity);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UpdateGroup(TC_UserGroupSetting entity)
        {
            if (entity != null)
            {
                using (IAMEntities db1 = new IAMEntities())
                {
                    db1.TC_UserGroupSetting.Attach(entity);
                    db1.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Modified);
                    db1.SaveChanges();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(TC_UserGroupSetting entity)
        {
            if (entity != null)
            {
                db.TC_UserGroupSetting.Attach(entity);
                db.TC_UserGroupSetting.DeleteObject(entity);
                
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
