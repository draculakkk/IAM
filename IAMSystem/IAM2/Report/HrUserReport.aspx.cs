using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseDataAccess;

namespace IAM.Report
{
    public partial class HrUserReport : BasePage
    {
        IAMEntities db = new IAMEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // BindHRUserRoleReport();

                if (!base.ReturnUserRole.EHR && !base.ReturnUserRole.Admin&&!base.ReturnUserRole.Leader)
                {
                    base.NoRole();
                }
            }
        }

        void BindHRUserRoleReport()
        {
            List<V_HRSm_User_Role> hrUserRoleList = new List<V_HRSm_User_Role>();
            string gonghao = txtgonghao.Text.Trim();
            string name = txtname.Text.Trim();
            string department = txtdepartment.Text.Trim();
            string hrusername = txtUserName.Text.Trim();
            string logintype = txtLoginType.Text.Trim();
            string rolename = txtrolename.Text.Trim();
            string companyname = txtCompanyName.Text.Trim();
            string islock = dplLock.SelectedValue;
            string gangwei = txtgangwei.Text.Trim();
            string leixing = dplType.SelectedValue;
            hrUserRoleList = new IAMEntityDAL.V_HRSm_User_RoleDAL().GetV_HRSm_UserList(gonghao, name, department, hrusername, logintype, rolename, companyname, islock,gangwei,leixing);
            
            AspNetPager1.PageSize = base.PageSize;
            AspNetPager1.RecordCount = hrUserRoleList.Count;
            hrUserRoleList = hrUserRoleList.OrderBy(item => item.hrusUser_code).ThenBy(item => item.hrrRoleCode).Skip((AspNetPager1.CurrentPageIndex-1)*base.PageSize).Take(base.PageSize).ToList();
            repeaterHRUserRole.DataSource = hrUserRoleList; 
            repeaterHRUserRole.DataBind();
            updatepagerhtml();
            
        }


        private void updatepagerhtml()
        {
            AspNetPager1.CustomInfoHTML = string.Format("共{0}页,当前第{1}页,共{2}条", AspNetPager1.PageCount, AspNetPager1.CurrentPageIndex, AspNetPager1.RecordCount);
        }


        protected void AspNetPager1_PageChanging(object sender, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            BindHRUserRoleReport();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindHRUserRoleReport();
        }

    }
}