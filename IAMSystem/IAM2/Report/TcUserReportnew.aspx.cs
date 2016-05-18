using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAM.Report
{
    public partial class TcUserReportnew :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!base.ReturnUserRole.TC && !base.ReturnUserRole.Admin&&!base.ReturnUserRole.Leader)
                {
                    base.NoRole();
                }
                //Bind();
            }
        }
        void Bind()
        {
            string gonghao = txtgonghao.Text.Trim();
            string Name = txtname.Text.Trim();
            string department = txtdepartment.Text.Trim();
            string UserName = txtTCusername.Text.Trim();
            int? xukejibie = ddlxukejibi.SelectedValue == string.Empty ? (int?)null : Convert.ToInt32(ddlxukejibi.SelectedValue);
            int? userStatus = ddlUserStatus.SelectedValue == string.Empty ? (int?)null : Convert.ToInt32(ddlUserStatus.SelectedValue);
            string gangwei = txtgangwei.Text.Trim();
            string groupname = txtGroupName.Text.Trim();
            string rolename = txtRoleName.Text.Trim();
            string leixing = dpltype.SelectedValue;
            string juesejinyong = groupjinyong.SelectedValue;
            var list = new IAMEntityDAL.V_TCReportDAL().Get_V_TC_Repost_List_as_User(gonghao, Name, department,gangwei, UserName, xukejibie, userStatus, leixing, groupname, rolename,juesejinyong);

            AspNetPager1.RecordCount = list.Count;
            AspNetPager1.PageSize = base.PageSize;
            list = list.OrderBy(item=>item.uUserID).Skip((AspNetPager1.CurrentPageIndex-1)*base.PageSize).Take(base.PageSize).ToList();
            repeaterTCUserInfo.DataSource = list; 
            repeaterTCUserInfo.DataBind();
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

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Bind();
        }
    }
}