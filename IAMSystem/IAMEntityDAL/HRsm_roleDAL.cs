using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class HRsm_roleDAL : BaseQuery
    {
        public enum RoleResourceType
        {
            主体账簿类型 = 1, 公司资源类型 = 2, 复合类型 = 3
        }

        BaseFind<HRsm_role> _DMLServices = new BaseFind<HRsm_role>();
        public int AddHRsm_Role(HRsm_role entity)
        {

            return _DMLServices.Add(entity);
        }

        public int UpdateHRsm_Role(HRsm_role entity)
        {
            return _DMLServices.Update(entity);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <returns></returns>
        public void UpdateHRsm_Role()
        {
            var list = NonExecute<List<HRsm_role>>(db =>
            {
                return db.HRsm_role.ToList();
            });

            foreach (HRsm_role item in list)
            {
                item.isSync = false;
                UpdateHRsm_Role(item);
            }
        }


        public List<HRsm_role> HRsmRoleList(out int count)
        {
            var list = NonExecute<List<HRsm_role>>(db =>
            {
                return db.HRsm_role.ToList();
            });
            count = list.Count;
            return list.OrderByDescending(item => item.Resource_type).ToList();
        }


        /// <summary>
        /// 同步HR角色信息
        /// </summary>
        /// <param name="entity"></param>
        public void SyncHRsmRole(HRsm_role entity)
        {
            HRsm_role module = NonExecute<HRsm_role>(db =>
            {
                return db.HRsm_role.FirstOrDefault(item => item.Pk_role == entity.Pk_role);
            });
            if (module != null)
            {
                UpdateHRsm_Role(entity);
            }
            else
            {
                AddHRsm_Role(entity);
            }
        }
    }
}
