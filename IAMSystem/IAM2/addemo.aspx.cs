using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAM
{
    public partial class addemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int countall, okcount;
            System.Text.StringBuilder stb = new System.Text.StringBuilder();
           // new BLL.ADSyncServices().SyncComputer(out countall, out okcount);
            //new BLL.ADSyncServices().SyncUserInfo(out countall, out okcount);
            //BLL.Sys_UserName_ConflictResolutionMail.SendMail(IAMEntityDAL.Unitity.SystemType.AD);
            new BLL.ADSyncServices().SyncComputer(out countall,out okcount);
            
            BLL.Sys_UserName_ConflictResolutionMail.SendMail(IAMEntityDAL.Unitity.SystemType.AD);
           // stb.Append("ad账号同步任务<br/>共:" + countall.ToString() + "<br/>成功:" + okcount.ToString() + "<br/>失败:" + (countall - okcount).ToString() + "");


           // Response.Write(stb.ToString());
        
            
           
           
        }
    }
}