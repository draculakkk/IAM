using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAMEntityDAL;
using IAMEntityDAL.xml;
using BaseDataAccess;
using System.Text;


namespace IAM.BLL
{
    public class ActionLog
    {
        public static void CreateLog(string user, string zhanghao, string systemType, string userType)
        {
            string mess = user + "在" + DateTime.Now.ToString() + "给" + systemType + "系统添加了一" + userType + "账号；<br/>账号名为:" + zhanghao;
            new LogDAL().AdduserActionLog(user, "添加", mess);
        }

        public void EditLog<T>(string user, string zhanghao, string systemType, string userType, T old, T nw, string rolemess = "") where T : new()
        {
            CompareEntity<T> cmp = new CompareEntity<T>();

            string mess1 = user + "在" + DateTime.Now.ToString() + "给" + systemType + "系统修改了一" + userType + "账号；<br/>账号名为:" + zhanghao + "具体修改字段信息:" + cmp.ReturnCompareString(old, nw, (Unitity.SystemType)Enum.Parse(typeof(Unitity.SystemType), systemType, true)) + "<br/>" + rolemess;
            new LogDAL().AdduserActionLog(user, "编辑", mess1);
        }

        public static void CreateLog(string user, string TemplateName)
        {
            string mess = "添加了一个模版<br/>模版名：" + TemplateName;
            new LogDAL().AdduserActionLog(user, "添加", mess);
        }

        public static void DisabledLog(string user, string zhanghao, string systemType, string userType)
        {
            string mess = user + "在" + DateTime.Now.ToString() + "将" + systemType + "系统中" + userType + "类账号禁用了；<br/>禁用账号名为:" + zhanghao;
            new LogDAL().AdduserActionLog(user, "添加", mess);
        }

    }

    public class CompareEntity<T> where T : new()
    {
        public string ReturnCompareString(T old, T nw, Unitity.SystemType SystemType)
        {
            System.Text.StringBuilder stb = new System.Text.StringBuilder();
            System.Reflection.PropertyInfo[] proper = old.GetType().GetProperties();
            ReadXml readxml = new ReadXml(SystemType);
            for (int i = 0; i < proper.Length; i++)
            {

                object Old_Value = proper[i].GetValue(old, null);
                object New_Value = proper[i].GetValue(nw, null);
                if (proper[i].Name.ToUpper() == "isSync".ToUpper())
                {
                    continue;
                }
                if (proper[i].Name.ToUpper() == "syncDate".ToUpper())
                {
                    continue;
                }

                if (proper[i].Name.ToUpper() == "EntityKey".ToUpper())
                {
                    continue;
                }
                if (proper[i].Name.ToUpper() == "EntityState".ToUpper())
                {
                    continue;
                }
                if (nw.GetType().Name == "AD_UserInfo")
                {
                    if (proper[i].Name.ToUpper() == "Posts".ToUpper() || proper[i].Name.ToUpper() == "dept".ToUpper() || proper[i].Name.ToUpper() == "parentDept".ToUpper() || proper[i].Name.ToUpper() == "NAME".ToUpper() || proper[i].Name.ToUpper() == "HRMoblePhone".ToUpper() || proper[i].Name.ToUpper() == "ToPostsDate".ToUpper() || proper[i].Name.ToUpper() == "LeavePostsDate".ToUpper() || proper[i].Name.ToUpper() == "IsRevoke".ToUpper() || proper[i].Name.ToUpper() == "RevokeDate".ToUpper() || proper[i].Name.ToUpper() == "IsSealed".ToUpper() || proper[i].Name.ToUpper() == "group".ToUpper())
                    {
                        continue;
                    }
                }

                if (nw.GetType().Name == "HRSm_user")
                {
                    if (proper[i].Name.ToUpper() == "p1".ToUpper() || proper[i].Name.ToUpper() == "p2".ToUpper() || proper[i].Name.ToUpper() == "p3".ToUpper() || proper[i].Name.ToUpper() == "p4".ToUpper() || proper[i].Name.ToUpper() == "p5".ToUpper() || proper[i].Name.ToUpper() == "TS".ToUpper() || proper[i].Name.ToUpper() == "Pk_corp".ToUpper())
                    {
                        continue;
                    }
                }
                if (nw.GetType().Name == "SAP_UserInfo")
                {
                    if (proper[i].Name.ToUpper() == "p1".ToUpper() || proper[i].Name.ToUpper() == "p2".ToUpper() || proper[i].Name.ToUpper() == "p3".ToUpper() || proper[i].Name.ToUpper() == "p4".ToUpper() || proper[i].Name.ToUpper() == "p5".ToUpper() || proper[i].Name.ToUpper() == "p6".ToUpper() || proper[i].Name.ToUpper() == "PASSWORD".ToUpper() || proper[i].Name.ToUpper() == "PASSWORD2".ToUpper())
                    {
                        continue;
                    }
                }

                if (nw.GetType().Name == "TC_UserInfo")
                {
                    if (proper[i].Name.ToUpper() == "LastLoginTime".ToUpper() || proper[i].Name.ToUpper() == "PASSWORD".ToUpper() || proper[i].Name.ToUpper() == "DefaultRoleID".ToUpper() || proper[i].Name.ToUpper() == "DefaultDisk".ToUpper() || proper[i].Name.ToUpper() == "DefaultLocalDisk".ToUpper() || proper[i].Name.ToUpper() == "GroupAdmin".ToUpper() || proper[i].Name.ToUpper() == "GroupStatus".ToUpper() || proper[i].Name.ToUpper() == "GroupOut".ToUpper() || proper[i].Name.ToUpper() == "DefaultGroup".ToUpper())
                    {
                        continue;
                    }
                }

                if (nw.GetType().Name == "HEC_User")
                {
                    if (proper[i].Name.ToUpper() == "password_lifespan_days".ToUpper() || proper[i].Name.ToUpper() == "password_lifespan_access".ToUpper() || proper[i].Name.ToUpper() == "USER_TYPE".ToUpper() || proper[i].Name.ToUpper() == "p6".ToUpper() || proper[i].Name.ToUpper() == "PASSWORD".ToUpper() || proper[i].Name.ToUpper() == "PASSWORD2".ToUpper() || proper[i].Name.ToUpper() == "DISABLED_DATE".ToUpper() || proper[i].Name.ToUpper() == "ISDISABLED".ToUpper() || proper[i].Name.ToUpper() == "createTime".ToUpper())
                    {
                        continue;
                    }
                }
                string oldstr, newstr;
                GetSys_userName(Old_Value, New_Value, out oldstr, out newstr);
                string ColumnsName = readxml.GetChineseStringByKey(proper[i].Name);
                if (oldstr != newstr)
                {
                    stb.Append(string.Format("<br/>{0}:{1}==>{2}", ColumnsName, Old_Value, New_Value));
                }
            }
            return stb.ToString();
        }

        void GetSys_userName(object value1, object value2, out string val1, out string val2)
        {

            val1 = val2 = string.Empty;
            if (value1 == null && value2 == null)
            {
                return;
            }
            if (value1 != null && value2 != null)
            {
                if (!value2.ToString().Trim().Equals(value1.ToString().Trim()))
                {
                    val1 = value1.ToString();
                    val2 = value2.ToString();
                }
            }

            if (value1 != null && value2 == null)
            {
                val1 = value1.ToString();
                val2 = "";
            }

            if (value2 != null && value1 == null)
            {
                val1 = "";
                val2 = value2.ToString();
            }
        }

