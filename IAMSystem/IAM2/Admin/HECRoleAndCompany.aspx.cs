using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseDataAccess;

namespace IAM
{
    public partial class HECRoleAndCompany : BasePage
    {
        IAMEntities db = new IAMEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                var companylist = from a in db.HEC_Company_Info orderby a.COMPANY_FULL_NAME ascending select new {name=a.COMPANY_CODE+"^"+a.COMPANY_FULL_NAME,text=a.COMPANY_FULL_NAME };
                var rolelist = from a in db.HEC_Role where a.ROLE_CODE != "RPL_PRESENTER" orderby a.ROLE_NAME ascending select new { name = a.ROLE_CODE + "^" + a.ROLE_NAME + "^" + a.START_DATE + "^" + a.END_DATE, text = a.ROLE_NAME };
                ListItem li = new ListItem("","");
                li.Selected = true;
                
                ddlCompany.DataSource = companylist;
                ddlCompany.DataValueField = "name";
                ddlCompany.DataTextField = "text";
                ddlCompany.DataBind();

                ddlRole.DataSource = rolelist;
                ddlRole.DataValueField = "name";
                ddlRole.DataTextField = "text";
                ddlRole.DataBind();
                ddlCompany.Items.Add(li);
                ddlRole.Items.Add(li);
                inputYes.Disabled = !base.ReturnUserRole.Admin;
                if (!base.ReturnUserRole.HEC&&!base.ReturnUserRole.Admin&&!base.ReturnUserRole.Leader)
                {
                    ClientScript.RegisterStartupScript(this.GetType(),"","alert('无权限查看该页面');window.close();",true);
                }
            }
        }
    }
}