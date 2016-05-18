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
    public partial class AD_Department_WorkGroupManager : BasePage
    {
        AD_Department_WorkGroupDAL _addepartmentServices = new AD_Department_WorkGroupDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
            btnCreate.Disabled = !base.ReturnUserRole.Admin;
            btnQuery.Enabled = base.ReturnUserRole.AD;
            if (base.ReturnUserRole.AD == false && base.ReturnUserRole.Admin == false&&base.ReturnUserRole.Leader==false)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('无权查看该页面');window.close();", true);
            }
        }

        void Bind()
        {
            string hrdept = txthrdepartment.Text.Trim();
            string addept = txtadepartment.Text.Trim();
            string center = txtcenter.Text.Trim();
            string keshi = txtkeshi.Text.Trim();
            string dept = txtDepartment.Text.Trim();
            string isjinyong = chkFalse.Checked ? "True" : "False";
            var list = _addepartmentServices.GetList().Where(item=>item.p1==isjinyong).ToList();
            if (hrdept != string.Empty)
            {
                list = list.Where(item=>item.HrDepartment!=null).ToList();
                list = list.Where(item => item.HrDepartment.Contains(hrdept)).ToList();
            }

            if (addept != string.Empty)
            {
                list = list.Where(item=>item.AdDepartment!=null).ToList();
                list = list.Where(item => item.AdDepartment.Contains(addept)).ToList();
            }

            if (center != string.Empty)
            {
                list = list.Where(item=>item.Center!=null).ToList();
                list = list.Where(item => item.Center.Contains(center)).ToList();
            }

            if (keshi != string.Empty)
            {
                list = list.Where(item=>item.KeShi!=null).ToList();
                list = list.Where(item => item.KeShi.Contains(keshi)).ToList();
            }

            if (dept != string.Empty)
            {
                list = list.Where(item=>item.Department!=null).ToList();
                list = list.Where(item => item.Department.Contains(dept)).ToList();
            }
            
            AspNetPager1.RecordCount=list.Count ;
            AspNetPager1.PageSize = base.PageSize;

            repeaterADDepartmentWorkgroup.DataSource =list.OrderBy(item=>item.ordercolumn).Skip((AspNetPager1.CurrentPageIndex-1)*base.PageSize).Take(base.PageSize).ToList() ;
            repeaterADDepartmentWorkgroup.DataBind();
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

        protected void repeaterADDepartmentWorkgroup_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "up")
            {
                var old = _addepartmentServices.GetOne(e.CommandArgument.ToString());
                var upen = _addepartmentServices.Get_UpOne(e.CommandArgument.ToString());
                if (upen != null)
                {
                    int? after = upen.ordercolumn;
                    int? before = old.ordercolumn;
                    old.ordercolumn = after;
                    upen.ordercolumn = before;
                    _addepartmentServices.UpdateAd_Department_WorkGroup(old);
                    _addepartmentServices.UpdateAd_Department_WorkGroup(upen);
                }
                else
                {
                    Response.Write("<script>alert('已在最上方');</script>");
                }
            }
            else if (e.CommandName == "down")
            {
                var old = _addepartmentServices.GetOne(e.CommandArgument.ToString());
                var upen = _addepartmentServices.Get_DownOne(e.CommandArgument.ToString());
                if (upen != null)
                {
                    int? after = upen.ordercolumn;                    
                    int? before = old.ordercolumn;
                    old.ordercolumn = after;
                    upen.ordercolumn = before;
                    _addepartmentServices.UpdateAd_Department_WorkGroup(old);
                    _addepartmentServices.UpdateAd_Department_WorkGroup(upen);
                }
                else
                {
                    Response.Write("<script>alert('已在最下方');</script>");
                }
            }
            Bind();
        }
    }
}