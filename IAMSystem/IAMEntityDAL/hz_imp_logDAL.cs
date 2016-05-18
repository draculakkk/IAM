using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
	public class hz_imp_LogDAL:BaseQuery
	{
        BaseFind<hz_imp_log> _hzImpLogServices = new BaseFind<hz_imp_log>();
        public int AddHzImpLog(hz_imp_log entity)
        {
            return _hzImpLogServices.Add(entity);
        }

        public int UpdateHzImpLog(hz_imp_log entity)
        {
            return _hzImpLogServices.Update(entity);
        }


        public List<hz_imp_log> HzIpmLogList(int PageSize, int PageIndex, out int count)
        {
            var list = NonExecute<List<hz_imp_log>>(db => {
                return db.hz_imp_log.ToList();
            });
            count = list.Count;
            return list.OrderByDescending(item=>item.errortype).Skip((PageIndex-1)*PageSize).Take(PageSize).ToList();
        }
	}
}