        System.Reflection.PropertyInfo[] OrderbyColumnName(System.Reflection.PropertyInfo[] p, Unitity.SystemType SystemType)
        {
            string tmp = "";
            switch (SystemType)
            {
                case Unitity.SystemType.HR:
                    tmp = "User_code USER_name User_note Pwdlevelcode user_password Able_time Disable_time Authen_type Locked_tag Disable_time Memo  Cuserid Dr Isca keyuser Langcode Pk_corp TS isSync syncDate p1 p2 p3 p4 p5,Pwdparam,Pwdtype";
                    p = p.OrderBy(item => tmp.IndexOf(item.Name.Substring(0, item.Name.Length))).ToArray();
                    break;
                case Unitity.SystemType.AD:
                    tmp = "CnName DisplayName Accountname Id PASSWORD ADMobile ADTel  ENABLE expiryDate DESCRIPTION Department Job EnableDrive Drive PATH Memo Email EmailDatabase Lync UserID Posts dept parentDept NAME HRMoblePhone ToPostsDate LeavePostsDate IsRevoke IsSealed Mailstorage [Group] IsSync SyncDate";
                    p = p.OrderBy(item => tmp.IndexOf(item.Name.Substring(0, item.Name.Length))).ToArray();
                    break;
                case Unitity.SystemType.SAP:
                    tmp = "BAPIBNAME LASTNAME DEPARTMENT_NAME LANGUAGE MOBLIE_NUMBER E_MAIL USERTYPE UCLASSTYPE START_DATE END_DATE DECIMAL_POINT_FORMAT DATE_FORMAT TIME_FORMAT OUTPUT_EQUIMENT NOWTIME_EQUIMENT OUTPUTED_DELETE USER_TIMEZONE SYSTEM_TIMEZONE p2 PASSWORD PASSWORD2 PARAMENTERID PARAMENTERVALUE PARAMETERTEXT p1 p3 p4 p5 p6,FIRSTNAME LOGIN_LANGUAGE";
                    p = p.OrderBy(item => tmp.IndexOf(item.Name.Substring(0, item.Name.Length))).ToArray();
                    break;
                case Unitity.SystemType.HEC:
                    tmp = "User_CD USER_CODE USER_NAME DESCRIPTION START_DATE END_DATE ISDISABLED DISABLED_DATE Memo USER_TYPE createTime frozen_date frozen_flag password_lifespan_days password_lifespan_access";
                    p = p.OrderBy(item => tmp.IndexOf(item.Name.Substring(0, item.Name.Length))).ToArray();
                    break;
                case Unitity.SystemType.TC:
                    tmp = "UserName UserID SystemName PASSWORD LicenseLevel mailAddress UserStatus Memo LastLoginTime DefaultRoleID DefaultDisk DefaultLocalDisk GroupAdmin GroupDefaultRole GroupStatus GroupOut DefaultGroup";
                    p = p.OrderBy(item => tmp.IndexOf(item.Name.Substring(0, item.Name.Length))).ToArray();
                    break;
            }
            return p;
        }


        public string ReturnCompareStringForMailUpdate(T old, T nw, Unitity.SystemType SystemType)
        {
            System.Text.StringBuilder stb = new System.Text.StringBuilder();
            stb.Append("<table class=\"info\" cellpadding=\"1\" cellspacing=\"1\" border=\"1\" width=\"100%\"><tr><th>字段名称</th><th>原值</th><th>现值</th></tr>");
            System.Reflection.PropertyInfo[] proper = old.GetType().GetProperties();
            proper = OrderbyColumnName(proper, SystemType);
            ReadXml readxml = new ReadXml(SystemType);
            var count = 0;
            for (int i = 0; i < proper.Length; i++)
            {

                object Old_Value = proper[i].GetValue(old, null);
                object New_Value = proper[i].GetValue(nw, null);
                if (proper[i].Name.ToUpper() == "isSync".ToUpper())
                {
                    continue;
                }
                if (proper[i].Name.ToUpper() == "syncDate".ToUpper())
                {
                    continue;
                }

                if (proper[i].Name.ToUpper() == "EntityKey".ToUpper())
                {
                    continue;
                }
                if (proper[i].Name.ToUpper() == "EntityState".ToUpper())
                {
                    continue;
                }
                if (nw.GetType().Name == "AD_UserInfo")
                {
                    if (proper[i].Name.ToUpper() == "Posts".ToUpper() || proper[i].Name.ToUpper() == "dept".ToUpper() || proper[i].Name.ToUpper() == "parentDept".ToUpper() || proper[i].Name.ToUpper() == "NAME".ToUpper() || proper[i].Name.ToUpper() == "HRMoblePhone".ToUpper() || proper[i].Name.ToUpper() == "ToPostsDate".ToUpper() || proper[i].Name.ToUpper() == "LeavePostsDate".ToUpper() || proper[i].Name.ToUpper() == "IsRevoke".ToUpper() || proper[i].Name.ToUpper() == "RevokeDate".ToUpper() || "IsSealed".ToUpper() == proper[i].Name.ToUpper() || proper[i].Name.ToUpper() == "group".ToUpper() || proper[i].Name.ToUpper() == "EnableDrive".ToUpper())
                    {
                        continue;
                    }
                }

                if (nw.GetType().Name == "HRSm_user")
                {
                    if (proper[i].Name.ToUpper() == "p1".ToUpper() || proper[i].Name.ToUpper() == "p2".ToUpper() || proper[i].Name.ToUpper() == "p3".ToUpper() || proper[i].Name.ToUpper() == "p4".ToUpper() || proper[i].Name.ToUpper() == "p5".ToUpper() || proper[i].Name.ToUpper() == "TS".ToUpper() || proper[i].Name.ToUpper() == "Pk_corp".ToUpper() || proper[i].Name.ToUpper() == "Pwdparam".ToUpper() || proper[i].Name.ToUpper() == "Pwdtype".ToUpper() || proper[i].Name.ToUpper() == "Cuserid".ToUpper())
                    {
                        continue;
                    }
                }
                if (nw.GetType().Name == "SAP_UserInfo")
                {
                    if (proper[i].Name.ToUpper() == "p1".ToUpper() || proper[i].Name.ToUpper() == "p2".ToUpper() || proper[i].Name.ToUpper() == "p3".ToUpper() || proper[i].Name.ToUpper() == "p4".ToUpper() || proper[i].Name.ToUpper() == "p5".ToUpper() || proper[i].Name.ToUpper() == "p6".ToUpper() || proper[i].Name.ToUpper() == "PASSWORD".ToUpper() || proper[i].Name.ToUpper() == "PASSWORD2".ToUpper())
                    {
                        continue;
                    }
                }

                if (nw.GetType().Name == "TC_UserInfo")
                {
                    if (proper[i].Name.ToUpper() == "LastLoginTime".ToUpper() || proper[i].Name.ToUpper() == "PASSWORD".ToUpper() || proper[i].Name.ToUpper() == "DefaultRoleID".ToUpper() || proper[i].Name.ToUpper() == "DefaultDisk".ToUpper() || proper[i].Name.ToUpper() == "DefaultLocalDisk".ToUpper() || proper[i].Name.ToUpper() == "GroupAdmin".ToUpper() || proper[i].Name.ToUpper() == "GroupStatus".ToUpper() || proper[i].Name.ToUpper() == "GroupOut".ToUpper() || proper[i].Name.ToUpper() == "DefaultGroup".ToUpper())
                    {
                        continue;
                    }
                }

                if (nw.GetType().Name == "HEC_User")
                {
                    if (proper[i].Name.ToUpper() == "password_lifespan_days".ToUpper() || proper[i].Name.ToUpper() == "password_lifespan_access".ToUpper() || proper[i].Name.ToUpper() == "USER_TYPE".ToUpper() || proper[i].Name.ToUpper() == "p6".ToUpper() || proper[i].Name.ToUpper() == "PASSWORD".ToUpper() || proper[i].Name.ToUpper() == "PASSWORD2".ToUpper() || proper[i].Name.ToUpper() == "DISABLED_DATE".ToUpper() || proper[i].Name.ToUpper() == "ISDISABLED".ToUpper() || proper[i].Name.ToUpper() == "createTime".ToUpper())
                    {
                        continue;
                    }
                }

                string oldstr, newstr;
                GetSys_userName(Old_Value, New_Value, out oldstr, out newstr);
                string ColumnsName = readxml.GetChineseStringByKey(proper[i].Name);
                if (oldstr != newstr)
                {
                    count++;
                    stb.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>", ColumnsName, KeyValue(nw.GetType().Name, proper[i].Name, Old_Value), KeyValue(nw.GetType().Name, proper[i].Name, New_Value)));
                }
            }
            stb.Append("</table>");
            if (count == 0) return "";
            return stb.ToString();
        }

