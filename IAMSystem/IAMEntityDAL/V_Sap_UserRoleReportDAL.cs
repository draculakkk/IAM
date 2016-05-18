using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class V_Sap_UserRoleReportDAL : BaseFind<V_Sap_UserRoleReport>
    {
        /// <summary>
        /// 获取sap 用户、角色报表
        /// </summary>
        /// <param name="Filed"></param>                                  
        /// <param name="IsUser"></param>
        /// <returns></returns>
        public List<V_Sap_UserRoleReport> GetV_Sap_UserRoleReport(string gonghao,string name,string department,string gangwei,string sapname,string leixing,string startdates,string enddates,string roleid,string rolename,string userType,string startdatee,string enddatee)
        {
            System.Text.StringBuilder stb=new StringBuilder();
            stb.Append("select * from dbo.V_Sap_UserRoleReport where 1=1");
            List<System.Data.SqlClient.SqlParameter> parms = new List<System.Data.SqlClient.SqlParameter>();
            if (!string.IsNullOrEmpty(gonghao))
            {
                stb.Append(string.Format(" and mgonghao like @gonghao"));
                parms.Add(new System.Data.SqlClient.SqlParameter("@gonghao","%"+gonghao+"%"));
            }

            if (!string.IsNullOrEmpty(name))
            {
                stb.Append(string.Format(" and ename like @name"));
                parms.Add(new System.Data.SqlClient.SqlParameter("@name","%"+name+"%"));
            }

            if (!string.IsNullOrEmpty(department))
            {
                stb.Append(string.Format(" and dname like @dept"));
                parms.Add(new System.Data.SqlClient.SqlParameter("@dept","%"+department+"%"));
            }

            if (!string.IsNullOrEmpty(gangwei))
            {
                stb.Append(" and eposts like @eposts");
                parms.Add(new System.Data.SqlClient.SqlParameter("@eposts","%"+gangwei+"%"));
            }

            if (!string.IsNullOrEmpty(sapname))
            {
                stb.Append(string.Format(" and uBAPIBNAME like @uba"));
                parms.Add(new System.Data.SqlClient.SqlParameter("@uba","%"+sapname+"%"));
            }

            if (!string.IsNullOrEmpty(leixing))
            {
                stb.Append(string.Format(" and mUserType='{0}'",leixing));
            }

            if (!string.IsNullOrEmpty(userType))
            {
                stb.Append(string.Format(" and uUSERTYPE='{0}'",userType));
            }

            //if (!string.IsNullOrEmpty(startdates) && !string.IsNullOrEmpty(enddates))
            //{
            //    stb.Append(string.Format(" and urStartDate like '%{0}%'", startdate));
            //}
                

            //if (!string.IsNullOrEmpty(enddate))
            //    stb.Append(string.Format(" and urEndDate like '%{0}%'", enddate));

            //if (!string.IsNullOrEmpty(roleid))
            //    stb.Append(string.Format(" and urRoleID like '%{0}%'", roleid));

            //if (!string.IsNullOrEmpty(rolename))
            //    stb.Append(string.Format(" and urRoleNAME like '%{0}%'", rolename));

            

            var list = new IAMEntities().ExecuteStoreQuery<V_Sap_UserRoleReport>(stb.ToString()).ToList<V_Sap_UserRoleReport>();
            return list;
        }


        /// <summary>
        /// 获取sap 用户 角色报表，在角色模版查询中使用
        /// </summary>
        /// <param name="gonghao"></param>
        /// <returns></returns>
        public List<V_Sap_UserRoleReport> GetV_Sap_UserRoleReport(string gonghao)
        {
            System.Text.StringBuilder stb = new StringBuilder();
            stb.Append("select * from dbo.V_Sap_UserRoleReport where 1=1");
            if (!string.IsNullOrEmpty(gonghao))
                stb.Append(string.Format(" and mgonghao like '%{0}%'", gonghao));
            List<V_Sap_UserRoleReport> list = new List<V_Sap_UserRoleReport>();
            using (IAMEntities db = new IAMEntities())
            {
                list = db.ExecuteStoreQuery<V_Sap_UserRoleReport>(stb.ToString()).ToList<V_Sap_UserRoleReport>();
            }
            return list;
        }

        /// <summary>
        /// 导出 Excel数据
        /// </summary>
        /// <param name="filepath">Excel模版文件</param>
        /// <param name="newfilepath">文件存放路径</param>
        /// <param name="Filed">筛选条件</param>
        /// <param name="User">是否是为用户</param>
        /// <returns></returns>
        public bool ReturnExcelExport(string filepath, string newfilepath, string gonghao, string name, string department,string gangwei, string sapname,string leixing, string startdates, string enddates, string roleid, string rolename, string userType,string startdatee,string enddatee, bool User)
        {
            List<V_Sap_UserRoleReport> SapUserList = GetV_Sap_UserRoleReport( gonghao, name, department,gangwei, sapname,leixing, startdates, enddates, roleid, rolename, userType,startdatee,enddatee);
            System.Data.DataTable dt = null;
            if (User)
            {
                dt = SapUserList.Select(item => new
                {
                    工号 = item.mgonghao,
                    姓名 = item.ename,
                    部门=item.dname,
                    SAP账号 = item.uBAPIBNAME,
                    类型=item.mUserType,
                    角色 = item.srROLEID,
                    角色名 = item.srROLENAME,
                    有效期从 = item.urStartDate,
                    有效期至 = item.urEndDate,
                    
                }).ToList().ToDataTable();
            }
            else
            {
                dt = SapUserList.Select(item => new
                {
                    角色 = item.srROLEID,
                    角色名 = item.srROLENAME,
                    工号 = item.mgonghao,
                    姓名 = item.ename,
                    部门=item.dname,
                    SAP账号 = item.uBAPIBNAME, 
                    类型=item.mUserType,
                    有效期从 = item.urStartDate,
                    有效期至 = item.urEndDate,
                  
                }).ToList().ToDataTable();
                  //域部门 = item.uDEPARTMENT,
                  //  语言=item.uLANGUAGE,
                  //  移动电话=item.uMOBLIE_NUMBER,
                  //  Email=item.uEMAIL,
                  //  sap账号类型=item.mUserType,
                  //  许可证编号=item.u
            }

            return OLEDBExcelHelper.OLEDBExcelHelper.Write(filepath,newfilepath,dt);
        }

    }
}
