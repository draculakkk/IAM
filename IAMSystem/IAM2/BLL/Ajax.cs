using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAMEntityDAL;
using BaseDataAccess;

namespace IAM.BLL
{
    public partial class Ajax
    {
        public string Fun(HttpContext context)
        {
            string type = context.Request.QueryString["type"];
            string Filed = context.Request["Filed"];
            switch (type)
            {
                case "hrEmployee": return ExportHREmployee(context);
                case "hrUser": return ExportHRUserReport(context, Filed);
                case "hrrole": return ExportHRRoleReport(context, Filed);
                case "hecrole": return ExportHECRoleReport(context, Filed, false);
                case "hecuser": return ExportHECRoleReport(context, Filed, true);
                case "aduser": return ExportADReport(context, true);
                case "adgroup": return ExportADReport(context, false);
                case "sapuser": return ExportSAPReport(context, Filed, true);
                case "saprole": return ExportSAPReport(context, Filed, false);
                case "tcuser": return ExportTCReport_as_User(context);
                case "tcrole": return ExportTCReport_as_Role(context);
                case "addTemplateInfo": return AddTemplate(context);
                case "taskmail": return UpdateTaskMail(context);
                case "adcomputer": return AD_ComputerWorkgroup(context);
                case "workgroup": return AD_WorkComputer(context);
                case "Transfer": return UserTransfer(context);
                case "defaultWorkgroup": return CreateDefaultWorkGroup(context);
                case "defaultWorkgroupEdit": return DefaultWorkGroupEdit(context);
                case "ADW": return ADWorkgroupAction(context);
                case "log": return GetLogMess(context);
                case "confiter": return ExportConfiter(context);
                case "hecgangwei": return ExportHECGangwei(context);
                default: return "";
            }
        }

