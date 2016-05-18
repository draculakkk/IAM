using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IAMEntityDAL
{
    public class MailTemplateHelper
    {
        /// <summary>
        /// 运行模版并获取结果
        /// </summary>
        /// <typeparam name="T">模版中的模型对象</typeparam>
        /// <param name="model">模型对象</param>
        /// <param name="templateName">模版名称</param>
        /// <returns></returns>
        public static string RunTemplate<T>(T model, string templateName) where T : class
        {

            var path = "";
            if (System.Web.HttpContext.Current != null)
            {
                path = System.Web.HttpContext.Current.Server.MapPath("~/mailTemplate");
            }
            else
            {
               path=AppDomain.CurrentDomain.BaseDirectory.ToString()+ "mailTemplate";
            }
            var tmp = RazorEngine.Razor.Parse<T>(File.ReadAllText( Path.Combine(path, templateName + ".cshtml")),model);
            return tmp;
        }
    }
}
