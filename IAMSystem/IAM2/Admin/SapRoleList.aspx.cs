using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseDataAccess;

namespace IAM.Admin
{
    public partial class SapRoleList : BasePage
    {
        public string fdajfdlajfd;
        protected void Page_Load(object sender, EventArgs e)
        {
            btnYes.Disabled = !base.ReturnUserRole.Admin;
            if (!base.ReturnUserRole.Admin && !base.ReturnUserRole.SAP&&!base.ReturnUserRole.Leader)
            {
                ClientScript.RegisterStartupScript(this.GetType(),"","alert('你无权限查看该页面');window.close();",true);
            }
                int count = 0;
                List<SAP_Role> list = new IAMEntityDAL.SAPRoleDAL().GetSapRole(int.MaxValue,1,out count).OrderBy(item=>item.ROLEID).ToList();

                System.Text.StringBuilder stb = new System.Text.StringBuilder();
                stb.Append("<select id=\"ddlRoleList\">");
                stb.Append("<option value=\"\"></option>");

                foreach (var item in list)
                {
                    //ListItem lii = new ListItem(item.ROLEID,item.ROLEID.Trim()+"^"+item.ROLENAME.Trim());
                    stb.Append("<option value=\"" + item.ROLEID.Trim() + "^" + item.ROLENAME.Trim() + "\">" + item.ROLEID.Trim()+ "</option>");
                }
                stb.Append("</select>");
                fdajfdlajfd = stb.ToString();
        }


    }
}