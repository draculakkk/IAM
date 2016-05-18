using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Xml;
using System.Data.OleDb;
using System.Net;

using System.Diagnostics;
using System.Configuration;
using System.Security.Principal;


using System.Web.Script.Serialization;

namespace BLL
{
    public class Vela3Utility
    {
    //    /// <summary>
    //    /// 参数strMsgIDs为配置文件MessageInfo.xml时的ID编号
    //    /// </summary>
    //    /// <param name="page"></param>
    //    /// <param name="strMsg"></param>
    //    public static void ShowMessageForAtlas(Page page, String strKey)
    //    {
    //        String strMessage = String.Empty;
    //        DsMessageInfo dsMessageInfo = GetMessageInfo();
    //        if (dsMessageInfo.MessageInfos.Rows.Count > 0)
    //        {
    //            DsMessageInfo.MessageInfosRow[] drs = (DsMessageInfo.MessageInfosRow[])dsMessageInfo.MessageInfos.Select("Key = '" + strKey + "'");
    //            if (drs.Length > 0)
    //            {
    //                strMessage = drs[0].Message;
    //            }
    //        }
    //        //string strScript = "<script language=javascript>alert('" + strMessage + "');</script>";
    //        string strScript = "alert('" + strMessage + "');";
    //        page.ClientScript.RegisterClientScriptBlock(page.GetType(), "ShowMessage", strScript, true);

    //        // ScriptManager.RegisterStartupScript(page, page.GetType(), "ShowMessage", strScript, true);

    //    }

    //    private static DsMessageInfo GetMessageInfo()
    //    {
    //        String strCacheKey = String.Empty;
    //        String strMessageFileName = String.Empty;
    //        String strMessageFileFullPath = String.Empty;
    //        DsMessageInfo dsMessageInfo;
    //        if (System.Threading.Thread.CurrentThread.CurrentCulture.Equals(new CultureInfo("zh-CN")))
    //        {
    //            strCacheKey = "Atos.SysFramework.MessageInfoCN";
    //            strMessageFileName = Vela3Configuration.MessageCN;
    //        }
    //        else
    //        {
    //            strCacheKey = "Atos.SysFramework.MessageInfoEN";
    //            strMessageFileName = Vela3Configuration.MessageEN;
    //        }
    //        strMessageFileFullPath = System.Web.HttpRuntime.AppDomainAppPath + strMessageFileName;
    //        if (CacheManager.Get(strCacheKey) != null)
    //        {
    //            dsMessageInfo = CacheManager.Get(strCacheKey) as DsMessageInfo;
    //        }
    //        else
    //        {
    //            dsMessageInfo = new DsMessageInfo();
    //            if (File.Exists(strMessageFileFullPath))
    //            {
    //                XmlDocument doc = new XmlDocument();
    //                doc.Load(strMessageFileFullPath);
    //                XmlNode root = doc.DocumentElement;
    //                if (root == null)
    //                {
    //                    throw new Exception(strMessageFileName + "文件结构已被破坏。");
    //                }
    //                else
    //                {
    //                    foreach (XmlNode xmlNode in root.ChildNodes)
    //                    {
    //                        if (xmlNode.Attributes != null)
    //                        {
    //                            DsMessageInfo.MessageInfosRow messageInfoRow = dsMessageInfo.MessageInfos.NewMessageInfosRow();
    //                            messageInfoRow.Key = xmlNode.Attributes["Key"].Value;
    //                            messageInfoRow.Message = xmlNode.Attributes["Message"].Value;
    //                            dsMessageInfo.MessageInfos.Rows.Add(messageInfoRow);
    //                        }
    //                    }
    //                }
    //                CacheManager.Insert(strCacheKey, dsMessageInfo);
    //            }
    //        }
    //        return dsMessageInfo;
    //    }

    //    /// <summary>
    //    /// 校验信息：根据KEY值得到显示信息
    //    /// </summary>
    //    /// <param name="strKey"></param>
    //    /// <returns></returns>
    //    public static string GetMessage(String strKey)
    //    {
    //        String strMessage = String.Empty;
    //        DsMessageInfo dsMessageInfo = GetMessageInfo();
    //        if (dsMessageInfo.MessageInfos.Rows.Count > 0)
    //        {
    //            DsMessageInfo.MessageInfosRow[] drs = (DsMessageInfo.MessageInfosRow[])dsMessageInfo.MessageInfos.Select("Key = '" + strKey + "'");
    //            if (drs.Length > 0)
    //            {
    //                strMessage = drs[0].Message;
    //            }
    //        }
    //        return strMessage;
    //    }

    //    #region 下拉列表遍历 xxLiu
    //    /// <summary>
    //    /// 下拉列表遍历.如果不存在该值,则默认添加
    //    /// </summary>
    //    /// <param name="DDlist"></param>
    //    /// <param name="value"></param>
    //    /// <param name="text"></param>
    //    public static void DownListSetValueWithAddVal(DropDownList DDlist, string value, string text)
    //    {
    //        if (DDlist.Items.FindByValue(value) != null)
    //        {
    //            DDlist.SelectedValue = value;
    //        }
    //        else
    //        {
    //            DDlist.ClearSelection();
    //            ListItem Li = new ListItem(text,value);
    //            DDlist.Items.Add(Li);
    //            Li.Selected = true;
    //        }
    //    }
    //    /// <summary>
    //    /// 下拉列表遍历
    //    /// </summary>
    //    /// <param name="DDlist"></param>
    //    /// <param name="value"></param>
    //    public static void DownListSetValue(System.Web.UI.WebControls.DropDownList DDlist, string value)
    //    {
    //        if (DDlist.Items.FindByValue(value) != null)
    //        {
    //            DDlist.SelectedValue = value;
    //        }
    //        else
    //        {
    //            DDlist.SelectedIndex = -1;
    //        }
    //    }

    //    public static void DownListSetText(System.Web.UI.WebControls.DropDownList DDlist, string text, string currentText)
    //    {
    //        if (DDlist.Items.FindByText(text) != null)
    //        {
    //            DDlist.Items.FindByText(currentText).Selected = false;
    //            DDlist.Items.FindByText(text).Selected = true;
    //        }
    //        else
    //        {
    //            DDlist.SelectedIndex = -1;
    //        }
    //    }

    //    /// <summary>
    //    /// 遍历CheckBoxList
    //    /// </summary>
    //    /// <param name="DDlist"></param>
    //    /// <param name="value"></param>
    //    public static void CheckBoxListSetValue(System.Web.UI.WebControls.CheckBoxList checkBoxList, string value)
    //    {
    //        if (checkBoxList.Items.FindByValue(value) != null)
    //        {
    //            checkBoxList.Items.FindByValue(value).Selected = true;
    //            //checkBoxList.SelectedValue = value;
    //        }
    //    }

