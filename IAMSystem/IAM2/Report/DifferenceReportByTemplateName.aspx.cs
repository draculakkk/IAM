using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseDataAccess;

namespace IAM.Report
{
    public partial class DifferenceReportByTemplateName : BasePage
    {
        public class ItemModel
        {
            public string Group { get; set; }
            public string RoleName { get; set; }
            public string Company { get; set; }
            public string RoleName2 { get; set; }
            public string Company2 { get; set; }
            public string zhanghao { get; set; }
            public string zhanghao2 { get; set; }

        }
        public class PageModel
        {
            public List<ItemModel> SAP = new List<ItemModel>();
            public List<ItemModel> HR = new List<ItemModel>();
            public List<ItemModel> HEC = new List<ItemModel>();
            public List<ItemModel> TC = new List<ItemModel>();
            public List<ItemModel> AD = new List<ItemModel>();
            public List<ItemModel> ADComputer = new List<ItemModel>();
            public List<ItemModel> HEC2 = new List<ItemModel>();

            public List<ItemModel> SAPother = new List<ItemModel>();
            public List<ItemModel> HRother = new List<ItemModel>();
            public List<ItemModel> HECother = new List<ItemModel>();
            public List<ItemModel> TCother = new List<ItemModel>();
            public List<ItemModel> ADother = new List<ItemModel>();
            public List<ItemModel> ADComputerother = new List<ItemModel>();
            public List<ItemModel> HEC2other = new List<ItemModel>();

            public List<ItemModel> SAPsystem = new List<ItemModel>();
            public List<ItemModel> HRsystem = new List<ItemModel>();
            public List<ItemModel> HECsystem = new List<ItemModel>();
            public List<ItemModel> TCsystem = new List<ItemModel>();
            public List<ItemModel> ADsystem = new List<ItemModel>();
            public List<ItemModel> ADComputersystem = new List<ItemModel>();
            public List<ItemModel> HEC2System = new List<ItemModel>();
        }

