using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseDataAccess;
using IAMEntityDAL;

namespace IAM.rolelist
{
    public partial class HECRoleList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!base.ReturnUserRole.HEC && !base.ReturnUserRole.Admin)
                {
                    base.NoRole();
                }
                int count;
                List<HEC_Role> list = new HECRoleDAL().GetHECRole(int.MaxValue,int.MaxValue,out count);
                repeaterHecRole.DataSource = list;
                repeaterHecRole.DataBind();
            }
        }
    }
}