    //    /// <summary>
    //    /// 遍历RadioButtonList
    //    /// </summary>
    //    /// <param name="rbl"></param>
    //    /// <param name="value"></param>
    //    public static void RadioButtonListSetValue(RadioButtonList rbl, string value)
    //    {
    //        if (rbl.Items.FindByValue(value) != null)
    //        {
    //            rbl.SelectedValue = value;
    //            // rbl.Items.FindByValue(value).Selected = true;
    //            //checkBoxList.SelectedValue = value;
    //        }
    //    }
    //    #endregion

    //    /// <summary>
    //    /// 保存上传的文件
    //    /// </summary>
    //    /// <param name="type">上传文件相关的业务类型</param>
    //    /// <param name="filename">文件名</param>
    //    /// <param name="server">HttpServerUtility</param>
    //    /// <param name="postedFile">HttpPostedFile</param>
    //    /// <param name="savedFilename">保存后的文件名</param>
    //    /// <param name="savedPath">保存后的路径</param>
    //    /// <param name="IsNeedProcessFileName">是否需要处理文件名：True-不需要对该文件名是否唯一得处理；False-需要对该文件名唯一处理</param>
    //    /// <returns>保存成功/失败</returns>
    //    public static bool SaveUploadFile(
    //        UploadFileType type,
    //        string filename,
    //        HttpServerUtility server,
    //        HttpPostedFile postedFile,
    //        ref string savedFilename,
    //        ref string savedPath,
    //        bool IsNeedProcessFileName,
    //        ref string oppositePath)
    //    {
    //        return SaveUploadSpecialFile(type, filename, server, postedFile, ref savedFilename, ref savedPath, IsNeedProcessFileName, Vela3Configuration.UploadFolder, ref oppositePath);
    //        /*
    //                    string strDomain = "";
    //                    string strLogin = "administrator";
    //                    string strPwd = "P@ssword";

    //                    if (ConfigurationManager.AppSettings["Domain"] != null)
    //                        strDomain = ConfigurationManager.AppSettings["Domain"].ToString();

    //                    if (ConfigurationManager.AppSettings["Login"] != null)
    //                        strLogin = ConfigurationManager.AppSettings["Login"].ToString();

    //                    if (ConfigurationManager.AppSettings["Pwd"] != null)
    //                        strPwd = ConfigurationManager.AppSettings["Pwd"].ToString();

    //                    WindowsImpersonationContext context = NetworkSecurity.ImpersonateUser(strDomain, strLogin, strPwd, LogonType.LOGON32_LOGON_INTERACTIVE, LogonProvider.LOGON32_PROVIDER_DEFAULT);

    //                    //step1:构造唯一文件名
    //                    savedFilename = "";
    //                    string singletonFilename;

    //                    if (IsNeedProcessFileName)
    //                    {
    //                        singletonFilename = MakeSingletonUploadFilename(filename);
    //                    }
    //                    else
    //                    {
    //                        singletonFilename = filename;
    //                    }
    //                    if (singletonFilename.Length == 0)
    //                        return false;

    //                    //step2:从配置文件中取得上传文件路径，由于该路径是跨服务器的，所以暂时不用server.MapPath
    //                    //string dir = server.MapPath("~") + Configuration.UploadFolder;
    //                    string dir = Vela3Configuration.UploadFolder;

    //                    //step3:建立“YYYYMM”的文件夹，目标是对文件按月份进行分类
    //                    if (dir.EndsWith(@"\") || dir.EndsWith(@"/"))
    //                    {
    //                        dir += DateTime.Now.ToString("yyyyMM");
    //                    }
    //                    else
    //                    {
    //                        dir += @"\" + DateTime.Now.ToString("yyyyMM");
    //                    }

    //                    //step4:创建最终存放文件的文件夹名称
    //                    switch (type)
    //                    {
    //                        case UploadFileType.Document:
    //                            dir += @"\Document";
    //                            break;
    //                        default:
    //                            break;
    //                    }

    //                    if (!Directory.Exists(dir))
    //                    {
    //                        Directory.CreateDirectory(dir);
    //                    }

    //                    try 
    //                    {
    //                        if (dir.EndsWith(@"\") || dir.EndsWith(@"/"))
    //                        {
    //                            postedFile.SaveAs(dir + singletonFilename);
    //                        }
    //                        else
    //                        {
    //                            postedFile.SaveAs(dir + @"\" + singletonFilename);
    //                        }
    //                    }
    //                    catch 
    //                    { 
    //                        return false; 
    //                    }

    //                    savedFilename = singletonFilename;
    //                    savedPath = dir;

    //                    context.Undo();

    //                    return true;
    //         */
    //    }

    //    /// <summary>
    //    /// 保存上传的临时文件
    //    /// </summary>
    //    /// <param name="type">上传文件相关的业务类型</param>
    //    /// <param name="filename">文件名</param>
    //    /// <param name="server">HttpServerUtility</param>
    //    /// <param name="postedFile">HttpPostedFile</param>
    //    /// <param name="savedFilename">上传后的文件名</param>
    //    /// <param name="savedPath">上传后的路径</param>
    //    /// <param name="IsNeedProcessFileName">是否需要处理文件名：True-不需要对该文件名是否唯一得处理；False-需要对该文件名唯一处理</param>
    //    /// <param name="TypeDir">如果是临时文件：TypeDir="Temp"；临时文件：TypeDir="Temporary"；如果是下载文件：TypeDir="DownLoadFiles"，如果是摸板文件：TypeDir="Templet"</param>
    //    /// <returns>保存成功/失败</returns>
    //    public static bool SaveUploadTempFile(
    //        UploadFileType type,
    //        string filename,
    //        HttpServerUtility server,
    //        HttpPostedFile postedFile,
    //        ref string savedFilename,
    //        ref string savedPath,
    //        bool IsNeedProcessFileName,
    //        ref string oppositePath)
    //    {
    //        return SaveUploadSpecialFile(type, filename, server, postedFile, ref savedFilename, ref savedPath, IsNeedProcessFileName, Vela3Configuration.TempFolder, ref oppositePath);
    //    }

