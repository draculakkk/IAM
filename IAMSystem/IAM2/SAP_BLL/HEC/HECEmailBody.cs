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

namespace IAM2.SAP_BLL.HEC
{
    public class MailInfoHEC : MailInfoTemplate
    {
        public HEC_User UserInfoNew { get; set; }
        public HEC_User UserInfoOld { get; set; }
        public List<V_HECUSER_Role> RoleList { get; set; }
        public List<HEC_User_Gangwei> GangWeiList { get; set; }
    }

    public class HEC_MailServices : RoleList
    {
        MailInfoHEC HECentity = new MailInfoHEC();
        public List<V_HECUSER_Role> HECRoles { get; set; }
        public List<HEC_User_Gangwei> HECGangwei { get; set; }
        public HEC_MailServices(HEC_User newt, HEC_User oldt)
        {
            HECentity.UserInfoNew = newt;
            HECentity.UserInfoOld = oldt;
            HECentity.SystemName = this.SystemType.ToString();
        }
        public override Unitity.SystemType SystemType
        {
            get { return Unitity.SystemType.HEC; }
        }

        public override void CreateMail(string actioner, string usertype)
        {
            HECentity.UserInfo = UserInfoAdd<HEC_User>(HECentity.UserInfoNew);
            HECentity.UserName = HECentity.UserInfoNew.User_CD;
            HECentity.Actioner = actioner;
            HECentity.SystemType = usertype;
            HECentity.RoleList  = this.HECRoles;
            HECentity.GangWeiList = this.HECGangwei;            
            IAMEntityDAL.MailInfo m = ReturnMailInfo();
            m.Body = MailTemplateHelper.RunTemplate<MailInfoHEC>(HECentity, "AddUserInfoByHEC");
            SendMail(m);
        }

        public override void UpdateMail(string actioner, string usertype)
        {
            HECentity.UserInfo = UserInfoUpdate<HEC_User>(HECentity.UserInfoNew,HECentity.UserInfoOld);
            HECentity.UserName = HECentity.UserInfoNew.User_CD;
            HECentity.Actioner = actioner;
            HECentity.SystemType = usertype;
            HECentity.RoleList = this.HECRoles;
            HECentity.GangWeiList = this.HECGangwei;
            IAMEntityDAL.MailInfo m = ReturnMailInfo();
            m.Body = MailTemplateHelper.RunTemplate<MailInfoHEC>(HECentity, "UpdateUserInfoByHEC");
            SendMail(m);
        }
        public override void DisabledMail(string actioner, string usertype)
        {
            HECentity.UserInfo = UserInfoUpdate<HEC_User>(HECentity.UserInfoNew, HECentity.UserInfoOld);
            HECentity.UserName = HECentity.UserInfoNew.User_CD;
            HECentity.Actioner = actioner;
            HECentity.SystemType = usertype;
            HECentity.RoleList = this.HECRoles;
            HECentity.GangWeiList = this.HECGangwei;
            IAMEntityDAL.MailInfo m = ReturnMailInfo();
            m.Body = MailTemplateHelper.RunTemplate<MailInfoHEC>(HECentity, "DisabledUserInfoByHEC");
            SendMail(m);
        }
    }
}