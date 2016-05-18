using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseDataAccess;
using IAMEntityDAL;

namespace IAM.Report
{
    public partial class ADComputerReport : BasePage
    {
        public string pagetype;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!base.ReturnUserRole.AD&&!base.ReturnUserRole.Admin&&!base.ReturnUserRole.Leader)
                {
                    base.NoRole();
                }

                pagetype = Request.QueryString["type"];
                //Bind();
            }
        }

        void Bind()
        {
            pagetype = Request.QueryString["type"];
            string gonghao = txtgonghao.Text.Trim();
            string name = txtuname.Text.Trim();
            string gangwei = txtgangwei.Text.Trim();
            string computername = txtName.Text.Trim();
            string userty = dplType.SelectedValue;
            string jinyong = dpljinyong.SelectedValue;
            string workgroup = txtWorkgroup.Text.Trim();
            string dept = txtDepartment.Text.Trim();
            List<V_AdcomputerWorkGroupInfo> list = new V_AdcomputerWorkGroupInfoDAL().GetList(gonghao,name,dept,gangwei,computername,userty,workgroup,jinyong).OrderBy(item=>item.bgonghao).ToList();
            AspNetPager1.RecordCount = list.Count;
            AspNetPager1.PageSize = base.PageSize;
            list = list.OrderBy(item => item.aName).Skip((AspNetPager1.CurrentPageIndex - 1) * base.PageSize).Take(base.PageSize).ToList();
            repeater1AdComputerInfo.DataSource = list;
            repeater1AdComputerInfo.DataBind();
            updatepagerhtml();
        }

        private void updatepagerhtml()
        {
            AspNetPager1.CustomInfoHTML = string.Format("共{0}页,当前第{1}页,共{2}条", AspNetPager1.PageCount, AspNetPager1.CurrentPageIndex, AspNetPager1.RecordCount);
        }


        protected void AspNetPager1_PageChanging(object sender, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            Bind();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Bind();
        }
    }
}