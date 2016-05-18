using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Script.Serialization;
using BaseDataAccess;
using IAMEntityDAL;

namespace IAM2.BLL
{
    public class HEC_Gangwei_Ajax
    {
        static string GetJson(object _obj)
        {
            JavaScriptSerializer java = new JavaScriptSerializer();
            java.MaxJsonLength = int.MaxValue;
            StringBuilder stb = new StringBuilder();
            java.Serialize(_obj, stb);
            return stb.ToString();
        }

        private static string HECCompanyInfo()
        {
            int count;
            var list = new HECCompanyInfoDAL().GetHECCompanyInfo(string.Empty, int.MaxValue, 1, out count);
            return GetJson(list);
        }

        private static string HECDepartMent(HttpContext context)
        {
            string PARENT_UNIT_CODE = context.Request["PARENT_UNIT_CODE"];
            string CompanyCode = context.Request["COMPANY_CODE"];
            var list = new HECDepartMentDAL().GetList(PARENT_UNIT_CODE, CompanyCode);
            return GetJson(list);
        }

        private static string HECGangwei(HttpContext context)
        {
            string UNIT_CODE = context.Request["UNIT_CODE"];
            string CompanyCode = context.Request["CompanyCode"];
            var list = new HECGangweiDAL().GetList(UNIT_CODE,CompanyCode);
            return GetJson(list);
        }

        public static string ResponseJson(HttpContext context)
        {
            string type = context.Request.QueryString["type"];
            switch (type)
            {
                case "Company": return GetJson(new { mess = HECCompanyInfo() });
                case "ParentDepartMent": return GetJson(new { mess = HECDepartMent(context) });
                case "DepartMent": return GetJson(new { mess = HECDepartMent(context) });
                case "Gangwei": return GetJson(new { mess = HECGangwei(context) });
                default: return GetJson(new { mess = "参数报错" });
            }
        }

    }
}