    //    /// <summary>
    //    /// 保存上传的临时文件
    //    /// </summary>
    //    /// <param name="type">上传文件相关的业务类型</param>
    //    /// <param name="filename">文件名</param>
    //    /// <param name="server">HttpServerUtility</param>
    //    /// <param name="postedFile">HttpPostedFile</param>
    //    /// <param name="savedFilename">上传后的文件名</param>
    //    /// <param name="savedPath">上传后的路径</param>
    //    /// <param name="IsNeedProcessFileName">是否需要处理文件名：True-不需要对该文件名是否唯一得处理；False-需要对该文件名唯一处理</param>
    //    /// <param name="TypeDir">如果是临时文件：TypeDir="Temp"；临时文件：TypeDir="Temporary"；如果是下载文件：TypeDir="DownLoadFiles"，如果是摸板文件：TypeDir="Templet"</param>
    //    /// <returns>保存成功/失败</returns>
    //    public static bool SaveUploadTemporaryFile(
    //        UploadFileType type,
    //        string filename,
    //        HttpServerUtility server,
    //        HttpPostedFile postedFile,
    //        ref string savedFilename,
    //        ref string savedPath,
    //        bool IsNeedProcessFileName,
    //        ref string oppositePath)
    //    {
    //        return SaveUploadSpecialFile(type, filename, server, postedFile, ref savedFilename, ref savedPath, IsNeedProcessFileName, Vela3Configuration.TemporaryFolder, ref oppositePath);
    //    }

    //    /// <summary>
    //    /// 保存上传的下载文件
    //    /// </summary>
    //    /// <param name="type">上传文件相关的业务类型</param>
    //    /// <param name="filename">文件名</param>
    //    /// <param name="server">HttpServerUtility</param>
    //    /// <param name="postedFile">HttpPostedFile</param>
    //    /// <param name="savedFilename">上传后的文件名</param>
    //    /// <param name="savedPath">上传后的路径</param>
    //    /// <param name="IsNeedProcessFileName">是否需要处理文件名：True-不需要对该文件名是否唯一得处理；False-需要对该文件名唯一处理</param>
    //    /// <param name="TypeDir">如果是临时文件：TypeDir="Temp"；临时文件：TypeDir="Temporary"；如果是下载文件：TypeDir="DownLoadFiles"，如果是摸板文件：TypeDir="Templet"</param>
    //    /// <returns>保存成功/失败</returns>
    //    public static bool SaveUploadDownLoadFile(
    //        UploadFileType type,
    //        string filename,
    //        HttpServerUtility server,
    //        HttpPostedFile postedFile,
    //        ref string savedFilename,
    //        ref string savedPath,
    //        bool IsNeedProcessFileName,
    //        ref string oppositePath)
    //    {
    //        return SaveUploadSpecialFile(type, filename, server, postedFile, ref savedFilename, ref savedPath, IsNeedProcessFileName, Vela3Configuration.DownLoadFilesFolder, ref oppositePath);
    //    }

    //    /// <summary>
    //    /// 保存上传的摸板文件
    //    /// </summary>
    //    /// <param name="type">上传文件相关的业务类型</param>
    //    /// <param name="filename">文件名</param>
    //    /// <param name="server">HttpServerUtility</param>
    //    /// <param name="postedFile">HttpPostedFile</param>
    //    /// <param name="savedFilename">上传后的文件名</param>
    //    /// <param name="savedPath">上传后的路径</param>
    //    /// <param name="IsNeedProcessFileName">是否需要处理文件名：True-不需要对该文件名是否唯一得处理；False-需要对该文件名唯一处理</param>
    //    /// <param name="TypeDir">如果是临时文件：TypeDir="Temp"；临时文件：TypeDir="Temporary"；如果是下载文件：TypeDir="DownLoadFiles"，如果是摸板文件：TypeDir="Templet"</param>
    //    /// <returns>保存成功/失败</returns>
    //    public static bool SaveUploadTempletFile(
    //        UploadFileType type,
    //        string filename,
    //        HttpServerUtility server,
    //        HttpPostedFile postedFile,
    //        ref string savedFilename,
    //        ref string savedPath,
    //        bool IsNeedProcessFileName,
    //        ref string oppositePath)
    //    {
    //        return SaveUploadSpecialFile(type, filename, server, postedFile, ref savedFilename, ref savedPath, IsNeedProcessFileName, Vela3Configuration.TempletFolder, ref oppositePath);
    //    }

    //    /// <summary>
    //    /// 保存特殊上传的文件
    //    /// </summary>
    //    /// <param name="type">上传文件相关的业务类型</param>
    //    /// <param name="filename">文件名</param>
    //    /// <param name="server">HttpServerUtility</param>
    //    /// <param name="postedFile">HttpPostedFile</param>
    //    /// <param name="savedFilename">上传后的文件名</param>
    //    /// <param name="savedPath">上传后的路径</param>
    //    /// <param name="IsNeedProcessFileName">是否需要处理文件名：True-不需要对该文件名是否唯一得处理；False-需要对该文件名唯一处理</param>
    //    /// <param name="TypeDir">如果是临时文件：TypeDir="Temp"；临时文件：TypeDir="Temporary"；如果是下载文件：TypeDir="DownLoadFiles"，如果是摸板文件：TypeDir="Templet"</param>
    //    /// <param name="oppositePath">存放到数据表中的相对路径</param>
    //    /// <returns>保存成功/失败</returns>
    //    private static bool SaveUploadSpecialFile(
    //        UploadFileType type,
    //        string filename,
    //        HttpServerUtility server,
    //        HttpPostedFile postedFile,
    //        ref string savedFilename,
    //        ref string savedPath,
    //        bool IsNeedProcessFileName,
    //        string TypeDir,
    //        ref string oppositePath)
    //    {
            
    //        WindowsImpersonationContext context = NetworkImpersonateUser();   

    //        //step1:构造唯一文件名
    //        //savedFilename = "";
    //        string singletonFilename;

    //        if (IsNeedProcessFileName)
    //        {
    //            singletonFilename = MakeSingletonUploadFilename(filename);
    //        }
    //        else
    //        {
    //            singletonFilename = filename;
    //        }
    //        if (singletonFilename.Length == 0)
    //            return false;

    //        //step2:从配置文件中取得上传文件路径，由于该路径是跨服务器的，所以暂时不用server.MapPath
    //        //string dir = server.MapPath("~") + Configuration.UploadFolder;
    //        //if (TypeDir.Length > 0 && (!TypeDir.EndsWith(@"\") && !TypeDir.EndsWith(@"/")))
    //        //{
    //        //    TypeDir += @"\";
    //        //}

