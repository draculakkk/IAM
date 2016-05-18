using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAM
{
    public partial class sapDemo : System.Web.UI.Page
    {
        string hecName = "shacHec";
        string hecPassword = "shacHec";
        protected void Page_Load(object sender, EventArgs e)
        {
            txtbox1.Enabled = false;
            if (!IsPostBack)
            {
                try
                {
                    int allcount, okcount;
                    new BLL.SAPSyncServices().SyncSapUserInfo(out allcount, out okcount);
                    //BLL.Sys_UserName_ConflictResolutionMail.SendMail(IAMEntityDAL.Unitity.SystemType.SAP);
                    Response.Write(string.Format("sap账号同步信息<br/>共：{0}<br/>成功：{1}<br/>失败:{2}<br/>", allcount, okcount, allcount - okcount));
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
               
            }
        }

        protected void button1_Click(object sender, EventArgs e)
        {

            txtbox1.Enabled = true;
        }
    }
}