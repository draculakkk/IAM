using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class Sys_UserName_ConflictResolutionDAL : BaseFind<Sys_UserName_ConflictResolution>
    {
        public enum SysStatus
        {
            新冲突 = 1,
            以IAM为主 = 2,
            以原系统为主 = 3,
            过期未解决 = 4
        }

        public int AddSysUserNameConflicResolution(Sys_UserName_ConflictResolution entity)
        {
            return Add(entity);
        }

        public int UpdateSysUserNameConflicResolution(Sys_UserName_ConflictResolution entity)
        {
            using (IAMEntities db = new IAMEntities())
            {
                db.Sys_UserName_ConflictResolution.Attach(entity);
                db.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Modified);
                return db.SaveChanges();
            }
        }

        public int DeleteAll()
        {
            List<Sys_UserName_ConflictResolution> list = NonExecute<List<Sys_UserName_ConflictResolution>>(db =>
            {
                return db.Sys_UserName_ConflictResolution.ToList();
            });
            IAMEntities dal = new IAMEntities();
            foreach (var item in list)
            {
                dal.Sys_UserName_ConflictResolution.Detach(item);
                dal.ObjectStateManager.ChangeObjectState(item, System.Data.EntityState.Deleted);
            }
            int _re = dal.SaveChanges();
            dal.Dispose();
            return _re;
        }

        /// <summary>
        /// 删除未解决的冲突问题
        /// </summary>
        /// <returns></returns>
        public void DeleteBySync(Unitity.SystemType systemtype)
        {
            var sql = "update Sys_UserName_ConflictResolution set state=4 where state=1 and systype='" + systemtype.ToString() + "'AND DATEDIFF(DAY,CreateTime,GETDATE())>=0";
            using (IAMEntities db = new IAMEntities())
            {
                db.ExecuteStoreCommand(sql);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 检查是当天否有同样的角色还未解决；
        /// </summary>
        /// <param name="username"></param>
        /// <param name="sysvalue"></param>
        /// <param name="iamvalue"></param>
        /// <param name="systype"></param>
        /// <param name="rolename"></param>
        /// <returns></returns>
        public Sys_UserName_ConflictResolution GetOne(string username, string sysvalue, string iamvalue, string systype, string rolename)
        {
            DateTime time = DateTime.Now;
            Sys_UserName_ConflictResolution tmp = null;
            try
            {
                using (IAMEntities db = new IAMEntities())
                {
                    var lis = db.Sys_UserName_ConflictResolution.Where(item => item.UserName == username && item.STATE == 1 && item.CollSysValue.Trim() == sysvalue && item.CollIAMValue.Trim() == iamvalue && item.SysType.Trim() == sysvalue).ToList();
                    tmp = lis.FirstOrDefault(item => (Convert.ToDateTime(item.CreateTime) - time).Days == 0 && item.CollName == rolename);
                }
                return tmp;
            }
            catch (Exception ex)
            {
                new LogDAL().AddsysErrorLog(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 检查当天是否产生同样的新账号，源系统中有账号情况
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Sys_UserName_ConflictResolution GetOneUser(string username, string systemname)
        {
            Sys_UserName_ConflictResolution tmp = null;
            DateTime time = DateTime.Now;
            using (IAMEntities db = new IAMEntities())
            {
                var lis = db.Sys_UserName_ConflictResolution.Where(item => item.UserName == username && item.CollSysValue == "源系统新增账号" && item.STATE == 1 && item.SysType == systemname).ToList();
                tmp = lis.FirstOrDefault(item => (Convert.ToDateTime(item.CreateTime) - time).Days == 0);
            }
            return tmp;
        }

        public int DeleteByKey(Guid id)
        {
            Sys_UserName_ConflictResolution module = NonExecute<Sys_UserName_ConflictResolution>(db =>
            {
                return db.Sys_UserName_ConflictResolution.FirstOrDefault(item => item.ID == id);
            });
            if (module != null)
            {
                using (IAMEntities db = new IAMEntities())
                {
                    db.Sys_UserName_ConflictResolution.Detach(module);
                    db.ObjectStateManager.ChangeObjectState(module, System.Data.EntityState.Deleted);
                    return db.SaveChanges();
                }
            }
            else
                return 0;
        }

        public List<Sys_UserName_ConflictResolution> ReturnList()
        {
            return NonExecute<List<Sys_UserName_ConflictResolution>>(db => { return db.Sys_UserName_ConflictResolution.ToList(); });
        }

        public List<Sys_UserName_ConflictResolution> ReturnList(string SystemType, int? State, string UserName, string neirong, out int count, int pagesize, int pageindex)
        {
            IAMEntities db = new IAMEntities();
            var list = db.Sys_UserName_ConflictResolution.Where(item => 1 == 1);
            if (!string.IsNullOrEmpty(SystemType))
                list = list.Where(item => item.SysType == SystemType);
            if (State != null)
                list = list.Where(item => item.STATE == State);
            if (!string.IsNullOrEmpty(UserName))
                list = list.Where(item => item.UserName == UserName);
            if (!string.IsNullOrEmpty(neirong))
                list = list.Where(item => item.UserName.Contains(neirong) || item.CollName.Contains(neirong) || item.CollIAMValue.Contains(neirong) || item.CollSysValue.Contains(neirong));
            list = list.Where(item => item.CollSysValue != "源系统新增账号");
            count = list.Count();
            list = list.OrderByDescending(item => item.CreateTime).Skip((pageindex - 1) * pagesize).Take(pagesize);
            return list.ToList();
        }

        public List<Sys_UserName_ConflictResolution> ReturnList(Unitity.SystemType? type, int state = 1)
        {

            if (type != null)
            {
                string typ = type.ToString();
                return NonExecute<List<Sys_UserName_ConflictResolution>>(db =>
                {
                    return db.Sys_UserName_ConflictResolution.Where(item => item.SysType.Trim() == typ && item.STATE == state).ToList();
                });
            }
            else
            {
                return NonExecute<List<Sys_UserName_ConflictResolution>>(db =>
                {
                    return db.Sys_UserName_ConflictResolution.Where(item => item.STATE == state).ToList();
                });
            }
        }

        public bool ExportExcel(string filepath, string newfilepath, Unitity.SystemType? type, int state)
        {
            List<Sys_UserName_ConflictResolution> list = ReturnList(type, state);
            if (list == null && list.Count <= 0)
                return false;
            System.Data.DataTable dt = list.Select(item => new
            {
                系统名称 = item.SysType,
                用户账号 = item.UserName,
                字段名称 = item.CollName,
                IAM系统 = item.CollIAMValue,
                应用系统 = item.CollSysValue,
                创建时间 = item.CreateTime,
                解决时间 = item.ApprovedTime,
                解决备注 = state == 1 ? "" : item.Remark
            }).ToDataTable();
            return OLEDBExcelHelper.OLEDBExcelHelper.Write(filepath, newfilepath, dt);
        }

        public List<string> GetSendSystemName()
        {
            using (var db = new IAMEntities())
            {
                return db.Sys_UserName_ConflictResolution.Where(item => item.STATE == 1).Select(x => x.SysType).Distinct().ToList();
            }
        }


    }
}
