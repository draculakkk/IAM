using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAMEntityDAL;
using IAM.BLL;
using BaseDataAccess;

namespace IAM2.SAP_BLL
{
    public class MailInfoTemplate
    {
        public string Actioner { get; set; }

        public string time { get { return DateTime.Now.ToString(); } }

        public string SystemName { get; set; }

        public string UserName { get; set; }

        public string SystemType { get; set; }

        public string UserInfo { get; set; }        
    }

    

    public interface iUserInfo
    {
        string UserInfoAdd<T>(T entity) where T:new();
        string UserInfoUpdate<T>(T newt,T oldt) where T:new();
        Unitity.SystemType SystemType { get; }
    }

    public abstract class RoleList:iUserInfo
    {

        public abstract Unitity.SystemType SystemType { get; }
        public virtual string UserInfoAdd<T>(T entity) where T:new()
        {
            return new CompareEntity<T>().ReturnCompareStringForMailAdd(entity,this.SystemType);
        }
        public virtual string UserInfoUpdate<T>(T newt, T oldt)where T:new()
        {
            return new CompareEntity<T>().ReturnCompareStringForMailUpdate(newt,oldt,this.SystemType);
        }
        public abstract void CreateMail(string actioner,string usertype);
        public abstract void UpdateMail(string actioner, string usertype);
        public abstract void DisabledMail(string actioner, string usertype);
        protected IAMEntityDAL.MailInfo ReturnMailInfo()
        {
            IAMEntityDAL.MailInfo mailinfo = new IAMEntityDAL.MailInfo();
            mailinfo.Authorizationid = new Guid(System.Web.Configuration.WebConfigurationManager.AppSettings["mailAurthID"]);
            mailinfo.SendMode = 1;
            mailinfo.SendTime = DateTime.Now;
            string title = this.SystemType.ToString();
            if (title == Unitity.SystemType.ADComputer.ToString())
            {
                title = Unitity.SystemType.AD.ToString();
            }
            string maddress = "";
            maddress = new TaskEmailDAL().GetList().FirstOrDefault(item => item.SystemName.Trim() == title).EmailAddress;
            if (!string.IsNullOrEmpty(maddress))
            {
                maddress = maddress.Trim();
                mailinfo.To = maddress.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
                mailinfo.To = new string[] { "yangjian@shac.com.cn" };
            //mailinfo.Subject = title + "添加账号信息";
            mailinfo.URLS = new string[] { };
            return mailinfo;
        }
        protected void SendMail(IAMEntityDAL.MailInfo m)
        {
            MySendMail.Send(m);
        }
    }
}