using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class ADComputerDAL : BaseFind<AD_Computer>
    {
        /// <summary>
        /// 添加AD_Computer 信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddADComputer(AD_Computer entity)
        {
            var mo = NonExecute<AD_Computer>(db => {
                return db.AD_Computer.FirstOrDefault(item=>item.NAME==entity.NAME);
            });
            if (mo == null)
                return Add(entity);
            else
                return 1;
        }


        /// <summary>
        /// 更新AD_Computer信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateADComputer(AD_Computer entity)
        {
            using (IAMEntities db = new IAMEntities())
            {
                db.AD_Computer.Attach(entity);
                db.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Modified);
                return db.SaveChanges();
            }
        }


        /// <summary>
        /// 查询所有AD_Computer信息
        /// </summary>
        /// <param name="count">返回当前查询集合数目</param>
        /// <returns></returns>
        public List<AD_Computer> GetAdComputerList(out int count)
        {
            List<AD_Computer> list = NonExecute<List<AD_Computer>>(db =>
            {
                return db.AD_Computer.ToList();
            });
            list = list.Where(item => item.IsDelete != true).ToList();
            count = list.Count;
            return list.OrderByDescending(item => item.SyncDate).ToList();
        }


        public int CreateOrUpdate(List<AD_Computer> computerlist)
        {
            try
            {
                foreach (var item in computerlist)
                {
                    AD_Computer model = NonExecute<AD_Computer>(db =>
                    {
                        return db.AD_Computer.FirstOrDefault(a => a.NAME == item.NAME && a.DESCRIPTION == item.DESCRIPTION);
                    });
                    if (model != null)
                        UpdateADComputer(item);
                    else
                        AddADComputer(item);
                }
                return 1;
            }
            catch(Exception ex)
            {
                new LogDAL().AddsysErrorLog(ex.ToString());
                return 0;
            }
        }

        /// <summary>
        /// ad 同步计算机信息
        /// 2014-11-12 haiboax 第一次同步时直接添加进数据库，以后同步直接进入冲突解决方案
        /// </summary>
        /// <param name="entity"></param>
        public void SyncADComputer(AD_Computer entity)
        {
            try
            { 
                AD_Computer model = NonExecute<AD_Computer>(db =>
                {
                    return db.AD_Computer.FirstOrDefault(item => item.NAME == entity.NAME);
                });
                if (model != null)
                    new DeferencesSlution.Alignment_Value_Fun().AddConflic<AD_Computer>(model, entity, Unitity.SystemType.ADComputer, "NAME", model.NAME, model.NAME);
                else
                {
                    if (base.IsFirstTime)
                        AddADComputer(entity);
                    else
                    {
                        if(new Sys_UserName_ConflictResolutionDAL().GetOneUser(entity.NAME,"ADComputer")==null)
                        new DeferencesSlution.UserRoleAlignmentValue().IsAddNewNotInIAM<AD_Computer>(entity, entity.NAME, Unitity.SystemType.ADComputer, "AD_Computer", "", "IAM系统无该账号", "源系统新增账号");
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                new LogDAL().AddsysErrorLog("AD_Computer Insert"+ex.ToString());
                throw ex;
#else
                 new LogDAL().AddsysErrorLog(ex.ToString());
#endif

            }
        }


    }
}
