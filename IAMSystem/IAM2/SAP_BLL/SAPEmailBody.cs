using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAMEntityDAL;
using BLL;
using BaseDataAccess;
using IAM2.BLL;
using IAM.BLL;

namespace IAM2.SAP_BLL
{
    public class MailInfoSAP : MailInfoTemplate
    {
        public List<SAP_User_Role> SAPRoleList { get; set; }
        public List<SAP_Parameters> SAPParameters { get; set; }
        public SAP_UserInfo UserInfoNew { get; set; }
        public SAP_UserInfo UserInfoOld { get; set; }
    }

  

    public class SAP_MailService : RoleList
    {
        MailInfoSAP entity = new MailInfoSAP();
        public SAP_MailService(SAP_UserInfo newt,SAP_UserInfo oldt)
        {
            this.entity.SystemName = this.SystemType.ToString();
            this.entity.UserInfoNew = newt;
            this.entity.UserInfoOld = oldt;
        }

        public override Unitity.SystemType SystemType
        {
            get { return Unitity.SystemType.SAP; }
        }
        public List<SAP_User_Role> SAPRoleList { get; set; }
        public List<SAP_Parameters> SAPParameters { get; set; }      

        public override void CreateMail(string actioner, string usertype)
        {
            entity.UserInfo = UserInfoAdd<SAP_UserInfo>(entity.UserInfoNew);
            entity.UserName = entity.UserInfoNew.BAPIBNAME;
            entity.Actioner = actioner;
            entity.SystemType = usertype;
            entity.SAPRoleList = this.SAPRoleList;
            entity.SAPParameters = this.SAPParameters;
            IAMEntityDAL.MailInfo m = ReturnMailInfo();
            m.Body = MailTemplateHelper.RunTemplate<MailInfoSAP>(entity, "AddUserInfoBySAP");
            SendMail(m);
        }
        public override void DisabledMail(string actioner, string usertype)
        {
            entity.UserInfo = UserInfoUpdate<SAP_UserInfo>(entity.UserInfoNew,entity.UserInfoOld);
            entity.UserName = entity.UserInfoNew.BAPIBNAME;
            entity.Actioner = actioner;
            entity.SystemType = usertype;
            entity.SAPRoleList = this.SAPRoleList;
            entity.SAPParameters = this.SAPParameters;
            IAMEntityDAL.MailInfo m = ReturnMailInfo();
            m.Body = MailTemplateHelper.RunTemplate<MailInfoSAP>(entity, "UpdateUserInfoBySAP");
            SendMail(m); 
        }
        public override void UpdateMail(string actioner, string usertype)
        {
            entity.UserInfo = UserInfoAdd<SAP_UserInfo>(entity.UserInfoNew);
            entity.UserName = entity.UserInfoNew.BAPIBNAME;
            entity.Actioner = actioner;
            entity.SystemType = usertype;
            entity.SAPRoleList = this.SAPRoleList;
            entity.SAPParameters = this.SAPParameters;
            IAMEntityDAL.MailInfo m = ReturnMailInfo();
            m.Body = MailTemplateHelper.RunTemplate<MailInfoSAP>(entity, "DisUserInfoBySAP");
            SendMail(m);
        }
    }
}