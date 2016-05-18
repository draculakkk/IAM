using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAMEntityDAL;
using BLL;
using BaseDataAccess;
using IAM2.BLL;
using IAM.BLL;
using IAM2.SAP_BLL;

namespace IAM2.SAP_BLL.TC
{
    public class TCMailInfo : MailInfoTemplate
    {
        public TC_UserInfo UserInfoNew { get; set; }
        public TC_UserInfo UserInfoOld { get; set; }
        public List<TC_UserGroupSetting> RoleList { set; get; }
    }

    public class TC_MailService : RoleList
    {
        TCMailInfo tcentity = new TCMailInfo();
        public List<TC_UserGroupSetting> RoleList { get; set; }
        public TC_MailService(TC_UserInfo newt, TC_UserInfo oldt)
        {
            tcentity.UserInfoNew = newt;
            tcentity.UserInfoOld = oldt;
            tcentity.SystemName = this.SystemType.ToString();
        }
        public override Unitity.SystemType SystemType
        {
            get { return Unitity.SystemType.TC; }
        }
        public override void CreateMail(string actioner, string usertype)
        {
            tcentity.Actioner = actioner;
            tcentity.SystemType = usertype;
            tcentity.RoleList = this.RoleList;
            tcentity.UserName = tcentity.UserInfoNew.UserID;
            tcentity.UserInfo = UserInfoAdd<TC_UserInfo>(tcentity.UserInfoNew);
            IAMEntityDAL.MailInfo m = ReturnMailInfo();
            m.Body = MailTemplateHelper.RunTemplate<TCMailInfo>(tcentity, "AddUserInfoByTC");
            SendMail(m);
        }
        public override void UpdateMail(string actioner, string usertype)
        {
            tcentity.Actioner = actioner;
            tcentity.SystemType = usertype;
            tcentity.RoleList = this.RoleList;
            tcentity.UserName = tcentity.UserInfoNew.UserID;
            tcentity.UserInfo = UserInfoUpdate<TC_UserInfo>(tcentity.UserInfoNew,tcentity.UserInfoOld);
            IAMEntityDAL.MailInfo m = ReturnMailInfo();
            m.Body = MailTemplateHelper.RunTemplate<TCMailInfo>(tcentity, "UpdateUserInfoByTC");
            SendMail(m);
        }
        public override void DisabledMail(string actioner, string usertype)
        {
            tcentity.Actioner = actioner;
            tcentity.SystemType = usertype;
            tcentity.RoleList = this.RoleList;
            tcentity.UserName = tcentity.UserInfoNew.UserID;
            tcentity.UserInfo = UserInfoUpdate<TC_UserInfo>(tcentity.UserInfoNew, tcentity.UserInfoOld);
            IAMEntityDAL.MailInfo m = ReturnMailInfo();
            m.Body = MailTemplateHelper.RunTemplate<TCMailInfo>(tcentity, "DisabledUserInfoByTC");
            SendMail(m);
        }
    }
}