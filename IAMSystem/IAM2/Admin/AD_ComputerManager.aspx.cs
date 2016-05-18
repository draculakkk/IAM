using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAMEntityDAL;
using BaseDataAccess;
namespace IAM.Admin
{
    public partial class AD_ComputerManager : BasePage
    {
        public bool IsAdmin = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!base.ReturnUserRole.Admin && !base.ReturnUserRole.AD&&!base.ReturnUserRole.Leader)
                {
                    Response.Write("<script>alert('你无权限查看该页面');window.location.href='./hremployeemanager.aspx';</script>");
                    return;
                }
                //Bind();
            }
            IsAdmin = base.ReturnUserRole.Admin;

        }

        void Bind()
        {
            string gonghao = txtgonghao.Text.Trim();
            string name = txtuname.Text.Trim();
            string gangwei = txtgongwei.Text.Trim();
            string deptname = txtDepartment.Text.Trim();

            string computername = txtName.Text.Trim();
            string type = dplType.SelectedValue;
            string jinyong = dpljinyong.SelectedValue;
            List<v_adComputerInfo> list = new v_adComputerInfoDAL().GetList(gonghao,name,deptname,gangwei,computername,type,jinyong);
            AspNetPager1.RecordCount = list.Count;
            AspNetPager1.PageSize = base.PageSize;
            list = list.OrderBy(item => item.aName).Skip((AspNetPager1.CurrentPageIndex - 1) * base.PageSize).Take(base.PageSize).ToList();
            repeater1AdComputerInfo.DataSource = list;
            repeater1AdComputerInfo.DataBind();
            updatepagerhtml();
        }

        public string EidtString(object name, object gonghao)
        {

            return string.Format("<input type=\"button\" value=\"编辑\"  style='margin-right:7px;' onclick=\"AddComputer('{0}','{1}');\"/>",name,gonghao);
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