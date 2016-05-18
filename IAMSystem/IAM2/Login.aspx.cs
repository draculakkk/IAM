using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAMEntityDAL;
using BaseDataAccess;

namespace IAM
{
    public partial class Login : System.Web.UI.Page
    {

#if DEBUG
        public bool isDebug = true;
#else 
        public bool isDebug = false;
#endif
        UserRoleDAL _userrole = new UserRoleDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Session.Clear();
                Session.RemoveAll();

#if DEBUG
                //btnButton_Click(sender, new EventArgs());
#else
btnButton_Click(sender,new  EventArgs ());
#endif
            }
        }

        protected void btnButton_Click(object sender, EventArgs e)
        {

#if DEBUG
            string adname = loginname.Text;

#else
            string adname = HttpContext.Current.Request.LogonUserIdentity.Name.Split('\\').LastOrDefault();

#endif
            if (!string.IsNullOrEmpty(adname))
            {
                UserRole RoleEntity = _userrole.GetUserRole(adname);


                if (RoleEntity != null)
                {
                    Session["userinfo"] = RoleEntity;
                    Response.Redirect("~/LayoutBase.aspx");
                }
                else
                {
                    RoleEntity = new UserRole();
                    RoleEntity.adname = adname;
                    RoleEntity.roles = "EndUser";
                    Session["userinfo"] = RoleEntity;
                    Response.Redirect("~/LayoutBase.aspx");
                }
            }
        }
    }
}