    //        string dir = String.Empty;
    //        if (Vela3Configuration.UploadServerIP.Length > 0
    //            && (!Vela3Configuration.UploadServerIP.EndsWith(@"\") && !Vela3Configuration.UploadServerIP.EndsWith(@"/"))
    //            && (!TypeDir.StartsWith(@"\") && !TypeDir.StartsWith(@"/"))
    //            )
    //        {
    //            dir = Vela3Configuration.UploadServerIP + @"\" + TypeDir;
    //        }
    //        else
    //        {
    //            dir = Vela3Configuration.UploadServerIP + TypeDir;
    //        }

    //        oppositePath = "";

    //        //step3:创建最终存放文件的文件夹名称
    //        switch (type)
    //        {
    //            case UploadFileType.Document:
    //                dir = Vela3Configuration.UploadServerIP + Vela3Configuration.Document;
    //                break;
    //            default:
    //                break;
    //        }

    //        //step4:建立“YYYYMM”的文件夹，目标是对文件按月份进行分类
    //        if (!TypeDir.Equals(Vela3Configuration.TempletFolder))   //摸板文件由于数量较少，所以不要按月份分类
    //        {
    //            if (dir.EndsWith(@"\") || dir.EndsWith("/"))
    //            {
    //                dir += DateTime.Now.ToString("yyyyMM");
    //            }
    //            else
    //            {
    //                dir += @"\" + DateTime.Now.ToString("yyyyMM");
    //            }
    //        }

    //        oppositePath = dir.Replace(Vela3Configuration.UploadServerIP, "");

    //        if (oppositePath.IndexOf(Vela3Configuration.Document) >= 0)
    //        {
    //            oppositePath = oppositePath.Replace(Vela3Configuration.Document, "");
    //        }

    //        if (oppositePath.IndexOf(Vela3Configuration.UploadFolder) >= 0)
    //        {
    //            oppositePath = oppositePath.Replace(Vela3Configuration.UploadFolder, "");
    //        }

    //        if (!Directory.Exists(dir))
    //        {
    //            Directory.CreateDirectory(dir);
    //        }

    //        //step5:保存上传文件
    //        try
    //        {
    //            if (dir.EndsWith(@"\") || dir.EndsWith(@"/"))
    //            {
    //                postedFile.SaveAs(dir + singletonFilename);
    //            }
    //            else
    //            {
    //                postedFile.SaveAs(dir + @"\" + singletonFilename);
    //            }
    //        }
    //        catch
    //        {
    //            return false;
    //        }

    //        savedFilename = singletonFilename;
    //        savedPath = dir;

    //       context.Undo();   

    //        return true;

    //    }

    //    /// <summary>
    //    /// 保存特殊上传的文件
    //    /// </summary>
    //    /// <param name="type">上传文件相关的业务类型</param>
    //    /// <param name="filename">文件名</param>
    //    /// <param name="server">HttpServerUtility</param>
    //    /// <param name="postedFile">HttpPostedFile</param>
    //    /// <param name="savedFilename">上传后的文件名</param>
    //    /// <param name="savedPath">上传后的路径</param>
    //    /// <param name="IsNeedProcessFileName">是否需要处理文件名：True-不需要对该文件名是否唯一得处理；False-需要对该文件名唯一处理</param>
    //    /// <param name="TypeDir">如果是临时文件：TypeDir="Temp"；临时文件：TypeDir="Temporary"；如果是下载文件：TypeDir="DownLoadFiles"，如果是摸板文件：TypeDir="Templet"</param>
    //    /// <param name="oppositePath">存放到数据表中的相对路径</param>
    //    /// <returns>保存成功/失败</returns>
    //    public static bool SaveUploadSpecialFile_ForMasterData(
    //        UploadFileType type,
    //        string filename,
    //        HttpPostedFile postedFile,
    //        ref string savedFilename,
    //        ref string savedPath)
    //    {
    //        WindowsImpersonationContext context = NetworkImpersonateUser();

    //        //step1:构造唯一文件名
    //        savedFilename = "";
    //        string singletonFilename = filename;

    //        if (singletonFilename.Length == 0)
    //            return false;

    //        //step2:从配置文件中取得上传文件路径，由于该路径是跨服务器的，所以暂时不用server.MapPath
    //        string dir = String.Empty;
    //        if (Vela3Configuration.UploadServerIP.Length > 0
    //            && (!Vela3Configuration.UploadServerIP.EndsWith(@"\") && !Vela3Configuration.UploadServerIP.EndsWith(@"/")))
    //        {
    //            dir = Vela3Configuration.UploadServerIP + @"\" + Vela3Configuration.MasterDataFolder;
    //        }
    //        else
    //        {
    //            dir = Vela3Configuration.UploadServerIP + Vela3Configuration.MasterDataFolder;
    //        }

    //        if (!Directory.Exists(dir))
    //        {
    //            Directory.CreateDirectory(dir);
    //        }

    //        string strFilenameWithPath = String.Empty;
    //        if (dir.EndsWith(@"\") || dir.EndsWith(@"/"))
    //        {
    //            strFilenameWithPath = dir + singletonFilename;
    //        }
    //        else
    //        {
    //            strFilenameWithPath = dir + @"\" + singletonFilename;
    //        }

    //        //step5:保存上传文件
    //        try
    //        {
    //            if (File.Exists(strFilenameWithPath))
    //            {
    //                File.Delete(strFilenameWithPath);
    //            }

    //            postedFile.SaveAs(strFilenameWithPath);
    //        }
    //        catch
    //        {
    //            return false;
    //        }

    //        savedFilename = singletonFilename;
    //        savedPath = dir;

    //        context.Undo();

    //        return true;

    //    }

    //    public enum UploadFileType
    //    {
    //        Document = 1,
    //        Other = -1
    //    }

    //    /// <summary>
    //    /// 构造唯一不重复文件名，以当前时间戳为文件名前部
    //    /// </summary>
    //    /// <param name="filename">原始文件名</param>
    //    /// <returns>构造后文件名</returns>
    //    public static string MakeSingletonUploadFilename(string filename)
    //    {
    //        if (filename.Trim().Length == 0)
    //            return "";

    //        string name = "";
    //        name = Path.GetFileName(filename);
    //        name = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff_") + name;
    //        return name;
    //    }

    //    /// <summary>
    //    /// 检查上传的文件扩展名是否合法
    //    /// </summary>
    //    /// <param name="filename">文件名</param>
    //    /// <returns></returns>
    //    public static bool IsValidUploadFilenameExtetion(string filename)
    //    {
    //        string ext = Path.GetExtension(filename).ToLower().Remove(0, 1);
    //        return Vela3Configuration.AllowUploadFormat.Contains(ext);
    //    }

