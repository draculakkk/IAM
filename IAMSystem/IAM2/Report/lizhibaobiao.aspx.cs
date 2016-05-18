using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAMEntityDAL;
using BaseDataAccess;

namespace IAM2.Report
{
    public partial class lizhibaobiao : IAM.BasePage
    {
        public class UserNameReport
        {
            public string code { get; set; }
            public string name { get; set; }
            public int hr { get; set; }
            public int ad { get; set; }
            public int adcomputer { get; set; }
            public int sap { get; set; }
            public int hec { get; set; }
            public int tc { get; set; }
            public string DepartName { get; set; }
        }
        IAMEntities db = new IAMEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { }
        }

        string retrunsql()
        {
            string sql = "";
            if (dplreporttype.SelectedValue == "0")
                sql = @"SELECT distinct a.code,a.name,c.NAME AS DepartName,
CASE WHEN (SELECT jishu FROM dbo.v_ReportByLeave WHERE systemname='HR' AND code=a.code) IS NULL THEN 0 ELSE (SELECT jishu FROM dbo.v_ReportByLeave WHERE systemname='HR' AND code=a.code) END hr,
CASE WHEN (SELECT jishu FROM dbo.v_ReportByLeave WHERE systemname='AD'AND code=a.code) IS NULL THEN 0 ELSE  (SELECT jishu FROM dbo.v_ReportByLeave WHERE systemname='AD'AND code=a.code) END ad,
CASE WHEN (SELECT jishu FROM dbo.v_ReportByLeave WHERE systemname='ADComputer'AND code=a.code) IS NULL THEN 0 ELSE (SELECT jishu FROM dbo.v_ReportByLeave WHERE systemname='ADComputer'AND code=a.code) END  adcomputer,
CASE WHEN (SELECT jishu FROM dbo.v_ReportByLeave WHERE systemname='HEC'AND code=a.code) IS NULL THEN 0 ELSE (SELECT jishu FROM dbo.v_ReportByLeave WHERE systemname='HEC'AND code=a.code) END  hec,
CASE WHEN (SELECT jishu FROM dbo.v_ReportByLeave WHERE systemname='SAP'AND code=a.code) IS NULL THEN 0 ELSE (SELECT jishu FROM dbo.v_ReportByLeave WHERE systemname='SAP'AND code=a.code) END  sap,
CASE WHEN (SELECT jishu FROM dbo.v_ReportByLeave WHERE systemname='TC'AND code=a.code) IS NULL THEN 0 ELSE (SELECT jishu FROM dbo.v_ReportByLeave WHERE systemname='TC'AND code=a.code) END  tc
FROM dbo.v_ReportByLeave a INNER JOIN dbo.HREmployee h ON a.code=h.code INNER JOIN dbo.HRDepartment c ON h.dept=c.dept
ORDER BY a.code ASC";
            if (dplreporttype.SelectedValue == "1")
                sql = @"SELECT distinct a.code,a.name,c.NAME AS DepartName,
CASE WHEN (SELECT jishu FROM dbo.v_ReportByPosts b WHERE systemname='HR' AND b.code=a.code ) IS NULL THEN 0 ELSE (SELECT jishu FROM dbo.v_ReportByPosts b WHERE systemname='HR' AND b.code=a.code ) END   hr,
CASE WHEN (SELECT jishu FROM dbo.v_ReportByPosts b WHERE systemname='AD' AND b.code=a.code)  IS NULL THEN 0 ELSE (SELECT jishu FROM dbo.v_ReportByPosts b WHERE systemname='AD' AND b.code=a.code)  END  ad,
CASE WHEN (SELECT jishu FROM dbo.v_ReportByPosts b WHERE systemname='ADComputer' AND b.code=a.code) IS NULL THEN 0 ELSE (SELECT jishu FROM dbo.v_ReportByPosts b WHERE systemname='ADComputer' AND b.code=a.code) END adcomputer,
CASE WHEN  (SELECT jishu FROM dbo.v_ReportByPosts b WHERE systemname='HEC' AND b.code=a.code) IS NULL THEN  0 ELSE (SELECT jishu FROM dbo.v_ReportByPosts b WHERE systemname='HEC' AND b.code=a.code)END    hec,
CASE WHEN (SELECT jishu FROM dbo.v_ReportByPosts b WHERE systemname='SAP' AND b.code=a.code) IS NULL THEN 0 ELSE (SELECT jishu FROM dbo.v_ReportByPosts b WHERE systemname='SAP' AND b.code=a.code) END    sap,
CASE WHEN (SELECT jishu FROM dbo.v_ReportByPosts b WHERE systemname='TC' AND b.code=a.code) IS NULL THEN 0 ELSE (SELECT jishu FROM dbo.v_ReportByPosts b WHERE systemname='TC' AND b.code=a.code) END    tc
FROM dbo.v_ReportByPosts a INNER JOIN dbo.HREmployee h ON a.code=h.code INNER JOIN dbo.HRDepartment c ON h.dept=c.dept
ORDER BY a.code ASC";
            return sql;
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            bind();
        }

        void bind()
        {
            string sql = retrunsql();
            if (!string.IsNullOrEmpty(sql))
            {
                var list = db.ExecuteStoreQuery<UserNameReport>(sql);
                List<UserNameReport> listlizhi = list.ToList();
                AspNetPager1.RecordCount = listlizhi.Count;
                AspNetPager1.PageSize = base.PageSize;
                listlizhi = listlizhi.OrderBy(item => item.code).Skip((AspNetPager1.CurrentPageIndex - 1) * base.PageSize).Take(base.PageSize).ToList();
                RptReport.DataSource = listlizhi;
                RptReport.DataBind();
                updatepagerhtml();
            }
        }

        protected void AspNetPager1_PageChanging(object sender, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            bind();
        }
        private void updatepagerhtml()
        {
            AspNetPager1.CustomInfoHTML = string.Format("共{0}页,当前第{1}页,共{2}条", AspNetPager1.PageCount, AspNetPager1.CurrentPageIndex, AspNetPager1.RecordCount);
        }

        protected void btnOutExpr_Click(object sender, EventArgs e)
        {
             string sql = retrunsql();
             if (!string.IsNullOrEmpty(sql))
             {
                 var list = db.ExecuteStoreQuery<UserNameReport>(sql);
                 List<UserNameReport> listlizhi = list.ToList();
                 var newlist = listlizhi.Select(x => new { 
                 工号=x.code,
                 姓名=x.name,
                 部门=x.DepartName,
                 域控=x.ad,
                 计算机=x.adcomputer,
                 人事管理系统=x.hr,
                 SAP=x.sap,
                 预算管理系统=x.hec,
                 TeamCenter=x.tc
                 });
                 System.Data.DataTable dt = newlist.ToDataTable();
                 string file = "downloadFile/lizhibaobiao_" + Guid.NewGuid() + ".xls";
                 ExcelHelper.ReturnExcelExport(System.Web.HttpContext.Current.Server.MapPath("~/Template/ExcelTemplate/lizhibaobiao.xlsx"),
                     System.Web.HttpContext.Current.Server.MapPath("~/" + file),
                     dt);
                 Response.Redirect("~/" + file);
             }
        }
    }
}