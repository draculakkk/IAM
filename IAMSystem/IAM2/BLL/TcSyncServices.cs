using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BaseDataAccess;
using IAMEntityDAL;
using System.Text.RegularExpressions;
using System.Text;

namespace IAM.BLL
{
    public class myData
    {
        public int id { get; set; }
        /// <summary>
        /// 组（例：cae.pe)
        /// </summary>
        public string group { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }

        private static string groupname = "group";
        private static string roleName = "role";
        public static List<myData> getData(WebReferenceTCNew.Tree2 tree)
        {
            List<myData> list = new List<myData>();
            if (tree.type == groupname)
            {//是组
                string gtmp = tree.title;
                getData2(tree, ref gtmp, list);
            }
            if (tree.type == roleName)
            {//为角色
                list.Add(new myData() { id = (tree.title).GetHashCode(), Role = tree.title, group = "" });
            }

            return list;
        }

        private static void getData2(WebReferenceTCNew.Tree2 tree, ref string group, List<myData> list)
        {
            foreach (var item in tree.childs)
            {
                if (item.type == roleName)
                {//为角色，最后一级
                    list.Add(new myData() { id = (group + item.title).GetHashCode(), Role = item.title, group = group });
                }
                if (item.type == groupname)
                {//为组，继续查找子组
                    string grouptmp = (item.title + "." + group);
                    getData2(item, ref grouptmp, list);
                }
            }
        }
    }
    public class TcSyncServices
    {
        WebReferenceTCNew.Shactcservice _autoServices = new WebReferenceTCNew.Shactcservice();

        /// <summary>
        /// 同步Tc 用户组和角色信息
        /// </summary>
        /// <param name="Allcount"></param>
        /// <param name="Okcount"></param>
        public void SyncRole(out int Allcount, out int Okcount,int flag=0)
        {
            _autoServices.Timeout = 1000 * 60 * 1;
            _autoServices.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap11;
            //_autoServices.Url = "http://10.124.88.148:7001/axis2/services/Shactcservice.ShactcserviceHttpSoap11Endpoint/";
            //"http://10.124.88.155:8080/axis2/services/Shactcservice.ShactcserviceHttpSoap11Endpoint/";
            try
            {
                if (flag >= 10)
                {
                    Okcount = Allcount = 99999;
                    new LogDAL().AddsysErrorLog("TC访问超时，接口无提供数据");
                    return;
                }
                var xxf = _autoServices.maina();               
                List<myData> listData = myData.getData(xxf);
                Allcount = listData.Count;
                Okcount = 0;
                foreach (var item in listData)
                {
                    BaseDataAccess.TC_Role entity = new TC_Role();
                    entity.RoleID = item.id.ToString();
                    entity.RoleName = item.Role + "." + item.group.Replace(".Organization", string.Empty);
                    entity.Memo = "";
                    try
                    {
                        new TCRoleDAL().SyncRole(entity);
                        Okcount++;
                    }
                    catch (Exception ex)
                    {
#if DEBUG
                        throw ex;
#else
                    new LogDAL().AddsysErrorLog(ex.ToString());
                    continue;
#endif
                    }
                }
            }
            catch(Exception ex)
            {
                if (flag <= 10)
                {
                    Okcount = Allcount = 99999;
                    SyncRole(out Allcount, out Okcount, flag++);
                }
                else
                {
                    Okcount = Allcount = 99999;
                    new LogDAL().AddsysErrorLog(ex.ToString());
                }
            }
        }

        /// <summary>
        /// 转化Tc接口lastlogintime
        /// </summary>
        /// <param name="_str"></param>
        /// <returns></returns>
        private DateTime? getdate(string _str)
        {
            string va = _str;
            try
            {
                string date1 = va.Substring(0, 4) + "-" + va.Substring(4, 2) + "-" + va.Substring(6, 2) + " " + va.Substring(8, 2) + ":" + va.Substring(10, 2) + ":" + va.Substring(12, 2);
                DateTime date = new DateTime();
                DateTime.TryParse(date1, out date);
                return date;
            }
            catch (Exception e) { return null; }
        }

        private List<TC_UserGroupSetting> GetUserGroupSetting(string[] groupsettings, string UserID)
        {
            List<TC_UserGroupSetting> list = new List<TC_UserGroupSetting>();
            try
            {
                foreach (string item in groupsettings)
                {
                    string[] settingvalue = ReturnRegexedString(item + ",");
                    TC_UserGroupSetting tmp = new TC_UserGroupSetting();
                    tmp.p1 = settingvalue[0].Replace("groupname:",string.Empty);//groupname
                    tmp.p2 = settingvalue[1].Replace("rolename:",string.Empty);//rolename
                    tmp.GroupAdmin = settingvalue[2].Replace("groupadmin:", string.Empty) == string.Empty ? (short?)null : settingvalue[2].Replace("groupadmin:", string.Empty).Equals("true") ? (short)1 : (short)0;
                    tmp.GroupStatus = settingvalue[3].Replace("membstatus:", string.Empty) == string.Empty ? (short?)null : settingvalue[3].Replace("membstatus:", string.Empty).Equals("true") ? (short)1 : (short)0;
                    tmp.GroupDefaultRole = settingvalue[4].Replace("defaultrole:", string.Empty) == string.Empty ? (short?)null : settingvalue[4].Replace("defaultrole:", string.Empty).Equals("true") ? (short)1 : (short)0;
                    tmp.GroupOut = settingvalue[5].Replace("ExterManage:", string.Empty) == string.Empty ? (short?)null : settingvalue[5].Replace("ExterManage:", string.Empty).Equals("true") ? (short)1 : (short)0;
                    tmp.Memo = settingvalue[1].Replace("rolename:", string.Empty) + "." + settingvalue[0].Replace("groupname:", string.Empty);
                    tmp.ID = Guid.NewGuid();
                    tmp.UserID = UserID;
                    
                    list.Add(tmp);
                }
            }
            catch (Exception ex)
            {
                
            }
            return list;
        }


