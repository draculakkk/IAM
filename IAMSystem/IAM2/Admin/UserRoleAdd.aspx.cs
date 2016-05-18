using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAMEntityDAL;
using BaseDataAccess;
using System.Text;
namespace IAM.Admin
{
    public partial class UserRoleAdd : BasePage
    {
        UserRoleDAL _UserRoleServices = new UserRoleDAL();
        LogDAL _logservices = new LogDAL();
        bool IsUpdate
        {
            get
            {
                if (Request.QueryString["adname"] != null)
                    return true;
                else
                    return false;
            }
        }

        string ADName
        {
            get
            {
                if (Request.QueryString["adname"] != null)
                    return Request.QueryString["adname"];
                else
                    return string.Empty;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!base.ReturnUserRole.Admin&&!base.ReturnUserRole.Leader)
                {
                    base.NoRole();
                    return;
                }

                btnUpdate.Visible = IsUpdate;
                btnSave.Visible = !IsUpdate;
                BindUpdate();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string adname = txtADname.Text;
            if (string.IsNullOrEmpty(adname))
                Response.Write("<script>alert('账号名不能为空');</script>");
            else if (_UserRoleServices.IsExets(adname))
            {
                Response.Write("<script>alert('账号名不能重复');</script>");
            }
            else
            {
                UserRole item = new UserRole();
                item.roles = Roles();
                item.adname = adname;
                if (_UserRoleServices.AddUserRole(item) > 0)
                {
                    _logservices.AdduserActionLog(base.UserInfo.adname, "添加用户权限", "为用户" + adname + "添加了" + item.roles + "权限");
                    Response.Write("<script>alert('添加成功！');window.opener.location='userrolemanager.aspx';window.close();</script>");

                }
            }

        }

        string Roles()
        {
            StringBuilder stbroles = new StringBuilder();
            if (chxAD.Checked)
                stbroles.Append("AD,");
            if (chxAdmin.Checked)
                stbroles.Append("Admin,");
            if (chxEHR.Checked)
                stbroles.Append("EHR,");
            if (chxEndUser.Checked)
                stbroles.Append("EndUser,");
            if (chxHEC.Checked)
                stbroles.Append("HEC,");
            if (chxSAP.Checked)
                stbroles.Append("SAP,");
            if (chxTC.Checked)
                stbroles.Append("TC,");
            if (chkLeader.Checked)
                stbroles.Append("Leader,");
            return stbroles.ToString().TrimEnd(',');
        }

        void BindUpdate()
        {
            if (!IsUpdate)
                return;
            txtADname.Text = ADName;
            var entity = _UserRoleServices.GetUserRole(ADName);
            if (entity != null)
            {
                foreach (string s in entity.roles.Split(',').ToList())
                {
                    if (s == "EndUser")
                        chxEndUser.Checked = true;
                    if (s == "Admin")
                        chxAdmin.Checked = true;
                    if (s == "TC")
                        chxTC.Checked = true;
                    if (s == "SAP")
                        chxSAP.Checked = true;
                    if (s == "AD")
                        chxAD.Checked = true;
                    if (s == "EHR")
                        chxEHR.Checked = true;
                    if (s == "HEC")
                        chxHEC.Checked = true;
                    if (s == "Leader")
                        chkLeader.Checked = true;

                }
            }
        }





        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string adname = txtADname.Text;
            if (string.IsNullOrEmpty(adname))
                Response.Write("<script>alert('AD账号不能为空');</script>");
            else
            {
                UserRole item = new UserRole();
                item.roles = Roles();
                item.adname = adname;
                _UserRoleServices.UpdateUserRole(item);
                
                    _logservices.AdduserActionLog(base.UserInfo.adname, "更新用户权限", "为用户" + adname + "更新了" + item.roles + "权限");
                    Response.Write("<script>alert('修改成功！');window.opener.location='userrolemanager.aspx';window.close();</script>");
                
            }
        }
    }
}