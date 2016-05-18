using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAM.BLL;
using IAMEntityDAL;
using BaseDataAccess;
using IAM2.SAP_BLL;

namespace IAM2.SAP_BLL.HR
{
    public class MailInfoHR : MailInfoTemplate
    {
        public HRSm_user UserInfoNew { get; set; }
        public HRSm_user UserInfoOld { get; set; }
        public List<HRsm_user_role> Rolelist { get; set; }
    }

    public class HR_MailService : RoleList
    {
        MailInfoHR hrentity = new MailInfoHR();
        public List<HRsm_user_role> Rolelist { get; set; }
        public HR_MailService(HRSm_user newt, HRSm_user oldt)
        {
            hrentity.UserInfoNew = newt;
            hrentity.UserInfoOld = oldt;
            hrentity.SystemName = this.SystemType.ToString();
        }
        public override Unitity.SystemType SystemType
        {
            get { return Unitity.SystemType.HR; }
        }
        public override void CreateMail(string actioner, string usertype)
        {
            hrentity.UserInfo = UserInfoAdd<HRSm_user>(hrentity.UserInfoNew);
            hrentity.UserName = hrentity.UserInfoNew.User_code;
            hrentity.Actioner = actioner;
            hrentity.SystemType = usertype;
            hrentity.Rolelist = this.Rolelist;            
            IAMEntityDAL.MailInfo m = ReturnMailInfo();
            m.Body = MailTemplateHelper.RunTemplate<MailInfoHR>(hrentity, "AddUserInfoByHR");
            SendMail(m);
        }
        public override void UpdateMail(string actioner, string usertype)
        {
            hrentity.UserInfo = UserInfoUpdate<HRSm_user>(hrentity.UserInfoNew,hrentity.UserInfoOld);
            hrentity.UserName = hrentity.UserInfoNew.User_code;
            hrentity.Actioner = actioner;
            hrentity.SystemType = usertype;
            hrentity.Rolelist = this.Rolelist;
            IAMEntityDAL.MailInfo m = ReturnMailInfo();
            m.Body = MailTemplateHelper.RunTemplate<MailInfoHR>(hrentity, "UpdateUserInfoByHR");
            SendMail(m);
        }
        public override void DisabledMail(string actioner, string usertype)
        {
            hrentity.UserInfo = UserInfoUpdate<HRSm_user>(hrentity.UserInfoNew, hrentity.UserInfoOld);
            hrentity.UserName = hrentity.UserInfoNew.User_code;
            hrentity.Actioner = actioner;
            hrentity.SystemType = usertype;
            hrentity.Rolelist = this.Rolelist;
            IAMEntityDAL.MailInfo m = ReturnMailInfo();
            m.Body = MailTemplateHelper.RunTemplate<MailInfoHR>(hrentity, "DisabledUserInfoByHR");
            SendMail(m);
        }
    }
}