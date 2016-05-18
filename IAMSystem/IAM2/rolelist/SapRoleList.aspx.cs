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
    public partial class SapRoleList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!base.ReturnUserRole.SAP && !base.ReturnUserRole.Admin)
                {
                    base.NoRole();
                }
                int count;
                List<SAP_Role> list = new SAPRoleDAL().GetSapRole(int.MaxValue,int .MaxValue,out count);
                repeaterSapRole.DataSource = list;
                repeaterSapRole.DataBind();
            }
        }
    }
}