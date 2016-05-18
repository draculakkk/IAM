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
    public partial class AD_Zhiji_WorkGroupManager : BasePage
    {
        IAMEntities db = new IAMEntities();
        AD_Zhiji_WorkGroupDAL _adzhijiservices = new AD_Zhiji_WorkGroupDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (base.ReturnUserRole.AD == false && base.ReturnUserRole.Admin == false && base.ReturnUserRole.Leader == false)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('无权查看该页面');window.close();", true);
                return;
            }

            if (!IsPostBack)
            {
                Bind();
                btnAddNew.Disabled = !base.ReturnUserRole.Admin;
                foreach (RepeaterItem i in repeaterADzhijiWorkgroup.Items)
                {
                    //System.Web.UI.HtmlControls.HtmlButton btn = (System.Web.UI.HtmlControls.HtmlButton)i.FindControl("inpedit");
                   // btn.Disabled = !base.ReturnUserRole.Admin;
                }
            }
        }

        void Bind()
        {
            string zhiji = txtjobzhiji.Text.Trim();
            string workgroup = txtworkgroup.Text.Trim();
            string jinyong = chkFalse.Checked ? "True" : "False";
            var list = _adzhijiservices.GetList().Where(item => item.p1 == jinyong).ToList();//查询可用职级工作组
            list = list.Where(item=>item.Zhiji!=""&&item.WorkGroup!="").ToList();
            if (!string.IsNullOrEmpty(zhiji))
                list = list.Where(item => item.Zhiji.Contains(zhiji)).ToList();
            if (!string.IsNullOrEmpty(workgroup))
                list = list.Where(item => item.WorkGroup.Contains(workgroup)).ToList();
            repeaterADzhijiWorkgroup.DataSource = list.OrderBy(item=>item.WorkGroup);
            repeaterADzhijiWorkgroup.DataBind();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Bind();
        }
    }
}