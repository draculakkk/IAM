using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAM.BLL
{
    public class Untityone
    {
        public static string SubString(string Str, int Index, int length)
        {
            string tmpStr = Str;
            if (tmpStr.Length >= length)
            {
                return tmpStr.Substring(Index, length)+"...";
            }
            else
            {
                return tmpStr;
            }
        }


        public static string GetRoleName(string mRole)
        {
            string[] roles = mRole.Split('.');
            if (roles != null && roles.Length > 0)
                return roles[0];
            else
                return string.Empty;
        }

        public static string GetGroupName(string mRole)
        {
            string rolename = GetRoleName(mRole);
            return mRole.Replace(rolename + ".", "");
        }


        public static void outexcel(System.Web.UI.Page page, string value)
        {
            page.Response.AppendHeader("Content-Disposition", "attachment;filename="+Guid.NewGuid().ToString()+".xls");
            page.Response.Charset = "UTF-8";
            page.Response.ContentEncoding = System.Text.Encoding.Default;
            page.Response.ContentType = "application/ms-excel";
            System.Text.StringBuilder stb = new System.Text.StringBuilder();
            value = System.Text.RegularExpressions.Regex.Replace(value, @"<input\stype='button'.+?>", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);// .Replace(@"<input\stype='button'.+?>","");
            stb.Append(value);
            System.IO.StringWriter tw = new System.IO.StringWriter(stb);
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            page.Response.Write(tw.ToString());
            page.Response.Flush();
            page.Response.End();
           
        }
    }
}