using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
   public class V_AdcomputerWorkGroupInfoDAL:BaseFind<V_AdcomputerWorkGroupInfo>
    {
       /// <summary>
       /// 获取ad 计算机工作组信息
       /// </summary>
       /// <param name="gonghao">工号</param>
       /// <param name="department">部门</param>
       /// <param name="computername">计算机名称</param>
       /// <param name="workgroup">工作组</param>
       /// <returns></returns>
       public List<V_AdcomputerWorkGroupInfo> GetList(string gonghao,string name, string department,string gangwei, string computername,string leixing,string workgroup,string jinyong)
       {
           IAMEntities db=new IAMEntities();
           var list = db.V_AdcomputerWorkGroupInfo.Where(item=>1==1);
           if (!string.IsNullOrEmpty(gonghao))
           {
               list = list.Where(item=>item.bgonghao!=null);
               list = list.Where(item => item.bgonghao.Contains(gonghao.Trim()));
           }
           if (!string.IsNullOrEmpty(name))
           {
               list = list.Where(item=>item.ename!=null);
               list = list.Where(item => item.ename.Contains(name));
           }
           if (!string.IsNullOrEmpty(department))
           {
               list = list.Where(item=>item.pname!=null);
               list = list.Where(item => item.pname.Contains(department.Trim()));
           }
           if (!string.IsNullOrEmpty(gangwei))
           {
               list = list.Where(item=>item.eposts!=null);
               list = list.Where(item=>item.eposts.Contains(gangwei));
           }


           if (!string.IsNullOrEmpty(computername))
           {
               list = list.Where(item=>item.aName!=null);
               list = list.Where(item => item.aName.Contains(computername.Trim()));
           }

           if (!string.IsNullOrEmpty(leixing))
           {
               list = list.Where(item=>item.bUserType==leixing);
           }

           if (!string.IsNullOrEmpty(jinyong))
           {
               int jj=Convert.ToInt32(jinyong);
               list = list.Where(item=>item.aenable==jj);
           }

           if (!string.IsNullOrEmpty(workgroup))
           {
               list = list.Where(item => item.wworkgroup != null) ;
               list = list.Where(item => item.wworkgroup.Contains(workgroup.Trim()));
           }
           return list.ToList();
       }

      /// <summary>
      /// 导出ad 计算机用户信息
      /// </summary>
      /// <param name="filepath"></param>
      /// <param name="newfilepath"></param>
      /// <param name="gonghao"></param>
      /// <param name="department"></param>
      /// <param name="computername"></param>
      /// <param name="workgroup"></param>
      /// <returns></returns>

       public bool ReturnExcelReport_Computer(string filepath, string newfilepath, string gonghao,string name, string department,string gangwei, string computername,string leixing, string workgroup,string jinyong)
       {
           List<V_AdcomputerWorkGroupInfo> list = GetList(gonghao,name,department,gangwei,computername,leixing,workgroup,jinyong);
           System.Data.DataTable dt = null;
           dt = list.Select(item => new { 
           工号=item.bgonghao,
           部门=item.pname,
           计算机名称=item.aName,
           类型=item.bUserType,
           描述=item.aDESCRIPTION,
           工作组=item.wworkgroup,
           最后登录时间=item.aExpiryDate
           }).ToDataTable();
           return OLEDBExcelHelper.OLEDBExcelHelper.Write(filepath,newfilepath,dt);
       }

       /// <summary>
       /// 导出计算机工作组信息
       /// </summary>
       /// <param name="filepath"></param>
       /// <param name="newfilepath"></param>
       /// <param name="gonghao"></param>
       /// <param name="department"></param>
       /// <param name="computername"></param>
       /// <param name="workgroup"></param>
       /// <returns></returns>
       public bool ReturnExcelReport_workgroup(string filepath, string newfilepath, string gonghao,string name, string department,string gangwei, string computername,string leixing, string workgroup,string jinyong)
       {
           List<V_AdcomputerWorkGroupInfo> list = GetList(gonghao,name, department,gangwei, computername,leixing, workgroup,jinyong);
           System.Data.DataTable dt = null;
           dt = list.Select(item => new
           {
               工作组 = item.wworkgroup,
               工号 = item.bgonghao,
               部门 = item.pname,
               计算机名称 = item.aName,
               类型=item.bUserType,
               描述 = item.aDESCRIPTION,               
               最后登录时间 = item.aExpiryDate
           }).ToDataTable();
           return OLEDBExcelHelper.OLEDBExcelHelper.Write(filepath, newfilepath, dt);
       }
    }
}
