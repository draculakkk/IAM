using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BaseDataAccess;
using IAMEntityDAL;
using System.Web.Services.Protocols;

namespace IAM.BLL
{
    public class SAPSyncServices
    {
        private System.Net.NetworkCredential UserADCount()
        {     
            System.Net.NetworkCredential fff = new System.Net.NetworkCredential();
            fff.UserName = "C_IAMWEB";//System.Configuration.ConfigurationManager.AppSettings["sapUserAD4"];
            fff.Password = @"Cp}{44~(rF7XBqGS7t)G[Es5w4kG}p=s>5{hM-H(";//System.Configuration.ConfigurationManager.AppSettings["sapUserPassword"];
            return fff;
        }


        SAPRoleDAL RoleServices = new SAPRoleDAL();
        /// <summary>
        /// 同步sap Role 任务
        /// </summary>
        /// <param name="AllCount"></param>
        /// <param name="Okcount"></param>
        public void SyncSapRoles(out int AllCount, out int Okcount)
        {
            WebReferenceSapRoleList.ZIAM_GET_ROLES_LISTService _roleServices = new WebReferenceSapRoleList.ZIAM_GET_ROLES_LISTService();
            _roleServices.Url = "http://10.91.234.29:8006/sap/bc/srt/rfc/sap/ziam_get_roles_list/600/ziam_get_roles_list/ziam_get_roles_list";//"http://10.91.234.28:8006/sap/bc/srt/rfc/sap/ziam_get_roles_list/600/ziam_get_roles_list/ziam_get_roles_list";
            _roleServices.PreAuthenticate = true;
            _roleServices.SoapVersion = SoapProtocolVersion.Soap12;
            _roleServices.Credentials = UserADCount();
            var xxf = _roleServices.ZiamGetRolesList(new WebReferenceSapRoleList.ZiamGetRolesList() { }).RolesListEt.ToList();
            AllCount = xxf.Count;
            Okcount = 0;
            foreach (var item in xxf)
            {
                
                SAP_Role entity = new BaseDataAccess.SAP_Role() { ROLEID = item.AgrName, ROLENAME = item.Text, STARTDATE = DateTime.Now.ToString("yyyy-MM-dd"), ENDDATE = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd") };
                try
                {

                    RoleServices.AddSapRole(entity);
                    Okcount++;
                }
                catch (Exception ex)
                {
#if DEBUG
                    throw ex;
#else
                    continue;
#endif
                }
            }
        }

        /// <summary>
        /// 同步sap Role 任务 sap 测试环境webservcies 2014-07-30
        /// </summary>
        /// <param name="AllCount"></param>
        /// <param name="Okcount"></param>
        public void SyncSapRolesnew(out int AllCount, out int Okcount)
        {
            WebReferenceSapRoleNew.service _roleServices = new WebReferenceSapRoleNew.service();
            _roleServices.Url = "http://10.91.234.29:8006/sap/bc/srt/rfc/sap/ziam_get_roles_list/600/ziam_get_roles_list/ziam_get_roles_list";//"http://10.91.234.28:8006/sap/bc/srt/rfc/sap/ziam_get_roles_list/600/ziam_get_roles_list/ziam_get_roles_list";
            _roleServices.PreAuthenticate = true;
            _roleServices.SoapVersion = SoapProtocolVersion.Soap11;
            
            _roleServices.Credentials = UserADCount();
            var xxf = _roleServices.ZiamGetRolesList(new WebReferenceSapRoleNew.ZiamGetRolesList()).RolesListEt.ToList();
            AllCount = xxf.Count;
            Okcount = 0;
           
            foreach (var item in xxf)
            {
                SAP_Role entity = new BaseDataAccess.SAP_Role() { ROLEID = item.AgrName, ROLENAME = item.Text, STARTDATE = DateTime.Now.ToString("yyyy-MM-dd"), ENDDATE = DateTime.Now.AddYears(1).ToString("yyyy-MM-dd") };
                try
                {

                    RoleServices.AddSapRole(entity);
                    Okcount++;
                }
                catch (Exception ex)
                {
#if DEBUG
                    throw ex;
#else
                    continue;
#endif
                }
            }
        }

        public void SyncSapUserInfo(out int AllCount, out int Okcount)
        {
            WebReferenceSapUserListNew1.service _userlistServices = new WebReferenceSapUserListNew1.service();
           // WebReferenceSapUserList.ZIAM_GET_SAP_USER_LISTService _userlistServices = new WebReferenceSapUserList.ZIAM_GET_SAP_USER_LISTService();
            _userlistServices.PreAuthenticate = true;
            _userlistServices.SoapVersion = SoapProtocolVersion.Default;
            _userlistServices.Credentials = UserADCount();

            var xxxxf = _userlistServices.ZiamGetSapUserList(new WebReferenceSapUserListNew1.ZiamGetSapUserList() {  MaxRows=int.MaxValue}).Userlist;

            //List<WebReferenceSapUserInfo.ZiamReadSapUserInfomationResponse> UserInformationList = new List<WebReferenceSapUserInfo.ZiamReadSapUserInfomationResponse>();
            System.Collections.Generic.Dictionary<string, WebReferenceSapUserInfoNew.ZiamReadSapUserInfomationResponse> dirc = new Dictionary<string, WebReferenceSapUserInfoNew.ZiamReadSapUserInfomationResponse>();

            List<string> bmpbName = new List<string>();


            int flag = 0;
            foreach (var userlist in xxxxf)
            {
                
                try
                {
                    WebReferenceSapUserInfoNew.service _userInfoServices = new WebReferenceSapUserInfoNew.service();
                    //WebReferenceSapUserInfo.ZIAM_READ_SAP_USER_INFOMATIONService _userInfoServices = new WebReferenceSapUserInfo.ZIAM_READ_SAP_USER_INFOMATIONService();
                    _userInfoServices.PreAuthenticate = true;
                    _userInfoServices.SoapVersion = SoapProtocolVersion.Default;
                    _userInfoServices.Credentials = UserADCount();
                    
                   
                    WebReferenceSapUserInfoNew.ZiamReadSapUserInfomationResponse module = _userInfoServices.ZiamReadSapUserInfomation(new WebReferenceSapUserInfoNew.ZiamReadSapUserInfomation() { UserId = userlist.Username });
                    if (module != null)
                    {
                        dirc.Add(userlist.Username, module);
                        
                        bmpbName.Add(userlist.Username);
                        flag++;
                    }
                    if (flag == 500)
                        break;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            AllCount = dirc.Count;
            Okcount = 0;
            List<SAP_UserInfo> listsap = new List<SAP_UserInfo>();
            foreach (var item in bmpbName)
            {
                BaseDataAccess.SAP_UserInfo entity = null;

                WebReferenceSapUserInfoNew.ZiamReadSapUserInfomationResponse module = new WebReferenceSapUserInfoNew.ZiamReadSapUserInfomationResponse();
                dirc.TryGetValue(item, out module);
                List<SAP_User_Role> lis = new List<BaseDataAccess.SAP_User_Role>();
                List<SAP_Parameters> lisp = new List<SAP_Parameters>();
                entity = SapUserInfoEntity(module, item,out lis,out lisp);
                try
                {
                    listsap.Add(entity);
                    new IAMEntityDAL.SAPUserInfoDAL().SyncSapUserInfomation(entity);
                    new IAMEntityDAL.SAPUserRoleDAL().CreateOrUpdate(lis);
                    new IAMEntityDAL.SAP_ParametersDAL().SyncUserParameters(lisp);
                    Okcount++;
                }
                catch (Exception ex)
                {
#if DEBUG
                    new LogDAL().AddsysErrorLog(string.Format("TC账号同步--{0}",ex.ToString()));
                    throw ex;
#else
                    continue;
#endif
                }
            }

            int count;
            var listold = new SAPUserInfoDAL().GetSapUserInfo(int.MaxValue,int.MaxValue,null,null,null,out count);
            foreach (var item in listold)
            {
                var tmp = listsap.FirstOrDefault(i=>i.BAPIBNAME.Trim()==item.BAPIBNAME.Trim());
                if (tmp == null)
                    new IAMEntityDAL.DeferencesSlution.UserRoleAlignmentValue().IsAddNewNotInIAM<SAP_UserInfo>(item,item.BAPIBNAME,Unitity.SystemType.SAP,"SAP_UserInfo","","IAM系统有该账号","源系统中无该账号");
            }
            //发送账号冲突邮件
            //BLL.Sys_UserName_ConflictResolutionMail.SendMail(IAMEntityDAL.Unitity.SystemType.SAP);

        }


        BaseDataAccess.SAP_UserInfo SapUserInfoEntity(WebReferenceSapUserInfoNew.ZiamReadSapUserInfomationResponse entity, string UserBname, out List<SAP_User_Role> listrole,out List<SAP_Parameters> listparmaters)
        {
            BaseDataAccess.SAP_UserInfo SapUserInfoMation = new BaseDataAccess.SAP_UserInfo()
            {
                DATE_FORMAT = entity.Defaults.Datfm,
                TIME_FORMAT = entity.Defaults.Timefm,
                LASTNAME = entity.Address.Lastname,
                FIRSTNAME = entity.Address.Firstname,
                USERTYPE = entity.Logondata.Ustyp,
                START_DATE = entity.Logondata.Gltgv.Trim(),
                END_DATE = entity.Logondata.Gltgb.Trim(),
                PASSWORD = string.Empty,
                PASSWORD2 = string.Empty,
                UCLASSTYPE = entity.Uclass.Sysid,
                LANGUAGE = entity.Defaults.Langu,
                LOGIN_LANGUAGE = entity.Defaults.Langu,
                DECIMAL_POINT_FORMAT = entity.Defaults.Dcpfm,
                OUTPUT_EQUIMENT = entity.Defaults.Spld,
                NOWTIME_EQUIMENT = entity.Defaults.Spdb,
                OUTPUTED_DELETE = entity.Defaults.Spda,
                USER_TIMEZONE = entity.Logondata.Tzone,
                p1 = entity.Uclass.LicType,
                E_MAIL = entity.Address.EMail,
                MOBLIE_NUMBER = entity.Address.Tel1Numbr,
                SYSTEM_TIMEZONE = "",//entity.Logondata.Tzone,
                BAPIBNAME = UserBname,
                PARAMENTERID = "",
                PARAMENTERVALUE = "",
                PARAMETERTEXT = "",
                DEPARTMENT_NAME = entity.Address.Department
            };

            listrole = new List<BaseDataAccess.SAP_User_Role>();
            if (entity.ActivitygroupsEt != null && entity.ActivitygroupsEt.Length > 0)
            {
                var lis = entity.ActivitygroupsEt.ToList();
                foreach (var item in lis)
                {
                    SAP_User_Role r = new BaseDataAccess.SAP_User_Role();
                    r.BAPIBNAME = UserBname;
                    r.ROLEID = item.AgrName;
                    r.ROLENAME = item.AgrText;
                    r.START_DATE = item.FromDat;
                    r.END_DATE = item.ToDat;
                    listrole.Add(r);
                }
            }

            listparmaters = new List<SAP_Parameters>();
            if (entity.ParameterEt != null && entity.ParameterEt.Length > 0)
            {
                var lis = entity.ParameterEt.ToList();
                foreach (var item in lis)
                {
                    SAP_Parameters p = new SAP_Parameters();
                    p.id = Guid.NewGuid();
                    p.BAPIBNAME = UserBname;
                    p.PARAMENTERID = item.Parid;
                    p.PARAMENTERVALUE = item.Parva;
                    p.PARAMETERTEXT = item.Partxt;
                    listparmaters.Add(p);
                }
            }

            return SapUserInfoMation;
        }

    }
}