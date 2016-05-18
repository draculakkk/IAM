using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAM
{
    public partial class hecdemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string hecName = "shacHec";
            string hecPassword = "shacHec";
            IAM2.WebReference_HEC_bumen.auto_service _services = new IAM2.WebReference_HEC_bumen.auto_service();
            var list = _services.execute(new IAM2.WebReference_HEC_bumen.parameter() {  ws_user_name=hecName, ws_password=hecPassword}).unit_records.ToList();
            gridview1.DataSource = list;
            gridview1.DataBind();
        }
    }
}