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
    public partial class AD_Department_WorkGroupCreate : BasePage
    {
        IAMEntities db = new IAMEntities();
        List<V_HrDepartment> hrDepartmentList;
        List<V_HrDepartment> HRDepartmentList
        {
            get
            {

                if (ViewState["hrdepartment"] != null)
                {
                    hrDepartmentList = ViewState["hrdepartment"] as List<V_HrDepartment>;
                }
                else
                {
                    hrDepartmentList = db.V_HrDepartment.ToList();
                    ViewState["hrdepartment"] = hrDepartmentList;
                }
                return hrDepartmentList;
            }
            set
            {
                hrDepartmentList = value;
            }
        }

        bool IsUpdate
        {
            get
            {
                if (Request.QueryString["id"] == null)
                    return false;
                else
                    return true;
            }
        }

        Guid ModuleID
        {
            get
            {
                if (Request.QueryString["id"] == null)
                    return Guid.NewGuid();
                else
                    return new Guid(Request.QueryString["id"]);
            }
        }

        AD_Department_WorkGroupDAL _addepartmentgroupservices = new AD_Department_WorkGroupDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                
                if (IsUpdate)
                {
                    bind();
                }
                BindOrder();
                ControlButton();
            }


            if (base.ReturnUserRole.AD == false && base.ReturnUserRole.Admin == false && base.ReturnUserRole.Leader == false)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('无权查看该页面');window.close();", true);
            }
        }

        void BindOrder()
        {
            if (!IsUpdate)
            {
                int count = _addepartmentgroupservices.GetList().Count;
                lblOrder.Text = (count + 1).ToString();
            }
        
        }

        void ControlButton()
        {
            if (base.ReturnUserRole.Admin == false)
            {
                btnYes.Enabled = false;
            }
        }

        void bind()
        {
            AD_Department_WorkGroup mo = _addepartmentgroupservices.GetList().FirstOrDefault(item => item.ID == ModuleID);
            txtAdDepartment.Text = mo.AdDepartment;
            txtCenter.Text = mo.Center;
            txtkeshi.Text = mo.KeShi;
            txtEmailDatabase.Text = mo.EmailDataBase;
            txtDepartment.Text = mo.Department;
            txtHRDepartment.Text = mo.HrDepartment;
            chkFalse.Checked = bool.Parse(mo.p1);
            lblOrder.Text= mo.ordercolumn.ToString();
        }



        AD_Department_WorkGroup WorkGroupModule()
        {
            AD_Department_WorkGroup mo = new AD_Department_WorkGroup();
            mo.ID = ModuleID;
            mo.HrDepartment = txtHRDepartment.Text.Trim();
            mo.AdDepartment = txtAdDepartment.Text.Trim();
            mo.Center = txtCenter.Text.Trim();
            mo.KeShi = txtkeshi.Text.Trim();
            mo.Department = txtDepartment.Text.Trim();
            mo.EmailDataBase = txtEmailDatabase.Text.Trim();
            mo.p1 = chkFalse.Checked ? "True" : "False";
            mo.p2 = string.Format("{0}  {1}  {2}", mo.Center, mo.Department, mo.KeShi).Trim();
            mo.ordercolumn = Convert.ToInt32(lblOrder.Text);
            return mo;
        }

        bool ischeck(out string mess)
        {
            bool istrue = true;
            mess = "";
            if (string.IsNullOrEmpty(txtAdDepartment.Text.Trim()))
            {
                mess = "ad部门组不能为空";
                istrue = false;
            }
            else if (string.IsNullOrEmpty(
            txtEmailDatabase.Text.Trim()))
            {
                mess = "邮件数据库不能为空";
                istrue = false;
            }
            else if (string.IsNullOrEmpty(
            txtHRDepartment.Text.Trim()))
            {
                mess = "HR部门不能为空";
                istrue = false;
            }
            else if (string.IsNullOrEmpty(txtDepartment.Text.Trim()) && string.IsNullOrEmpty(txtCenter.Text.Trim()) && string.IsNullOrEmpty(txtkeshi.Text.Trim()))
            {
                mess = "中心、部门、科室 三个字段不能同时为空";
                istrue = false;
            }
            return istrue;
        }


        protected void btnYes_Click(object sender, EventArgs e)
        {
            string mess = "";
            if (!ischeck(out mess))
            {
                Response.Write("<script>alert('"+mess+"');</script>");
                return;
            }

            int _issu = 0;
            if (!IsUpdate)
            {
                _issu = _addepartmentgroupservices.AddAd_Department_WorkGroup(WorkGroupModule());
            }
            else
            {
                _issu = _addepartmentgroupservices.UpdateAd_Department_WorkGroup(WorkGroupModule());
            }

            if (_issu > 0)
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('操作成功');window.close();", true);
        }
    }
}