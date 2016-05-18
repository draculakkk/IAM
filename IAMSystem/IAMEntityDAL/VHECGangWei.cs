using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class VHECGangWei : BaseFind<V_HEC_Gangwei>
    {
        IAMEntities db = new IAMEntities();
        public List<V_HEC_Gangwei> GetVHECGangwei(string gonghao, string xingming, string bumen, string gangwei, string heczhanghao, string zhanghaoleixing, string gongsi, string hecbumen, string hecgangwei, string shifouzhugangwei, string shifouqiyong, int pagesize, int pageindex, out int count, bool isUser = true)
        {
            var list = db.V_HEC_Gangwei.Where(x => 1 == 1);
            if (!string.IsNullOrEmpty(gonghao))
                list = list.Where(x => x.gonghao.Contains(gonghao));
            if (!string.IsNullOrEmpty(xingming))
                list = list.Where(x => x.xingming.Contains(xingming));
            if (!string.IsNullOrEmpty(bumen))
                list = list.Where(x => x.bumen.Contains(bumen));
            if (!string.IsNullOrEmpty(gangwei))
                list = list.Where(x => x.hrgangwei.Contains(gangwei));
            if (!string.IsNullOrEmpty(heczhanghao))
                list = list.Where(x => x.zhanghao.Contains(heczhanghao));
            if (!string.IsNullOrEmpty(zhanghaoleixing))
                list = list.Where(x => x.zhanghaoleixing.Contains(zhanghaoleixing));
            if (!string.IsNullOrEmpty(gongsi))
                list = list.Where(x => x.COMPANY_CODE.Contains(gongsi) || x.COMPANY_NAME.Contains(gongsi));
            if (!string.IsNullOrEmpty(hecbumen))
                list = list.Where(x => x.UNIT_CODE.Contains(bumen) || x.UNIT_NAME.Contains(bumen));
            if (!string.IsNullOrEmpty(hecgangwei))
                list = list.Where(x => x.POSITION_CODE.Contains(hecgangwei) || x.POSITION_NAME.Contains(hecgangwei));
            if (!string.IsNullOrEmpty(shifouzhugangwei))
                list = list.Where(x => x.PRIMARY_POSITION_FLAG == shifouzhugangwei);
            if (!string.IsNullOrEmpty(shifouqiyong))
                list = list.Where(x => x.ENABLED_FLAG == shifouqiyong);
            count = list.Count();
            if (isUser)
                list = list.OrderBy(x => x.zhanghao).Skip((pageindex - 1) * pagesize).Take(pagesize);
            else
                list = list.OrderBy(x => x.POSITION_NAME).Skip((pageindex - 1) * pagesize).Take(pagesize);
            return list.ToList();
        }
        public bool ReturnExcelExport(string filepath, string newfilepath, string gonghao, string xingming, string bumen, string gangwei, string heczhanghao, string zhanghaoleixing, string gongsi, string hecbumen, string hecgangwei, string shifouzhugangwei, string shifouqiyong, bool isUser = true)
        {
            int count = 0;
            var list = GetVHECGangwei(gonghao, xingming, bumen, gangwei, heczhanghao, zhanghaoleixing, gongsi, hecbumen, hecgangwei, shifouzhugangwei, shifouqiyong, int.MaxValue, 1, out  count, isUser);
            System.Data.DataTable dt = null;
            if (isUser)
            {
                dt = list.Select(x => new
                {
                    工号 = x.gonghao,
                    部门 = x.bumen,
                    姓名 = x.xingming,
                    岗位 = x.hrgangwei,
                    账号 = x.zhanghao,
                    类型 = x.zhanghaoleixing,
                    公司代码 = x.COMPANY_CODE,
                    公司名称 = x.COMPANY_NAME,
                    部门代码 = x.UNIT_CODE,
                    部门名称 = x.UNIT_NAME,
                    岗位代码 = x.POSITION_CODE,
                    岗位名称 = x.POSITION_NAME,
                    是否主岗位 = x.PRIMARY_POSITION_FLAG == "Y" ? "是" : "否",
                    是否启用 = x.ENABLED_FLAG == "Y" ? "是" : "否"
                }).ToDataTable();
            }
            else
            {
                dt = list.Select(x => new
                {
                    公司代码 = x.COMPANY_CODE,
                    公司名称 = x.COMPANY_NAME,
                    部门代码 = x.UNIT_CODE,
                    部门名称 = x.UNIT_NAME,
                    岗位代码 = x.POSITION_CODE,
                    岗位名称 = x.POSITION_NAME,
                    工号 = x.gonghao,
                    部门 = x.bumen,
                    姓名 = x.xingming,
                    岗位 = x.hrgangwei,
                    账号 = x.zhanghao,
                    类型 = x.zhanghaoleixing,
                    是否主岗位 = x.PRIMARY_POSITION_FLAG == "Y" ? "是" : "否",
                    是否启用 = x.ENABLED_FLAG == "Y" ? "是" : "否"
                }).ToDataTable();
            }
            return OLEDBExcelHelper.OLEDBExcelHelper.Write(filepath, newfilepath, dt);
        }
    }


}
