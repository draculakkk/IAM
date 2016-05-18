using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAM
{
    /// <summary>
    /// ValidateLoginName 的摘要说明
    /// </summary>
    public class ValidateLoginName : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            BLL.Ajax ajax = new BLL.Ajax();
            context.Response.Write(ajax.ValidateLoginNameOne(context));
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