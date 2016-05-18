using BaseDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAM
{
    /// <summary>
    /// ExcelExportAjax 的摘要说明
    /// </summary>
    public class ExcelExportAjax : IHttpHandler,System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (((UserRole)context.Session["userinfo"]) == null)
            {
                context.Response.Write("请登录");
                return;
            }
            context.Response.ContentType = "text/plain";
            BLL.Ajax ajax = new BLL.Ajax();
            context.Response.Write(ajax.Fun(context));
            //context.Response.Write("Hello World");
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