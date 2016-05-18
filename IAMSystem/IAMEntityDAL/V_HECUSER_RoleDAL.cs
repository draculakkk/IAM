using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
    public class V_HECUSER_RoleDAL : BaseFind<V_HECUSER_Role>
    {

        /// <summary>
        /// 或去HEC 用户或角色报表
        /// </summary>
        /// <param name="Filed">条件搜索</param>
        /// <param name="IsUser">是否搜素用户，true为是，false为否</param>
        /// <returns></returns>
        public List<V_HECUSER_Role> Get_HECUser_RoleList(string gonghao, string name, string department, string gangwei, string hecname, string leixing, DateTime? startdate, DateTime? enddate, string rolename, string companyname, string jinyong)
        {
            List<V_HECUSER_Role> list = NonExecute<List<V_HECUSER_Role>>(db =>
            {
                return db.V_HECUSER_Role.ToList();
            });

            if (!string.IsNullOrEmpty(gonghao))
            {
                list = list.Where(item => item.mgonghao.Contains(gonghao)).ToList();
            }
            if (!string.IsNullOrEmpty(name))
            {
                list = list.Where(item => item.ename != null).ToList();
                list = list.Where(item => item.ename.Contains(name)).ToList();
            }
            if (!string.IsNullOrEmpty(department))
            {
                list = list.Where(item => item.dname != null).ToList();
                list = list.Where(item => item.dname.Contains(department)).ToList();
            }
            if (!string.IsNullOrEmpty(gangwei))
            {
                list = list.Where(item => item.posts != null).ToList();
                list = list.Where(item => item.posts.Contains(gangwei)).ToList();
            }
            if (!string.IsNullOrEmpty(hecname))
            {
                list = list.Where(item => item.uUSERNAME != null).ToList();
                list = list.Where(item => item.uUSERNAME.Contains(hecname)).ToList();
            }
            if (!string.IsNullOrEmpty(leixing))
            {
                list = list.Where(item => item.mUserType == leixing).ToList();
            }

            if (startdate != null)
            {
                list = list.Where(item => item.uSTARTDATE != null).ToList();
                list = list.Where(item => Convert.ToDateTime(item.uSTARTDATE) >= startdate).ToList();
            }
            if (enddate != null)
            {
                list = list.Where(item => item.uENDDATE != null).ToList();
                string tmp = Convert.ToDateTime(enddate).ToString("yyyy-MM-dd");
                list = list.Where(item => item.uENDDATE.Contains(tmp)).ToList();
            }

            if (!string.IsNullOrEmpty(rolename))
            {
                list = list.Where(item => item.rROLENAME != null).ToList();
                list = list.Where(item => item.rROLENAME.Contains(rolename)).ToList();
            }
            if (!string.IsNullOrEmpty(companyname))
            {
                list = list.Where(item => item.cCOMPNYFULLNAME != null).ToList();
                list = list.Where(item => item.cCOMPNYFULLNAME.Contains(companyname)).ToList();
            }

            if (!string.IsNullOrEmpty(jinyong))
            {
                int jy = 0;
                if (jinyong == "1")
                    jy = 1;
                else
                    jy = 0;
                list = list.Where(item => item.ISDISABLED == jy).ToList();
            }
            return list;
        }

        /// <summary>
        /// 获取HEC 用户或角色报表，根据工号
        /// </summary>
        /// <param name="gonghao">条件搜索</param>
        /// <returns></returns>
        public List<V_HECUSER_Role> Get_HECUser_RoleList(string gonghao)
        {
            IAMEntities db = new IAMEntities();
           var list = db.V_HECUSER_Role.Where(x=>1==1);
            list = list.Where(item=>item.mgonghao!=null);
            if (!string.IsNullOrEmpty(gonghao))
                return list.Where(item => item.mgonghao.Trim() == gonghao).ToList();
            return list.ToList();
        }

        /// <summary>
        /// 导出 excel文件
        /// </summary>
        /// <param name="filepath">excel 模版路径</param>
        /// <param name="newfilepath">复制文件路径</param>
        /// <param name="filed">筛选条件</param>
        /// <param name="Isuser">是否为用户筛选条件，是为true，否为false</param>
        /// <returns></returns>

        public bool ReturnExcelExport(string filepath, string newfilepath, string gonghao, string name, string department, string gangwei, string hecname, string leixing, DateTime? startdate, DateTime? enddate, string rolename, string companyname,string jinyong, bool Isuser)
        {
            List<V_HECUSER_Role> list = Get_HECUser_RoleList(gonghao, name, department,gangwei, hecname,leixing,startdate, enddate, rolename, companyname,jinyong );
            list = list.Where(item => item.ename != "SUPER").ToList();
            System.Data.DataTable dt = null;
            if (Isuser)
            {
                
                dt = list.Select(item => new
                {
                    工号 = item.code,
                    姓名 = item.ename,
                    部门 = item.dname,
                    账号 = item.uUSERNAME,
                    账号有效期从=item.uSTARTDATE,
                    账号有效期至 = Convert.ToDateTime(item.uENDDATE).ToString("yyyy-MM-dd") == "2099-12-31" ? "" : item.uENDDATE,
                    类型 = item.mUserType,
                    角色 = item.rROLECODE,
                    角色名称 = item.rROLENAME,
                    公司名称 = item.cCOMPNYFULLNAME,
                    角色有效期从 = item.uROLESTARTDATE,
                    角色有效期至 = item.uROLEENDDATE,
                    是否冻结 = item.frozen_flag == "Y" ? "是" : "否",
                    冻结时间 = item.frozen_date
                }).ToList().ToDataTable();
            }
            else
            {
                
                dt = list.Select(item => new
                {
                    角色 = item.rROLECODE,
                    角色名称 = item.rROLENAME,
                    工号 = item.code,
                    部门 = item.dname,
                    姓名 = item.ename,
                    账号 = item.uUSERNAME,
                    账号有效期从=item.uSTARTDATE,
                    账号有效期至=Convert.ToDateTime(item.uENDDATE).ToString("yyyy-MM-dd")=="2099-12-31"?"":item.uENDDATE,
                    类型 = item.mUserType,
                    公司名称 = item.cCOMPNYFULLNAME,
                    角色有效期从 = item.uROLESTARTDATE,
                    角色有效期至 = item.uROLEENDDATE,
                    是否冻结 = item.frozen_flag=="Y"?"是":"否",
                    冻结时间 = item.frozen_date
                }).ToList().ToDataTable();
            }
            return OLEDBExcelHelper.OLEDBExcelHelper.Write(filepath, newfilepath, dt);
        }

    }
}