        public string ReturnCompareStringForMailAdd(T nw, Unitity.SystemType SystemType)
        {
            System.Text.StringBuilder stb = new System.Text.StringBuilder();
            System.Reflection.PropertyInfo[] proper = nw.GetType().GetProperties();
            proper = OrderbyColumnName(proper, SystemType);
            stb.Append("<table class=\"info\" cellpadding=\"1\" cellspacing=\"1\" border=\"1\" width=\"100%\"><tr><th>字段名称</th><th>原值</th><th>现值</th></tr>");
            ReadXml readxml = new ReadXml(SystemType);
            for (int i = 0; i < proper.Length; i++)
            {

                object New_Value = proper[i].GetValue(nw, null);
                if (proper[i].Name.ToUpper() == "isSync".ToUpper())
                {
                    continue;
                }
                if (proper[i].Name.ToUpper() == "syncDate".ToUpper())
                {
                    continue;
                }

                if (proper[i].Name.ToUpper() == "EntityKey".ToUpper())
                {
                    continue;
                }
                if (proper[i].Name.ToUpper() == "EntityState".ToUpper())
                {
                    continue;
                }
                if (nw.GetType().Name == "AD_UserInfo")
                {
                    if (proper[i].Name.ToUpper() == "Posts".ToUpper() || proper[i].Name.ToUpper() == "dept".ToUpper() || proper[i].Name.ToUpper() == "parentDept".ToUpper() || proper[i].Name.ToUpper() == "NAME".ToUpper() || proper[i].Name.ToUpper() == "HRMoblePhone".ToUpper() || proper[i].Name.ToUpper() == "ToPostsDate".ToUpper() || proper[i].Name.ToUpper() == "LeavePostsDate".ToUpper() || proper[i].Name.ToUpper() == "IsRevoke".ToUpper() || proper[i].Name.ToUpper() == "RevokeDate".ToUpper() || "IsSealed".ToUpper() == proper[i].Name.ToUpper() || proper[i].Name.ToUpper() == "group".ToUpper() || proper[i].Name.ToUpper() == "EnableDrive".ToUpper())
                    {
                        continue;
                    }
                }

                if (nw.GetType().Name == "HRSm_user")
                {
                    if (proper[i].Name.ToUpper() == "p1".ToUpper() || proper[i].Name.ToUpper() == "p2".ToUpper() || proper[i].Name.ToUpper() == "p3".ToUpper() || proper[i].Name.ToUpper() == "p4".ToUpper() || proper[i].Name.ToUpper() == "p5".ToUpper() || proper[i].Name.ToUpper() == "TS".ToUpper() || proper[i].Name.ToUpper() == "Pk_corp".ToUpper() || proper[i].Name.ToUpper() == "Pwdparam".ToUpper() || proper[i].Name.ToUpper() == "Pwdtype".ToUpper() || proper[i].Name.ToUpper() == "Cuserid".ToUpper())
                    {
                        continue;
                    }
                }
                if (nw.GetType().Name == "SAP_UserInfo")
                {
                    if (proper[i].Name.ToUpper() == "p1".ToUpper() || proper[i].Name.ToUpper() == "p2".ToUpper() || proper[i].Name.ToUpper() == "p3".ToUpper() || proper[i].Name.ToUpper() == "p4".ToUpper() || proper[i].Name.ToUpper() == "p5".ToUpper() || proper[i].Name.ToUpper() == "p6".ToUpper() || proper[i].Name.ToUpper() == "PASSWORD".ToUpper() || proper[i].Name.ToUpper() == "PASSWORD2".ToUpper())
                    {
                        continue;
                    }
                }

                if (nw.GetType().Name == "TC_UserInfo")
                {
                    if (proper[i].Name.ToUpper() == "LastLoginTime".ToUpper() || proper[i].Name.ToUpper() == "DefaultRoleID".ToUpper() || proper[i].Name.ToUpper() == "DefaultDisk".ToUpper() || proper[i].Name.ToUpper() == "DefaultLocalDisk".ToUpper() || proper[i].Name.ToUpper() == "GroupAdmin".ToUpper() || proper[i].Name.ToUpper() == "GroupStatus".ToUpper() || proper[i].Name.ToUpper() == "GroupOut".ToUpper() || proper[i].Name.ToUpper() == "DefaultGroup".ToUpper())
                    {
                        continue;
                    }
                }

                if (nw.GetType().Name == "HEC_User")
                {
                    if (proper[i].Name.ToUpper() == "password_lifespan_days".ToUpper() || proper[i].Name.ToUpper() == "password_lifespan_access".ToUpper() || proper[i].Name.ToUpper() == "USER_TYPE".ToUpper() || proper[i].Name.ToUpper() == "p6".ToUpper() || proper[i].Name.ToUpper() == "PASSWORD".ToUpper() || proper[i].Name.ToUpper() == "PASSWORD2".ToUpper() || proper[i].Name.ToUpper() == "DISABLED_DATE".ToUpper() || proper[i].Name.ToUpper() == "ISDISABLED".ToUpper() || proper[i].Name.ToUpper() == "createTime".ToUpper())
                    {
                        continue;
                    }
                }

                string oldstr, newstr;
                GetSys_userName(New_Value, New_Value, out oldstr, out newstr);
                string ColumnsName = readxml.GetChineseStringByKey(proper[i].Name);
                //if (oldstr != newstr)
                //{
                stb.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td></td></tr>", ColumnsName, KeyValue(nw.GetType().Name, proper[i].Name, New_Value)));
                //}
            }
            stb.Append("</table>");
            return stb.ToString();
        }

