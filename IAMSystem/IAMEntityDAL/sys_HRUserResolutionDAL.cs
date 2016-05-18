using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class sys_HRUserResolutionDAL : BaseFind<sys_HRUserResolution>
    {

        public int AddSysUserNameConflicResolution(sys_HRUserResolution entity)
        {
            return Add(entity);
        }

        public int UpdateSysUserNameConflicResolution(sys_HRUserResolution entity)
        {
            using (IAMEntities db = new IAMEntities())
            {
                db.sys_HRUserResolution.Attach(entity);
                db.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Modified);
                return db.SaveChanges();
            }
        }

        public int DeleteAll()
        {
            List<sys_HRUserResolution> list = NonExecute<List<sys_HRUserResolution>>(db =>
            {
                return db.sys_HRUserResolution.ToList();
            });
            IAMEntities dal = new IAMEntities();
            foreach (var item in list)
            {
                dal.sys_HRUserResolution.Detach(item);
                dal.ObjectStateManager.ChangeObjectState(item, System.Data.EntityState.Deleted);
            }
            int _re = dal.SaveChanges();
            dal.Dispose();
            return _re;
        }

        public int DeleteByKey(Guid id)
        {
            sys_HRUserResolution module = NonExecute<sys_HRUserResolution>(db =>
            {
                return db.sys_HRUserResolution.FirstOrDefault(item => item.id == id);
            });
            if (module != null)
            {
                using (IAMEntities db = new IAMEntities())
                {
                    db.sys_HRUserResolution.Detach(module);
                    db.ObjectStateManager.ChangeObjectState(module, System.Data.EntityState.Deleted);
                    return db.SaveChanges();
                }
            }
            else
                return 0;
        }

        public List<sys_HRUserResolution> ReturnList()
        {
            return NonExecute<List<sys_HRUserResolution>>(db => { return db.sys_HRUserResolution.ToList(); });
        }

    }
}
