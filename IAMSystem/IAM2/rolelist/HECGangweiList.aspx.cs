using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAM2.rolelist
{
    public partial class HECGangweiList : IAM.BasePage
    {
        BaseDataAccess.IAMEntities db = new BaseDataAccess.IAMEntities();
        public class vhec
        {
            public string COMPANY_FULL_NAME { get; set; }
            public string COMPANY_CODE { get; set; }
            public string UNIT_NAME { get; set; }
            public string UNIT_CODE { get; set; }
            public string POSITION_NAME { get; set; }
            public string POSTITION_CODE { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }
        void Bind()
        {
            string sql = @"SELECT DISTINCT MAX(a.COMPANY_CODE) COMPANY_CODE,MAX(a.COMPANY_FULL_NAME) COMPANY_FULL_NAME,MAX(b.UNIT_CODE) UNIT_CODE,MAX(b.UNIT_NAME) UNIT_NAME,MAX(c.POSTITION_CODE) POSTITION_CODE,MAX(c.POSITION_NAME) POSITION_NAME
FROM dbo.HEC_Company_Info  a INNER JOIN HEC_DepartMent_Info b ON a.COMPANY_CODE=b.COMPANY_CODE INNER JOIN HEC_Gangwei_Info c ON b.UNIT_CODE=c.UNIT_CODE 
WHERE b.ENABLED_FLAG='Y'AND c.ENABLED_FLAG='Y'
GROUP BY a.COMPANY_CODE,a.COMPANY_FULL_NAME,b.UNIT_CODE,b.UNIT_NAME,c.POSTITION_CODE,c.POSITION_NAME order by COMPANY_FULL_NAME,UNIT_NAME,POSTITION_CODE";
            var list = db.ExecuteStoreQuery<vhec>(sql);
            var list1 = list.ToList();
            AspNetPager1.RecordCount = list1.Count;
            AspNetPager1.PageSize = 25;
            repeater1.DataSource = list1.OrderBy(item => item.COMPANY_FULL_NAME).ThenBy(x => x.UNIT_NAME).ThenBy(x => x.POSITION_NAME).Skip((AspNetPager1.CurrentPageIndex - 1) * 17).Take(17).ToList();
            repeater1.DataBind();
            updatepagerhtml();
        }

        private void updatepagerhtml()
        {
            AspNetPager1.CustomInfoHTML = string.Format("共{0}页,当前第{1}页,共{2}条", AspNetPager1.PageCount, AspNetPager1.CurrentPageIndex, AspNetPager1.RecordCount);
        }

        protected void AspNetPager1_PageChanging(object sender, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            Bind();
        }
    }
}