        /// <summary>
        /// TC Groups Setting 数据转化
        /// </summary>
        /// <param name="NormalString">groupsetting 数据</param>
        /// <returns></returns>
        public string[] ReturnRegexedString(string NormalString)
        {
            //Regex _regex = new Regex(@"[:]\w+.[,]|[:]\w+[?.]\w+[,]|[:]\w.+[?.]\w+[?.]\w+[,]|[:]\w.+\s\w+[,]|[:][,]", RegexOptions.IgnoreCase);//[:]\w+.[,]|[:]\w+[?.]\w+[,]|[:]\w+[\s]\w+[,]
            //MatchCollection _math = _regex.Matches(NormalString);
            //StringBuilder tmpstb = new StringBuilder();
            //foreach (Match mc in _math)
            //{
            //    string tmp = mc.Value.Replace(":", string.Empty);
            //    tmpstb.Append(tmp);
            //}
            string[] groups = NormalString.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return groups;

        }

        /// <summary>
        /// 同步Tc UserInfo信息
        /// </summary>
        /// <param name="Allcount"></param>
        /// <param name="Okcount"></param>
        public void SyncUserInfo(out int Allcount, out int Okcount,int flag=0)
        {
            _autoServices.Timeout = 1000 * 60 * 1;
            _autoServices.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
            //_autoServices.Url = "http://10.124.88.148:7001/axis2/services/Shactcservice.ShactcserviceHttpSoap12Endpoint/";
            List<WebReferenceTCNew.UserInfo> xxf = new List<WebReferenceTCNew.UserInfo>();
            try
            {               
                 xxf = _autoServices.showinfo().ToList();
            }
            catch (Exception ex)
            {
                flag++;
                if (flag < 10)
                {
                    Allcount = Okcount = 99999;
                    SyncUserInfo(out Allcount, out Okcount, flag);
                }
                else
                {
                    new LogDAL().AddsysErrorLog(ex.ToString());
                }
            }

            if (flag >= 10 && xxf.Count <= 0)
            {
                Okcount = Allcount = 99999;
                new LogDAL().AddsysErrorLog("TC访问超时，接口无提供数据");
                return;
            }

                Allcount = xxf.Count;
                Okcount = 0;
                List<TC_UserInfo> listt = new List<TC_UserInfo>();
                foreach (var item in xxf)
                {
                    TC_UserInfo entity = new TC_UserInfo()
                    {
                        UserID = item.userID.ToUpper(),
                        UserName = item.userName,
                        UserStatus = item.status == string.Empty ? (short?)null : short.Parse(item.status),
                        DefaultGroup = item.defgroup,
                        LicenseLevel = item.license_level != null ? short.Parse(item.license_level) : (short?)null,
                        mailAddress = item.mailaddress,
                        SystemName = item.OSName,
                        LastLoginTime = getdate(item.last_sync_date)
                    };
                   
                    List<TC_UserGroupSetting> listGroupsSetting = GetUserGroupSetting(item.groupmember_setting, item.userID);
                    try
                    {
                        listt.Add(entity);//接口中数据
                        new TC_UserInfoDAL().SyncUserInfo(entity, string.Join("", item.groups == null ? new string[] { "" } : item.groups), listGroupsSetting);
                        Okcount++;
                    }
                    catch (Exception ex)
                    {
#if DEBUG
                    new LogDAL().AddsysErrorLog(string.Format("tc账号同步--{0}",ex.ToString()));
                    throw ex;
#else
                        new LogDAL().AddsysErrorLog(ex.ToString());
                        continue;
#endif
                    }
                }

                

                if (listt.Count <= 0)
                {
                    Okcount = Allcount = 99999;
                    new LogDAL().AddsysErrorLog("TC接口提供数据为空");
                    return;
                }

                int count;
                var listold = new TC_UserInfoDAL().GetTCUserInfolist(out count);
                var listiamrole = new TC_UserGroupSettingDAL().GetList();
                foreach (var i in listold)
                {
                    var tmp = listt.FirstOrDefault(it => it.UserID.ToUpper().Trim() == i.UserID.ToUpper().Trim());
                    if (tmp == null)
                    {
                        var tmprole = listiamrole.Where(item => item.UserID == i.UserID);
                        new IAMEntityDAL.DeferencesSlution.UserRoleAlignmentValue().IsAddNewNotInIAM<TC_UserInfo>(i, i.UserID, Unitity.SystemType.TC, "TC_UserInfo", "", "IAM系统中有该账号", "源系统中无该账号");
                        foreach (var x in tmprole)
                        {
                            if (new IAMEntityDAL.Sys_UserName_ConflictResolutionDAL().GetOne(x.UserID, "源系统中无该组权限", "IAM系统有该组权限", "TC", x.Memo) == null)
                                new IAMEntityDAL.DeferencesSlution.UserRoleAlignmentValue().IsAddNewNotInIAM<TC_UserGroupSetting>(x, x.UserID, Unitity.SystemType.TC, "TC_UserGroupSetting", x.Memo, "IAM系统有该组权限", "源系统中无该组权限");
                        }
                    }
                }

                //发送账号冲突邮件
                //BLL.Sys_UserName_ConflictResolutionMail.SendMail(IAMEntityDAL.Unitity.SystemType.TC);
            
        }

    }
}