        string UpdateTaskMail(HttpContext context)
        {
            try
            {
                string sys = context.Request["sys"];
                string mail = context.Request["mail"];
                string quanxian = context.Request["quan"];
                BaseDataAccess.TaskEmail module = new TaskEmail();
                if (!string.IsNullOrEmpty(sys) && !string.IsNullOrEmpty(mail))
                {
                    module.SystemName = sys;
                    module.EmailAddress = mail;
                    module.p1 = quanxian;
                    if (new IAMEntityDAL.TaskEmailDAL().UpdateTaskEmail(module) > 0)
                        return "ok";
                    else
                        return string.Empty;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "error:" + ex.ToString();
            }
        }


        string ExportHREmployee(HttpContext context)
        {
            try
            {
                string gonghao = context.Request["gonghao"];
                string name = context.Request["name"];
                string department = context.Request["department"];
                string gangwei = context.Request["gangwei"];
                string lizhiriqi = context.Request["lizhiriqi"];
                string lizhiriqi2 = context.Request["lizhiriqi2"];
                string shifoulizhi = context.Request["shifoulizhi"];

                string filepath = context.Server.MapPath("~/template/ExcelTemplate/HREmployeeList.xlsx");
                string filename = "HREmployeeList" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xlsx";
                string newfilepath = context.Server.MapPath("~/downloadFile/" + filename);

                bool isok = new IAMEntityDAL.HREmployeeDAL().ExportExcelHrEmployee(filepath, newfilepath,
                    gonghao, name, department, gangwei, lizhiriqi, lizhiriqi2, shifoulizhi
                    );
                if (isok)
                    return filename;
                else
                    return "error:无数据可导！";
            }
            catch (Exception ex)
            {
                return "error:" + ex.ToString();
            }


        }

        string ExportConfiter(HttpContext context)
        {
            try
            {
                string systemtype = context.Request["systype"];
                string state = context.Request["state"];
                Unitity.SystemType? st = null;
                if (!string.IsNullOrEmpty(systemtype))
                {
                    st = (Unitity.SystemType)Enum.Parse(typeof(Unitity.SystemType), systemtype, true);
                }
                string filepath = context.Server.MapPath("~/template/ExcelTemplate/UserConfiterTemplate.xlsx");
                string filename = Guid.NewGuid().ToString() + ".xlsx";
                string newfilepath = context.Server.MapPath("~/downloadFile/" + filename);
                bool isok = new Sys_UserName_ConflictResolutionDAL().ExportExcel(filepath, newfilepath, st, Convert.ToInt32(state));
                if (isok)
                    return filename;
                else
                    return "error:导出错误";
            }
            catch (Exception ex)
            {
                return "error:" + ex.Message;
            }
        }

        /// <summary>
        /// 导出hr 用户报表
        /// </summary>
        /// <param name="context"></param>
        /// <param name="Filed"></param>
        /// <returns></returns>
        string ExportHRUserReport(HttpContext context, string Filed)
        {
            try
            {
                string filepath = context.Server.MapPath("~/template/ExcelTemplate/HRuserReport.xlsx");
                string filename = Guid.NewGuid().ToString() + ".xlsx";
                string newfilepath = context.Server.MapPath("~/downloadFile/" + filename);
                string gonghao = context.Request["gonghao"];
                string name = context.Request["name"];
                string department = context.Request["department"];
                string hrusername = context.Request["hrusername"];
                string logintype = context.Request["logintype"];
                string rolename = context.Request["rolename"];
                string companyname = context.Request["companyname"];
                string islock = context.Request["islock"];
                string gangwei = context.Request["gangwei"];
                string leixing=context.Request["leixing"];
                bool isok = new IAMEntityDAL.V_HRSm_User_RoleDAL().ReturnExcelExport(filepath, newfilepath, gonghao, name, department, hrusername, logintype, rolename, companyname, islock, gangwei, true,leixing);
                if (isok)
                    return filename;
                else
                    return "error:无数据可导！";
            }
            catch (Exception ex)
            {
                return "error:" + ex.ToString();
            }

        }

        /// <summary>
        /// 导出hr 角色报表
        /// </summary>
        /// <param name="context"></param>
        /// <param name="Filed"></param>
        /// <returns></returns>
        string ExportHRRoleReport(HttpContext context, string Filed)
        {
            try
            {

                string filepath = context.Server.MapPath("~/template/ExcelTemplate/HRRoleReport.xlsx");
                string filename = Guid.NewGuid().ToString() + ".xlsx";
                string newfilepath = context.Server.MapPath("~/downloadFile/" + filename);
                string gonghao = context.Request["gonghao"];
                string name = context.Request["name"];
                string department = context.Request["department"];
                string hrusername = context.Request["hrusername"];
                string logintype = context.Request["logintype"];
                string rolename = context.Request["rolename"];
                string companyname = context.Request["companyname"];
                string islock = context.Request["islock"];
                string gangwei = context.Request["gangwei"];
                string leixing = context.Request["leixing"];
                bool isok = new IAMEntityDAL.V_HRSm_User_RoleDAL().ReturnExcelExport(filepath, newfilepath, gonghao, name, department, hrusername, logintype, rolename, companyname, islock, gangwei, false,leixing);
                if (isok)
                    return filename;
                else
                    return "error:无数据可导！";
            }
            catch (Exception ex)
            {
                return "error:" + ex.ToString();
            }
        }

        /// <summary>
        /// 导出HEC 角色报表
        /// </summary>
        /// <param name="context"></param>
        /// <param name="Filed"></param>
        /// <returns></returns>
        string ExportHECRoleReport(HttpContext context, string Filed, bool isUser)
        {
            try
            {
                string filepath = context.Server.MapPath("~/template/ExcelTemplate/HecRoleReport.xlsx");
                string filename = Guid.NewGuid().ToString() + ".xlsx";
                string newfilepath = context.Server.MapPath("~/downloadFile/" + filename);
                if (isUser)
                {
                    filepath = context.Server.MapPath("~/template/ExcelTemplate/HECUserReport.xlsx");
                    newfilepath = context.Server.MapPath("~/downloadFile/" + filename);
                }

                var gonghao = context.Request["gonghao"];
                var name = context.Request["gonghao"];
                var department = context.Request["department"];
                var gangwei = context.Request["gangwei"];

                var hecname = context.Request["hecname"];
                var leixing = context.Request["leixing"];
                DateTime? startdate = context.Request["startdate"] == string.Empty ? (DateTime?)null : Convert.ToDateTime(context.Request["startdate"]);
                DateTime? enddate = context.Request["enddate"] == string.Empty ? (DateTime?)null : Convert.ToDateTime(context.Request["enddate"]);

                var rolename = context.Request["rolename"];
                var companyname = context.Request["companyname"];
                var jinyong = context.Request["jinyong"];

                bool isok = new IAMEntityDAL.V_HECUSER_RoleDAL().ReturnExcelExport(filepath, newfilepath, gonghao, name, department, gangwei, hecname, leixing, startdate, enddate, rolename, companyname, jinyong, isUser);
                if (isok)
                    return filename;
                else
                    return "error:无数据可导！";
            }
            catch (Exception ex)
            {
                return "error:" + ex.ToString();
            }
        }

        string ExportHECGangwei(HttpContext context)
        {
            string gonghao = context.Request["gonghao"];
            string bumen = context.Request["bumen"];
            string xingming = context.Request["xingming"];
            string hrgangwei = context.Request["hrgangwei"];
            string heczhanghao = context.Request["heczhanghao"];
            string zhanghaoleixing = context.Request["zhanghaoleixing"];
            string gongsi = context.Request["gongsi"];
            string hecbumen = context.Request["hecbumen"];
            string hecgangwei = context.Request["hecgangwei"];
            string shifouzhuagangwei = context.Request["shifouzhuagangwei"];
            string shifouqiyong = context.Request["shifouqiyong"];
            string isuser = context.Request["isuser"];

            string filepath = context.Server.MapPath("~/template/ExcelTemplate/hecUserGangwei.xlsx");
            string filename = Guid.NewGuid().ToString() + ".xlsx";
            string newfilepath = context.Server.MapPath("~/downloadFile/" + filename);
            if (isuser != "1")
            {
                filepath = context.Server.MapPath("~/template/ExcelTemplate/hecgangweiuser.xlsx");
                newfilepath = context.Server.MapPath("~/downloadFile/" + filename);
            }
            bool isok = new IAMEntityDAL.VHECGangWei().ReturnExcelExport(filepath, newfilepath, gonghao, xingming, bumen, hrgangwei, heczhanghao, zhanghaoleixing, gongsi, hecbumen, hecgangwei, shifouzhuagangwei, shifouqiyong, isuser == "1" ? true : false);
            if (isok)
                return filename;
            else
                return "error:无数据可导！";
        }

        /// <summary>
        /// 导出Ad 用户或组EXcel 信息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="Filed"></param>
        /// <param name="IsUser"></param>
        /// <returns></returns>
        string ExportADReport(HttpContext context, bool IsUser)
        {
            try
            {
                string filepath = context.Server.MapPath("~/template/ExcelTemplate/AdGroupReport.xlsx");
                string filename = Guid.NewGuid().ToString() + ".xlsx";
                string newfilepath = context.Server.MapPath("~/downloadFile/" + filename);
                if (IsUser)
                {
                    filepath = context.Server.MapPath("~/template/ExcelTemplate/AdUserReport.xlsx");
                    newfilepath = context.Server.MapPath("~/downloadFile/" + filename);
                }

                string gonghao = context.Request["gonghao"];
                string department = context.Request["department"];
                string name = context.Request["name"];
                string gangwei = context.Request["gangwei"];
                string adusername = context.Request["adusername"];
                string leixing = context.Request["leixing"];
                DateTime? StartDate = context.Request["StartDate"] == string.Empty ? (DateTime?)null : Convert.ToDateTime(context.Request["StartDate"]);
                DateTime? EndDate = context.Request["EndDate"] == string.Empty ? (DateTime?)null : Convert.ToDateTime(context.Request["EndDate"]);
                string qiyong = context.Request["qiyong"];
                string workgroupName = context.Request["workgroupName"];


                bool isok = new IAMEntityDAL.V_AD_UserWorkGroupDAL().ExcelExport(filepath, newfilepath, gonghao, department, name, gangwei, adusername, leixing, StartDate, EndDate, qiyong, workgroupName, IsUser);
                if (isok)
                    return filename;
                else
                    return "error:无数据可导！";
            }
            catch (Exception ex)
            {
                return "error:" + ex.ToString();
            }
        }

        /// <summary>
        /// 导出sap 用户或角色EXcel 信息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="Filed"></param>
        /// <param name="IsUser"></param>
        /// <returns></returns>
        string ExportSAPReport(HttpContext context, string Filed, bool IsUser)
        {
            try
            {
                string filepath = context.Server.MapPath("~/template/ExcelTemplate/SapRoleReport.xlsx");
                string filename = Guid.NewGuid().ToString() + ".xlsx";
                string newfilepath = context.Server.MapPath("~/downloadFile/" + filename);
                if (IsUser)
                {
                    filepath = context.Server.MapPath("~/template/ExcelTemplate/SapUserReport.xlsx");
                    newfilepath = context.Server.MapPath("~/downloadFile/" + filename);
                }

                string gonghao = context.Request["gonghao"];
                string name = context.Request["name"];
                string department = context.Request["department"];
                string gangwei = context.Request["gangwei"];
                string sapname = context.Request["sapname"];
                string leixing = context.Request["leixing"];
                string startdates = context.Request["startdates"];
                string enddates = context.Request["enddates"];
                string startdatee = context.Request["startdatee"];
                string enddatee = context.Request["enddatee"];
                string roleid = context.Request["roleid"];
                string rolename = context.Request["rolename"];
                string userType = context.Request["userType"];

                bool isok = new IAMEntityDAL.V_Sap_UserRoleReportDAL().ReturnExcelExport(filepath, newfilepath, gonghao, name, department, gangwei, sapname, leixing, startdates, enddates, roleid, rolename, userType, startdatee, enddatee, IsUser);
                if (isok)
                    return filename;
                else
                    return "error:无数据可导！";

            }
            catch (Exception ex)
            {
                return "error:" + ex.ToString();
            }
        }

        /// <summary>
        /// 导出Tc 用户报表 Excel
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        string ExportTCReport_as_User(HttpContext context)
        {
            try
            {
                string filepath = context.Server.MapPath("~/template/ExcelTemplate/TcUserReport.xlsx");
                string filename = Guid.NewGuid().ToString() + ".xlsx";
                string newfilepath = context.Server.MapPath("~/downloadFile/" + filename);
                string gonghao = context.Request["gonghao"];
                string Name = context.Request["Name"];
                string department = context.Request["department"];
                string UserName = context.Request["UserName"];
                int? xukejibie = context.Request["xukejibie"] == string.Empty ? (int?)null : Convert.ToInt32(context.Request["xukejibie"]);
                int? userStatus = context.Request["userStatus"] == string.Empty ? (int?)null : Convert.ToInt32(context.Request["userStatus"]);
                string gangwei = context.Request["gangwei"];
                string leixing = context.Request["leixing"];
                string groupname = context.Request["groupname"];
                string rolename = context.Request["rolename"];
                string juesejinyong = context.Request["juesejinyong"];
                int userstatus = Convert.ToInt32(context.Request["status"]);

                bool isok = new IAMEntityDAL.V_TCReportDAL().ReturnExcelExport_as_User(filepath, newfilepath, gonghao, Name, department, gangwei, UserName, xukejibie, userStatus, leixing, groupname, rolename, juesejinyong);
                if (isok)
                    return filename;
                else
                    return "error:无数据导出";
            }
            catch (Exception ex)
            {
                return "error:" + ex.ToString();
            }
        }

        /// <summary>
        /// 导出 TC角色报表Excel
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        string ExportTCReport_as_Role(HttpContext context)
        {
            try
            {
                string filepath = context.Server.MapPath("~/template/ExcelTemplate/TcRoleReport.xlsx");
                string filename = Guid.NewGuid().ToString() + ".xlsx";
                string newfilepath = context.Server.MapPath("~/downloadFile/" + filename);
                string gonghao = context.Request["gonghao"];
                string Name = context.Request["Name"];
                string department = context.Request["department"];
                string UserName = context.Request["UserName"];
                int? xukejibie = context.Request["xukejibie"] == string.Empty ? (int?)null : Convert.ToInt32(context.Request["xukejibie"]);
                int? userStatus = context.Request["userStatus"] == string.Empty ? (int?)null : Convert.ToInt32(context.Request["userStatus"]);
                string gangwei = context.Request["gangwei"];
                string groupname = context.Request["groupname"];
                string rolename = context.Request["rolename"];
                string leixing = context.Request["leixing"];
                string juesejinyong = context.Request["juesejinyong"];
                bool isok = new IAMEntityDAL.V_TCReportDAL().ReturnExcelExport_as_Role(filepath, newfilepath, gonghao, Name, department, gangwei, UserName, xukejibie, userStatus, leixing, groupname, rolename, juesejinyong);
                if (isok)
                    return filename;
                else
                    return "error:无数据导出";
            }
            catch (Exception ex)
            {
                return "error:" + ex.ToString();
            }
        }

        string AD_ComputerWorkgroup(HttpContext context)
        {
            try
            {
                string filepath = context.Server.MapPath("~/template/ExcelTemplate/ADComputerReport.xlsx");
                string filename = Guid.NewGuid().ToString() + ".xlsx";
                string newfilepath = context.Server.MapPath("~/downloadFile/" + filename);
                string gonghao = context.Request["gonghao"];
                string computername = context.Request["computername"];
                string workgroup = context.Request["workgroup"];
                string dept = context.Request["department"];
                string name = context.Request["name"];
                string gangwei = context.Request["gangwei"];
                string leixing = context.Request["leixing"];
                string jinyong = context.Request["jinyong"];
                bool isok = new IAMEntityDAL.V_AdcomputerWorkGroupInfoDAL().ReturnExcelReport_Computer(filepath, newfilepath, gonghao, name, dept, gangwei, computername, leixing, workgroup, jinyong);
                if (isok)
                    return filename;
                else
                    return "error:无数据导出";
            }
            catch (Exception ex)
            {
                return "error:" + ex.ToString();
            }
        }

        string AD_WorkComputer(HttpContext context)
        {
            try
            {
                string filepath = context.Server.MapPath("~/template/ExcelTemplate/ADWorkgroupComputerReport.xlsx");
                string filename = Guid.NewGuid().ToString() + ".xlsx";
                string newfilepath = context.Server.MapPath("~/downloadFile/" + filename);
                string gonghao = context.Request["gonghao"];
                string computername = context.Request["computername"];
                string workgroup = context.Request["workgroup"];
                string dept = context.Request["department"];
                string name = context.Request["name"];
                string leixing = context.Request["leixing"];
                string gangwei = context.Request["gangwei"];
                string jinyong = context.Request["jinyong"];
                bool isok = new IAMEntityDAL.V_AdcomputerWorkGroupInfoDAL().ReturnExcelReport_workgroup(filepath, newfilepath, gonghao, name, dept, gangwei, computername, leixing, workgroup, jinyong);
                if (isok)
                    return filename;
                else
                    return "error:无数据导出";
            }
            catch (Exception ex)
            {
                return "error:" + ex.ToString();
            }
        }

        #region 创建岗位模版
        /// <summary>
        /// 创建岗位模版
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        string AddTemplate(HttpContext context)
        {
            Guid TemplateId = new Guid(context.Request["tempid"]);
            string systemname = context.Request["sysname"];
            string roleid = context.Request["rolelist"];
            string usertype = context.Request["usertype"];
            usertype = context.Server.UrlDecode(usertype);
            string username = context.Request["username"];
            try
            {
                List<RoleTemplateInfo> list = new List<RoleTemplateInfo>();
                switch (systemname)
                {
                    case "HR": list = GetHrRoleList(context.Server.UrlDecode(roleid), TemplateId, usertype, username); break;
                    case "HEC": list = GetHecRoleList(context.Server.UrlDecode(roleid), TemplateId, usertype, username); break;
                    case "HEC2":
                        list = GetHECGangwei(context.Server.UrlDecode(roleid),TemplateId,usertype,username);
                        break;
                    case "SAP": list = GetSapRoleList(context.Server.UrlDecode(roleid), TemplateId, usertype, username); break;
                    case "AD":
                        list = GetADRoleList(context.Server.UrlDecode(roleid), TemplateId, usertype, username, "AD");
                        break;
                    case "TC": list = GetTCRoleList(context.Server.UrlDecode(roleid), TemplateId, usertype, username); break;
                    case "ADComputer":
                        list = GetADRoleList(context.Server.UrlDecode(roleid), TemplateId, usertype, username, "ADComputer");
                        break;
                }

                new IAMEntityDAL.RoleTemplateInfoDAL().CreateRoleTemplateInfo(list, TemplateId, false);
                return "ok";
            }
            catch (Exception ex)
            {
                return "error:" + ex.Message;
            }

        }

        List<RoleTemplateInfo> GetADRoleList(string rolelist, Guid Templateid, string usertype, string username, string type)
        {
            string[] arrUserInfo = rolelist.Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
            List<RoleTemplateInfo> list = new List<RoleTemplateInfo>();
            foreach (string item in arrUserInfo)
            {
                RoleTemplateInfo entity = new RoleTemplateInfo();
                entity.ID = Guid.NewGuid();
                entity.TemplateID = Templateid;
                entity.RoleID = item;
                entity.RoleName = item;
                entity.SystemName = type;
                entity.CompanyName = "";
                entity.p1 = usertype;
                entity.p2 = username;
                list.Add(entity);
            }
            return list;
        }

        List<RoleTemplateInfo> GetHrRoleList(string rolelist, Guid Templateid, string usertype, string username)
        {
            string[] arrUserInfo = rolelist.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);


            List<RoleTemplateInfo> list = new List<RoleTemplateInfo>();
            foreach (string item in arrUserInfo)
            {
                string[] RoleTmp = item.Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);
                string companyname = string.Empty;

                RoleTemplateInfo entity = new RoleTemplateInfo();
                entity.ID = Guid.NewGuid();
                entity.TemplateID = Templateid;
                entity.RoleID = RoleTmp[1];
                entity.RoleName = RoleTmp[3];
                entity.SystemName = "HR";
                entity.CompanyName = RoleTmp[4];
                entity.p1 = usertype;
                entity.p2 = username;
                list.Add(entity);
            }
            return list;

        }

