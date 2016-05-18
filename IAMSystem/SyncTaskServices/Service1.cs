using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Timers;

namespace SyncTaskServices
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }
        Timer timer1;
        protected override void OnStart(string[] args)
        {
            LogDAL.LogAdd("IAM Sync windows Services 启用成功");
            timer1 = new Timer();
            timer1.Interval = 1000 * 10;
            timer1.Enabled = true;
            timer1.Elapsed += new ElapsedEventHandler(timer1_Elapsed);
        }

        void timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                string AcountName = System.Configuration.ConfigurationSettings.AppSettings["AcountName"];
                if (HttpGet(AcountName))
                {
                    
                    
                }
                else
                {
                    LogDAL.LogAdd("windows services 模拟访问失败");
                    timer1_Elapsed(sender, e);
                    
                }
            }
            catch (Exception ex)
            {
                
            }
        }


        HttpWebRequest RequestByGet(string URI)
        {
            HttpWebRequest GetRequest = (HttpWebRequest)WebRequest.Create(URI);
            string Acount = System.Configuration.ConfigurationSettings.AppSettings["Acount"];
            string PassWord = System.Configuration.ConfigurationSettings.AppSettings["PassWord"];
            string Domian = System.Configuration.ConfigurationSettings.AppSettings["Domian"];
            GetRequest.Credentials = new System.Net.NetworkCredential(Acount, PassWord, Domian);
            GetRequest.ProtocolVersion = HttpVersion.Version10;
            GetRequest.AllowAutoRedirect = true;
            GetRequest.KeepAlive = true;
            GetRequest.Headers.Add("Accept-Language", "zh-cn");
            GetRequest.Accept = "image/gif, image/jpeg, image/pjpeg, image/pjpeg, application/xaml+xml, application/vnd.ms-xpsdocument, application/x-ms-xbap, application/x-ms-application, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
            GetRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.2; Trident/4.0; .NET CLR 2.0.50727; .NET CLR 3.0.04506.30; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; InfoPath.2; CIBA; .NET4.0C; .NET4.0E)";
            return GetRequest;
        }

        bool HttpGet(string AccountComputer)
        {
            try
            {
                bool isscussful = false;
                string URI = AccountComputer + "/admin/synclistmanager.aspx";
                HttpWebRequest GetRequest = RequestByGet(URI);
                GetRequest.Method = "GET";
                HttpWebResponse GetResponse = (HttpWebResponse)GetRequest.GetResponse();
                if (GetResponse.StatusCode == HttpStatusCode.OK)
                {
                    isscussful = true;
                }
                else
                {
                    isscussful = false;
                }
                GetResponse.Close();
                return isscussful;
            }
            catch
            {
                return false;
            }
        }

        protected override void OnStop()
        {
            LogDAL.LogAdd("IAM Sync windows Services 关闭");
        }
    }
}
