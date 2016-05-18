using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.IO;

namespace IAM
{


    public class Global : System.Web.HttpApplication
    {
        private bool IsSync(string name)
        {

            if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings[name])) return false;
            string isture = System.Configuration.ConfigurationManager.AppSettings[name] + "";
            if (string.IsNullOrEmpty(isture)) return false;
            return Convert.ToInt32(isture.ToString()) == 1 ? true : false;
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            if (IsSync("synchr"))
            {
                //HR 同步任务方法
                BLL.SyncTaskBase.HRSyncTask.HRSyncTaskList();
            }

            if (IsSync("synccomputer"))
            {
                BLL.SyncTaskBase.ADSyncTask.SyncComputer();
            }

            if (IsSync("synchec"))
            {
                BLL.SyncTaskBase.HECSyncTask.SyncHECInfo();

            }

            if (IsSync("synctc"))
            {
                BLL.SyncTaskBase.TCSyncTask.SyncTCInfo();
            }

            if (IsSync("syncsap"))
            {
                BLL.SyncTaskBase.SapSyncTask.SyncSap();
            }
            BLL.SyncTaskBase.EmailServicesTask.SendMail();
        }

        public void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            Guid id = new IAMEntityDAL.LogDAL().AddsysErrorLog(string.Format("应用程序报错\n报错信息：{0}", ex.ToString()));
#if DEBUG
            Response.Redirect("~/error.aspx?id=" + id.ToString() + "&message=" + HttpUtility.UrlEncode(ex.ToString()));
#else
            Response.Redirect("~/error.aspx?message=错误信息代码：" + id.ToString());
#endif
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
           
        }
    }
}