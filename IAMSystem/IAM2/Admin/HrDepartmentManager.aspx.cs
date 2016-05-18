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
    public partial class HrDepartmentManager : BasePage
    {
        HRDepartmentDAL _hrdepartmentServices = new HRDepartmentDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();  
            }
        }

        void Bind()
        {
            int count;

            List<V_HrDepartment> list = _hrdepartmentServices.VDepartmentList(base.PageSize, AspNetPager1.CurrentPageIndex, out count);
            AspNetPager1.PageSize = base.PageSize;
            AspNetPager1.RecordCount = count;
            repeater1HrDepartment.DataSource = list;
            repeater1HrDepartment.DataBind();
            updatepagerhtml();
        }

        protected void AspNetPager1_PageChanging(object sender, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            Bind();
        }

        private void updatepagerhtml()
        {
            AspNetPager1.CustomInfoHTML = string.Format("共{0}页,当前第{1}页", AspNetPager1.PageCount, AspNetPager1.CurrentPageIndex);
        }
    }
}