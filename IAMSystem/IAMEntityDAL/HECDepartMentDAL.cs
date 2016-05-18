using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class HECDepartMentDAL : BaseFind<HEC_DepartMent_Info>
    {
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int NewUpdate(HEC_DepartMent_Info entity)
        {
            using (IAMEntities db = new IAMEntities())
            {
                db.HEC_DepartMent_Info.Attach(entity);
                db.ObjectStateManager.ChangeObjectState(entity, System.Data.EntityState.Modified);
                return db.SaveChanges();
            }
        }

        public int NewUpdate(HEC_DepartMent_Info entity, bool sql)
        {
            string sqll = string.Format(@"UPDATE HEC_DepartMent_Info SET UNIT_CODE='{0}',UNIT_NAME='{1}',PARENT_UNIT_CODE='{2}',PARENT_UNIT_NAME='{3}',COMPANY_CODE='{4}',COMPANY_NAME='{5}',
ENABLED_FLAG='{6}' WHERE ID='{7}'", entity.UNIT_CODE, entity.UNIT_NAME, entity.PARENT_UNIT_CODE, entity.PARENT_UNIT_NAME, entity.COMPANY_CODE, entity.COMPANY_NAME, entity.ENABLED_FLAG, entity.ID);
            using (IAMEntities db = new IAMEntities())
            {
                return db.ExecuteStoreCommand(sqll);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int NewDelete(Guid id)
        {
            using (IAMEntities db = new IAMEntities())
            {
                var tmp = db.HEC_DepartMent_Info.FirstOrDefault(item => item.ID == id);
                if (tmp != null)
                {
                    db.HEC_DepartMent_Info.DeleteObject(tmp);
                    db.ObjectStateManager.ChangeObjectState(tmp, System.Data.EntityState.Deleted);
                    return db.SaveChanges();
                }
                return 0;
            }
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <param name="count"></param>

        /// <returns></returns>
        public List<HEC_DepartMent_Info> GetList(string PARENT_UNIT_CODE, string Company_Code, int pagesize, int pageindex, out int count)
        {
            IAMEntities db = new IAMEntities();
            var list = db.HEC_DepartMent_Info.Where(item => 1 == 1);
            if (!string.IsNullOrEmpty(PARENT_UNIT_CODE))
                list = list.Where(x => x.PARENT_UNIT_CODE == PARENT_UNIT_CODE);
            if (!string.IsNullOrEmpty(Company_Code))
                list = list.Where(x => x.COMPANY_CODE == Company_Code);
            count = list.Count();
            list = list.OrderBy(x => x.UNIT_CODE).Skip((pageindex - 1) * pagesize).Take(pagesize);
            return list.ToList();
        }

        /// <summary>
        /// 查询可用公司对应获取部门
        /// </summary>
        /// <param name="PARENT_UNIT_CODE"></param>
        /// <param name="Company_Code"></param>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <returns></returns>
        public List<HEC_DepartMent_Info> GetList(string PARENT_UNIT_CODE, string Company_Code)
        {
            IAMEntities db = new IAMEntities();
            string sql = "SELECT NEWID() ID,* from (SELECT DISTINCT UNIT_CODE,UNIT_NAME,PARENT_UNIT_CODE,PARENT_UNIT_NAME,COMPANY_CODE,COMPANY_NAME,ENABLED_FLAG FROM dbo.HEC_DepartMent_Info WHERE COMPANY_CODE='" + Company_Code + "' AND ENABLED_FLAG='Y') a  ORDER BY UNIT_CODE ";
            var list = db.ExecuteStoreQuery<HEC_DepartMent_Info>(sql);
            return list.ToList();
            //var list = db.HEC_DepartMent_Info.Where(item => 1 == 1);
            //if (!string.IsNullOrEmpty(PARENT_UNIT_CODE))
            //    list = list.Where(x => x.PARENT_UNIT_CODE == PARENT_UNIT_CODE);
            //else
            //    list = list.Where(x => x.PARENT_UNIT_CODE == null || x.PARENT_UNIT_CODE == "");
            //if (!string.IsNullOrEmpty(Company_Code))
            //    list = list.Where(x => x.COMPANY_CODE == Company_Code);
            //list = list.Where(x => x.ENABLED_FLAG == "Y");
            //return list.ToList();
        }




        public void SyncDepartMent(List<HEC_DepartMent_Info> SyncList)
        {
            IAMEntities db = new IAMEntities();
            foreach (var x in SyncList)
            {
                var tmp = db.HEC_DepartMent_Info.FirstOrDefault(y => y.UNIT_CODE == x.UNIT_CODE && y.PARENT_UNIT_CODE == x.PARENT_UNIT_CODE && y.COMPANY_CODE == x.COMPANY_CODE);
                if (tmp == null)
                    Add(x);
                else
                {
                    //if (!tmp.COMPANY_NAME.Trim().Equals(x.COMPANY_NAME.Trim()))
                    //{
                    //    new LogDAL().AddMasterDataModifyLog(Unitity.SystemType.HEC, tmp.COMPANY_NAME, x.COMPANY_NAME, "HEC_DepartMent_Info", "公司名称");
                    //}
                    //if (!tmp.UNIT_NAME.Trim().Equals(x.UNIT_NAME.Trim()))
                    //{
                    //    new LogDAL().AddMasterDataModifyLog(Unitity.SystemType.HEC, tmp.UNIT_NAME, x.UNIT_NAME, "HEC_DepartMent_Info", "部门名称");
                    //}

                    tmp.COMPANY_CODE = x.COMPANY_CODE;
                    tmp.COMPANY_NAME = x.COMPANY_NAME;
                    tmp.UNIT_CODE = x.UNIT_CODE;
                    tmp.UNIT_NAME = x.UNIT_NAME;
                    tmp.PARENT_UNIT_CODE = x.PARENT_UNIT_CODE;
                    tmp.PARENT_UNIT_NAME = x.PARENT_UNIT_NAME;
                    NewUpdate(tmp,true);
                }
            }
        }


    }
}
