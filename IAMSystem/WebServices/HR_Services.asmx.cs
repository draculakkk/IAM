using BaseDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebServices
{
    /// <summary>
    /// HR_Services 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class HR_Services : System.Web.Services.WebService
    {

        [WebMethod]
        public List<EmployeeInfocs> GetHREmployeeList(string gonghao,string name,string department,            string gangwei,string shifoulizhi)
        {
            IAMEntities db = new IAMEntities();
            var list = db.HREmployee.Join(db.HRDepartment, item => item.dept, itm => itm.dept, (item, itm) => new EmployeeInfocs
            {
                code = item.code,
                posts = item.posts,
                deptname = itm.name,
                name = item.name,
                moblephone = item.moblePhone,
                topostDate = item.toPostsDate,
                leavePostsDate = item.leavePostsDate,
                userScope = item.userScope,
                isSync = item.isSync,
                syncdate = item.syncDate
            }).ToList();
            if (!string.IsNullOrEmpty(gonghao))
                list = list.Where(item => item.code.Contains(gonghao)).ToList();
            if (!string.IsNullOrEmpty(name))
                list = list.Where(itme => itme.name.Contains(name)).ToList();
            if (!string.IsNullOrEmpty(department))
            {
                list = list.Where(item => item.deptname != null).ToList();
                list = list.Where(item => item.deptname.Contains(department)).ToList();
            }

            if (!string.IsNullOrEmpty(gangwei))
            {
                list = list.Where(item => item.posts != null).ToList();
                list = list.Where(item => item.posts.Contains(gangwei)).ToList();
            }
            if (!string.IsNullOrEmpty(shifoulizhi))
            {
                if (shifoulizhi.Equals("0"))//在职
                    list = list.Where(item => item.leavePostsDate == null).ToList();
                if (shifoulizhi.Equals("1")) //离职
                    list = list.Where(item => item.leavePostsDate != null).ToList();
            }

            return list;
        }

    }
}