        List<RoleTemplateInfo> GetHecRoleList(string rolelist, Guid Templateid, string usertype, string username)
        {
            List<RoleTemplateInfo> list = new List<RoleTemplateInfo>();
            if (rolelist != string.Empty)
            {
                string[] arrUserInfo = rolelist.Split('^');
                RoleTemplateInfo newmdule = new RoleTemplateInfo();
                newmdule.ID = Guid.NewGuid();
                newmdule.RoleID = arrUserInfo[1];
                newmdule.RoleName = arrUserInfo[2];
                newmdule.SystemName = "HEC";
                newmdule.CompanyName = arrUserInfo[6];
                newmdule.StartDate = arrUserInfo[3];
                newmdule.EndDate = arrUserInfo[4];
                newmdule.TemplateID = Templateid;
                newmdule.p2 = username;
                newmdule.p1 = usertype;
                list.Add(newmdule);
            }
            return list;
        }


        List<RoleTemplateInfo> GetHECGangwei(string rolelist, Guid Templateid, string usertype, string username)
        {
            string[] StrGangWei = rolelist.Split("^".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            List<RoleTemplateInfo> list = new List<RoleTemplateInfo>();
            if (StrGangWei.Length >= 6)
            {
                RoleTemplateInfo tmp = new RoleTemplateInfo();
                tmp.ID = Guid.NewGuid();
                tmp.CompanyName = StrGangWei[0] + "^" + StrGangWei[1];
                tmp.RoleName = StrGangWei[2] + "^" + StrGangWei[3] + "^" + StrGangWei[4] + "^" + StrGangWei[5];
                tmp.RoleID = StrGangWei[4];
                tmp.p1 = usertype;
                tmp.p2 = username;
                tmp.SystemName = "HEC2";
                list.Add(tmp);

            }
            return list;
        }

        List<RoleTemplateInfo> GetSapRoleList(string rolelist, Guid Templateid, string usertype, string username)
        {
            List<RoleTemplateInfo> list = new List<RoleTemplateInfo>();
            if (!string.IsNullOrEmpty(rolelist))
            {
                string[] _value = rolelist.Split('^');
                RoleTemplateInfo entity = new RoleTemplateInfo();
                entity.ID = Guid.NewGuid();
                entity.RoleID = _value[0];
                entity.RoleName = _value[1];
                entity.StartDate = _value[2];
                entity.EndDate = _value[3];
                entity.SystemName = "SAP";
                entity.TemplateID = Templateid;
                entity.CompanyName = string.Empty;
                entity.p1 = usertype;
                entity.p2 = username;
                list.Add(entity);
            }
            return list;
        }

        List<RoleTemplateInfo> GetTCRoleList(string rolelist, Guid Templateid, string usertype, string username)
        {
            List<RoleTemplateInfo> list = new List<RoleTemplateInfo>();
            if (!string.IsNullOrEmpty(rolelist))
            {
                string[] rolearr = rolelist.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (rolearr != null && rolearr.Length > 0)
                {
                    foreach (var it in rolearr)
                    {
                        string[] tmp = it.Split('^');
                        RoleTemplateInfo entity = new RoleTemplateInfo();
                        entity.ID = Guid.NewGuid();
                        entity.TemplateID = Templateid;
                        entity.CompanyName = string.Empty;
                        entity.SystemName = "TC";
                        entity.RoleName = tmp[1];
                        entity.RoleID = tmp[0];
                        entity.StartDate = string.Empty;
                        entity.EndDate = string.Empty;
                        entity.p2 = username;
                        entity.p1 = usertype;
                        list.Add(entity);
                    }
                }

            }
            return list;
        }
        #endregion
    }
}