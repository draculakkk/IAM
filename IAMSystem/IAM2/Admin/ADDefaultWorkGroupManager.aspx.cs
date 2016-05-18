using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseDataAccess;
using IAMEntityDAL;

namespace IAM.Admin
{
    public partial class ADDefaultWorkGroupManager : BasePage
    {
        AD_DefaultWorkGroupDAL _services = new AD_DefaultWorkGroupDAL();
        public bool IsAdmin = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (base.ReturnUserRole.AD == false && base.ReturnUserRole.Admin == false && base.ReturnUserRole.Leader == false)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('无权查看该页面');window.location.href='./HREmployeeManager.aspx';", true);
                return;
            }
            if (!IsPostBack)
            {
                Bind();
                inputAddNew.Disabled = !base.ReturnUserRole.Admin;
                foreach (RepeaterItem i in repeaterDefaultWork.Items)
                {
                    //System.Web.UI.HtmlControls.HtmlButton inputedit = (System.Web.UI.HtmlControls.HtmlButton)i.FindControl("inputedit");
                    Button btnDelete = (Button)i.FindControl("btnDelete");
                    //inputedit.Disabled = !base.ReturnUserRole.Admin;
                    btnDelete.Enabled = base.ReturnUserRole.Admin;
                }
            }
            IsAdmin = base.ReturnUserRole.Admin;
        }
        void Bind()
        {
            string name = txtGroupName.Text.Trim();
            var list = _services.GetList();
            if (!string.IsNullOrEmpty(name))
                list = list.Where(item=>item.NAME==name).ToList();
            repeaterDefaultWork.DataSource = list;
            repeaterDefaultWork.DataBind();
        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            Guid id = new Guid(e.CommandArgument.ToString());
            if (_services.DeleteDefaultWorkGroup(id) >= 0)
            {
                Response.Write("<script>alert('删除成功');window.location.href=window.location;</script>");
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Bind();
        }
    }
}