        object KeyValue(string TableName, string ColumnName, object obj)
        {
            try
            {
                string value = obj.ToString();
                if (TableName == "HEC_User")
                {
                    if (ColumnName.ToUpper() == "frozen_flag".ToUpper())
                    {
                        return value == "N" ? "非冻结" : "冻结";
                    }
                    else
                    {
                        return obj;
                    }
                }
                else if (TableName == "TC_UserInfo")
                {
                    if (ColumnName.ToUpper() == "UserStatus".ToUpper())
                    {
                        return value == "0" ? "活动" : "非活动";
                    }
                    else if (ColumnName.ToUpper() == "LicenseLevel".ToUpper())
                    {
                        return value == "0" ? "作者" : "客户";
                    }
                    else
                    {
                        return obj;
                    }
                }
                else if (TableName == "AD_UserInfo")
                {
                    if (ColumnName.ToUpper() == "ENABLE".ToUpper())
                    {
                        return value.Trim() == "False" ? "禁用" : "启用";
                    }
                    else if (ColumnName.ToUpper() == "EnableDrive".ToUpper())
                    {
                        return value == "0" ? "禁用" : "启用";
                    }
                    else
                    {
                        return obj;
                    }
                }
                else if (TableName == "AD_Computer")
                {
                    if (ColumnName.ToUpper() == "ENABLE".ToUpper())
                    {
                        return value == "0" ? "禁用" : "启用";
                    }
                    else
                    {
                        return obj;
                    }
                }// Isca,Locked_tag,Authen_type,Pwdlevelcode FROM dbo.HRSm_user
                else if (TableName == "HRSm_user")
                {
                    if (ColumnName.ToUpper() == "Isca".ToUpper())
                    {
                        return value == "Y" ? "是" : "否";
                    }
                    else if (ColumnName.ToUpper() == "Locked_tag".ToUpper())
                    {
                        return value == "N" ? "否" : "是";
                    }
                    else if (ColumnName.ToUpper() == "Authen_type".ToUpper())
                    {
                        return value == "ncca" ? "CA认证" : value == "staticpwd" ? "静态密码认证" : obj;
                    }
                    else if (ColumnName.ToUpper() == "Pwdlevelcode".ToUpper())
                    {
                        switch (value)
                        {
                            case "senior": return "管理级";
                            case "junior": return "普通级";
                            case "update": return "预置级";
                            default: return "默认";
                        }
                    }
                    else
                    {
                        return obj;
                    }
                }
                else
                    return obj;
            }
            catch
            {
                return obj;
            }
        }

    }

    public class CompareRoleList
    {
        public static string HrRoleMess(List<V_HRSm_User_Role_new> list, string currid)
        {
            StringBuilder stb = new StringBuilder();
            List<V_HRSm_User_Role_new> addlist = list.Where(item => item.urDr == 2).ToList();
            List<V_HRSm_User_Role_new> dellist = list.Where(item => item.urDr == 1).ToList();
            List<V_HRSm_User_Role_new> old = new List<V_HRSm_User_Role_new>();
            if (addlist != null && addlist.Count == 0 && dellist != null && dellist.Count == 0)
                return "";

            using (IAMEntities db = new IAMEntities())
            {
                old = db.V_HRSm_User_Role_new.Where(item => item.uCuserid == currid).ToList();
            }
            stb.Append("<table class=\"info\" cellpadding=\"1\" cellspacing=\"1\" border=\"1\" width=\"100%\"><tr><th>角色名称</th><th>角色代码</th><th>公司名称</th><th>添加</th><th>删除</th></tr>");
            foreach (var item in addlist)
            {
                stb.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>是</td><td></td></tr>", item.rRole_Name, item.rRole_code, item.cUNTTNAME));
            }
            StringBuilder stbdel = new StringBuilder();
            foreach (var item in dellist)
            {

                var tmp = old.FirstOrDefault(t => t.urcuserid == currid);
                if (tmp != null)
                {
                    stb.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td></td><td>是</td></tr>", item.rRole_Name, item.rRole_code, item.cUNTTNAME));
                    stbdel.Append(string.Format(@" DELETE HRsm_user_role WHERE Cuserid='{0}' AND Pk_role='{1}' AND dbo.HRsm_user_role.Pk_corp='{2}'", item.uCuserid, item.rPk_role, item.cPk_corp));
                }
            }

            if (!string.IsNullOrEmpty(stbdel.ToString()))
            {
                using (IAMEntities db = new IAMEntities())
                {
                    db.ExecuteStoreCommand(stbdel.ToString());
                    db.SaveChanges();
                }
            }

            stb.Append("</table>");
            return stb.ToString();
        }

        public static string HECRoleMess(List<V_HECUSER_Role> Newlist, List<V_HECUSER_Role> Oldlist, string usercd)
        {
            StringBuilder stb = new StringBuilder();
            List<V_HECUSER_Role> newlist = Newlist.Where(item => item.isdr == 2).ToList();
            List<V_HECUSER_Role> dellist = Newlist.Where(item => item.isdr == 1).ToList();
            List<V_HECUSER_Role> normal = Newlist.Where(item => item.isdr == 0).ToList();
            List<V_HECUSER_Role> deflist = new List<V_HECUSER_Role>();
            //if (newlist.Count == 0 && dellist.Count == 0)
            //    return "";
            StringBuilder stbn = new StringBuilder();
            stbn.Append("<table class=\"info\" cellpadding=\"1\" cellspacing=\"1\" border=\"1\" width=\"100%\"><caption style=\"text-align:left;color:black;background:white;\">角色信息更改</caption>");
            stbn.Append("<tr><th rowspan=\"2\">公司名称</th><th rowspan=\"2\">角色名称</th><th colspan=\"2\" style=\"width:150px\">有效期从</th><th colspan=\"2\" style=\"width:150px\">有效期至</th><th rowspan=\"2\">更新</th></tr>");
            stbn.Append("<tr><th style=\"width:75px;\">原值</th><th style=\"width:75px;\">现值</th><th style=\"width:75px;\">原值</th><th style=\"width:75px;\">现值</th></tr>");
            int flag = 0;
            foreach (var x in normal)
            {
                var xx = Oldlist.FirstOrDefault(item => item.uID == x.uID);
                bool isadd = false;
                if (xx != null)
                {
                    if (xx.uROLEENDDATE == null)
                    {
                        xx.uROLEENDDATE = "";
                    }
                    if (xx.uROLESTARTDATE.Trim().Equals(x.uROLESTARTDATE.Trim()) && xx.uROLEENDDATE.Trim().Equals(x.uROLEENDDATE.Trim()))
                    {
                        continue;
                    }
                    if (!xx.uROLESTARTDATE.Trim().Equals(x.uROLESTARTDATE.Trim()) || !xx.uROLEENDDATE.Trim().Equals(x.uROLEENDDATE.Trim()))
                    {
                        stbn.Append(string.Format("<tr><td>{0}</td><td>{1}</td>", xx.cCOMPNYFULLNAME, xx.rROLENAME));
                    }

                    if (!xx.uROLESTARTDATE.Trim().Equals(x.uROLESTARTDATE.Trim()))
                    {
                        stbn.Append(string.Format("<td>{0}</td><td>{1}</td>", xx.uROLESTARTDATE, x.uROLESTARTDATE));
                        flag++;
                        isadd = true;
                    }
                    else
                    {
                        stbn.Append(string.Format("<td>{0}</td><td>{1}</td>", string.Empty, string.Empty));
                    }
                    if (!xx.uROLEENDDATE.Trim().Equals(x.uROLEENDDATE.Trim()))
                    {
                        flag++;
                        isadd = true;
                        stbn.Append(string.Format("<td>{0}</td><td>{1}</td>", xx.uROLEENDDATE, x.uROLEENDDATE));
                    }
                    else
                    {
                        stbn.Append(string.Format("<td>{0}</td><td>{1}</td>", string.Empty, string.Empty));
                    }
                    if (isadd)
                    {
                        stbn.Append("<td>是</td></tr>");
                    }
                }
            }
            stbn.Append("</table>");
            if (flag == 0)
                stbn.Clear();
            if (newlist.Count == 0 && dellist.Count == 0 && flag == 0)
                return "";
            using (IAMEntities db = new IAMEntities())
            {
                deflist = db.V_HECUSER_Role.Where(item => item.uUSERNAME == usercd).ToList();
            }
            stb.Append("<br/><br/><table class=\"info\" cellpadding=\"1\" cellspacing=\"1\" border=\"1\" width=\"100%\"><tr><th>角色代码</th><th>角色名称</th><th>公司名称</th><th>有效期</th><th>添加</th><th>删除</th></tr>");
            foreach (var t in newlist)
            {
                stb.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}至{4}</td><td>是</td><td></td></tr>", t.rROLECODE, t.rROLENAME, t.cCOMPNYFULLNAME, t.uROLESTARTDATE, t.uROLEENDDATE));
            }
            StringBuilder stbdel = new StringBuilder();
            foreach (var t in dellist)
            {
                var tpm = deflist.FirstOrDefault(item => item.uID == t.uID);
                if (tpm != null)
                {
                    stb.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}至{4}</td><td></td><td>是</td></tr>", t.rROLECODE, t.rROLENAME, t.cCOMPNYFULLNAME, t.uROLESTARTDATE, t.uROLEENDDATE));
                    stbdel.Append(string.Format(" update dbo.HEC_User_Info set ROLE_END_DATE='" + DateTime.Now.ToString("yyyy-MM-dd") + "' WHERE ROLE_CODE='{0}' AND USER_NAME='{1}' and COMPANY_CODE='{2}'", t.rROLECODE, t.uUSERNAME, t.cCOMPANYCODE));
                }
            }

