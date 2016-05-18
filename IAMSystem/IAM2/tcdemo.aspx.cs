using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAM
{
    public partial class tcdemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                    System.Text.StringBuilder stb = new System.Text.StringBuilder();
                    int allcount, okcount;
                    new BLL.TcSyncServices().SyncUserInfo(out allcount, out okcount);
                   // new BLL.TcSyncServices().SyncRole(out allcount,out okcount);
                    stb.Append(string.Format("tc账号信息任务<br/>共：{0}<br/>成功：{1}<br/>失败：{2}<br/>", allcount, okcount, allcount - okcount));
                    BLL.Sys_UserName_ConflictResolutionMail.SendMail(IAMEntityDAL.Unitity.SystemType.TC);
                    Response.Write(stb.ToString());
                
                
            }
        }
    }
}