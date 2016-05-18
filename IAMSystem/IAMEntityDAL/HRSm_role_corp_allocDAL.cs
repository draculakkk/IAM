using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;


namespace IAMEntityDAL
{
   public class HRSm_role_corp_allocDAL:BaseQuery
    {
        BaseFind<HRSm_role_corp_alloc> _DMLServices = new BaseFind<HRSm_role_corp_alloc>();
        public int AddHRsmRoleCorpAlloc(HRSm_role_corp_alloc entity)
        {
            return _DMLServices.Add(entity);
        }

        public int UpdateHRsmRoleCorpAlloc(HRSm_role_corp_alloc entity)
        {
            return _DMLServices.Update(entity);
        }

        public void UpdateHRsmRoleCorpAlloc()
        {
            List<HRSm_role_corp_alloc> list = NonExecute<List<HRSm_role_corp_alloc>>(db=>db.HRSm_role_corp_alloc.ToList());
            foreach (var item in list)
            {
                item.isSync = false;
                UpdateHRsmRoleCorpAlloc(item);
            }
        }


        /// <summary>
        /// 同步公司与角色关系
        /// </summary>
        /// <param name="module"></param>
        public void SyncHRsmRoleCorpAlloc(HRSm_role_corp_alloc module)
        {
            HRSm_role_corp_alloc entity = NonExecute<HRSm_role_corp_alloc>(db=>db.HRSm_role_corp_alloc.FirstOrDefault(item=>item.Pk_role_corp_alloc==module.Pk_role_corp_alloc));
            if (entity != null)
                UpdateHRsmRoleCorpAlloc(module);
            else
                AddHRsmRoleCorpAlloc(module);
        }


        public List<HRSm_role_corp_alloc> HRSmRoleCorpAlloc(int PageSize, int PageIndex, out int count)
        {
            var list = NonExecute<List<HRSm_role_corp_alloc>>(db => {
                return db.HRSm_role_corp_alloc.ToList();
            });
            count = list.Count;
            return list.OrderByDescending(item=>item.Dr).Skip((PageIndex-1)*PageSize).Take(PageSize).ToList();
        }
    }
}