            if (!string.IsNullOrEmpty(stbdel.ToString()))
            {
                using (IAMEntities db = new IAMEntities())
                {
                    db.ExecuteStoreCommand(stbdel.ToString());
                    db.SaveChanges();
                }
            }

            stb.Append("</table>");
            return stbn.ToString() + stb.ToString();
        }

        public static string HECGangWeiMess(List<HEC_User_Gangwei> newlist, List<HEC_User_Gangwei> oldlist, string usercd)
        {
            IAMEntities db = new IAMEntities();
            List<HEC_User_Gangwei> listadd = new List<HEC_User_Gangwei>();
            List<HEC_User_Gangwei> listdel = new List<HEC_User_Gangwei>();
            List<HEC_User_Gangwei> listdef = new List<HEC_User_Gangwei>();
            List<HEC_User_Gangwei> listnor = db.HEC_User_Gangwei.Where(item => item.EMPLOYEE_CODE == usercd).ToList();
            listadd = newlist.Where(item => item.isdelete == 2).ToList();
            listdel = newlist.Where(item => item.isdelete == 1).ToList();
            listdef = newlist.Where(item => item.isdelete == 0).ToList();
            StringBuilder stbsql = new StringBuilder();
            StringBuilder stb = new StringBuilder();
            stb.Append("<br/><br/><table class=\"info\" cellpadding=\"1\" cellspacing=\"1\" border=\"1\" width=\"100%\"><caption style=\"text-align:left;color:black;background:white;\">岗位更改:</caption><tr><th>账号名称</th><th>账号姓名</th><th>公司代码</th><th>公司名称</th><th>部门代码</th><th>部门名称</th><th>岗位代码</th><th>岗位名称</th><th>添加</th><th>删除</th></tr>");
            foreach (var x in listadd)
            {
                stb.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td>是</td><td></td></tr>", x.EMPLOYEE_CODE, x.EMPLOYEE_NAME, x.COMPANY_CODE, x.COMPANY_NAME, x.UNIT_CODE, x.UNIT_NAME, x.POSITION_CODE, x.POSITION_NAME));
                stbsql.Append(string.Format("UPDATE dbo.HEC_User_Gangwei SET isdelete=0 WHERE ID='{0}'",x.ID));
            }
            foreach (var x in listdel)
            {
                var tmp = listnor.FirstOrDefault(item => item.ID == x.ID);
                if (tmp == null)
                    continue;
                stb.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td><td>{6}</td><td>{7}</td><td></td><td>是</td></tr>", x.EMPLOYEE_CODE, x.EMPLOYEE_NAME, x.COMPANY_CODE, x.COMPANY_NAME, x.UNIT_CODE, x.UNIT_NAME, x.POSITION_CODE, x.POSITION_NAME));
                stbsql.Append(string.Format("delete HEC_User_Gangwei where ID='{0}'",x.ID));
            }

            stb.Append("</table>");
            StringBuilder stbn = new StringBuilder();
            stbn.Append("<table class=\"info\" cellpadding=\"1\" cellspacing=\"1\" border=\"1\" width=\"100%\"><caption style=\"text-align:left;color:black;background:white;\">岗位信息更改</caption>");
            stbn.Append("<tr><th rowspan=\"2\">公司部门</th><th rowspan=\"2\">岗位名称</th><th colspan=\"2\" style=\"width:150px\">是否主岗位</th><th colspan=\"2\" style=\"width:150px\">是否启用</th><th rowspan=\"2\">更新</th></tr>");
            stbn.Append("<tr><th style=\"width:75px;\">原值</th><th style=\"width:75px;\">现值</th><th style=\"width:75px;\">原值</th><th style=\"width:75px;\">现值</th></tr>");
            int flag = 0;
            foreach (var x in listdef)
            {
                var tmp = oldlist.FirstOrDefault(item=>item.ID==x.ID);
                if (tmp.PRIMARY_POSITION_FLAG == x.PRIMARY_POSITION_FLAG && tmp.ENABLED_FLAG == x.ENABLED_FLAG)
                    continue;
                else
                {
                    stbn.Append(string.Format("<tr><td>{0}</td><td>{1}</td>", x.COMPANY_CODE + "/" + x.COMPANY_NAME + "/" + x.UNIT_CODE + "/" + x.UNIT_NAME, x.POSITION_CODE + "/" + x.POSITION_NAME));
                    if (tmp.PRIMARY_POSITION_FLAG != x.PRIMARY_POSITION_FLAG)
                    {
                        flag++;
                        stbn.Append(string.Format("<td>{0}</td><td>{1}</td>", tmp.PRIMARY_POSITION_FLAG == "Y" ? "是" : "否", x.PRIMARY_POSITION_FLAG == "Y" ? "是" : "否"));
                    }
                    else
                    {
                        stbn.Append("<td></td><td></td>");
                    }
                    if (tmp.ENABLED_FLAG != x.ENABLED_FLAG)
                    {
                        flag++;
                        stbn.Append(string.Format("<td>{0}</td><td>{1}</td>", tmp.ENABLED_FLAG == "Y" ? "是" : "否", x.ENABLED_FLAG == "Y" ? "是" : "否"));
                    }
                    else
                    {
                        stbn.Append("<td></td><td></td>");
                    }
                    stbn.Append("<td>是</td></tr></table>");
                }
            }
            if (flag == 0)
                stbn.Clear();
            if (!string.IsNullOrEmpty(stbsql.ToString()))
            {
                db.ExecuteStoreCommand(stbsql.ToString());
                db.SaveChanges();
            }
            return stb.ToString() + "<br/>" + stbn.ToString() ;
        }

        public static string SapRoleMess(List<SAP_User_Role> Newlist, List<SAP_User_Role> Oldlilst, string userid)
        {
            IAMEntities db = new IAMEntities();
            List<SAP_User_Role> newlist = Newlist.Where(item => item.isdr == 2).ToList();
            List<SAP_User_Role> oldlist = Newlist.Where(item => item.isdr == 1).ToList();
            List<SAP_User_Role> normal = newlist.Where(item => item.isdr == 0).ToList();
            List<SAP_User_Role> defaultlist = db.SAP_User_Role.Where(item => item.BAPIBNAME == userid).ToList();

            System.Text.StringBuilder stb = new StringBuilder();
            if (newlist == null && oldlist == null)
                return stb.ToString();
            stb.Append("<table class=\"info\" cellpadding=\"1\" cellspacing=\"1\" border=\"1\" width=\"100%\"><tr><th>角色代码</th><th>角色名称</th><th>有效期从</th><th>有效期至</th><th>添加</th><th>删除</th></tr>");
            foreach (var i in newlist)
            {
                stb.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>是</td><td></td></tr>", i.ROLEID, i.ROLENAME, i.START_DATE, i.END_DATE));
            }
            StringBuilder stbdel = new StringBuilder();
            foreach (var i in oldlist)
            {
                if (defaultlist != null && defaultlist.Count > 0)
                {
                    var tmp = defaultlist.FirstOrDefault(x => x.BAPIBNAME == userid && x.ROLEID == i.ROLEID && x.isdr == 0);
                    if (tmp != null)
                    {
                        stb.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td></td><td>是</td></tr>", i.ROLEID, i.ROLENAME, i.START_DATE, i.END_DATE));
                        stbdel.Append(string.Format(" update dbo.SAP_User_Role set END_DATE='" + DateTime.Now.ToString("yyyy-MM-dd") + "' end WHERE ROLEID='{0}' AND BAPIBNAME='{1}'", i.ROLEID, i.BAPIBNAME));
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            StringBuilder stbn = new StringBuilder();
            stbn.Append("<table class=\"info\" cellpadding=\"1\" cellspacing=\"1\" border=\"1\" width=\"100%\"><caption style=\"text-align:left;color:black;background:white;\">角色信息更改</caption>");
            stbn.Append("<tr><th rowspan=\"2\">角色ID</th><th rowspan=\"2\">角色名称</th<th colspan=\"2\" style=\"width:150px\">有效期从</th><th colspan=\"2\" style=\"width:150px\">有效期至</th><th rowspan=\"2\">更新</th></tr>");
            stbn.Append("<tr><th style=\"width:75px;\">原值</th><th style=\"width:75px;\">现值</th><th style=\"width:75px;\">原值</th><th style=\"width:75px;\">现值</th></tr>");
            int flag = 0;
            foreach (var x in normal)
            {
                var xx = oldlist.FirstOrDefault(item => item.ID == x.ID);
                if (xx != null)
                {
                    stbn.Append(string.Format("<tr><td>{0}</td><td>{1}</td>", xx.ROLEID, xx.ROLENAME));
                    if (xx.START_DATE != x.START_DATE)
                    {
                        stbn.Append(string.Format("<td>{0}</td><td>{1}</td>", xx.START_DATE, x.START_DATE));
                        flag++;
                    }
                    else
                    {
                        stbn.Append(string.Format("<td>{0}</td><td>{1}</td>", xx.START_DATE, x.START_DATE));
                    }
                    if (xx.END_DATE != x.END_DATE)
                    {
                        flag++;
                        stbn.Append(string.Format("<td>{0}</td><td>{1}</td>", xx.END_DATE, x.END_DATE));
                    }
                    else
                    {
                        stbn.Append(string.Format("<td>{0}</td><td>{1}</td>", xx.END_DATE, x.END_DATE));
                    }
                    stbn.Append("<td>是</td></tr>");
                }
            }
            stbn.Append("</table>");
            if (flag == 0)
                stbn.Clear();

            if (newlist.Count == 0 && oldlist.Count == 0 && flag == 0)
                return "";

            if (!string.IsNullOrEmpty(stbdel.ToString()))
            {
                db.ExecuteStoreCommand(stbdel.ToString());
                db.SaveChanges();
            }

            stb.Append("</table>");
            return stb.ToString() + stbn.ToString();
        }

        public static string SapParemters(List<SAP_Parameters> list, string userid)
        {
            IAMEntities db = new IAMEntities();
            List<SAP_Parameters> newlist = list.Where(item => item.isdr == 2).ToList();
            List<SAP_Parameters> oldlist = list.Where(item => item.isdr == 1).ToList();
            List<SAP_Parameters> defaultlist = db.SAP_Parameters.Where(item => item.BAPIBNAME == userid).ToList();
            if (newlist.Count == 0 && oldlist.Count == 0)
                return "";

            System.Text.StringBuilder stb = new StringBuilder();
            if (newlist == null && oldlist == null)
                return stb.ToString();
            stb.Append("<table class=\"info\" cellpadding=\"1\" cellspacing=\"1\" border=\"1\" width=\"100%\"><tr><th>参数Id</th><th>参数值</th><th>短文本</th><th>添加</th><th>删除</th></tr>");
            foreach (var i in newlist)
            {
                stb.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>是</td><td></td></tr>", i.PARAMENTERID, i.PARAMENTERVALUE, i.PARAMETERTEXT));
            }

            foreach (var i in oldlist)
            {
                if (defaultlist != null && defaultlist.Count > 0)
                {
                    var tmp = defaultlist.FirstOrDefault(item => item.BAPIBNAME == userid && item.PARAMENTERID == i.PARAMENTERID);
                    if (tmp != null)
                    {
                        stb.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td></td><td>是</td></tr>", i.PARAMENTERID, i.PARAMENTERVALUE, i.PARAMETERTEXT));
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            stb.Append("</table>");
            return stb.ToString();
        }

        public static string TCRoleMess(List<V_TCReport> Newlist, List<V_TCReport> Oldlist, string userid)
        {
            List<V_TCReport> newlist = Newlist.Where(item => item.isdr == 2).ToList();
            List<V_TCReport> dellist = Newlist.Where(item => item.isdr == 1).ToList();
            List<V_TCReport> normal = Newlist.Where(item => item.isdr == 0).ToList();
            List<V_TCReport> mylist = new IAMEntities().V_TCReport.Where(item => item.urUserID == userid).ToList();


            StringBuilder stbn = new StringBuilder();
            stbn.Append("<table class=\"info\" cellpadding=\"1\" cellspacing=\"1\" border=\"1\" width=\"100%\"><caption style=\"text-align:left;color:black;background:white;\">角色信息更改</caption>");
            stbn.Append("<tr><th rowspan=\"2\">角色名称</th><th rowspan=\"2\">组名称</th><th colspan=\"2\" style=\"width:150px;\">状态更改</th><th rowspan=\"2\">操作</th></tr>");
            stbn.Append("<tr> <th style=\"width:75px;\">原值</th><th style=\"width:75px;\">现值</th></tr>");
            int flag = 0;
            foreach (var x in normal)
            {
                var o = Oldlist.FirstOrDefault(xx => xx.urid == x.urid);
                if (o != null)
                {
                    if (o.urGroupStatus != x.urGroupStatus)
                    {
                        stbn.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>", IAM.BLL.Untityone.GetRoleName(x.urMemo), IAM.BLL.Untityone.GetGroupName(x.urMemo), o.urGroupStatus == 1 ? "活动" : "非活动", x.urGroupStatus == 1 ? "活动" : "非活动", "编辑账号"));
                        flag++;
                    }
                }
            }
            stbn.Append("</table>");
            if (flag == 0)
                stbn.Clear();
            if (newlist.Count == 0 && dellist.Count == 0 && flag == 0)
                return "";

            StringBuilder stb = new StringBuilder();
            stb.Append("<br/><br/><table class=\"info\" cellpadding=\"1\" cellspacing=\"1\" border=\"1\" width=\"100%\"><tr><th>角色名称</th><th>组名称</th><th>状态</th><th>添加</th><th>删除</th></tr>");
            foreach (var i in newlist)
            {
                stb.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>是</td><td></td></tr>", i.urp2, i.urp1, i.urGroupStatus == 1 ? "活动" : "非活动"));
            }
            StringBuilder delsql = new StringBuilder();
            foreach (var i in dellist)
            {
                if (mylist != null && mylist.Count > 0)
                {
                    var tmp = mylist.FirstOrDefault(item => item.urUserID == userid && item.urMemo == i.urMemo);
                    if (tmp != null)
                    {
                        stb.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td></td><td>是</td></tr>", i.urp2, i.urp1, i.urGroupStatus == 1 ? "活动" : "非活动"));
                        delsql.Append(string.Format(" DELETE dbo.TC_UserGroupSetting WHERE Id='"+i.urid+"'  "));

                    }
                    else
                    {
                        continue;
                    }
                }
            }

            if (!string.IsNullOrEmpty(delsql.ToString()))
            {
                using (IAMEntities db = new IAMEntities())
                {
                    db.ExecuteStoreCommand(delsql.ToString());
                    db.SaveChanges();
                }
            }


            stb.Append("</table>");
            return stbn.ToString() + stb.ToString();
        }

        public static string AD(List<AD_UserWorkGroup> list, string accname)
        {
            List<AD_UserWorkGroup> newlist = list.Where(item => item.isdr == 2).ToList();
            List<AD_UserWorkGroup> dellist = list.Where(item => item.isdr == 1).ToList();
            List<AD_UserWorkGroup> mylist = new IAMEntities().AD_UserWorkGroup.Where(item => item.Uid == accname).ToList();
            if (newlist.Count == 0 && dellist.Count == 0)
                return "";
            StringBuilder stb = new StringBuilder();
            stb.Append("<table class=\"info\" cellpadding=\"1\" cellspacing=\"1\" border=\"1\" width=\"100%\"><tr><th>登陆名</th><th>组名称</th><th>添加</th><th>删除</th></tr>");
            foreach (var i in newlist)
            {
                stb.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>添加</td><td></td></tr>", accname, i.GroupName));
            }
            StringBuilder stbdel = new StringBuilder();
            foreach (var i in dellist)
            {
                if (mylist != null && mylist.Count > 0)
                {
                    var tmp = mylist.FirstOrDefault(item => item.GroupName == i.GroupName && item.Uid == accname);
                    if (tmp != null)
                    {
                        stb.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td></td><td>删除</td></tr>", accname, i.GroupName));
                        stbdel.Append(string.Format(" delete AD_UserWorkGroup where GroupName='{0}' and Uid='{1}'", i.GroupName, accname));
                    }
                    else
                        continue;
                }
            }
            if (!string.IsNullOrEmpty(stbdel.ToString()))
            {
                using (IAMEntities db = new IAMEntities())
                {
                    db.ExecuteStoreCommand(stbdel.ToString());
                    db.SaveChanges();
                }
            }
            stb.Append("</table>");
            return stb.ToString();
        }

        public static string ADComputer(string gonghao)
        {
            StringBuilder stbcomputer = new StringBuilder();
            StringBuilder stbcmpwork = new StringBuilder();
            StringBuilder sql = new StringBuilder();
            using (IAMEntities db = new IAMEntities())
            {

                var list = from a in db.AD_Computer
                           join
                               b in db.AccountMaping on a.NAME equals b.zhanghao
                           where b.gonghao == gonghao && b.type == "ADComputer" && a.ENABLE == 1 && (a.IsDelete == false || a.IsDelete == null)
                           select new
                           {
                               NAME = a.NAME,
                               DESCRIPTION = a.DESCRIPTION,
                               ExpiryDate = a.ExpiryDate,
                               p1 = a.ExpiryDate,
                               ENABLE = a.ENABLE,
                               memo = a.Memo
                           };
                if (list != null && list.Count() > 0)
                {
                    stbcomputer.Append("<br/><br/><table class=\"info\" cellpadding=\"1\" cellspacing=\"1\" border=\"1\" width=\"100%\"><caption style=\"text-align:left;color:black;background:white;\">计算机信息:</caption>  <tr><th>计算机名称</th><th>描述</th><th>启用</th><th>启用时间</th><th>备注</th></tr>");
                    stbcmpwork.Append(string.Format("<br/><br/><table class=\"info\" cellpadding=\"1\" cellspacing=\"1\" border=\"1\" width=\"100%\"><caption style=\"text-align:left;color:black;background:white;\">计算机组:</caption><tr><th>计算机名称</th><th>组名称</th><th>添加</th><th>删除</th></tr>"));
                    foreach (var c in list)
                    {
                        stbcomputer.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>", c.NAME, c.DESCRIPTION, c.ENABLE, c.p1, c.memo));
                        var listg = db.AD_Computer_WorkGroups.Where(item => item.ComputerName == c.NAME).ToList();
                        if (listg != null && listg.Count() > 0)
                        {
                            foreach (var w in listg)
                            {

                                if (w.isdr == 1)
                                {
                                    sql.Append(string.Format("delete AD_Computer_WorkGroups where id='{0}'", w.Id));
                                    stbcmpwork.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", w.ComputerName, w.WorkGroup, w.isdr == 0 ? "是" : "", w.isdr == 1 ? "是" : ""));
                                }
                                if (w.isdr == 2)
                                {
                                    sql.Append(string.Format("update  AD_Computer_WorkGroups set isdr=0 where id='{0}'", w.Id));
                                    stbcmpwork.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", w.ComputerName, w.WorkGroup, w.isdr == 0 ? "是" : "", w.isdr == 1 ? "是" : ""));
                                }
                            }
                        }
                    }
                    stbcmpwork.Append("</table>");
                    stbcomputer.Append("</table>");
                }
                if (!string.IsNullOrEmpty(sql.ToString()))
                {
                    db.ExecuteStoreCommand(sql.ToString());
                    db.SaveChanges();
                }
            }

            return stbcomputer.ToString() + "<br/>" + stbcmpwork.ToString();
        }

        public static string ADComputerByCreateSystem(string computername)
        {
            StringBuilder stbcomputer = new StringBuilder();
            StringBuilder stbcmpwork = new StringBuilder();
            StringBuilder sql = new StringBuilder();
            using (IAMEntities db = new IAMEntities())
            {
                var comp = db.AD_Computer.FirstOrDefault(item => item.NAME == computername);

                var list = db.AD_Computer_WorkGroups.Where(item => item.ComputerName == computername).ToList();
                if (comp != null)
                {
                    stbcomputer.Append("<br/><br/><table class=\"info\" cellpadding=\"1\" cellspacing=\"1\" border=\"1\" width=\"100%\"><caption style=\"text-align:left;color:black;background:white;\">计算机信息:</caption>  <tr><th>计算机名称</th><th>描述</th><th>启用</th><th>启用时间</th><th>备注</th></tr>");
                    stbcomputer.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>", comp.NAME, comp.DESCRIPTION, comp.ENABLE, comp.p1, comp.Memo));
                    stbcmpwork.Append(string.Format("<br/><br/><table class=\"info\" cellpadding=\"1\" cellspacing=\"1\" border=\"1\" width=\"100%\"><caption style=\"text-align:left;color:black;background:white;\">计算机组:</caption><tr><th>计算机名称</th><th>组名称</th><th>添加</th><th>删除</th></tr>"));
                    foreach (var w in list)
                    {

                        if (w.isdr == 1)
                        {
                            stbcmpwork.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", w.ComputerName, w.WorkGroup, w.isdr == 2 ? "是" : "", w.isdr == 1 ? "是" : ""));
                            sql.Append(string.Format("delete AD_Computer_WorkGroups where id='{0}'", w.Id));
                        }
                        if (w.isdr == 2)
                        {
                            sql.Append(string.Format("update  AD_Computer_WorkGroups set isdr=0 where id='{0}'", w.Id));
                            stbcmpwork.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", w.ComputerName, w.WorkGroup, w.isdr == 2 ? "是" : "", w.isdr == 1 ? "是" : ""));
                        }
                    }
                    stbcmpwork.Append("</table>");
                    stbcomputer.Append("</table>");
                }
                if (!string.IsNullOrEmpty(sql.ToString()))
                {
                    db.ExecuteStoreCommand(sql.ToString());
                    db.SaveChanges();
                }
            }

            return stbcomputer.ToString() + "<br/>" + stbcmpwork.ToString();
        }
    }

    /// <summary>
    /// 用户账号同步冲突解决 发送邮件
    /// </summary>
    public class Sys_UserName_ConflictResolutionMail
    {
        private static string ConflictInfoString(Unitity.SystemType type)
        {
            var list = new Sys_UserName_ConflictResolutionDAL().ReturnList(type);

            if (list != null && list.Count > 0)
            {
                StringBuilder stb = new StringBuilder();
                stb.Append("<table class=\"info\" cellpadding=\"1\" cellspacing=\"1\" border=\"1\" width=\"100%\">");
                stb.Append(@"<tr><th>用户账号</th><th>系统名称</th><th>字段名称</th><th>IAM系统值</th><th>源系统值</th><th>创建时间</th></tr>");
                list = list.OrderBy(item => item.UserName).ToList();
                foreach (var item in list)
                {
                    stb.Append(string.Format("<tr><th>{0}</th><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>", item.UserName, type.ToString(), item.CollName, item.CollIAMValue, item.CollSysValue, item.CreateTime));
                }
                stb.Append("</table>");
                return stb.ToString();
            }
            else
                return string.Empty;

        }

        public static void SendMail(Unitity.SystemType type)
        {
            AddUserMail MailModule = new AddUserMail();
            MailModule.Actioner = "System";
            MailModule.SystemName = type.ToString();
            string _confil = ConflictInfoString(type);
            if (string.IsNullOrEmpty(_confil))
                return;
            MailModule.UserInfo = _confil;
            IAMEntityDAL.MailInfo mailinfo = new IAMEntityDAL.MailInfo();
            mailinfo.Body = MailTemplateHelper.RunTemplate<AddUserMail>(MailModule, "UserInfoConflied");
            mailinfo.Authorizationid = new Guid(System.Web.Configuration.WebConfigurationManager.AppSettings["mailAurthID"]);
            mailinfo.SendMode = 1;
            mailinfo.SendTime = DateTime.Now;
            string maddress = "";
            string typemail = type.ToString();
            if (type == Unitity.SystemType.ADComputer)
                typemail = "AD";
            maddress = new TaskEmailDAL().GetList().FirstOrDefault(item => item.SystemName.Trim() == typemail).EmailAddress;
            if (!string.IsNullOrEmpty(maddress))
            {
                maddress = maddress.Trim();
                mailinfo.To = maddress.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
                mailinfo.To = new string[] { "yangjian@shac.com.cn" };
            mailinfo.Subject = type.ToString() + "同步账号信息";
            mailinfo.URLS = new string[] { };
            MySendMail.Send(mailinfo);
        }
    }

    public class SAPParmetersMail
    {
        public static void SendMail(List<string> UserName, List<SAP_Parameters> SapParmeters, List<SAP_User_Role> SapRoles, string actioner)
        {
            string table = "<table class=\"info\" cellpadding=\"1\" cellspacing=\"1\" border=\"1\" width=\"100%\">";
            BLL.AddUserMail addmail = new BLL.AddUserMail();
            addmail.Actioner = actioner;
            addmail.SystemName = "";
            StringBuilder stbuser = new StringBuilder();
            stbuser.Append(table);
            stbuser.Append("<tr><th>账号</th><th>姓名</th></tr>");
            int flagu = 0;
            foreach (var x in UserName)
            {
                flagu++;
                var en = new SAPUserInfoDAL().GetOneTCUser(x);
                stbuser.Append(string.Format("<tr><td>{0}</td><td>{1}</td></tr>", x, en != null ? en.LASTNAME : ""));
            }
            stbuser.Append("</table>");
            StringBuilder stbparms = new StringBuilder();
            stbparms.Append(table);
            stbparms.Append("<tr><th>参数文本</th><th>参数值</th><th>短文本</th><th>添加</th><th>删除</th></tr>");
            int flagp = 0;
            foreach (var x in SapParmeters)
            {
                flagp++;
                stbparms.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>", x.PARAMENTERID, x.PARAMENTERVALUE, x.PARAMETERTEXT, x.isdr == 2 ? "添加" : "", x.isdr == 1 ? "删除" : ""));
            }
            stbparms.Append("</table>");

            StringBuilder stbroles = new StringBuilder();
            stbroles.Append(table);
            stbroles.Append("<tr><th>角色Id</th><th>角色名称</th><th>有效期从</th><th>有效期至</th><th>添加</th><th>删除</th></tr>");
            int flagr = 0;
            foreach (var x in SapRoles)
            {
                flagr++;
                stbroles.Append(string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>", x.ROLEID, x.ROLENAME, x.START_DATE, x.END_DATE, x.isdr == 2 ? "添加" : "", x.isdr == 1 ? "删除" : ""));
            }
            stbroles.Append("</table>");
            if (flagu == 0)
                stbuser.Clear();
            if (flagp == 0)
                stbparms.Clear();
            if (flagr == 0)
                stbroles.Clear();
            if (flagp == 0 && flagr == 0)
                return;
            addmail.UserInfo = stbuser.ToString();
            addmail.RoleString = stbroles.ToString();
            addmail.SystemName = stbparms.ToString();

            IAMEntityDAL.MailInfo info = new IAMEntityDAL.MailInfo();
            info.Authorizationid = new Guid(System.Web.Configuration.WebConfigurationManager.AppSettings["mailAurthID"]);
            info.SendMode = 1;
            info.SendTime = DateTime.Now;
            string maddress = "";
            maddress = new TaskEmailDAL().GetList().FirstOrDefault(item => item.SystemName.Trim() == "SAP").EmailAddress;
            if (!string.IsNullOrEmpty(maddress))
            {
                maddress = maddress.Trim();
                info.To = maddress.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
                info.To = new string[] { "yangjian@shac.com.cn" };
            info.Subject = "SAP批量操作";
            info.URLS = new string[] { };

            info.Body = MailTemplateHelper.RunTemplate<AddUserMail>(addmail, "sappiliang");
            MySendMail.Send(info);
        }
    }



}