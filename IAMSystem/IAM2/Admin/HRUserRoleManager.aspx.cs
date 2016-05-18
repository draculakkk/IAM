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
    public partial class HRUserRoleManager : BasePage
    {
        HRsm_user_roleDAL _hrsmuserroleservices = new HRsm_user_roleDAL();
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
            string compangykey = dplCompany.SelectedValue;
            string username=string.IsNullOrEmpty(txtUserName.Text)?string.Empty:txtUserName.Text.Trim();
            List<V_HRSm_User_Role> list = _hrsmuserroleservices.VHRSmUserRole(base.PageSize, AspNetPager1.CurrentPageIndex,compangykey,username, out count);
            AspNetPager1.PageSize = base.PageSize;
            AspNetPager1.RecordCount = count;
            repeater1UserRole.DataSource = list;
            repeater1UserRole.DataBind();
            updatepagerhtml();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Bind();
        }


        protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
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