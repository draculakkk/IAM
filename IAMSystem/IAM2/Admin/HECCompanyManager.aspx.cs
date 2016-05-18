using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseDataAccess;
using IAMEntityDAL;

namespace IAM.Admin
{
    public partial class HECCompanyManager :BasePage
    {
        HECCompanyInfoDAL _hecCompanyInfoServices = new HECCompanyInfoDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        void Bind()
        {
            string companyname = txtCompanyName.Text.Trim();
            int count=0;
            List<HEC_Company_Info> CompanyInfolist = _hecCompanyInfoServices.GetHECCompanyInfo(companyname,base.PageSize,AspNetPager1.CurrentPageIndex,out count);
            AspNetPager1.RecordCount = count;
            AspNetPager1.PageSize = base.PageSize;
            updatepagerhtml();
            repeater1HECCompanyInfo.DataSource = CompanyInfolist;
            repeater1HECCompanyInfo.DataBind();
            updatepagerhtml();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Bind();
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