    //    public static string SharedPathCombine(params string[] folders)
    //    {
    //        if (folders == null || folders.Length == 0) return string.Empty;
    //        StringBuilder sb = new StringBuilder();
    //        foreach (string folder in folders)
    //        {
    //            if (string.IsNullOrEmpty(folder)) continue;
    //            if (sb.Length == 0)
    //            {
    //                sb.Append(folder);
    //            }
    //            else
    //            {
    //                char lastChar = sb[sb.Length - 1];
    //                char firstChar = folder[0];
    //                if (lastChar == '\\' && firstChar == '\\')
    //                {
    //                    sb.Append(folder.TrimStart('\\'));
    //                }
    //                else if (lastChar != '\\' && firstChar == '\\')
    //                {
    //                    sb.Append(folder);
    //                }
    //                else if (lastChar == '\\' && firstChar != '\\')
    //                {
    //                    sb.Append(folder);
    //                }
    //                else
    //                {
    //                    sb.Append('\\').Append(folder);
    //                }
    //            }
    //        }
    //        return sb.ToString();
    //    }

    //    /// <summary>
    //    /// 登陆远程主机，用于文件上传
    //    /// </summary>
    //    /// <returns></returns>
    //    public static WindowsImpersonationContext NetworkImpersonateUser()
    //    {
    //        string strDomain = "";
    //        string strLogin = "";
    //        string strPwd = "";

    //        strDomain = Vela3Configuration.Domain;
    //        strLogin = Vela3Configuration.Login;
    //        strPwd = Vela3Configuration.Pwd;

    //        //if (ConfigurationManager.AppSettings["Domain"] != null)
    //        //    strDomain = ConfigurationManager.AppSettings["Domain"].ToString();

    //        //if (ConfigurationManager.AppSettings["Login"] != null)
    //        //    strLogin = ConfigurationManager.AppSettings["Login"].ToString();

    //        //if (ConfigurationManager.AppSettings["Pwd"] != null)
    //        //    strPwd = ConfigurationManager.AppSettings["Pwd"].ToString();

    //        //本地调式时，用：LOGON32_LOGON_NEW_CREDENTIALS
    //        if (Vela3Configuration.IsLocalFileUpload)
    //        {
    //            //下面此句为本地调用
    //            WindowsImpersonationContext context = NetworkSecurity.ImpersonateUser(strDomain, strLogin, strPwd, LogonType.LOGON32_LOGON_NEW_CREDENTIALS, LogonProvider.LOGON32_PROVIDER_DEFAULT);
    //            return context;

    //        }
    //        else
    //        {
    //            //下面此句为远程调用
    //            WindowsImpersonationContext context = NetworkSecurity.ImpersonateUser(strDomain, strLogin, strPwd, LogonType.LOGON32_LOGON_INTERACTIVE, LogonProvider.LOGON32_PROVIDER_DEFAULT);
    //            return context;

    //        }

    //    }

    //    /// <summary>
    //    /// 判断服务器上的文件是否存在
    //    /// </summary>
    //    /// <param name="typeDir">类型文件夹</param>
    //    /// <param name="fileName">文件名</param>
    //    /// <returns></returns>
    //    public static bool IsExistFile(string typeDir, string fileName, out string filepath)
    //    {
    //        bool result = false;

    //        WindowsImpersonationContext context = NetworkImpersonateUser();

    //        string dir = String.Empty;
    //        if (Vela3Configuration.UploadServerIP.Length > 0
    //            && (!Vela3Configuration.UploadServerIP.EndsWith(@"\") && !Vela3Configuration.UploadServerIP.EndsWith(@"/"))
    //            && (!typeDir.StartsWith(@"\") && !typeDir.StartsWith(@"/"))
    //            )
    //        {
    //            dir = Vela3Configuration.UploadServerIP + @"\" + typeDir;
    //        }
    //        else
    //        {
    //            dir = Vela3Configuration.UploadServerIP + typeDir;
    //        }
    //        if (dir.EndsWith(@"\") || dir.EndsWith("/"))
    //        {
    //            dir += fileName;
    //        }
    //        else
    //        {
    //            dir += @"\" + fileName;
    //        }

    //        result = File.Exists(dir);

    //        context.Undo();

    //        filepath = dir;
    //        return result;
    //    }

    //    #region 显示信息
    //    public static void MessageBox(Page page, string message)
    //    {
    //        ScriptManager.RegisterStartupScript(page, typeof(Page), DateTime.Now.ToString(), string.Format("alert('{0}');", message), true);
    //    }
    //    public static void MessageBox(Page page, string message, bool isClose)
    //    {
    //        if (isClose)
    //        {
    //            ScriptManager.RegisterStartupScript(page, typeof(Page), DateTime.Now.ToString(), string.Format("alert('{0}');window.close();", message), true);
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(page, typeof(Page), DateTime.Now.ToString(), string.Format("alert('{0}');", message), true);
    //        }
    //    }
    //    #endregion

    //    #region 注册脚本
    //    public static void RegisterScript(Page page, string script)
    //    {
    //        ScriptManager.RegisterStartupScript(page, typeof(Page), Guid.NewGuid().ToString(), string.Format("{0}", script), true);
    //    }
    //    #endregion

    //    #region 分页方法
    //    /// <summary>
    //    /// 分页方法
    //    /// </summary>
    //    /// <param name="FromJoin">表名</param>
    //    /// <param name="OrderBy">排序</param>
    //    /// <param name="PageIndex">当前页号</param>
    //    /// <param name="PageSize">页显示的行数</param>
    //    /// <param name="RecordCount">记录总数</param>
    //    /// <param name="SelectColumn">查询列</param>
    //    /// <param name="SqlCond">查询条件</param>
    //    /// <returns></returns>
    //    public static DataSet SelectDataForSearchPage(String FromJoin, String OrderBy, Object PageIndex, Object PageSize, ref Object RecordCount, String SelectColumn, String SqlCond)
    //    {
    //        return SelectDataForSearchPage(FromJoin, OrderBy, PageIndex, PageSize, ref RecordCount, SelectColumn, SqlCond, false);
    //    }

    //    public static DataSet SelectDataForSearchPage(String FromJoin, String OrderBy, Object PageIndex, Object PageSize, ref Object RecordCount, String SelectColumn, String SqlCond, Boolean IsLast)
    //    {
    //        return DatabaseManager.usp3_SelectDataForSearchPage(FromJoin, IsLast, OrderBy, PageIndex, PageSize, ref RecordCount, SelectColumn, SqlCond);
    //    }

