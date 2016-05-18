using BaseDataAccess;
using IAMEntityDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAM.ConflictResolution
{
    public partial class HRUserResolution1 : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!base.ReturnUserRole.Admin)
                {
                    base.NoRole();
                }
                Bind();
            }
        }

        void Bind()
        {
            var list = new sys_HRUserResolutionDAL().ReturnList().Where(item =>
            {
                if (dlpsysType.SelectedValue == "") return true;
                else return item.state == dlpsysType.SelectedValue;
            }).ToList();
            AspNetPager1.RecordCount = list.Count;
            AspNetPager1.PageSize = base.PageSize;
            list=dlpsysType.SelectedValue == "已处理" ? list.OrderByDescending(item => item.updatetime).ToList() : list.OrderBy(item => item.cardNo).ToList();
            repeaterUserDeferences.DataSource =list.Skip((AspNetPager1.CurrentPageIndex - 1) * base.PageSize).Take(base.PageSize).ToList(); 
            repeaterUserDeferences.DataBind();
            updatepagerhtml();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Bind();
        }

        List<Guid> ReturnCheckedItems()
        {
            List<Guid> jj = new List<Guid>();
            Repeater fdla = (Repeater)this.Page.Master.FindControl("ContentPlaceHolder1").FindControl("repeaterUserDeferences");
            for (int i = 0; i < fdla.Items.Count; i++)
            {
                System.Web.UI.HtmlControls.HtmlInputCheckBox checkbox = (System.Web.UI.HtmlControls.HtmlInputCheckBox)fdla.Items[i].FindControl("repcheckbox");
                if (checkbox.Checked == true)
                {
                    jj.Add(new Guid(checkbox.Value));
                }
            }
            return jj;
        }

        protected void btniam_ServerClick(object sender, EventArgs e)
        {
            string sql = "";
            List<Guid> list = ReturnCheckedItems();
            foreach (var item in list)
            {
                sql += "update [sys_HRUserResolution] set [state]='已处理',[updatetime]=getdate() where ID='" + item.ToString() + "';";
            }
            int count = 0;
            using (IAMEntities db = new IAMEntities())
            {
                count = db.ExecuteStoreCommand(sql);
                db.SaveChanges();
            }
            if (count > 0)
            {
                Response.Write("<script>alert('更新成功');</script>");
            }
            Bind();
        }


        public string GetHrPartMent(object obj)
        {
            if (obj != null)
            {
                string tmp = obj.ToString();
                int count;
                var dep = new HRDepartmentDAL().VDepartmentList(int.MaxValue,1,out count);
                var dname = dep.FirstOrDefault(item=>item.dept==tmp);
                if (dname != null)
                    return dname.name;
                else
                    return tmp;
            }
            else
                return "";
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



        protected void btnsystemhtml_Click1(object sender, EventArgs e)
        {
            DataTable dt = null;
            dt = new sys_HRUserResolutionDAL().ReturnList().Where(item =>
            {
                if (dlpsysType.SelectedValue == "") return true;
                else return item.state == dlpsysType.SelectedValue;
            }).ToList().Select(x => new
            {
                工号 = x.cardNo,
                姓名 = x.name,
                字段 = x.porpert,
                源值 = x.porpert == "部门" ? GetHrPartMent (x.oldvalue): x.oldvalue,
                现值 = x.porpert == "部门" ? GetHrPartMent(x.newvalue) : x.newvalue,
                产生时间 = x.createtime,
                处理时间 = x.updatetime,
                状态 = x.state
            }).ToList().ToDataTable();
            string file = "downloadFile/HRUserResolution_" + Guid.NewGuid() + ".xls";
            ExcelHelper.ReturnExcelExport(System.Web.HttpContext.Current.Server.MapPath("~/Template/ExcelTemplate/HRUserResolution.xlsx"),
                System.Web.HttpContext.Current.Server.MapPath("~/"+file),
                dt);
            Response.Redirect("~/" + file);
        }
    }
}