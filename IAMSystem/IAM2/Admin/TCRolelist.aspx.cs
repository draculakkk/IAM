using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAM.Admin
{
    public partial class TCRolelist : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnYes.Disabled = !base.ReturnUserRole.TC;
                if (!base.ReturnUserRole.TC && !base.ReturnUserRole.Admin&&!base.ReturnUserRole.Leader)
                {
                    base.NoRole();
                }

                int count = 0;
                repeaterRole.DataSource = new IAMEntityDAL.TCRoleDAL().GetRoleList(out count).OrderBy(item=>item.RoleName);
                repeaterRole.DataBind();
            }
        }
    }
}