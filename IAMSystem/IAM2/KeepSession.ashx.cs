using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using BaseDataAccess;
using IAMEntityDAL;

namespace IAM2
{
    /// <summary>
    /// KeepSession 的摘要说明
    /// </summary>
    public class KeepSession : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
             
         UserRole UserInfo=(UserRole)context.Session["userinfo"];
         if (UserInfo != null)
         {
             context.Response.Write("ok");
         }
         else
         {
             context.Response.Write("error");
         }
        
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