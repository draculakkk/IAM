using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BaseDataAccess;
using IAMEntityDAL;
using IAMEntityDAL.xml;
using IAMEntityDAL.Properties;

namespace IAM.BLL
{
    public class AddUserMail
    {
        public string Actioner { get; set; }

        public string time { get { return DateTime.Now.ToString(); } }

        public string SystemName { get; set; }

        public string UserName { get; set; }

        public string SystemType { get; set; }

        public string UserInfo { get; set; }

        public string RoleString { get; set; }
    }

    public class MailServices
    {
        /// <summary>
        /// 新建账号时发送的邮件
        /// </summary>
        /// <param name="MailModule"></param>
        public void CreateUserMail(AddUserMail MailModule)
        {
            
            IAMEntityDAL.MailInfo mailinfo = new IAMEntityDAL.MailInfo();
            mailinfo.Body = MailTemplateHelper.RunTemplate<AddUserMail>(MailModule, "AddUserInfo");
            mailinfo.Authorizationid = new Guid(System.Web.Configuration.WebConfigurationManager.AppSettings["mailAurthID"]);
            mailinfo.SendMode = 1;
            mailinfo.SendTime = DateTime.Now;
            string title = MailModule.SystemName;
            if (MailModule.SystemName == Unitity.SystemType.ADComputer.ToString())
            {
                MailModule.SystemName = Unitity.SystemType.AD.ToString();
            }
            string maddress = "";
            maddress = new TaskEmailDAL().GetList().FirstOrDefault(item => item.SystemName.Trim() == MailModule.SystemName).EmailAddress;
            if (!string.IsNullOrEmpty(maddress))
            {
                maddress = maddress.Trim();
                mailinfo.To = maddress.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
                mailinfo.To = new string[] { "yangjian@shac.com.cn" };
            mailinfo.Subject = title + "添加账号信息";
            mailinfo.URLS = new string[] { };
            MySendMail.Send(mailinfo);
        }

        /// <summary>
        /// 编辑账号时发送的邮件
        /// </summary>
        /// <param name="MailModule"></param>
        public void UpdateUserMail(AddUserMail MailModule)
        {
            
            if (string.IsNullOrEmpty(MailModule.UserInfo)&&string.IsNullOrEmpty(MailModule.RoleString))
            {
                return;
            }
            
            IAMEntityDAL.MailInfo mailinfo = new IAMEntityDAL.MailInfo();
            mailinfo.Body = MailTemplateHelper.RunTemplate<AddUserMail>(MailModule, "EditUserInfo");
            mailinfo.Authorizationid = new Guid(System.Web.Configuration.WebConfigurationManager.AppSettings["mailAurthID"]);
            mailinfo.SendMode = 1;
            mailinfo.SendTime = DateTime.Now;
            string title = MailModule.SystemName;
            if (MailModule.SystemName == Unitity.SystemType.ADComputer.ToString())
            {
                MailModule.SystemName = Unitity.SystemType.AD.ToString();
            }
            string maddress = "";
            maddress = new TaskEmailDAL().GetList().FirstOrDefault(item => item.SystemName.Trim() == MailModule.SystemName).EmailAddress;
            if (!string.IsNullOrEmpty(maddress))
            {
                maddress = maddress.Trim();
                mailinfo.To = maddress.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
                mailinfo.To = new string[] { "yangjian@shac.com.cn" };
            mailinfo.Subject = title + "更新账号信息";
            mailinfo.URLS = new string[] { };
            MySendMail.Send(mailinfo);
        }

        /// <summary>
        /// 禁用账号时发送的邮件
        /// </summary>
        /// <param name="MailModule"></param>
        public void DisabledUserMail(AddUserMail MailModule)
        {
            IAMEntityDAL.MailInfo mailinfo = new IAMEntityDAL.MailInfo();
            mailinfo.Authorizationid = new Guid(System.Web.Configuration.WebConfigurationManager.AppSettings["mailAurthID"]);
            mailinfo.Body = MailTemplateHelper.RunTemplate<AddUserMail>(MailModule, "DisabledUserInfo");
            mailinfo.Authorizationid = new Guid(System.Web.Configuration.WebConfigurationManager.AppSettings["mailAurthID"]);
            mailinfo.SendMode = 1;
            mailinfo.SendTime = DateTime.Now;
            string maddress = "";
            maddress = new TaskEmailDAL().GetList().FirstOrDefault(item => item.SystemName.Trim() == MailModule.SystemName).EmailAddress;
            if (!string.IsNullOrEmpty(maddress))
            {
                maddress = maddress.Trim();
                mailinfo.To = maddress.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
                mailinfo.To = new string[] { "yangjian@shac.com.cn" };
            mailinfo.Subject = MailModule.SystemName + "禁用账号信息";
            mailinfo.URLS = new string[] { };
            MySendMail.Send(mailinfo);
        }
    }
}