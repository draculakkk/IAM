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
    public partial class ADGroupReport : BasePage
    {
        IAMEntities db = new IAMEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!base.ReturnUserRole.AD && !base.ReturnUserRole.Admin&&!base.ReturnUserRole.Leader)
                {
                    base.NoRole();
                }
               // Bind();
            }
        }

        void Bind()
        {
            List<V_AD_UserWorkGroup> listUserWorkGroup = new List<V_AD_UserWorkGroup>();
            string gonghao = txtgonghao.Text.Trim();
            string department = txtPartment.Text.Trim();
            string name = txtName.Text.Trim();
            string gangwei = txtgangwei.Text.Trim();

            string adusername = txtUserName.Text.Trim();
            string type = dlptype.SelectedValue;
            string jinyong = dplEnable.SelectedValue;
            DateTime? StartDate = txtStartDate.Text.Trim() == string.Empty ? (DateTime?)null : Convert.ToDateTime(txtStartDate.Text.Trim());
            DateTime? EndDate = txtEndDate.Text.Trim() == string.Empty ? (DateTime?)null : Convert.ToDateTime(txtEndDate.Text.Trim());
            string workgroupName = txtgroupname.Text.Trim();

            if (string.IsNullOrEmpty(txtStartDate.Text.Trim()) && !string.IsNullOrEmpty(txtEndDate.Text.Trim()))
            {
                Response.Write("<script>alert('必须填写失效日期从字段值');</script>"); return;
            }

            if (!string.IsNullOrEmpty(txtStartDate.Text.Trim()) && string.IsNullOrEmpty(txtEndDate.Text.Trim()))
            {
                Response.Write("<script>alert('必须填写失效日期至字段值');</script>"); return;
            }

            listUserWorkGroup = new IAMEntityDAL.V_AD_UserWorkGroupDAL().Get_V_AD_UserWorkGroupList(gonghao, department, name, gangwei, adusername, type, StartDate, EndDate, jinyong, workgroupName);

            AspNetPager1.RecordCount = listUserWorkGroup.Count;
            AspNetPager1.PageSize = base.PageSize;
            listUserWorkGroup = listUserWorkGroup.OrderBy(item => item.uwGroupName).Skip((AspNetPager1.CurrentPageIndex - 1) * base.PageSize).Take(base.PageSize).ToList();

            repeater1ADUserGrounp.DataSource = listUserWorkGroup.OrderBy(item=>item.uwGroupName);
            repeater1ADUserGrounp.DataBind();
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