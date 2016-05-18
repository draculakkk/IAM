using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAM2.Admin
{
    /// <summary>
    /// HECGangweiInfo 的摘要说明
    /// </summary>
    public class HECGangweiInfo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Write(IAM2.BLL.HEC_Gangwei_Ajax.ResponseJson(context));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}