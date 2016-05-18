using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAM
{
    public class HrCompanyEntity
    {
        public string CompanyKey { get; set; }//公司主键
        public string CompanyNumber { get; set; }//编码
        public string CompanyName { get; set; }//名称
        public string PreCompanyKey { get; set; }//上级公司主键

        /// <summary>
        /// 初始化HR公司信息
        /// </summary>
        public static Dictionary<string, HrCompanyEntity> PublicHrCompanyDefault
        {
            get
            {
                Dictionary<string, HrCompanyEntity> tmplist = new Dictionary<string, HrCompanyEntity>();
                tmplist.Add("0001", new HrCompanyEntity() { CompanyKey="0001", CompanyName="集团", CompanyNumber="0001" });
                tmplist.Add("1001", new HrCompanyEntity() { CompanyKey="1001", CompanyName="汇众总公司", CompanyNumber="1001", PreCompanyKey="0001"});
                tmplist.Add("1002", new HrCompanyEntity() {  CompanyKey="1002",CompanyName="汇众分公司", CompanyNumber="1002",PreCompanyKey="1001"});
                return tmplist;
            }
            
        }


        public static string CompanyNameByKey(string key)
        {
            HrCompanyEntity module = null;
            PublicHrCompanyDefault.TryGetValue(key,out module);
            if (module != null)
                return module.CompanyName;
            else
                return string.Empty;
        }
    }
}