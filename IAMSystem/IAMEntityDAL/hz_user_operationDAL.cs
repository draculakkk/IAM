using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class hz_user_operationDAL:BaseQuery
    {
        BaseFind<hz_user_operation> _DMLServices = new BaseFind<hz_user_operation>();
        public int AddHzzUserOperation(hz_user_operation entity)
        {
            return _DMLServices.Add(entity);
        }

        public int UpdateHzUserOperation(hz_user_operation entity)
        {
            return _DMLServices.Update(entity);
        }

        public List<hz_user_operation> HzUserOperation(int PageSize, int PageIndex, out int count)
        {
            var list = NonExecute<List<hz_user_operation>>(db => {
                return db.hz_user_operation.ToList();
            });
            count = list.Count;
            return list.OrderByDescending(item=>item.Able_time).Skip((PageIndex-1)*PageSize).Take(PageSize).ToList();
        }
    }
}
