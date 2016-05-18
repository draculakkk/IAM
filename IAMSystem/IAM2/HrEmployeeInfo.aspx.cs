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
    public partial class HrEmployeeInfo : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["gonghao"] != null)
            {
                string gonghao = Request.QueryString["gonghao"];
                HREmployee EmployeeModel = new IAMEntities().HREmployee.FirstOrDefault(item => item.code == gonghao);
                if (EmployeeModel != null)
                {
                    lblgonghao.Text = gonghao;
                    lblName.Text = EmployeeModel.name;
                    lblGangwei.Text = EmployeeModel.posts;
                    lblPhone.Text = EmployeeModel.moblePhone;
                    lblComeDate.Text = EmployeeModel.toPostsDate != null ? DateTime.Parse(EmployeeModel.toPostsDate.ToString()).ToShortDateString() : "";
                    lblPostDate.Text = EmployeeModel.leavePostsDate != null ? DateTime.Parse(EmployeeModel.leavePostsDate.ToString()).ToShortDateString() : "";
                    V_HrDepartment departmentModel = new IAMEntities().V_HrDepartment.FirstOrDefault(item => item.dept == EmployeeModel.dept);
                    if (departmentModel != null)
                    {
                        lblPartment.Text = departmentModel.name;
                        lblPrePartment.Text = departmentModel.shangjiName;
                    }
                }
                else
                {
                    Response.Write("<script>alert('该员工已离职或不存在');window.close();</script>");
                }
            }
        }
    }
}