    //    public static DataSet SelectFullData(String SelectColumn, String FromJoin, String SqlCond, String OrderBy)
    //    {
    //        object RecordCount = 0;
    //        return DatabaseManager.usp3_SelectDataForSearchPage(FromJoin, false, OrderBy, -1, -1, ref RecordCount, SelectColumn, SqlCond);
    //    }
    //    #endregion

    //    #region 欧元人民币转换

    //    public static object GetParameterValueByKey(object key)
    //    {
    //        Hashtable Parameters = CacheManager.Get("SYS_PARAMETERS") as Hashtable;
    //        if (Parameters == null)
    //        {
    //            Parameters = new Hashtable();
    //            CacheManager.Insert("SYS_PARAMETERS", Parameters);
    //        }

    //        if (!Parameters.Contains(key))
    //        {
    //            DataSet dsParam = ProcedureManager.usp3_SelectParameterValueByKey(Convert.ToString(key).Replace("'", "''"));

    //            if (dsParam.Tables[0].Rows.Count > 0 && dsParam.Tables[0].Rows[0]["PARAMETER_VALUE"] != DBNull.Value)
    //                Parameters.Add(key, dsParam.Tables[0].Rows[0]["PARAMETER_VALUE"]);
    //            else
    //                throw new OverflowException("Invalid parameter");
    //        }
    //        return Parameters[key];
    //    }

    //    /// <summary>
    //    /// 人民币转换欧元
    //    /// </summary>
    //    /// <param name="rmb"></param>
    //    /// <returns></returns>
    //    public static Decimal RmbToEur(object rmb)
    //    {
    //        try
    //        {
    //            Decimal rate = Convert.ToDecimal(GetParameterValueByKey("EXCHANGE_RATE"));
    //            Decimal currency = Convert.ToDecimal(rmb);
    //            return currency / rate;
    //        }
    //        catch (OverflowException e0)
    //        {
    //            throw e0;
    //        }
    //        catch (Exception e1)
    //        {
    //            throw e1;
    //        }
    //    }

    //    /// <summary>
    //    /// 将人民币转化成欧元，并且格式化
    //    /// </summary>
    //    /// <param name="rmb">人民币金额</param>
    //    /// <returns></returns>
    //    public static string FormatNumberToEur(object rmb)
    //    {
    //        try
    //        {
    //            return string.Format("€{0}", FormatNumber(string.Format("{0}", RmbToEur(rmb)), 2));
    //        }
    //        catch
    //        {
    //            return "";
    //        }
    //    }

    //    /// <summary>
    //    /// 将金额格式化成欧元
    //    /// </summary>
    //    /// <param name="num">欧元金额</param>
    //    /// <param name="digit">小数位数</param>
    //    /// <returns></returns>
    //    public static string FormatNumberToEur(string num, int digit)
    //    {
    //        try
    //        {
    //            return string.Format("€{0}", FormatNumber(num, digit));
    //        }
    //        catch
    //        {
    //            return "";
    //        }
    //    }

    //    /// <summary>
    //    /// 格式化数字
    //    /// </summary>
    //    /// <param name="num">要格式化的数字</param>
    //    /// <param name="digit">小数位数</param>
    //    /// <returns></returns>
    //    public static string FormatNumber(string num, int digit)
    //    {
    //        if (num != "" && num != "&nbsp;")
    //        {
    //            decimal n = decimal.Parse(num);
    //            num = n.ToString("N" + digit);

    //            int j = num.IndexOf(".");

    //            if (j > 0)
    //            {
    //                string fraction = num.Remove(0, j + 1);
    //                num = num.TrimEnd('.');
    //            }
    //        }
    //        return num;
    //    }

    //    #endregion

    //    /// <summary>
    //    /// 格式化数字为货币（￥）
    //    /// </summary>
    //    /// <param name="num"></param>
    //    /// <param name="digit"></param>
    //    /// <returns></returns>
    //    public static string FormatNumberToMoney(string num, int digit)
    //    {
    //        if (num != "" && num != "&nbsp;")
    //        {
    //            decimal n = decimal.Parse(num);
    //            num = n.ToString("C" + digit);

    //            int j = num.IndexOf(".");

    //            if (j > 0)
    //            {
    //                num = num.TrimEnd('.');
    //            }
    //        }
    //        num = num.Replace("$", "￥");
    //        return num;
    //    }

    //    static public string StringSeperator
    //    {
    //        get { return "@#$"; }
    //    }

    //    public static DateTime CalAddWorkDay(DateTime startDay, int addDays)
    //    {
    //        object result = DateTime.Now;
    //        DatabaseManager.usp3_DateAdd(addDays, ref result, startDay);

    //        return (DateTime)result;
    //    }

    //    /// <summary>
    //    /// 把AtosGridView导出到Excel
    //    /// </summary>
    //    /// <param name="agv">AtosGridView</param>
    //    /// <param name="excelFileName">导出的excel文件名称</param>
    //    public static void ExportExcel(UserControl uc, Atos.Controls.AtosGridView agv, String excelFileName)
    //    {
    //        ExportExcel(agv, excelFileName);
    //        //string s = @"<style> .text { mso-number-format:\@; } </style> ";
    //        //uc.Response.Clear();
    //        //uc.Response.Charset = "GB2312";
    //        //uc.Response.AppendHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(excelFileName, Encoding.UTF8) + ".xls");
    //        //uc.Response.ContentType = "application/vnd.ms-excel";
    //        //uc.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
    //        //uc.Response.Write("<meta http-equiv='Content-Type' content='application/vnd.ms-excel; Charset=GB2312'>");
    //        //StringBuilder sb = new StringBuilder();
    //        //StringWriter writer = new StringWriter(sb);
    //        //HtmlTextWriter writer2 = new HtmlTextWriter(writer);
    //        //agv.RenderControl(writer2);
    //        //uc.Response.Write(s);
    //        ////BasePage page = agv.Page as BasePage;
    //        ////if (page.CurrentSessionInfo.UserCultureInfo.Equals(new CultureInfo("zh-CN")))
    //        ////{
    //        ////   // LgCExchangAdapter.LgcExchang(page, sb, 1, 1);
    //        ////}
    //        ////else
    //        ////{
    //        ////   // LgCExchangAdapter.LgcExchang(page, sb, 1, 2);
    //        ////}

    //        //uc.Response.Write(Encoding.GetEncoding("GB2312").GetBytes(sb.ToString()));
    //        ////uc.Response.Write(sb.ToString());
    //        //uc.Response.End();
    //        //ApplicationInstance.CompleteRequest();

