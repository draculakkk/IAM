using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IAMEntityDAL
{
    public class ExcelHelper
    {
        /// <summary>
        /// 生成excel文档
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="newfilepath"></param>
        /// <param name="status"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool ReturnExcelExport(string filepath, string newfilepath, DataTable dt)
        {
            return OLEDBExcelHelper.OLEDBExcelHelper.Write(filepath, newfilepath, dt);
        }
    }
}
