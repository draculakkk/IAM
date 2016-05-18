using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseDataAccess;

namespace IAM.Report
{
    public partial class HECRoleReport : BasePage
    {
        
        IAMEntities db = new IAMEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!base.ReturnUserRole.HEC && !base.ReturnUserRole.Admin&&!base.ReturnUserRole.Leader)
                {
                    base.NoRole();
                }

               // BindHECUserRoleReport();
            }
        }

        void BindHECUserRoleReport()
        {
            List<V_HECUSER_Role> hecUserRoleLIst = new List<V_HECUSER_Role>();
            string gonghao = txtgonghao.Text.Trim();
            string name = txtname.Text.Trim();
            string department = txtdepartment.Text.Trim();
            string gangwei = txtgangwei.Text.Trim();

            string hecname = txtUserName.Text.Trim();
            string leixing = dpltype.SelectedValue;
            DateTime? startdate = txtStartDate.Text.Trim() == string.Empty ? (DateTime?)null : Convert.ToDateTime(txtStartDate.Text.Trim());
            DateTime? enddate = txtEndDate.Text.Trim() == string.Empty ? (DateTime?)null : Convert.ToDateTime(txtEndDate.Text.Trim());
            
            string rolename = txtrolename.Text.Trim();
            string companyname = txtCompanyName.Text.Trim();
            string jinyong = dpljinyong.SelectedValue;

            hecUserRoleLIst = new IAMEntityDAL.V_HECUSER_RoleDAL().Get_HECUser_RoleList(gonghao, name, department,gangwei, hecname,leixing,startdate, enddate, rolename, companyname,jinyong );
            AspNetPager1.RecordCount = hecUserRoleLIst.Count;
            AspNetPager1.PageSize = base.PageSize;
            hecUserRoleLIst = hecUserRoleLIst.OrderByDescending(item => item.rROLECODE).Skip((AspNetPager1.CurrentPageIndex-1)*base.PageSize).Take(base.PageSize).ToList();

            repeater1HECUserrole.DataSource = hecUserRoleLIst;
            repeater1HECUserrole.DataBind();
            updatepagerhtml();
           
        }

        private void updatepagerhtml()
        {
            AspNetPager1.CustomInfoHTML = string.Format("共{0}页,当前第{1}页,共{2}条", AspNetPager1.PageCount, AspNetPager1.CurrentPageIndex, AspNetPager1.RecordCount);
        }


        protected void AspNetPager1_PageChanging(object sender, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            BindHECUserRoleReport();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindHECUserRoleReport();
        }


    }
}