        public PageModel Models = new PageModel();
        IAMEntities db = new IAMEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!base.ReturnUserRole.Admin && !base.ReturnUserRole.Leader)
                {
                    base.NoRole();
                }
            }
        }

        void bind()
        {
            if (string.IsNullOrEmpty(TextBox2.Text.Trim()) || string.IsNullOrEmpty(TextBox3.Text.Trim()))
            {
                Models = new PageModel();
                return;
            }
            string leftid = TextBox2.Text.Trim();
            string right = TextBox3.Text.Trim();

            string sql = @"SELECT DISTINCT a.RoleName AS RoleName,a.companyName AS Company ,b.RoleName AS RoleName2,b.CompanyName AS Company2,a.zhanghao,b.zhanghao2
	  FROM (
	  (SELECT a.ID,  a.RoleName, a.SystemName,a.TemplateID ,a.CompanyName,a.p2 zhanghao FROM dbo.RoleTemplateInfo a INNER JOIN dbo.RoleTemplate b ON a.TemplateID=b.ID 
	  WHERE b.TemplateName=@temp1 AND SystemName='{0}' and a.p1='{2}') a
	  FULL JOIN 

	  (SELECT a.ID,  a.RoleName,a.SystemName,a.TemplateID,a.CompanyName,a.p2 zhanghao2 FROM dbo.RoleTemplateInfo a INNER JOIN dbo.RoleTemplate b ON a.TemplateID=b.ID 
	  WHERE b.TemplateName=@temp2 AND SystemName='{1}' and a.p1='{3}') b
	  ON a.CompanyName=b.CompanyName AND b.RoleName=a.RoleName )";

            //HEC 系统模版对比
            var heclist = db.ExecuteStoreQuery<ItemModel>(string.Format(sql, "HEC", "HEC", "员工", "员工"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@temp1",leftid),
            new System.Data.SqlClient.SqlParameter("@temp2",right)
            });
            Models.HEC = heclist.ToList();

            var heclistother = db.ExecuteStoreQuery<ItemModel>(string.Format(sql, "HEC", "HEC", "其他", "其他"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@temp1",leftid),
            new System.Data.SqlClient.SqlParameter("@temp2",right)
            });
            Models.HECother = heclistother.ToList();

            var heclistsytem = db.ExecuteStoreQuery<ItemModel>(string.Format(sql, "HEC", "HEC", "系统", "系统"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@temp1",leftid),
            new System.Data.SqlClient.SqlParameter("@temp2",right)
            });
            Models.HECsystem = heclistsytem.ToList();

            var hec2 = db.ExecuteStoreQuery<ItemModel>(string.Format(sql, "HEC2", "HEC2", "员工", "员工"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@temp1",leftid),
            new System.Data.SqlClient.SqlParameter("@temp2",right)
            });
            Models.HEC2 = hec2.ToList();

            var hec2other = db.ExecuteStoreQuery<ItemModel>(string.Format(sql, "HEC2", "HEC2", "其他", "其他"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@temp1",leftid),
            new System.Data.SqlClient.SqlParameter("@temp2",right)
            });
            Models.HEC2other = hec2other.ToList();

            var hec2system = db.ExecuteStoreQuery<ItemModel>(string.Format(sql, "HEC2", "HEC2", "系统", "系统"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@temp1",leftid),
            new System.Data.SqlClient.SqlParameter("@temp2",right)
            });
            Models.HEC2System = hec2system.ToList();

            //HR 系统模版对比
            var hrlist = db.ExecuteStoreQuery<ItemModel>(string.Format(sql, "HR", "HR", "员工", "员工"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@temp1",leftid),
            new System.Data.SqlClient.SqlParameter("@temp2",right)
            });
            Models.HR = hrlist.ToList();

            var hrlistother = db.ExecuteStoreQuery<ItemModel>(string.Format(sql, "HR", "HR", "其他", "其他"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@temp1",leftid),
            new System.Data.SqlClient.SqlParameter("@temp2",right)
            });
            Models.HRother = hrlistother.ToList();

            var hrlistsytem = db.ExecuteStoreQuery<ItemModel>(string.Format(sql, "HR", "HR", "系统", "系统"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@temp1",leftid),
            new System.Data.SqlClient.SqlParameter("@temp2",right)
            });
            Models.HRsystem = hrlistsytem.ToList();



            //SAP 系统模版对比
            var saplist = db.ExecuteStoreQuery<ItemModel>(string.Format(sql, "SAP", "SAP", "员工", "员工"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@temp1",leftid),
            new System.Data.SqlClient.SqlParameter("@temp2",right)
            });
            Models.SAP = saplist.ToList();

            var saplistother = db.ExecuteStoreQuery<ItemModel>(string.Format(sql, "SAP", "SAP", "其他", "其他"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@temp1",leftid),
            new System.Data.SqlClient.SqlParameter("@temp2",right)
            });
            Models.SAPother = saplistother.ToList();

            var saplistsytem = db.ExecuteStoreQuery<ItemModel>(string.Format(sql, "SAP", "SAP", "系统", "系统"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@temp1",leftid),
            new System.Data.SqlClient.SqlParameter("@temp2",right)
            });
            Models.SAPsystem = saplistsytem.ToList();


            //TC 系统模版对比
            var tclist = db.ExecuteStoreQuery<ItemModel>(string.Format(sql, "TC", "TC", "员工", "员工"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@temp1",leftid),
            new System.Data.SqlClient.SqlParameter("@temp2",right)
            });
            Models.TC = tclist.ToList();

            var tclistother = db.ExecuteStoreQuery<ItemModel>(string.Format(sql, "TC", "TC", "其他", "其他"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@temp1",leftid),
            new System.Data.SqlClient.SqlParameter("@temp2",right)
            });
            Models.TCother = tclistother.ToList();

            var tclistsytem = db.ExecuteStoreQuery<ItemModel>(string.Format(sql, "TC", "TC", "系统", "系统"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@temp1",leftid),
            new System.Data.SqlClient.SqlParameter("@temp2",right)
            });
            Models.TCsystem = tclistsytem.ToList();

            var ADlist = db.ExecuteStoreQuery<ItemModel>(string.Format(sql, "AD", "AD", "员工", "员工"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@temp1",leftid),
            new System.Data.SqlClient.SqlParameter("@temp2",right)
            });
            Models.AD = ADlist.ToList();

            var ADlistother = db.ExecuteStoreQuery<ItemModel>(string.Format(sql, "AD", "AD", "其他", "其他"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@temp1",leftid),
            new System.Data.SqlClient.SqlParameter("@temp2",right)
            });
            Models.ADother = ADlistother.ToList();

            var ADlistsytem = db.ExecuteStoreQuery<ItemModel>(string.Format(sql, "AD", "AD", "系统", "系统"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@temp1",leftid),
            new System.Data.SqlClient.SqlParameter("@temp2",right)
            });
            Models.ADsystem = ADlistsytem.ToList();

            var adcomputer = db.ExecuteStoreQuery<ItemModel>(string.Format(sql, "ADComputer", "ADComputer", "员工", "员工"),
                 new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@temp1",leftid),
            new System.Data.SqlClient.SqlParameter("@temp2",right)
            });
            Models.ADComputer = adcomputer.ToList();

            var adcomputerother = db.ExecuteStoreQuery<ItemModel>(string.Format(sql, "ADComputer", "ADComputer", "其他", "其他"),
                 new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@temp1",leftid),
            new System.Data.SqlClient.SqlParameter("@temp2",right)
            });
            Models.ADComputerother = adcomputerother.ToList();

            var adcomputersystem = db.ExecuteStoreQuery<ItemModel>(string.Format(sql, "ADComputer", "ADComputer", "其他", "其他"),
                 new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@temp1",leftid),
            new System.Data.SqlClient.SqlParameter("@temp2",right)
            });
            Models.ADComputersystem = adcomputersystem.ToList();

        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            bind();
        }


        //void outexcel()
        //{

        //    HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=Excel.xls");
        //    HttpContext.Current.Response.Charset = "UTF-8";
        //    HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
        //    HttpContext.Current.Response.ContentType = "application/ms-excel";//image/JPEG;text/HTML;image/GIF;vnd.ms-excel/msword 
        //    System.Text.StringBuilder stb = new System.Text.StringBuilder();
        //    stb.Append(hidden1.Value);
        //    System.IO.StringWriter tw = new System.IO.StringWriter(stb);
        //    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

        //    HttpContext.Current.Response.Write(tw.ToString());
        //    HttpContext.Current.Response.End();

        //}

        protected void btnOutput_Click(object sender, EventArgs e)
        {
            BLL.Untityone.outexcel(this, hidden1.Value);
        }
    }
}