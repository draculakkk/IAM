using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAMEntityDAL;
using BaseDataAccess;

namespace IAM.rolelist
{
    public partial class TcRoleList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!base.ReturnUserRole.TC && !base.ReturnUserRole.Admin)
                {
                    base.NoRole();
                }
                int count;
                List<TC_Role> list = new TCRoleDAL().GetRoleList(out count);
                repeaterTcRole.DataSource = list.OrderBy(item=>item.RoleName).ToList();
                repeaterTcRole.DataBind();
            }
        }
    }
}