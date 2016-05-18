using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class HECCompanyInfoDAL:BaseFind<HEC_Company_Info>
    {
        /// <summary>
        /// 添加 HEC 公司信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddHECCompanyInfo(HEC_Company_Info entity)
        {
            return Add(entity);
        }

        /// <summary>
        /// 更新 HEC 公司信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int UpdateCompanyInfo(HEC_Company_Info entity)
        {
            return Update(entity);
        }

        /// <summary>
        /// 删除 HEC 公司信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int DeleteCompanyInfo(HEC_Company_Info entity)
        {
            return Delete(entity);
        }

        /// <summary>
        /// 获取所有 HEC 公司信息
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<HEC_Company_Info> GetHECCompanyInfo(string companyName, int PageSize, int PageIndex, out int count)
        {
            List<HEC_Company_Info> listcompany = NonExecute<List<HEC_Company_Info>>(db => {
                return db.HEC_Company_Info.ToList();
            });
            if (!string.IsNullOrEmpty(companyName))
                listcompany = listcompany.Where(item=>item.COMPANY_FULL_NAME.Contains(companyName)||item.COMPANY_SHORT_NAME.Contains(companyName)).ToList();

            count = listcompany.Count;
            return listcompany.OrderByDescending(item => item.START_DATE).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }

        /// <summary>
        /// 同步HEC公司信息
        /// </summary>
        /// <param name="entity"></param>
        public void SyncHECCompany(HEC_Company_Info entity)
        {
            try {
                HEC_Company_Info model = NonExecute<HEC_Company_Info>(db =>
                {
                    return db.HEC_Company_Info.FirstOrDefault(item => item.COMPANY_CODE == entity.COMPANY_CODE);
                });
                if (model != null)
                {
                    //if (!model.COMPANY_FULL_NAME.Trim().Equals(entity.COMPANY_FULL_NAME.Trim()))
                    //{
                    //    new LogDAL().AddMasterDataModifyLog(Unitity.SystemType.HEC, model.COMPANY_FULL_NAME, entity.COMPANY_FULL_NAME.Trim(), "HEC_Company_Info", "公司全名称");
                    //}
                    //if (!model.COMPANY_SHORT_NAME.Trim().Equals(entity.COMPANY_SHORT_NAME.Trim()))
                    //{
                    //    new LogDAL().AddMasterDataModifyLog(Unitity.SystemType.HEC, model.COMPANY_SHORT_NAME, entity.COMPANY_SHORT_NAME.Trim(), "HEC_Company_Info", "公司简称");
                    //}

                    UpdateCompanyInfo(entity);
                }
                else
                    AddHECCompanyInfo(entity);
            }
            catch (Exception ex)
            {
                new LogDAL().AddsysErrorLog(ex.ToString());
                throw ex;
            }
        }
    }
}
