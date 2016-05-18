using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseDataAccess;
using IAM.BLL;

namespace CCBMP.BackEnd.Control
{
    public partial class Left1 : System.Web.UI.UserControl
    {

        UserRole userentity = new UserRole();
        public SystemModule UserRoleModule = new SystemModule();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                userentity = (UserRole)Session["userinfo"];
                UserRoleModule=UserRoleManager.Query(userentity);
            }
        }

        

        

    }
}