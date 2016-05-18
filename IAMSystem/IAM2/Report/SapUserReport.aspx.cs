using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseDataAccess;
using IAMEntityDAL;

namespace IAM.Report
{
    public partial class SapUserReport : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!base.ReturnUserRole.SAP && !base.ReturnUserRole.Admin&&!base.ReturnUserRole.Leader)
                {
                    base.NoRole();
                }
                //Bind();
            }
        }

        void Bind()
        {
            string gonghao = txtgonghao.Text.Trim();
            string name = txtname.Text.Trim();
            string department = txtdepartment.Text.Trim();
            string gangwei = txtgangwei.Text.Trim();
            string sapname = txtusername.Text.Trim();
            string startdates = txtStartDates.Text.Trim();
            string enddates = txtEndDates.Text.Trim();
            string roleid = txtRoleID.Text.Trim();
            string rolename = txtRoleName.Text.Trim();
            string userType = dplUserType.SelectedValue;
            string leixing = dplleixing.SelectedValue;
            string startdatee = txtStartDatee.Text.Trim();
            string enddatee = txtEndDatee.Text.Trim();

            List<V_Sap_UserRoleReport> sapuserlist = new V_Sap_UserRoleReportDAL().GetV_Sap_UserRoleReport(gonghao, name, department,gangwei, sapname,leixing, startdates, enddates, roleid, rolename, userType,startdatee,enddatee);
            AspNetPager1.RecordCount=sapuserlist.Count;
            AspNetPager1.PageSize=base.PageSize;
            var list = sapuserlist.OrderBy(item => item.mgonghao).Skip((AspNetPager1.CurrentPageIndex-1)*base.PageSize).Take(base.PageSize) ;
            repeater1SAPUserrole.DataSource = list; 
            repeater1SAPUserrole.DataBind();
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

        public void btnQuery_Click(object sender, EventArgs e)
        {
            Bind();
        }
    }
}