    //        //HttpContext.Current.ApplicationInstance.CompleteRequest();
    //    }

        /// <summary>
        /// 把AtosGridView导出到Excel
        /// </summary>
        /// <param name="agv">AtosGridView</param>
        /// <param name="excelFileName">导出的excel文件名称</param>
        public static void ExportExcel(GridView agv, String excelFileName)
        {
            //string s = @"<style> .text { mso-number-format:\@; } </style> ";
            HttpResponse resp;
            resp = agv.Page.Response;
            resp.Clear();
            resp.Charset = "UTF-8";
            resp.AppendHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(excelFileName, Encoding.UTF8) + ".xls");
            resp.ContentType = "application/vnd.ms-excel";
            resp.ContentEncoding = Encoding.UTF8;
            //resp.Write("<meta http-equiv='Content-Type' content='application/vnd.ms-excel; Charset=GB2312'>");
            //StringBuilder sb = new StringBuilder();
            GC.Collect();
            StringWriter strWriter = new StringWriter();
            HtmlTextWriter txtWriter = new HtmlTextWriter(strWriter);
            agv.RenderControl(txtWriter);
            //resp.Write(s);
            resp.OutputStream.WriteByte(0xef);
            resp.OutputStream.WriteByte(0xbb);
            resp.OutputStream.WriteByte(0xbf);
            resp.Write(strWriter.ToString());
            resp.End();
        }

    //    public static void ExportExcel2(Atos.Controls.AtosGridView agv, String excelFileName)
    //    {
    //        //string s = @"<style> .text { mso-number-format:\@; } </style> ";
    //        HttpResponse resp;
    //        resp = agv.Page.Response;
    //        resp.Clear();
    //        resp.Charset = "UTF-8";
    //        resp.AppendHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(excelFileName, Encoding.UTF8) + ".xls");
    //        resp.ContentType = "application/vnd.ms-excel";
    //        resp.ContentEncoding = Encoding.UTF8;
    //        //resp.Write("<meta http-equiv='Content-Type' content='application/vnd.ms-excel; Charset=GB2312'>");
    //        //StringBuilder sb = new StringBuilder();
    //        GC.Collect();
    //        StringWriter strWriter = new StringWriter();
    //        HtmlTextWriter txtWriter = new HtmlTextWriter(strWriter);
    //        agv.RenderControl(txtWriter);
    //        //resp.Write(s);
    //        resp.OutputStream.WriteByte(0xef);
    //        resp.OutputStream.WriteByte(0xbb);
    //        resp.OutputStream.WriteByte(0xbf);
    //        resp.Write(strWriter.ToString().Replace("<tr","<tr style='Height:20px;' "));
    //        resp.End();
    //    }

    //    /// <summary> 
    //    /// 将DataTable中的数据导出到指定的excel文件中 
    //    /// </summary> 
    //    /// <param name="page">web页面对象</param> 
    //    /// <param name="tab">包含被导出数据的DataTable对象</param> 
    //    /// <param name="filename">excel文件的名称</param> 
    //    public static void ExportExcelDataTable(Page page, DataTable tab, string filename)
    //    {
    //        ExportExcelDataTable(page, tab, filename, -1, -1);
    //    }

    //    /// <summary> 
    //    /// 将DataTable中的数据导出到指定的excel文件中 
    //    /// </summary> 
    //    /// <param name="page">web页面对象</param> 
    //    /// <param name="tab">包含被导出数据的DataTable对象</param> 
    //    /// <param name="filename">excel文件的名称</param> 
    //    public static void ExportExcelDataTable(Page page, DataTable tab, string filename, int height, int fontSize)
    //    {
    //        string s = @"<style> .text { mso-number-format:\@; } </style> ";
    //        HttpResponse response = page.Response;
    //        DataGrid grid = new DataGrid();
    //        grid.DataSource = tab.DefaultView;
    //        grid.AllowPaging = false;
    //        grid.HeaderStyle.BackColor = System.Drawing.Color.Green;
    //        grid.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
    //        grid.HeaderStyle.Font.Bold = true;
    //        grid.ItemStyle.Wrap = false;
    //        if (height != -1)
    //        {
    //            //grid.ItemStyle.Height = Unit.Pixel(height);
    //            grid.HeaderStyle.ForeColor = System.Drawing.Color.White;
    //        }
    //        if (fontSize != -1)
    //        {
    //            grid.ItemStyle.Font.Size = FontUnit.Parse(fontSize.ToString());
    //            grid.ItemStyle.Font.Name = "Arial";
    //            grid.HeaderStyle.Font.Name = "Arial";
    //            grid.HeaderStyle.Font.Size = FontUnit.Parse(fontSize.ToString());
    //        }
    //        grid.DataBind();
    //        response.AppendHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode(filename, Encoding.GetEncoding("gb2312")));
    //        response.ContentEncoding = Encoding.GetEncoding("gb2312");
    //        response.ContentType = "application/ms-excel";
    //        response.Write("<meta http-equiv='Content-Type' content='application/vnd.ms-excel; Charset=GB2312'");
    //        StringBuilder sb = new StringBuilder();
    //        System.IO.StringWriter writer = new System.IO.StringWriter(sb);
    //        HtmlTextWriter writer2 = new HtmlTextWriter(writer);
    //        grid.RenderControl(writer2);
    //        response.Write(s);

    //        response.Write(sb.ToString());
    //        response.End();
    //    }

    //    public static void ExportExcel(Telerik.Web.UI.RadGrid gv, String excelFileName, params int[] hideIndex)
    //    {
    //        gv.ExportSettings.ExportOnlyData = true;
    //        gv.ExportSettings.IgnorePaging = true;
    //        gv.ExportSettings.FileName = excelFileName;

    //        foreach (int index in hideIndex)
    //        {
    //            if (index < gv.Columns.Count)
    //            {
    //                gv.Columns[index].Visible = false;
    //            }
    //        }

    //        gv.MasterTableView.ExportToExcel();
    //    }

    //    #region 转换
    //    /// <summary>
    //    /// 转换Guid
    //    /// </summary>
    //    /// <param name="value"></param>
    //    /// <returns></returns>
    //    public static Guid? ParseGuid(string value)
    //    {
    //        //add by fjshen
    //        if (string.IsNullOrEmpty(value))
    //        {
    //            return null;
    //        }
    //        try
    //        {
    //            return new Guid(value);
    //        }
    //        catch
    //        {
    //            return null;
    //        }
    //    }

