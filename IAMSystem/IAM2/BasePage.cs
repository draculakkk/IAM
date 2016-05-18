using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using BaseDataAccess;

namespace IAM
{
    public partial class BasePage : Page
    {
        private UserRole _userinfo;
        protected UserRole UserInfo
        {
            get { _userinfo = (UserRole)Session["userinfo"]; return _userinfo; }
            set { _userinfo = value; }
        }

        public BLL.SystemModule ReturnUserRole
        {
            get {
                return BLL.UserRoleManager.Query(UserInfo);
            }
        }

        protected int PageSize
        {
            get
            {
                return 20;
            }
        }

        protected void NoRole()
        {
            if (ReturnUserRole.AD || ReturnUserRole.Admin || ReturnUserRole.EHR || ReturnUserRole.TC || ReturnUserRole.SAP || ReturnUserRole.HEC||ReturnUserRole.Leader)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('你无权限查看该页面');window.location.href='./HREmployeeManager.aspx';", true);
            }
            if (ReturnUserRole.EndUser)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('你无权限查看该页面');window.location.href='/report/ReportByUserCode.aspx';", true);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!IsPostBack)
            {
                Error += new EventHandler(BasePage_Error);
                if (Session["userinfo"] == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                else
                    UserInfo = (UserRole)Session["userinfo"];
            }
            base.OnLoad(e);
        }

        void BasePage_Error(object sender, EventArgs e)
        {
            Exception errormess = Server.GetLastError();
            IAMEntityDAL.LogDAL _logservices = new IAMEntityDAL.LogDAL();
            _logservices.AddsysErrorLog(errormess.ToString());
#if DEBUG
            Response.Redirect("~/error.aspx?message=" +HttpUtility.UrlEncode( errormess.ToString()));
            
#else
Response.Redirect("error.aspx?message=系统错误<br/>请联系系统管理员;");
            _logservices.AddsysErrorLog(errormess.ToString());
#endif
        }
    }
}