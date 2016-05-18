using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAMEntityDAL;
using BaseDataAccess;

namespace IAM.ConflictResolution
{
    public partial class UserConflictResolution : BasePage
    {
        public class conflicttype
        {
            public Guid Id { get; set; }
            public string p2 { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!base.ReturnUserRole.Admin)
                {
                    base.NoRole();
                }

                Bind();

            }
        }

        void Bind()
        {
            try
            {
                string systemtype = dplsystemtype.SelectedValue;
                int state = Convert.ToInt32(dlpsysType.SelectedValue);
                string username = txtUserName.Text.Trim();
                string neirong = txtneirong.Text.Trim();
                int count;
                //var list = new Sys_UserName_ConflictResolutionDAL().ReturnList().Where(item => item.STATE == Convert.ToInt32(dlpsysType.SelectedValue)).ToList();
                //if (!string.IsNullOrEmpty(systemtype))
                //    list = list.Where(item => item.SysType.Trim() == systemtype).ToList();
                //list = list.Where(item => item.CollSysValue.Trim() != "源系统新增账号").ToList();
                var list = new Sys_UserName_ConflictResolutionDAL().ReturnList(systemtype, state, username, neirong, out count, base.PageSize, AspNetPager1.CurrentPageIndex);
                AspNetPager1.RecordCount = count;
                AspNetPager1.PageSize = base.PageSize;
                repeaterUserDeferences.DataSource = list;
                repeaterUserDeferences.DataBind();
                updatepagerhtml();
            }
            catch
            {
                using (IAMEntities db = new IAMEntities())
                {
                    db.ExecuteStoreCommand("UPDATE dbo.Sys_UserName_ConflictResolution SET CollSysValue='' WHERE CollSysValue IS NULL");
                    db.SaveChanges();
                }
                Bind();
            }

        }

        private void updatepagerhtml()
        {
            AspNetPager1.CustomInfoHTML = string.Format("共{0}页,当前第{1}页,共{2}条", AspNetPager1.PageCount, AspNetPager1.CurrentPageIndex, AspNetPager1.RecordCount);
        }

        protected void AspNetPager1_PageChanging(object sender, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            Bind();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Bind();
        }

        List<conflicttype> ReturnCheckedItems()
        {
            List<conflicttype> jj = new List<conflicttype>();
            Repeater fdla = (Repeater)this.Page.Master.FindControl("ContentPlaceHolder1").FindControl("repeaterUserDeferences");

            for (int i = 0; i < fdla.Items.Count; i++)
            {
                System.Web.UI.HtmlControls.HtmlInputCheckBox checkbox = (System.Web.UI.HtmlControls.HtmlInputCheckBox)fdla.Items[i].FindControl("repcheckbox");
                HiddenField hip2 = (HiddenField)fdla.Items[i].FindControl("hiddenp2");
                if (checkbox.Checked == true)
                {
                    conflicttype cf = new conflicttype();
                    cf.Id = new Guid(checkbox.Value);
                    cf.p2 = hip2.Value;
                    jj.Add(cf);
                }
            }
            return jj;
        }

        protected void btniam_ServerClick(object sender, EventArgs e)
        {
            string sql = "";

            List<conflicttype> list = ReturnCheckedItems();
            foreach (var item in list)
            {
                sql += "update Sys_UserName_ConflictResolution set STATE=2,ApprovedTime=getdate(), remark='" + hiddenMemo.Value + "' where ID='" + item.Id.ToString() + "';";
            }
            int count = 0;
            using (IAMEntities db = new IAMEntities())
            {
                count = db.ExecuteStoreCommand(sql);
                db.SaveChanges();
            }
            if (count > 0)
            {
                Response.Write("<script>alert('更新成功');</script>");
            }
            Bind();
        }

