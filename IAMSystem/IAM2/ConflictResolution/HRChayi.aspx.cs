using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseDataAccess;
using System.Data;
using IAMEntityDAL;

namespace IAM2.ConflictResolution
{
    public partial class HRChayi : IAM.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }
        void Bind()
        {
            IAMEntities db = new IAMEntities();
            string sel = dlpsysType.SelectedValue;
            if (string.IsNullOrEmpty(sel))
            {
                var lis = db.HRChayiLog.Join(db.HRDepartment, q => q.dept, h => h.dept, (q, h) => new
                {
                    q.code,
                    q.posts,
                    deptname = h.name,
                    name = q.name,
                    q.moblePhone,
                    q.syncDate,
                    q.p2,
                    q.p3,
                    q.p4,
                    q.Pk_psndoc,
                    q.ID
                }).OrderByDescending(x => x.code).ToList();
                AspNetPager1.RecordCount = lis.Count;
                AspNetPager1.PageSize = base.PageSize;
                repeaterUserDeferences.DataSource = lis.ToList().OrderBy(item => item.code).Skip((AspNetPager1.CurrentPageIndex - 1) * base.PageSize).Take(base.PageSize).ToList();
                repeaterUserDeferences.DataBind();
            }
            else
            {
                var lis = db.HRChayiLog.Join(db.HRDepartment, q => q.dept, h => h.dept, (q, h) => new
                {
                    q.code,
                    q.posts,
                    deptname = h.name,
                    name = q.name,
                    q.moblePhone,
                    q.syncDate,
                    q.p2,
                    q.p3,
                    q.p4,
                    q.Pk_psndoc,
                    q.ID
                }).Where(x => x.p2 == sel).ToList();
                AspNetPager1.RecordCount = lis.Count;
                AspNetPager1.PageSize = base.PageSize;
                if (sel == "已处理")
                    repeaterUserDeferences.DataSource = lis.ToList().OrderBy(item => item.code).Skip((AspNetPager1.CurrentPageIndex - 1) * base.PageSize).Take(base.PageSize).ToList();
                else
                    repeaterUserDeferences.DataSource = lis.ToList().OrderBy(item => item.syncDate).Skip((AspNetPager1.CurrentPageIndex - 1) * base.PageSize).Take(base.PageSize).ToList();
                repeaterUserDeferences.DataBind();
            }
            updatepagerhtml();
        }
        protected void AspNetPager1_PageChanging(object sender, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            Bind();
        }

        private void updatepagerhtml()
        {
            AspNetPager1.CustomInfoHTML = string.Format("共{0}页,当前第{1}页,共{2}条", AspNetPager1.PageCount, AspNetPager1.CurrentPageIndex, AspNetPager1.RecordCount);
        }
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Bind();
        }

        protected void btnsystemhtml_Click1(object sender, EventArgs e)
        {
            DataTable dt = null;
            IAMEntities db = new IAMEntities();
            dt = db.HRChayiLog.Join(db.HRDepartment, q => q.dept, h => h.dept, (q, h) => new
            {
                q.code,
                q.posts,
                deptname = h.name,
                name = q.name,
                q.moblePhone,
                q.syncDate,
                q.p2,
                q.p3,
                q.p4,
                q.Pk_psndoc,
                q.ID
            }).ToList().Where(xm =>
            {
                if (dlpsysType.SelectedValue == "")
                    return true;
                else
                    return xm.p2 == dlpsysType.SelectedValue;
            }).Select(x => new
            {
                工号 = x.code,
                姓名 = x.name,
                所在部门 = x.deptname,
                岗位 = x.posts,
                手机 = x.moblePhone,
                产生时间 = x.syncDate,
                处理时间 = x.p3,
                状态 = x.p2,
                备注 = x.p4
            }).ToList().ToDataTable();
            if (dt.Rows.Count <= 0)
            {
                Response.Write("<script>alert('无数据，不可导出');</script>");
                return;
            }
            string sel = dlpsysType.SelectedValue;
            DataView dv = dt.DefaultView;
            if (string.IsNullOrEmpty(sel) || sel == "已处理")
            {                
                dv.Sort = "工号 asc";
            }
            else if (sel == "未处理")
            {
                dv.Sort = "产生时间 desc";
            }
            dt = dv.ToTable();
            string file = "downloadFile/HRchayi_" + Guid.NewGuid() + ".xls";
            ExcelHelper.ReturnExcelExport(System.Web.HttpContext.Current.Server.MapPath("~/Template/ExcelTemplate/Hrchayi.xlsx"),
                System.Web.HttpContext.Current.Server.MapPath("~/" + file),
                dt);
            Response.Redirect("~/" + file);

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
            List<HRChayiLog> loglist = new IAMEntities().HRChayiLog.Where(x => list.Contains(x.ID)).ToList();
            foreach (var item in loglist)
            {
                sql += "update [HRChayiLog] set [p2]='已处理',[p4]='源系统不存在,iam系统中存在;故删除iam中该员工',[p3]=convert(nvarchar(20),getdate(),120) where ID='" + item.ID + "'";
                sql += "delete HREmployee where Pk_psndoc='" + item.Pk_psndoc + "'";
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
    }
}