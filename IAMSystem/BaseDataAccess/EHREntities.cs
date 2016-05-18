using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseDataAccess
{
   public partial class EHREntities
    {

       /// <summary>
        /// 提供HR所有人员信息视图view_bd_psndoc
       /// </summary>
       public class view_bd_psndoc
       {
           public string Pk_psndoc { get; set; }//人员主键

           public string Psncode { get; set; }//人员编码

           public string Jobcode { get; set; }//岗位编码

           public string Jobname { get; set; }//岗位名称

           public string Pk_deptdoc { get; set; }//部门主键

           public string Psnname { get; set; }//人员姓名
              
           public string Mobile { get; set; }//手机

           public string Indutydate { get; set; }//到职日期

           public DateTime? Outdutydate { get; set; }//离职日期

           public int psnlscope { get; set; }//人员归属范围

           public string Pk_psncl { get; set; }//人员类别编码

           public string Psnclasscode { get; set; }//人员类别名称

           public string Pk_corp { get; set; }//公司

       }


       /// <summary>
       /// 视图view_sm_user显示所有操作员记录
       /// </summary>
       public class view_sm_user
       {
           /// <summary>
           /// 生效日期
           /// </summary>
           public string Able_time { get; set; }

           ///<summary>
           ///认证方式
           ///<summary>
           public string Authen_type { get; set; }


           /// <summary>
           /// 操作员主键
           /// </summary>
           public string Cuserid { get; set; }


           /// <summary>
           /// 失效日期
           /// </summary>
           public string Disable_time { get; set; }


           /// <summary>
           /// 删除标记
           /// </summary>
           public int Dr { get; set; }


           /// <summary>
           /// 是否ca用户，Y,N
           /// </summary>
           public string Isca { get; set; }


           /// <summary>
           /// 是否关键用户
           /// </summary>
           public string KeyUser { get; set; }


           /// <summary>
           /// 语言
           /// </summary>
           public string Langcode { get; set; }


           /// <summary>
           /// 锁定标记
           /// </summary>
           public string Locked_tag { get; set; }


           /// <summary>
           /// 所属公司
           /// </summary>
           public string Pk_corp { get; set; }


           /// <summary>
           /// 密码级别
           /// </summary>
           public string PwdLevelCode { get; set; }


           /// <summary>
           /// 修改日期
           /// </summary>
           public string Pwdparam { get; set; }


           /// <summary>
           ///密码设置类型 
           /// </summary>
           public int Pwdtype { get; set; }


           /// <summary>
           /// 时间戳
           /// </summary>
           public string Ts { get; set; }


           /// <summary>
           /// 操作员编码
           /// </summary>
           public string User_code { get; set; }


           /// <summary>
           /// 操作员名称
           /// </summary>
           public string User_name { get; set; }


           /// <summary>
           /// 操作员名称
           /// </summary>
           public string User_note { get; set; }


           /// <summary>
           /// 操作员密码
           /// </summary>
           public string User_pagessword { get; set; }
       }


       /// <summary>
       /// 导出角色，创建表hz_roledel记录HR每天删除的角色信息
       /// </summary>
       public class hz_Roledel
       {
           /// <summary>
           /// 角色主键
           /// </summary>
           public string Pk_role { get; set; }

           /// <summary>
           /// 编码
           /// </summary>
           public string Role_code { get; set; }

           /// <summary>
           /// 名称
           /// </summary>
           public string Role_name { get; set; }

           /// <summary>
           /// 所属公司
           /// </summary>
           public string Pk_corp { get; set; }

           /// <summary>
           /// 所属公司
           /// </summary>
           public int Resource_type { get; set; }

           /// <summary>
           /// 时间戳
           /// </summary>
           public string Ts { get; set; }

           /// <summary>
           /// 删除标记
           /// </summary>
           public int Dr { get; set; }

       }



       /// <summary>
       /// 创建表hz_r_c_allocdel记录HR每天删除的关系信息,   角色分配公司关系
       /// </summary>
       public class hz_r_c_allocdel
       {
           /// <summary>
           /// 关心主键
           /// </summary>
           public string Pk_role_corp_alloc { get; set; }

           /// <summary>
           /// 公司主键
           /// </summary>
           public string Pk_corp { get; set; }

           /// <summary>
           /// 角色主键
           /// </summary>
           public string Pk_role { get; set; }

           /// <summary>
           /// 是否删除
           /// </summary>
           public int Dr { get; set; }


           /// <summary>
           /// 时间戳
           /// </summary>
           public string Ts { get; set; }
       }


       /// <summary>
       /// 用户角色关系信息
       /// </summary>
       public class view_sm_u_r
       {
           /// <summary>
           /// 委派主键
           /// </summary>
           public string Pk_user_role { get; set; }

           /// <summary>
           /// 操作员主键
           /// </summary>
           public string CuserId { get; set; }

           /// <summary>
           /// 公司主键
           /// </summary>
           public string Pk_corp { get; set; }

           /// <summary>
           /// 角色主键
           /// </summary>
           public string Pk_role { get; set; }

           /// <summary>
           /// 时间戳
           /// </summary>
           public string Ts { get; set; }

           /// <summary>
           /// 删除标记
           /// </summary>
           public int Dr { get; set; }
       }


       public class view_bd_deptdoc
       {
           /// <summary>
           /// 部门主键
           /// </summary>
           public string Pk_deptdoc { get; set; }

           /// <summary>
           /// 部门编码
           /// </summary>
           public string deptcode { get; set; }

           /// <summary>
           /// 部门名称
           /// </summary>
           public string Deptname { get; set; }

           /// <summary>
           /// 上级部门主键
           /// </summary>
           public string pk_fathedept { get; set; }

           /// <summary>
           /// hr撤销标记
           /// </summary>
           public string hrcanceled { get; set; }

           /// <summary>
           /// 封存标记
           /// </summary>
           public string canceled { get; set; }

           /// <summary>
           /// 封存日期
           /// </summary>
           public string Canceldate { get; set; }

           /// <summary>
           /// 公司
           /// </summary>
           public string Pk_corp { get; set; }


       }

    }
}
