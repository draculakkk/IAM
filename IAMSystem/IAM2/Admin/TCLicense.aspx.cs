using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseDataAccess;
using IAMEntityDAL;

namespace IAM2.Admin
{
    public partial class TCLicense : System.Web.UI.Page
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
            repeaterTCLicense.DataSource = new KeyValueDAL().GetList().Where(x => x.KEY == "TC License");
            repeaterTCLicense.DataBind();
        }

       

        protected void btnAdd_ServerClick(object sender, EventArgs e)
        {
            Guid id = new Guid(hiddenID.Value);
            int count = new KeyValueDAL().NewUpdate(id,hiddenvalue.Value);
            Response.Write("<script>alert('更新成功');</script>");
            Bind();
        }
    }
}