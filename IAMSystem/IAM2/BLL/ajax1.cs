using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAMEntityDAL;
using BaseDataAccess;
using System.Web.Script.Serialization;
using System.Text;

namespace IAM.BLL
{
    public partial class Ajax
    {
        /// <summary>
        /// 用户转移功能
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string UserTransfer(HttpContext context)
        {
            Guid oldid = new Guid(context.Request["Oldvalue"]);
            string newvalue = context.Request["newvalue"];
            string system = context.Request["system"];
            var enti = new AccountMapingDAL().GetOne(oldid);
            try
            {                
                if (new AccountMapingDAL().UserTransfer(oldid, newvalue,(Unitity.SystemType)Enum.Parse(typeof(Unitity.SystemType),system,true)) > 0)
                {
                    string sql = "";
                    
                    switch ((Unitity.SystemType)Enum.Parse(typeof(Unitity.SystemType), system, true))
                    {
                        case Unitity.SystemType.AD: //sql = "UPDATE dbo.AD_UserInfo SET Id='" + newvalue + "' WHERE Accountname='" + enti.zhanghao + "' ";                             
                            break;
                        case Unitity.SystemType.ADComputer://不需要
                            break;
                        //case Unitity.SystemType.HEC: sql = string.Format(@"UPDATE dbo.HEC_User SET USER_CODE='" + newvalue + "' WHERE User_CD='" + enti.zhanghao + "' UPDATE dbo.HEC_User_Info SET EMPLOYEE_CODE='{0}' WHERE USER_NAME='{1}'", newvalue, enti.zhanghao); break;
                        case Unitity.SystemType.HR: //不需要
                            break;
                        case Unitity.SystemType.TC: //不需要
                            break;
                        case Unitity.SystemType.SAP: //不需要
                            break;
                    }
                    if (!string.IsNullOrEmpty(sql))
                    {
                        int count = 0;
                        using (IAMEntities db = new IAMEntities())
                        {
                            count=db.ExecuteStoreCommand(sql);
                            db.SaveChanges();
                        }
                        if (count > 0)
                        {

                            new LogDAL().AdduserActionLog(((UserRole)System.Web.HttpContext.Current.Session["userinfo"]).adname, system + "账号转移", string.Format("将账号{0}由原来员工{1}转移给员工{2}", enti.zhanghao, enti.gonghao, newvalue));
                        }
                    }

                    return "ok";
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                string sql = string.Format("UPDATE dbo.AccountMaping SET gonghao='{0}' where ID='{1}'", enti.gonghao, oldid);
                using (IAMEntities db = new IAMEntities())
                {
                    db.ExecuteStoreCommand(sql);
                }
                new LogDAL().AddsysErrorLog("账号转移时报错:"+ex.ToString());
                return "error:" + ex.ToString();
            }
        }

        public string CreateDefaultWorkGroup(HttpContext context)
        {
            AD_DefaultWorkGroupDAL _services = new AD_DefaultWorkGroupDAL();
            string name = context.Request["Name"];
            string Des = context.Request["Des"];
            string Memo = context.Request["Memo"];
            string type = context.Request["atype"];
            string id = context.Request["id"];
            string isdelete = context.Request["isdelete"];
            AD_DefaultWorkGroup entity = new AD_DefaultWorkGroup();
            entity.NAME = name;
            entity.Memo = Memo;
            entity.DESCRIPTION = Des;
            entity.IsDelete = isdelete.Equals("qiyong") ? false : true;
            try
            {
                if (type == "add")
                {
                    entity.Id = Guid.NewGuid();
                    if (_services.Add(entity) > 0)
                    {
                        return "ok";
                    }
                    else
                    {
                        return "error:添加错误请联系管理员";
                    }
                }
                else
                {
                    entity.Id = new Guid(id);
                    if (_services.UpdateAdDefaultWorkGroup(entity) > 0)
                    {
                        return "ok";
                    }
                    else
                    {
                        return "error:更新错误请联系管理员";
                    }
                }
            }
            catch (Exception ex)
            {
                return "error:" + ex.ToString();
            }

        }

        public string DefaultWorkGroupEdit(HttpContext context)
        {
            try
            {
                StringBuilder stb = new StringBuilder();
                string id = context.Request["id"];
                var model = new AD_DefaultWorkGroupDAL().GetOne(new Guid(id));
                if (model == null)
                    model = new AD_DefaultWorkGroup();
                JavaScriptSerializer java = new JavaScriptSerializer();
                java.Serialize(model, stb);
                return stb.ToString();
            }
            catch (Exception ex)
            {
                return "error:" + ex.ToString();
            }
        }
    }
}