using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseDataAccess;

namespace IAM.Admin
{
    public partial class RoleTemplateManger : BasePage
    {
        class RoleTemplatelist
        {
            public Guid ID { get; set; }
            public string TemplateName { get; set; }
            public int HEC { get; set; }
            public int HEC2 { get; set; }
            public int HR { get; set; }
            public int SAP { get; set; }
            public int TC { get; set; }
           
            public int AD { get; set; }
            public int ADComputer { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!base.ReturnUserRole.Admin&&!base.ReturnUserRole.Leader)
                {
                    base.NoRole();
                }
                Bind();
            }
        }

        void Bind()
        {
            string template = txtTemplateName.Text.Trim();
            string sql = @"SELECT ID, TemplateName,(SELECT COUNT(*) FROM dbo.RoleTemplateInfo WHERE SystemName='HEC'AND TemplateID=a.ID) HEC,(SELECT COUNT(*) FROM dbo.RoleTemplateInfo WHERE SystemName='HR' AND TemplateID=a.ID) HR
       ,(SELECT COUNT(*) FROM dbo.RoleTemplateInfo WHERE SystemName='AD' AND TemplateID=a.ID) AD ,(SELECT COUNT(*) FROM dbo.RoleTemplateInfo WHERE SystemName='SAP' AND TemplateID=a.ID) SAP,(SELECT COUNT(*) FROM dbo.RoleTemplateInfo WHERE SystemName='TC' AND TemplateID=a.ID) TC,(SELECT count(*) FROM dbo.RoleTemplateInfo WHERE SystemName='ADComputer' AND TemplateID=a.ID) ADComputer,(SELECT count(*) FROM dbo.RoleTemplateInfo WHERE SystemName='HEC2' AND TemplateID=a.ID) HEC2 
FROM dbo.RoleTemplate a";
            if (!string.IsNullOrEmpty(template))
                sql += " WHERE TemplateName='"+template+"'";
            List<RoleTemplatelist> list = new List<RoleTemplatelist>();
            using (IAMEntities db = new IAMEntities())
            {
                 list = db.ExecuteStoreQuery<RoleTemplatelist>(sql).ToList();
            }

            repeaterRoleTemplate.DataSource = list;
            repeaterRoleTemplate.DataBind();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Bind();
        }
    }
}