        // //p2 user标记为账号字段冲突 role 标记为账号角色冲突
        protected void btnSystem_ServerClick(object sender, EventArgs e)
        {
            var listdata = new Sys_UserName_ConflictResolutionDAL().ReturnList().Where(item => item.STATE == Convert.ToInt32(dlpsysType.SelectedValue)).ToList();

            List<conflicttype> list = ReturnCheckedItems();
            System.Text.StringBuilder stb = new System.Text.StringBuilder();
            foreach (var item in list)
            {
                stb.Append("update Sys_UserName_ConflictResolution set STATE=3,ApprovedTime=getdate(), remark='" + hiddenMemo.Value + "' where ID='" + item.Id.ToString() + "';");
                Sys_UserName_ConflictResolution mo = listdata.FirstOrDefault(j => j.ID == item.Id);
                if (item.p2 == "role" && mo.CollSysValue != "源系统中无该账号")
                {
                    if (mo == null)
                        continue;
                    if (mo.CollSysValue == "源系统中无该组权限" || mo.CollSysValue == "源系统中无该组" || mo.CollSysValue == "源系统中无该角色" || mo.CollSysValue == "源系统无该角色" || mo.CollSysValue == "源系统无该角色" || mo.CollSysValue == "源系统中该组权限为删除" || mo.CollSysValue == "源系统中无该岗位")
                    {
                        if (mo.SysType.Trim() == "AD")
                            stb.Append(string.Format(" delete AD_UserWorkGroup where GroupName='{0}' and Uid='{1}'", mo.CollName, mo.UserName));
                        if (mo.SysType.Trim() == "HEC")
                        {
                            string[] hecroles = mo.CollName.Split("/".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                            if (hecroles.Length == 1)
                            {
                                stb.Append(string.Format(" delete dbo.HEC_User_Info WHERE ROLE_CODE='{0}' AND USER_NAME='{1}' ", mo.CollName, mo.UserName));
                            }
                            else if (hecroles.Length == 2)
                            {
                                stb.Append(string.Format(" delete dbo.HEC_User_Info WHERE ROLE_CODE='{0}' AND USER_NAME='{1}' and COMPANY_CODE='{2}'", hecroles[0], mo.UserName, hecroles[1]));
                            }
                            else if (hecroles.Length >= 3)
                            {
                                stb.Append(string.Format("DELETE dbo.HEC_User_Gangwei WHERE EMPLOYEE_CODE='{0}' AND POSITION_NAME='{1}' AND COMPANY_NAME='{2}' AND UNIT_NAME='{3}'", mo.UserName, hecroles[2], hecroles[0], hecroles[1]));
                            }
                        }
                        if (mo.SysType.Trim() == "SAP")
                            stb.Append(string.Format(" delete dbo.SAP_User_Role WHERE ROLEID='{0}' AND BAPIBNAME='{1}'", mo.CollName, mo.UserName));
                        if (mo.SysType.Trim() == "TC")
                            stb.Append(string.Format(" delete dbo.TC_UserGroupSetting WHERE UserID='{0}' AND Memo='{1}'", mo.UserName,mo.CollName));
                        if (mo.SysType.Trim() == "ADComputer")
                            stb.Append(string.Format(" DELETE dbo.AD_Computer_WorkGroups WHERE ComputerName='{0}' AND WorkGroup='{1}'", mo.CollName, mo.UserName));
                        if (mo.SysType.Trim() == "HR")
                        {
                            try
                            {
                                string[] roles = mo.CollName.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                                if (roles.Length > 0)
                                {
                                    stb.Append(string.Format(@" DELETE HRsm_user_role WHERE Cuserid=(
SELECT Cuserid FROM dbo.HRSm_user WHERE User_code='{0}') AND Pk_role=(
SELECT Pk_role FROM dbo.HRsm_role WHERE role_name='{1}') AND dbo.HRsm_user_role.Pk_corp='{2}'", mo.UserName, roles[0], roles[1]));
                                }
                            }
                            catch
                            { continue; }
                        }
                    }
                    else
                    {
                        stb.Append(" " + mo.Remark + ";");
                    }
                }
                else if (item.p2 == "role" && mo.CollSysValue == "源系统中无该账号")
                { }
                else
                {
                    stb.Append(string.Format("update {0} set {1}='{2}' where {3}='{4}';", mo.TableName, mo.P1, mo.CollSysValue, mo.UserCollName, mo.UserValue));
                }
            }

            int count = 0;
            if (stb.ToString() != string.Empty)
            {

                try
                {
                    using (IAMEntities db = new IAMEntities())
                    {
                        count = db.ExecuteStoreCommand(stb.ToString());
                        db.SaveChanges();
                    }

                }
                catch (Exception ex)
                {
                    //Response.Write(ex.ToString());
                }
            }
            if (count > 0)
            {
                Response.Write("<script>alert('更新成功');</script>");
            }
            Bind();
        }

        public string ReturnOpenLink(object _sysType, object _key)
        {
            switch (_sysType.ToString().ToUpper().Trim())
            {
                case "HR":
                    {
                        IAMEntities db = new IAMEntities();
                        string k = _key.ToString();
                        var en = db.HRSm_user.FirstOrDefault(x => x.User_code == k);
                        if (en != null)
                            return "OpenPage('../admin/HREmployeeCreate.aspx?id=" + en.Cuserid + "');";
                        else
                            return "OpenPage('../admin/HREmployeeCreate.aspx?id=" + _key + "');";
                    }
                case "SAP": return "OpenPage('../admin/sapusercreate.aspx?uid=" + _key + "');";
                case "AD": return "OpenPage('../admin/ADInfoManager.aspx?userid=" + _key + " ');";
                case "TC": return "OpenPage('../admin/TCUserInfoCreate.aspx?mzhanghao=" + _key + "');";
                case "HEC": return "OpenPage('../admin/HECUserInfoCreate.aspx?usercd=" + _key + "');";
                case "ADComputer": return "OpenPage('../AD_ComputerCreate.aspx?id=" + _key + "');";
                default: return string.Empty;
            }
        }

    }
}