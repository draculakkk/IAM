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
    public partial class UserRoleManager : BasePage
    {
        UserRoleDAL _UserRoleServices = new UserRoleDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!base.ReturnUserRole.Admin && !base.ReturnUserRole.Leader)
                {
                    base.NoRole();
                }

                Bind();

            }
        }

        void Bind()
        {
            string adname = txtADname.Text;
            string rolesname = dplType.SelectedValue;
            int count = 0;
            List<UserRole> list = _UserRoleServices.ReturnUserRoleList(base.PageSize, AspNetPager1.CurrentPageIndex, adname, rolesname, out count);
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

        protected void btnDelete_command(object sender, CommandEventArgs e)
        {
            if (_UserRoleServices.DeleteUserRole(e.CommandArgument.ToString()) > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('删除成功！');window.location=window.location;", true);
            }
        }

        protected void btndelete_Command1(object sender, CommandEventArgs e)
        {
            _UserRoleServices.DeleteUserRole(e.CommandArgument.ToString());
            Response.Write("<script>alert('删除成功！');window.location=window.location;</script>");
        }
    }
}