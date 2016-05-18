using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class V_TCReportDAL : BaseFind<V_TCReport>
    {
        public List<V_TCReport> Get_V_TC_Repost_List_as_User(string gonghao, string Name, string department,string gangwei, string UserName, int? xukejibie, int? userStatus, string leixing, string groupname, string rolename,string juesejinyong)
        {
            System.Text.StringBuilder stb = new StringBuilder();
            stb.Append("SELECT * FROM dbo.V_TCReport where 1=1");
            List<System.Data.SqlClient.SqlParameter> parms = new List<System.Data.SqlClient.SqlParameter>();
            if (!string.IsNullOrEmpty(gonghao))
            {
                stb.Append(string.Format(" and mgonghao like @gonghao"));
                parms.Add(new System.Data.SqlClient.SqlParameter("@gonghao", "%" + gonghao + "%"));
            }

            if (!string.IsNullOrEmpty(leixing))
            {
                stb.Append(string.Format(" and mUserType='{0}'",leixing));
            }

            if (!string.IsNullOrEmpty(Name))
            {
                stb.Append(string.Format(" and ename like @uname", Name));
                parms.Add(new System.Data.SqlClient.SqlParameter("@uname", "%"+Name+"%"));
            }

            if (!string.IsNullOrEmpty(department))
            {
                stb.Append(string.Format(" and dname like @dept"));
                parms.Add(new System.Data.SqlClient.SqlParameter("@dept", "%" + department + "%"));
            }

            if (!string.IsNullOrEmpty(UserName))
            {
                stb.Append(string.Format(" and uUserId like @username"));
                parms.Add(new System.Data.SqlClient.SqlParameter("@username", "%" + UserName + "%"));
            }

            if (xukejibie != null)
                stb.Append(string.Format(" and uLicenseLevel ={0}", xukejibie));

            if (userStatus != null)
                stb.Append(string.Format(" and uUserStatus ={0}", userStatus));

            if (!string.IsNullOrEmpty(groupname))
            {
                stb.Append(string.Format(" and urp1 like @group"));
                parms.Add(new System.Data.SqlClient.SqlParameter("@group", "%" + groupname + "%"));
            
            }

            if (!string.IsNullOrEmpty(rolename))
            {
                stb.Append(string.Format(" and urp2 like @role"));
                parms.Add(new System.Data.SqlClient.SqlParameter("@role", "%" + rolename + "%"));

            }
            if (!string.IsNullOrEmpty(juesejinyong))
            {
                stb.Append(string.Format(" and urGroupStatus="+juesejinyong));
            }

            List<V_TCReport> list = new List<V_TCReport>();

            using (IAMEntities db = new IAMEntities())
            {
                list = db.ExecuteStoreQuery<V_TCReport>(stb.ToString(),parms.ToArray()).ToList<V_TCReport>();
            }
            return list;
        }





        public bool ReturnExcelExport_as_User(string filepath, string newfilepath, string gonghao, string Name, string department,string gangwei, string UserName, int? xukejibie, int? userStatus, string leixing, string groupname, string rolename,string juesejinyong)
        {
            List<V_TCReport> list = Get_V_TC_Repost_List_as_User( gonghao,  Name,  department,gangwei,  UserName, xukejibie,  userStatus,leixing,  groupname,  rolename,juesejinyong);
            System.Data.DataTable dt = null;
            dt = list.Select(item => new
            {
                工号 = item.mgonghao,
                姓名 = item.uUserName,
                部门=item.dname,
                TC账号 = item.uUserID,
                类型=item.mUserType,
                许可级别 = item.uLicenseLevel == 1 ? "作者" : item.uLicenseLevel == 0 ? "客户" : "其他",
                用户状态 = item.uUserStatus == 0 ? "活动" : "非活动",
                最后登录时间=item.uLastLoginTime,
                组名称 = GetGroupName(item.urMemo),
                角色名称 = GetRoleName(item.urMemo),
                角色状态 = item.urGroupStatus == 0 ? "非活动" : "活动"
            }).ToDataTable();
            return OLEDBExcelHelper.OLEDBExcelHelper.Write(filepath, newfilepath, dt);
        }



        public bool ReturnExcelExport_as_Role(string filepath, string newfilepath, string gonghao, string Name, string department,string gangwei, string UserName, int? xukejibie, int? userStatus, string leixing, string groupname, string rolename,string juesejinyong)
        {
            List<V_TCReport> list = Get_V_TC_Repost_List_as_User(gonghao, Name, department,gangwei, UserName, xukejibie, userStatus, leixing, groupname, rolename,juesejinyong);
            System.Data.DataTable dt = null;
            dt = list.Select(item => new
            {
                组名称 = GetGroupName(item.urMemo),
                角色名称 = GetRoleName(item.urMemo),
                角色状态=item.urGroupStatus==0?"非活动":"活动",
                工号 = item.mgonghao,
                姓名 = item.uUserName,
                部门 = item.dname,
                TC账号 = item.uUserID,
                类型 = item.mUserType,
                许可级别 = item.uLicenseLevel==1?"作者":item.uLicenseLevel==0?"客户":"其他",
                用户状态 = item.uUserStatus == 0 ? "活动" : "非活动",
                最后登录时间 = item.uLastLoginTime               

            }).ToDataTable();
            return OLEDBExcelHelper.OLEDBExcelHelper.Write(filepath, newfilepath, dt);
        }



        private string GetRoleName(string mRole)
        {
            if (string.IsNullOrWhiteSpace(mRole))
                return "";
            string[] roles = mRole.Split('.');
            if (roles != null && roles.Length > 0)
                return roles[0];
            else
                return string.Empty;
        }

        private string GetGroupName(string mRole)
        {
            string rolename = GetRoleName(mRole);
            if (string.IsNullOrWhiteSpace(mRole))
                return "";
            return mRole.Replace(rolename + ".", "");
        }

    }
}