    //    /// <summary>
    //    /// 转换时间
    //    /// </summary>
    //    /// <param name="value"></param>
    //    /// <returns></returns>
    //    public static DateTime? ParseDateTime(string value)
    //    {
    //        //add by fjshen
    //        if (string.IsNullOrEmpty(value))
    //            return null;

    //        try
    //        {
    //            return DateTime.Parse(value);
    //        }
    //        catch
    //        {
    //            return null;
    //        }
    //    }

    //    /// <summary>
    //    /// 转换整型
    //    /// </summary>
    //    /// <param name="value"></param>
    //    /// <returns></returns>
    //    public static int? ParseInt(string value)
    //    {
    //        //add by fjshen
    //        int iValue;
    //        if (int.TryParse(value, out iValue))
    //        {
    //            return iValue;
    //        }
    //        return null;
    //    }

    //    /// <summary>
    //    /// 转换整型
    //    /// </summary>
    //    /// <param name="value"></param>
    //    /// <returns></returns>
    //    public static double? ParseDouble(string value)
    //    {
    //        //add by fjshen
    //        double dValue;
    //        if (double.TryParse(value, out dValue))
    //        {
    //            return dValue;
    //        }
    //        return null;
    //    }

    //    /// <summary>
    //    /// 空对象处理
    //    /// </summary>
    //    /// <param name="obj"></param>
    //    /// <returns></returns>
    //    public static string ConvertNullToEmpty(Object obj)
    //    {
    //        //add by fjshen
    //        if (obj == null)
    //        {
    //            return String.Empty;
    //        }
    //        else
    //        {
    //            return obj.ToString();
    //        }
    //    }

    //    public static bool HasJoin(string value1, string value2, string separator)
    //    {
    //        if (string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(value2))
    //        {
    //            return false;
    //        }
    //        if (separator.Length < 1)
    //        {
    //            return value1 == value2;
    //        }
    //        string[] values = value2.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
    //        foreach (string value in values)
    //        {
    //            if (value1.IndexOf(separator + value + separator) > -1)
    //            {
    //                return true;
    //            }
    //        }

    //        return false; ;
    //    }
    //    #endregion

    //    #region cookie
    //    public static void SetCookie(string name, string value)
    //    {
    //        HttpContext.Current.Request.Cookies.Remove(name);
    //        HttpCookie cookie = HttpContext.Current.Response.Cookies[name];
    //        while (null == cookie)
    //        {
    //            cookie = new HttpCookie(name);
    //        }
    //        cookie.Name = name;
    //        cookie.Value = HttpUtility.UrlEncode(value);

    //        cookie.Expires = DateTime.Now.AddDays(1);
    //        try
    //        {
    //            HttpContext.Current.Response.SetCookie(cookie);
    //        }
    //        catch
    //        {
    //            HttpContext.Current.Response.AppendCookie(cookie);
    //        }
    //    }


    //    public static string GetCookie(string name)
    //    {
    //        string returnValue = string.Empty;
    //        try
    //        {
    //            foreach (string keyName in HttpContext.Current.Request.Cookies.AllKeys)
    //            {
    //                if (keyName == name)
    //                {
    //                    returnValue = HttpContext.Current.Request.Cookies[name].Value;
    //                    break;
    //                }
    //            }
    //        }
    //        catch
    //        {
    //            returnValue = string.Empty;
    //        }

    //        return HttpUtility.UrlDecode(returnValue);
    //    }
    //    #endregion

    //    /// <summary>
    //    /// 用于DAC开票确认后的消息推送
    //    /// </summary>
    //    /// <param name="englishnames">英文名列表</param>
    //    public static void PushMessage(string[] englishnames, string projectcd)
    //    {
    //        System.Threading.ThreadPool.QueueUserWorkItem(o => {
    //            foreach (var englishname in englishnames)
    //            {
    //                PushMessage(englishname, projectcd);
    //            }
    //        });
    //    }

    //    public static string PushMessage(string englishname, string projectcd)
    //    {
    //        string result = String.Empty;
     
    //        var user = Vela3.Database.Service.USER_INFOManager.SelectByCondition(string.Format("ENGLISH_NAME='{0}'", englishname)).Tables[0];
    //        if (user != null && user.Rows.Count > 0)
    //        {
    //            string url = Vela3Configuration.PushUrl;
    //            string authencationKey = Vela3Configuration.PushKey;
    //            string pushmode = Vela3Configuration.PushModel;
    //            string msg = "您有待确认的开票记录，请注意查看";
    //            string csl = user.Rows[0]["ADAccount"].ToString();

    //            PushMessageModel postMessage = new PushMessageModel(csl, msg, pushmode);
    //            PushContent content = new PushContent();
    //            content.KeyNumber = projectcd;
    //            postMessage.CustomContent = content.getJsonString();

    //            string postStr = postMessage.getJsonString();

    //            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
    //            request.Method = "POST";
                
    //            request.Headers.Add("AuthencationKey", authencationKey);
    //            request.ContentType = "application/json";

    //            byte[] postBytes = Encoding.UTF8.GetBytes(postStr);
    //            request.ContentLength = postBytes.Length;

    //            using (Stream postStream = request.GetRequestStream())
    //            {
    //                postStream.Write(postBytes, 0, postBytes.Length);
    //            }

    //            try
    //            {
    //                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
    //                {
    //                    using (Stream stream = response.GetResponseStream())
    //                    {
    //                        StreamReader reader = new StreamReader(stream, Encoding.UTF8);
    //                        result = reader.ReadToEnd();
    //                    }
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                result = "0";
    //            }
    //        }
    //        return result;
    //    }
    }

    //public class PushMessageModel
    //{
    //    public string CSL { get; set; }

    //    public string Message { get; set; }

    //    public string PushModel { get; set; }

    //    public int Type { get; set; }

    //    public string Tag { get; set; }

    //    public int MessageType { get; set; }


    //    public string CustomContent { get; set; }

    //    public PushMessageModel(string csl, string message, string pushModel)
    //    {
    //        this.CSL = csl;
    //        this.Message = message;
    //        this.PushModel = pushModel;

    //        this.Type = 0;
    //        this.Tag = String.Empty;
    //        this.MessageType = 0;
    //    }

    //    public string getJsonString()
    //    {
    //        JavaScriptSerializer serializer = new JavaScriptSerializer();
    //        return serializer.Serialize(this);
    //    }
    //}

    ///// <summary>
    ///// 内容
    ///// </summary>
    //public class PushContent
    //{
    //    public string KeyNumber { get; set; }

    //    public string getJsonString()
    //    {

    //        JavaScriptSerializer serializer = new JavaScriptSerializer();
    //        return serializer.Serialize(this);
    //    }
    //}
}
