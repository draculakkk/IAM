using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{


    public class V_AD_UserWorkGroupDAL : BaseFind<V_AD_UserWorkGroup>
    {
        /// <summary>
        /// 导出Ad 用户或组报表
        /// </summary>
        /// <param name="Filed">筛选条件</param>
        /// <param name="IsUser">搜索类型，true搜索用户，false搜索组</param>
        /// <returns></returns>
        public List<V_AD_UserWorkGroup> Get_V_AD_UserWorkGroupList(string gonghao, string department, string name,string gangwei, string adusername,string leixing, DateTime? StartDate, DateTime? EndDate, string shifouqiyong, string workgroupName)
        {
            List<V_AD_UserWorkGroup> list = new List<V_AD_UserWorkGroup>();
            System.Text.StringBuilder stb = new StringBuilder();
            List<System.Data.SqlClient.SqlParameter> parms = new List<System.Data.SqlClient.SqlParameter>();
            stb.Append("SELECT * FROM V_AD_UserWorkGroup where 1=1 and isdr<>1");

            if (!string.IsNullOrEmpty(gonghao))
            {
                stb.Append(string.Format(" and ecode like @gonghao"));
                parms.Add(new System.Data.SqlClient.SqlParameter("@gonghao", "%" + gonghao + "%"));
            }

            if (!string.IsNullOrEmpty(department))
            {
                stb.Append(string.Format(" and dname like @dept"));
                parms.Add(new System.Data.SqlClient.SqlParameter("@dept", "%" + department + "%"));
            }

            if (!string.IsNullOrEmpty(name))
            {
                stb.Append(string.Format(" and ename like @name"));
                parms.Add(new System.Data.SqlClient.SqlParameter("@name", "%" + name + "%"));
            }

            if (!string.IsNullOrEmpty(gangwei))
            {
                stb.Append(" and uPosts like @posts");
                parms.Add(new System.Data.SqlClient.SqlParameter("@posts","%"+gangwei+"%"));
            }

            if (!string.IsNullOrEmpty(adusername))
            {
                stb.Append(string.Format(" and (uUserID like @adname or uAccountname like @ac)"));
                parms.Add(new System.Data.SqlClient.SqlParameter("@adname", "%" + adusername + "%"));
                parms.Add(new System.Data.SqlClient.SqlParameter("@ac", "%" + adusername + "%"));
            }

            if (!string.IsNullOrEmpty(leixing))
            {
                stb.Append(string.Format(" and mUserType='{0}'",leixing));
            }

            if (!string.IsNullOrEmpty(shifouqiyong))
            {
                stb.Append(string.Format(" and uENABLE="+shifouqiyong));
            }

            if (StartDate != null && EndDate != null)
            {
                stb.Append(string.Format(" and uexpirydate >=@start and uexpirydate <=@enddate"));
                parms.Add(new System.Data.SqlClient.SqlParameter("@start", StartDate));
                parms.Add(new System.Data.SqlClient.SqlParameter("@enddate", EndDate));
            }
            else
            {
                stb.Append(" and (uexpirydate is null or uexpirydate<'1980-12-31')");
            }

            if (!string.IsNullOrEmpty(workgroupName))
            {
                stb.Append(string.Format(" and uwGroupName LIKE @work"));
                parms.Add(new System.Data.SqlClient.SqlParameter("@work","%"+workgroupName+"%"));
            }

            using (IAMEntities db = new IAMEntities())
            {
                list = db.ExecuteStoreQuery<V_AD_UserWorkGroup>(stb.ToString(),parms.ToArray()).ToList<V_AD_UserWorkGroup>();
            }
            return list;
        }


        /// <summary>
        /// 在角色模版中使用，根据工号查询
        /// </summary>
        /// <param name="gonghao">工号</param>
        /// <returns></returns>
        public List<V_AD_UserWorkGroup> Get_V_AD_UserWorkGroupList(string gonghao)
        {
            List<V_AD_UserWorkGroup> list = new List<V_AD_UserWorkGroup>();
            string sql = "select * from V_AD_UserWorkGroup where 1=1";
            if (!string.IsNullOrEmpty(gonghao))
                sql += " and mgonghao like '%" + gonghao + "%'";
            using (IAMEntities db = new IAMEntities())
            {
                list = db.ExecuteStoreQuery<V_AD_UserWorkGroup>(sql).ToList<V_AD_UserWorkGroup>();
            }
            return list;
        }


        /// <summary>
        /// 导出Ad 用户或组报表
        /// </summary>
        /// <param name="filepath">Excel模版路径</param>
        /// <param name="newfilepath">Excel文件路径</param>
        /// <param name="Filed">筛选条件</param>
        /// <param name="IsUser">导出类型，true搜索用户，false搜索组</param>
        /// <returns></returns>
        public bool ExcelExport(string filepath, string newfilepath, string gonghao, string department, string name,string gangwei, string adusername,string leixing, DateTime? StartDate, DateTime? EndDate,string qiyong, string workgroupName, bool IsUser)
        {
            List<V_AD_UserWorkGroup> list = Get_V_AD_UserWorkGroupList(gonghao, department, name,gangwei, adusername,leixing, StartDate, EndDate,qiyong, workgroupName);
            System.Data.DataTable dt = null;
            if (IsUser)
            {
                dt = list.Select(item => new
                {
                   工号=item.ecode,
                   HR部门=item.dname,
                   姓名=item.ename,
                   账号=item.uUserID,
                   类型=item.mUserType,
                   组名称=item.uwGroupName,
                   CN=item.uCnName,
                   显示名=item.uDisplayName,
                   职级=item.uJob,
                   工作电话=item.uADMobile,
                   部门=item.uDepartment,
                   描述=item.uDESCRIPTION,
                   邮件=item.uEmail,
                   数据库=item.EmailDatabase,
                   lync=item.uLync,
                   映射路径=item.uPath,
                   启用状态=item.uENABLE==true?"启用":"禁用",
                   最后登陆时间=item.uLastLoginTime
                }).ToDataTable();
            }
            else
            {
                dt = list.Select(item => new
                {
                    组名称 = item.uwGroupName,
                    工号 = item.ecode,
                    HR部门 = item.dname,
                    姓名 = item.ename,
                    账号 = item.uUserID,
                    类型=item.mUserType,
                    CN = item.uCnName,
                    显示名 = item.uDisplayName,
                    职级 = item.uJob,
                    工作电话 = item.uADMobile,
                    部门 = item.uDepartment,
                    描述 = item.uDESCRIPTION,
                    邮件 = item.uEmail,
                    数据库 = item.EmailDatabase,
                    lync = item.uLync,
                    映射路径 = item.uPath,
                    启用状态 = item.uENABLE == true ? "启用" : "禁用",
                    最后登陆时间 = item.uLastLoginTime
                }).ToDataTable();
            }
            return OLEDBExcelHelper.OLEDBExcelHelper.Write(filepath, newfilepath, dt);

        }
    }
}
