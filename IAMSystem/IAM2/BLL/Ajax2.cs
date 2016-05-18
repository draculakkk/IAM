using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAMEntityDAL;
using BaseDataAccess;
using System.Web.Script.Serialization;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace IAM.BLL
{
    public partial class Ajax
    {
        /// <summary>
        /// 验证loginName 在系统中是否唯一性
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string ValidateLoginNameOne(HttpContext context)
        {
            string sys = context.Request.QueryString["sys"];
            string cn = context.Request["cn"];
            string loginname = context.Request["login"];
            string isupdate = context.Request["isupdate"];
            Unitity.SystemType tye = (Unitity.SystemType)Enum.Parse(typeof(Unitity.SystemType), sys, true);
            string ReturnStr = "";
            switch (tye)
            {
                case Unitity.SystemType.AD: ReturnStr = AdValidate(cn, loginname, isupdate); break;
                case Unitity.SystemType.ADComputer: ReturnStr = ADComputerValidate(loginname, isupdate); break;
                case Unitity.SystemType.HEC: ReturnStr = HECValidate(loginname, isupdate); break;
                case Unitity.SystemType.HR: ReturnStr = HRValidate(loginname, isupdate); break;
                case Unitity.SystemType.TC: ReturnStr = TCValidate(loginname, isupdate); break;
                case Unitity.SystemType.SAP: ReturnStr = SAPValidate(loginname, isupdate); break;
            }
            return ReturnStr;
        }
        /// <summary>
        /// 检查登录名是否已存在
        /// </summary>
        /// <param name="cn"></param>
        /// <param name="loginname"></param>
        /// <returns></returns>

        void ValidateUser(string UserName, string CNname, out string username, out string cnname, int flag,int uflag)
        {

            int ucount = 0;
            int cncount = 0;
            using (IAMEntities db = new IAMEntities())
            {
                string tmpu = UserName + (uflag == 0 ? "" : uflag.ToString());
                string tmpc = CNname + (flag == 0 ? "" : flag.ToString());
                ucount = db.AD_UserInfo.Where(item => item.Accountname == tmpu).Count();
                cncount = db.AD_UserInfo.Where(item => item.CnName == tmpc).Count();
            }
            if (ucount > 0 || cncount > 0)
            {
                if (cncount > 0)
                {
                    flag++;
                }
                if (ucount > 0)
                {
                    uflag++;
                }
                username = UserName + (uflag == 0 ? "" : uflag.ToString());
                cnname = CNname + (flag == 0 ? "" : flag.ToString());
                ValidateUser(UserName, CNname, out username, out cnname, flag,uflag);
            }
            else
            {
                //username = UserName + (uflag == 0 ? "" : uflag.ToString());
                //cnname = CNname + (flag == 0 ? "" : flag.ToString());
                if (uflag == 0 && flag == 0)
                {
                    username = UserName + (uflag == 0 ? "" : uflag.ToString());
                    cnname = CNname + (flag == 0 ? "" : flag.ToString());
                }
                else if (uflag>0&&flag>0)
                {
                    username = UserName + (uflag == 0 ? "" : uflag.ToString());
                    cnname = CNname + (uflag == 0 ? "" : uflag.ToString());
                }
                else if (uflag == 0 && flag > 0)
                {
                    username = UserName + (uflag == 0 ? "" : uflag.ToString());
                    cnname = CNname + (flag == 0 ? "" : flag.ToString());
                }
                else {
                    username = UserName + (uflag == 0 ? "" : uflag.ToString());
                    cnname = CNname + (flag == 0 ? "" : flag.ToString());
                }
            }

        }

        string AdValidate(string cn, string loginname, string isupdate)
        {

            if (isupdate == "1")
            {
                return GetJson(new { LoginName = loginname, CN = cn });
            }
            else
            {
                string newloginname, newcnname;
                ValidateUser(loginname, cn, out newloginname, out newcnname, 0,0);
                return GetJson(new { LoginName = newloginname, CN = newcnname });
            }
        }

        void ADComputer_ValidateUser(string UserName, out string username, int flag)
        {

            int ucount = 0;
            int cncount = 0;
            using (IAMEntities db = new IAMEntities())
            {
                string tmpu = UserName + (flag == 0 ? "" : flag.ToString());
                ucount = db.AD_Computer.Where(item => item.NAME == tmpu).Count();

            }
            if (ucount > 0 || cncount > 0)
            {
                flag++;
                username = UserName + (flag == 0 ? "" : flag.ToString());
                ADComputer_ValidateUser(UserName, out username, flag);
            }
            else
            {
                username = UserName + (flag == 0 ? "" : flag.ToString());

            }
        }
        string ADComputerValidate(string loginname, string isupdate)
        {
            if (string.IsNullOrEmpty(loginname))
                return string.Empty;
            if (isupdate == "1")
                return GetJson(new { lgn = loginname });
            else
            {
                string newloginname;
                ADComputer_ValidateUser(loginname, out newloginname, 0);
                return GetJson(new { lgn = newloginname });
            }
        }


        void HRSmUser_ValidateUser(string UserName, out string username, int flag)
        {

            int ucount = 0;
            using (IAMEntities db = new IAMEntities())
            {
                string tmpu = UserName + (flag == 0 ? "" : flag.ToString());
                ucount = db.HRSm_user.Where(item => item.User_code == tmpu).Count();
            }
            if (ucount > 0)
            {
                flag++;
                username = UserName + (flag == 0 ? "" : flag.ToString());
                HRSmUser_ValidateUser(UserName, out username, flag);
            }
            else
            {
                username = UserName + (flag == 0 ? "" : flag.ToString());

            }
        }
        string HRValidate(string loginname, string isupdate)
        {
            if (isupdate == "1")
            {
                return GetJson(new { status = loginname });
            }
            else
            {
                string newloginname;
                HRSmUser_ValidateUser(loginname, out newloginname, 0);
                return GetJson(new { status = newloginname });
            }
            //int count;
            //var lis = new HRSm_userDAL().HrSmUserList(int.MaxValue, int.MaxValue, null, null, null, null, out count);
            //lis = lis.Where(item => item.User_code.Trim() == loginname.Trim()).ToList();
            //if (lis == null)
            //    return GetJson(new { status = loginname });
            //else
            //{
            //    if (lis.Count == 1 && isupdate == "1")
            //    {
            //        return GetJson(new { status = loginname });
            //    }
            //    else if (lis.Count > 0 && isupdate == "0")
            //    {
            //        return GetJson(new { status = loginname + lis.Count.ToString() });
            //    }
            //    else
            //    {
            //        return GetJson(new { status = loginname });
            //    }
            //}

        }


        string HECValidate(string loginname, string isupdate)
        {


            if (string.IsNullOrEmpty(loginname))
                return "";
            else
            {
                var list = new HECUserDAL().GetOneHECUser(loginname.Trim());
                if (list == null)
                    return GetJson(new { status = "yes" });
                else
                {
                    if (list != null && isupdate == "1")
                    {
                        return GetJson(new { status = "yes" });
                    }
                    else if (list != null && isupdate == "0")
                    {
                        return GetJson(new { status = "no" });
                    }
                    else
                    {
                        return GetJson(new { status = "yes" });
                    }
                }
            }
        }



        void TCUser_ValidateUser(string UserName, out string username, int flag)
        {

            int ucount = 0;
            using (IAMEntities db = new IAMEntities())
            {
                string tmpu = UserName + (flag == 0 ? "" : flag.ToString());
                ucount = db.TC_UserInfo.Where(item => item.UserID == tmpu).Count();
            }
            if (ucount > 0)
            {
                flag++;
                username = UserName + (flag == 0 ? "" : flag.ToString());
                TCUser_ValidateUser(UserName, out username, flag);
            }
            else
            {
                username = UserName + (flag == 0 ? "" : flag.ToString());

            }
        }
        string TCValidate(string loginname, string isupdate)
        {
            if (string.IsNullOrEmpty(loginname))
                return "";
            else
            {
                if (isupdate != "1")
                {
                    return GetJson(new { lgn = loginname });
                }
                else
                {
                    //int count;
                    //var en = new TC_UserInfoDAL().GetTCUserInfolist(out count).Where(item => item.UserID == loginname).ToList();
                    //if (en == null || en.Count() == 0)
                    //    return GetJson(new { lgn = loginname });
                    //else
                    //    return GetJson(new { lgn = loginname + en.Count().ToString() });
                    string newloginname;
                    TCUser_ValidateUser(loginname, out newloginname, 0);
                    return GetJson(new { lgn = newloginname });

                }
            }
        }

        void SAPUser_ValidateUser(string UserName, out string username, int flag)
        {

            int ucount = 0;
            using (IAMEntities db = new IAMEntities())
            {
                string tmpu = UserName + (flag == 0 ? "" : flag.ToString());
                ucount = db.SAP_UserInfo.Where(item => item.BAPIBNAME == tmpu).Count();
            }
            if (ucount > 0)
            {
                flag++;
                username = UserName + (flag == 0 ? "" : flag.ToString());
                SAPUser_ValidateUser(UserName, out username, flag);
            }
            else
            {
                username = UserName + (flag == 0 ? "" : flag.ToString());

            }
        }
        string SAPValidate(string loginname, string isupdate)
        {
            if (string.IsNullOrEmpty(loginname))
                return "";
            if (isupdate == "1")
            {
                return GetJson(new { lgn = loginname });
            }
            else if (isupdate == "0")
            {
                //int count;
                //var en = new SAPUserInfoDAL().GetSapUserInfo(int.MaxValue, 1, string.Empty, string.Empty, string.Empty, out count).Where(item => item.BAPIBNAME.Trim() == loginname.Trim());
                //if (en != null && en.Count() > 0)
                //{
                //    count = 0;
                //    count = en.Count();
                //    return GetJson(new { lgn = loginname + count.ToString() });
                //}
                //else
                //{
                //    return GetJson(new { lgn = loginname });
                //}
                string newloginname;
                SAPUser_ValidateUser(loginname, out newloginname, 0);
                return GetJson(new { lgn = newloginname });

            }
            return GetJson(new { lgn = "" });
        }


        string GetJson(object _obj)
        {
            JavaScriptSerializer java = new JavaScriptSerializer();
            java.MaxJsonLength = int.MaxValue;
            StringBuilder stb = new StringBuilder();
            java.Serialize(_obj, stb);
            return stb.ToString();
        }

        /// <summary>
        /// 添加ad 工作组信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        string ADWorkgroupAction(HttpContext context)
        {
            string login = context.Request["login"];
            string workgrouplist = context.Request["value"];
            workgrouplist = context.Server.UrlDecode(workgrouplist);
            List<string> list = workgrouplist.Split(new string[] { "^" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            System.Text.StringBuilder stb = new StringBuilder();
            foreach (string a in list)
            {
                stb.Append(@"INSERT INTO dbo.AD_UserWorkGroup
        ( ID ,
          Uid ,
          GroupName ,
          IsSync ,
          SyncDate,memo
        ) VALUES  ( newid(),'" + login + "','" + a + "', null,null,'yes')");
            }
            int count = 0;
            using (IAMEntities db = new IAMEntities())
            {
                string sql = "delete dbo.AD_UserWorkGroup WHERE Uid='" + login + "'and memo='yes'";
                db.ExecuteStoreCommand(sql);
                db.ExecuteStoreCommand(stb.ToString());
            }
            return GetJson(new { obj = "yes" });
        }


        string GetLogMess(HttpContext context)
        {
            Guid id = new Guid(context.Request["id"]);
            var l = new LogDAL().GetLogOne(id);
            return l.mess;
        }
    }
}