using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
   public class V_HRSm_User_RoleDAL:BaseFind<V_HRSm_User_Role>
    {
       /// <summary>
       /// Hr 用户报表，角色报表
       /// </summary>
       /// <returns></returns> 
       public List<V_HRSm_User_Role> GetV_HRSm_UserList(string gonghao,string name,string department,string hrusername,string logintype,string rolename,string companyname,string islock,string gangwei,string leixing)
       {
           List<V_HRSm_User_Role> list= NonExecute<List<V_HRSm_User_Role>>(db => {
               return db.V_HRSm_User_Role.ToList();
           });
           if (!string.IsNullOrEmpty(gonghao))
           {
               list = list.Where(item => item.egonghao!=null).ToList();
               list = list.Where(item => item.egonghao.Contains(gonghao.Trim())).ToList();
           }
           if (!string.IsNullOrEmpty(name))
           {
               list = list.Where(item => item.ename!=null).ToList();
               list = list.Where(item => item.ename.Contains(name.Trim())).ToList();
           }
           if (!string.IsNullOrEmpty(department))
           {
               list = list.Where(item => item.dname!=null).ToList();
               list = list.Where(item => item.dname.Contains(department.Trim())).ToList();
           }
           if (!string.IsNullOrEmpty(hrusername))
           {
               list = list.Where(item => item.hrusUser_code!=null).ToList();
               list = list.Where(item => item.hrusUser_code.Contains(hrusername.Trim())).ToList();
           }
           if (!string.IsNullOrEmpty(logintype))
           {
               list = list.Where(item => item.Authen_type!=null).ToList();
               list = list.Where(item => item.Authen_type.Contains(logintype.Trim())).ToList();
           }
           if (!string.IsNullOrEmpty(rolename))
           {
               list = list.Where(item => item.role_name!=null).ToList();
               list = list.Where(item => item.role_name.Contains(rolename.Trim())).ToList();
           }
           if (!string.IsNullOrEmpty(companyname))
           {
               list = list.Where(item=>item.CompanyName!=null).ToList();
               list = list.Where(item => item.CompanyName.Contains(companyname.Trim())).ToList();
           }
           if (!string.IsNullOrEmpty(islock))
           {
               list = list.Where(item=>item.Locked_tag!=null).ToList();
               list = list.Where(item => item.Locked_tag == islock).ToList();
           }
           if (!string.IsNullOrEmpty(gangwei))
           {
               list = list.Where(item=>item.epost!=null).ToList();
               list = list.Where(item => item.epost.Contains(gangwei)).ToList();
           }
           if (!string.IsNullOrEmpty(leixing))
           {
               list = list.Where(item=>item.mUserType==leixing).ToList();
           }
           return list;
       }

       /// <summary>
       /// 根据用户工号，搜索 Hr 用户报表，角色报表
       /// </summary>
       /// <returns></returns> 
       public List<V_HRSm_User_Role> GetV_HRSm_UserList(string gonghao)
       {
           List<V_HRSm_User_Role> list = new List<V_HRSm_User_Role>();
           string sql = "SELECT * FROM V_HRSm_User_Role WHERE 1=1 and dr=0 ";
           if (!string.IsNullOrEmpty(gonghao))
               sql += "AND mgonghao like @gonghao  ";
           using (IAMEntities db = new IAMEntities())
           {
               list = db.ExecuteStoreQuery<V_HRSm_User_Role>(sql,new System.Data.SqlClient.SqlParameter("@gonghao","%"+gonghao+"%")).ToList<V_HRSm_User_Role>();
           }
           return list;
       }


       /// <summary>
       /// 导出Hr 用户报表及角色报表
       /// </summary>
       /// <param name="filepath">模版路径</param>
       /// <param name="newfilepath">文件路径</param>
       /// <param name="Filed">筛选条件</param>
       /// <param name="User">是否来自用户报表页面，是为true，则为false</param>
       /// <returns>成功为true，失败为false</returns>
       public bool ReturnExcelExport(string filepath, string newfilepath, string gonghao, string name, string department, string hrusername, string logintype, string rolename, string companyname, string islock,string gangwei,bool User,string leixing)
       {
           List<V_HRSm_User_Role> list = GetV_HRSm_UserList( gonghao, name, department, hrusername, logintype, rolename, companyname, islock,gangwei,leixing);
           System.Data.DataTable dt = new System.Data.DataTable();
           if (User)
           {
               dt = list.Select(item => new {工号=item.mgonghao,部门=item.dname,姓名=item.ename, 账号 = item.hrusUser_code,类型=item.mUserType, 角色 = item.hrrRoleCode, 角色名称 = item.role_name, 公司名称 = item.CompanyName, 锁定标记 = item.Locked_tag, 登录方式 = item.Authen_type }).ToList().ToDataTable();
           }
           else
           {
               dt = list.Select(item => new { 工号 = item.mgonghao, 部门 = item.dname, 姓名 = item.ename, 角色 = item.hrrRoleCode, 账号 = item.hrusUser_code,类型=item.mUserType, 角色名称 = item.role_name, 公司名称 = item.CompanyName, 锁定标记 = item.Locked_tag, 登录方式 = item.Authen_type }).ToList().ToDataTable();
           }
           
           return  OLEDBExcelHelper.OLEDBExcelHelper.Write(filepath,newfilepath,dt);
       }
    }
}
