using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseDataAccess;
using IAMEntityDAL;

namespace IAM.syncTest
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { }
        }

        protected void btnsap_Click(object sender, EventArgs e)
        {
            SAP_UserInfo en = new SAP_UserInfo();
            using (IAMEntities db = new IAMEntities())
            {
                en = db.SAP_UserInfo.FirstOrDefault(item => item.BAPIBNAME == "C_BAOLIANGZ");
            }
            if (en != null)
            {
                en.LANGUAGE = "2";
                en.OUTPUTED_DELETE = "G";
            }
            new SAPUserInfoDAL().SyncSapUserInfomation(en);
        }
    }
}