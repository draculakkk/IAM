using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class ADWorkGroupDAL:BaseFind<AD_workGroup>
    {
        /// <summary>
        /// 添加AD_workGroup
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddADWorkGroup(AD_workGroup entity)
        {
            var mo = NonExecute<AD_workGroup>(db => {
                return db.AD_workGroup.FirstOrDefault(item=>item.NAME==entity.NAME);
            });
            if (mo == null)

                return Add(entity);
            else
                return 1;
        }


        /// <summary>
        /// 更新AD_workGroup
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateADWorkGroup(AD_workGroup entity)
        {
            using (IAMEntities db = new IAMEntities())
            {
                db.AD_workGroup.Attach(entity);
                db.ObjectStateManager.ChangeObjectState(entity,System.Data.EntityState.Modified);
                return db.SaveChanges();
            }
        }

        /// <summary>
        /// 返回AD_workGroup信息
        /// </summary>
        /// <param name="count">返回AD_workGroup集合数目</param>
        /// <returns></returns>
        public List<AD_workGroup> GetADWorkGroupList(out int count)
        {
            List<AD_workGroup> listwork = NonExecute<List<AD_workGroup>>(item => {
                return item.AD_workGroup.ToList();
            });
            listwork = listwork.Where(item=>item.IsDelete!=false).ToList();
            count = listwork.Count;
            return listwork.OrderByDescending(item => item.SyncDate).ToList();
        }


        /// <summary>
        /// 同步AD_workGroup 信息
        /// </summary>
        /// <param name="entity">AD_workGroup 实体类</param>
        public void SyncUserWorkGroup(AD_workGroup entity)
        {
            try
            {
                AD_workGroup module = NonExecute<AD_workGroup>(db =>
                {
                    return db.AD_workGroup.FirstOrDefault(item => item.NAME==entity.NAME);
                });
                if (module != null)
                {
                    entity.Id = module.Id;
                    UpdateADWorkGroup(entity);
                }
                else
                    AddADWorkGroup(entity);
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
