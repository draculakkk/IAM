using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class HECRoleDAL : BaseFind<HEC_Role>
    {
        /// <summary>
        /// 添加 HEC 角色信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddHECRole(HEC_Role entity)
        {
            return Add(entity);
        }

        /// <summary>
        /// 更新 HEC角色信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateHECRole(HEC_Role entity)
        {
            return Update(entity);
        }

        /// <summary>
        /// 删除 HEC 角色信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int DeleteHECRole(HEC_Role entity)
        {
            return Delete(entity);
        }


        /// <summary>
        /// 获取 HEC 所有角色信息
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public List<HEC_Role> GetHECRole(int PageSize, int PageIndex, out int Count)
        {
            List<HEC_Role> ListRole = NonExecute<List<HEC_Role>>(db => {
                return db.HEC_Role.ToList();
            });
            Count = ListRole.Count;
            return ListRole.OrderByDescending(item=>item.START_DATE).Skip((PageIndex-1)*PageSize).Take(PageSize).ToList();
        }

        /// <summary>
        /// 同步HEC 角色信息
        /// </summary>
        /// <param name="entity"></param>
        public void SyncHECRole(HEC_Role entity)
        {
            try {
                HEC_Role model = NonExecute<HEC_Role>(db => {
                    return db.HEC_Role.FirstOrDefault(item=>item.ROLE_CODE==entity.ROLE_CODE);
                });
                if (model != null)
                {
                    //if (!model.ROLE_NAME.Trim().Equals(entity.ROLE_NAME.Trim()))
                    //{
                    //    new LogDAL().AddMasterDataModifyLog(Unitity.SystemType.HEC, model.ROLE_NAME, entity.ROLE_NAME.Trim(), "HEC_Role", "角色名称");
                    //}
                    UpdateHECRole(entity);
                }
                else
                    AddHECRole(entity);
            }
            catch (Exception ex)
            {
                new LogDAL().AddsysErrorLog(ex.ToString());
                throw ex;
            }
        }

    }
}
