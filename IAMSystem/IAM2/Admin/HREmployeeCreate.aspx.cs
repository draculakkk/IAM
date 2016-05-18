using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseDataAccess;
using IAMEntityDAL;

namespace IAM.Admin
{
    public partial class HREmployeeCreate : BasePage
    {
        HRSm_userDAL _userServices = new HRSm_userDAL();
        HRsm_user_roleDAL _userRoleServices = new HRsm_user_roleDAL();
        AccountMapingDAL _accServicer = new AccountMapingDAL();
        IAMEntities db = new IAMEntities();
        public bool IsAdmin = false;

        public string Currid
        {
            get
            {
                return Request.QueryString["id"];
            }
        }

        bool IsUpdate
        {
            get
            {
                if (Request.QueryString["id"] != null)
                    return true;
                else
                    return false;
            }
        }

        string _ecode;
        string Employeecode
        {
            get
            {
                if (txtEmployeeNumber.Text.Trim() == string.Empty)
                    _ecode = string.Empty;
                else
                    _ecode = txtEmployeeNumber.Text.Trim();
                return _ecode;
            }
            set { _ecode = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    QueryByCuserId();
                    QueryUserType();
                    editHRInfo();
                    txtEpolyeeEncode.Attributes.Add("readonly", "true");
                    txtEmployeeName.Attributes.Add("readonly", "true");
                    PageStutas("y");
                }
                else
                {
                    List<V_HRSm_User_Role_new> vhrSmUserRole = new List<V_HRSm_User_Role_new>();
                    ViewState["UserInfoList"] = vhrSmUserRole;
                    PageStutas("NaN");
                }
                btnQuery.Enabled = base.ReturnUserRole.Admin;
                btnSave.Enabled = base.ReturnUserRole.Admin;
                inputweipai.Disabled = !base.ReturnUserRole.Admin;
                if (!base.ReturnUserRole.EHR && !base.ReturnUserRole.EndUser && !base.ReturnUserRole.Admin && !base.ReturnUserRole.Leader)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('你无权限查看该页面');window.location.href='./HREmployeeManager.aspx';", true);
                }


            }

            IsAdmin = base.ReturnUserRole.Admin;
        }

        void PageStutas(string _type)
        {
            txtName.Attributes.Add("readonly", "true");
            txtGangwei.Attributes.Add("readonly", "true");
            txtPartment.Attributes.Add("readonly", "true");
            txtPrePartment.Attributes.Add("readonly", "true");
            txtPhoneNumber.Attributes.Add("readonly", "true");
            txtComeDate.Attributes.Add("readonly", "true");
            txtOutDate.Attributes.Add("readonly", "true");
            switch (_type)
            {
                case "NaN":
                    txtEpolyeeEncode.Attributes.Add("readonly", "true");
                    txtEmployeeName.Attributes.Add("readonly", "true");
                    txtEmpDescription.Attributes.Add("readonly", "true");
                    ddlPasswordLevel.Attributes.Add("readonly", "true");
                    txtPassword.Attributes.Add("readonly", "true");
                    txtEntryDate.Attributes.Add("readonly", "true");
                    txtFuilure.Attributes.Add("readonly", "true");
                    dplLoginType.Attributes.Add("readonly", "true");
                    chkIsSuoding.Attributes.Add("readonly", "true");
                    txtMemo.Attributes.Add("readonly", "true");
                    break;
                case "1":
                    txtEpolyeeEncode.Attributes.Add("readonly", "true");
                    txtEmployeeName.Attributes.Add("readonly", "true");
                    txtEmpDescription.Attributes.Add("readonly", "true");
                    ddlPasswordLevel.Attributes.Add("readonly", "true");
                    txtPassword.Attributes.Add("readonly", "true");
                    txtEntryDate.Attributes.Add("readonly", "true");
                    txtFuilure.Attributes.Add("readonly", "true");
                    dplLoginType.Attributes.Add("readonly", "true");
                    chkIsSuoding.Attributes.Add("readonly", "true");
                    txtMemo.Attributes.Add("readonly", "true");
                    Response.Write("<script>alert('不存在该工号员工或该员工已离职 请核实');</script>");
                    break;
                case "2":
                    txtEpolyeeEncode.Attributes.Add("readonly", "true");
                    txtEmployeeName.Attributes.Add("readonly", "true");
                    txtEmpDescription.Attributes.Add("readonly", "true");
                    ddlPasswordLevel.Attributes.Add("readonly", "true");
                    txtPassword.Attributes.Add("readonly", "true");
                    txtEntryDate.Attributes.Add("readonly", "true");
                    txtFuilure.Attributes.Add("readonly", "true");
                    dplLoginType.Attributes.Add("readonly", "true");
                    chkIsSuoding.Attributes.Add("readonly", "true");
                    txtMemo.Attributes.Add("readonly", "true");
                    Response.Write("<script>alert('该工号已拥有Hr员工类账号 故不可再添加员工类账号');</script>");
                    break;
                case "3":
                    txtEpolyeeEncode.Attributes.Add("readonly", "true");
                    txtEmployeeName.Attributes.Add("readonly", "true");
                    txtEmpDescription.Attributes.Add("readonly", "true");
                    ddlPasswordLevel.Attributes.Add("readonly", "true");
                    txtPassword.Attributes.Add("readonly", "true");
                    txtEntryDate.Attributes.Add("readonly", "true");
                    txtFuilure.Attributes.Add("readonly", "true");
                    dplLoginType.Attributes.Add("readonly", "true");
                    chkIsSuoding.Attributes.Add("readonly", "true");
                    txtMemo.Attributes.Add("readonly", "true");
                    Response.Write("未能在AD系统中检测到该工号所对应的AD员工类账号 故不可添加");
                    break;
                case "4":
                    txtEpolyeeEncode.Attributes.Remove("readonly");
                    txtEmployeeName.Attributes.Remove("readonly");
                    txtEmpDescription.Attributes.Remove("readonly");
                    ddlPasswordLevel.Attributes.Remove("readonly");
                    txtPassword.Attributes.Remove("readonly");
                    txtEntryDate.Attributes.Remove("readonly");
                    txtFuilure.Attributes.Remove("readonly");
                    dplLoginType.Attributes.Remove("readonly");
                    chkIsSuoding.Attributes.Remove("readonly");
                    txtMemo.Attributes.Remove("readonly");
                    break;
                case "-2":

                    break;
                case "y":
                    if (dplEmployeeType.SelectedValue == "员工")
                    {
                        txtEpolyeeEncode.Attributes.Add("readonly", "true");
                        txtEmployeeName.Attributes.Add("readonly", "true");
                    }
                    else
                    {
                        txtEpolyeeEncode.Attributes.Remove("readonly");
                        txtEmployeeName.Attributes.Remove("readonly");
                    }

                    txtEmpDescription.Attributes.Remove("readonly");
                    ddlPasswordLevel.Attributes.Remove("readonly");
                    txtPassword.Attributes.Remove("readonly");
                    txtEntryDate.Attributes.Remove("readonly");
                    txtFuilure.Attributes.Remove("readonly");
                    dplLoginType.Attributes.Remove("readonly");
                    chkIsSuoding.Attributes.Remove("readonly");
                    txtMemo.Attributes.Remove("readonly");
                    break;
            }
        }

        int Create()
        {
            HRSm_user userEntity = new HRSm_user();
            HRSm_user old = new HRSm_user();
            if (IsUpdate)
            {
                old = _userServices.GetOne(Currid);
                userEntity = BLL.Extensions.Clone<HRSm_user>(old);
            }

            userEntity.Cuserid = IsUpdate == false ? Guid.NewGuid().ToString().Substring(0, 20) : Request.QueryString["id"];
            userEntity.Able_time = string.IsNullOrEmpty(txtEntryDate.Text) ? DateTime.Now.ToString("yyyy-MM-dd") : txtEntryDate.Text;
            userEntity.Authen_type = dplLoginType.SelectedValue;
            userEntity.Disable_time = chkIsSuoding.Checked == true ? DateTime.Now.ToString("yyyy-MM-dd") : txtFuilure.Text;
            userEntity.Dr = 0;
            userEntity.Isca = "N";
            userEntity.keyuser = "";
            userEntity.Langcode = "simpchn";
            userEntity.Locked_tag = chkIsSuoding.Checked == true ? "Y" : "N";
            userEntity.Pwdlevelcode = ddlPasswordLevel.SelectedValue;
            userEntity.Pwdparam = DateTime.Now.ToString("yyyy-MM-dd");
            userEntity.Pwdtype = 0;
            userEntity.User_code = txtEpolyeeEncode.Text;
            userEntity.USER_name = txtEmployeeName.Text;
            userEntity.User_note = txtEmpDescription.Text;
            userEntity.user_password = txtPassword.Text;
            userEntity.isSync = false;
            userEntity.syncDate = DateTime.Now;
            userEntity.Memo = txtMemo.Text.Trim();
            var accountMaping = new AccountMaping()
            {
                id = Guid.NewGuid(),
                zhanghao = userEntity.User_code,
                gonghao = txtEmployeeNumber.Text.Trim(),
                type = Unitity.SystemType.HR.ToString(),
                UserType = dplEmployeeType.SelectedValue.Trim()
            };
            if (_userServices.AddOrUpdate(userEntity) > 0)
            {
                if (IsUpdate)
                {
                    var oldacc = new AccountMapingDAL().GetOne(userEntity.User_code, "HR");
                    new AccountMapingDAL().UpdateAccountMaping(accountMaping, oldacc);
                }
                else
                    new AccountMapingDAL().Add(accountMaping);
                List<V_HRSm_User_Role_new> listrole = (List<V_HRSm_User_Role_new>)ViewState["UserInfoList"];
                foreach (var item in listrole)
                {
                    item.urcuserid = userEntity.Cuserid;

                }
                //邮件服务
                BLL.AddUserMail addmail = new BLL.AddUserMail();
                addmail.Actioner = base.UserInfo.adname;
                addmail.SystemName = Unitity.SystemType.HR.ToString();
                addmail.SystemType = dplEmployeeType.SelectedValue;
                addmail.UserName = userEntity.User_code;

                if (!IsUpdate)
                {
                    BLL.ActionLog.CreateLog(base.UserInfo.adname, txtEpolyeeEncode.Text, Unitity.SystemType.HR.ToString(), dplEmployeeType.SelectedValue);
                    addmail.UserInfo = new BLL.CompareEntity<HRSm_user>().ReturnCompareStringForMailAdd(userEntity, Unitity.SystemType.HR);
                    addmail.RoleString = BLL.CompareRoleList.HrRoleMess(listrole, "");
                    new BLL.MailServices().CreateUserMail(addmail);
                }
                else
                {
                    if (chkIsSuoding.Checked)//禁用账号
                    {
                        BLL.ActionLog.DisabledLog(base.UserInfo.adname, userEntity.User_code, Unitity.SystemType.HR.ToString(), dplEmployeeType.SelectedValue);

                        addmail.UserInfo = new BLL.CompareEntity<HRSm_user>().ReturnCompareStringForMailUpdate(old, userEntity, Unitity.SystemType.HR);
                        new BLL.MailServices().DisabledUserMail(addmail);
                    }
                    else
                    {
                        //string mess = BLL.CompareRoleList.HrRoleMess((List<V_HRSm_User_Role_new>)ViewState["UserInfoList"],userEntity.Cuserid);
                        new BLL.ActionLog().EditLog<HRSm_user>(base.UserInfo.adname, txtEpolyeeEncode.Text, Unitity.SystemType.HR.ToString(), dplEmployeeType.SelectedValue, old, userEntity);
                        addmail.UserInfo = new BLL.CompareEntity<HRSm_user>().ReturnCompareStringForMailUpdate(old, userEntity, Unitity.SystemType.HR);
                        addmail.RoleString = BLL.CompareRoleList.HrRoleMess(listrole, old.Cuserid);
                        new BLL.MailServices().UpdateUserMail(addmail);
                    }
                }

                return _userRoleServices.AddHRsm_user_roleList(listrole);
            }
            return 0;
        }

        /// <summary>
        /// 查询操作用户详细信息
        /// </summary>
        void QueryByCuserId()
        {
            HRSm_user userEntity = db.HRSm_user.FirstOrDefault(item => item.Cuserid == Currid);
            if (userEntity != null)
            {
                txtEmployeeNumber.Text = userEntity.Cuserid;
                txtEntryDate.Text = userEntity.Able_time;
                dplLoginType.SelectedValue = userEntity.Authen_type;
                txtFuilure.Text = userEntity.Disable_time;
                chkIsSuoding.Checked = userEntity.Locked_tag == "Y" ? true : false;
                ddlPasswordLevel.SelectedValue = userEntity.Pwdlevelcode;
                txtEpolyeeEncode.Text = userEntity.User_code;
                txtEmployeeName.Text = userEntity.USER_name;
                txtEmpDescription.Text = userEntity.User_note;
                txtPassword.Text = userEntity.user_password;
                txtMemo.Text = userEntity.Memo;
                QueryUserRoleInfo(false);

            }
        }

        void editHRInfo()
        {
            HREmployee EmployeeModel = null;
            if (IsUpdate)
            {
                EmployeeModel = new IAMEntities().HREmployee.FirstOrDefault(item => item.code == Employeecode);
            }
            else
            {
                EmployeeModel = new IAMEntities().HREmployee.FirstOrDefault(item => item.code == Employeecode && (item.leavePostsDate == null || item.leavePostsDate > DateTime.Now) == true);
            }
            if (EmployeeModel != null)
            {
                txtName.Text = EmployeeModel.name;
                txtGangwei.Text = EmployeeModel.posts;

                txtPhoneNumber.Text = EmployeeModel.moblePhone;
                txtComeDate.Text = EmployeeModel.toPostsDate != null ? DateTime.Parse(EmployeeModel.toPostsDate.ToString()).ToShortDateString() : "";
                txtOutDate.Text = EmployeeModel.leavePostsDate != null ? DateTime.Parse(EmployeeModel.leavePostsDate.ToString()).ToShortDateString() : "";

                V_HrDepartment departmentModel = new IAMEntities().V_HrDepartment.FirstOrDefault(item => item.dept == EmployeeModel.dept);
                if (departmentModel != null)
                {
                    txtPartment.Text = departmentModel.name;

                    txtPrePartment.Text = departmentModel.shangjiName;
                    chkPartmentOut.Checked = (bool)departmentModel.isRevoke;
                    chkPartmentClose.Checked = (bool)departmentModel.isSealed;
                    txtPartmentOutDate.Text = departmentModel.revokeDate != null ? DateTime.Parse(departmentModel.revokeDate.ToString()).ToShortDateString() : "";

                }
            }
        }

        /// <summary>
        /// 查询用户权限信息
        /// </summary>
        void QueryUserRoleInfo(bool deleting)
        {
            List<V_HRSm_User_Role_new> vhrSmUserRole = new List<V_HRSm_User_Role_new>();
            if ((ViewState["UserInfoList"] == null || ((List<V_HRSm_User_Role_new>)ViewState["UserInfoList"]).Count == 0) && deleting == false)
            {
                vhrSmUserRole = db.V_HRSm_User_Role_new.Where(item => item.uCuserid == Currid && item.urDr != 1).ToList();
                ViewState["UserInfoList"] = vhrSmUserRole;
            }
            else
            {
                vhrSmUserRole = (List<V_HRSm_User_Role_new>)ViewState["UserInfoList"];

            }
            if (!string.IsNullOrEmpty(txtUserInfo.Text))
            {
                string[] arrUserInfo = txtUserInfo.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in arrUserInfo)
                {
                    string[] RoleTmp = item.Split(new char[] { '^' }, StringSplitOptions.RemoveEmptyEntries);

                    V_HRSm_User_Role_new entity = new V_HRSm_User_Role_new();
                    entity.cPk_corp = RoleTmp[0];
                    entity.rPk_role = RoleTmp[1];
                    entity.rRole_code = RoleTmp[2];
                    entity.rRole_Name = RoleTmp[3];
                    entity.cUNTTNAME = RoleTmp[4];
                    entity.rResource_type = Convert.ToInt32(RoleTmp[5]);
                    entity.uCuserid = IsUpdate == true ? Request.QueryString["id"] : string.Empty;
                    entity.urPk_user_role = Guid.NewGuid().ToString().Substring(0, 20);
                    entity.urDr = 2;

                    var ent = vhrSmUserRole.FirstOrDefault(k => k.cPk_corp.Trim() == entity.cPk_corp.Trim() && k.rPk_role.Trim() == entity.rPk_role.Trim() && k.rRole_code.Trim() == entity.rRole_code.Trim() && k.rRole_Name.Trim() == entity.rRole_Name.Trim() && k.cUNTTNAME.Trim() == entity.cUNTTNAME.Trim() && k.rResource_type == entity.rResource_type && k.urDr != 1);
                    if (ent == null)
                        vhrSmUserRole.Add(entity);
                    else
                        ScriptManager.RegisterStartupScript(update1, this.GetType(), "", "<script>alert('已存在" + entity.rRole_Name + "该角色');</script>", false);

                }
            }
            repeaterHRRole.DataSource = vhrSmUserRole.Where(item => item.urDr != 1);
            repeaterHRRole.DataBind();
        }

        /// <summary>
        /// 查询用户账号类型
        /// </summary>
        void QueryUserType()
        {
            IAMEntities db = new IAMEntities();
            var hrmaping = db.HRSm_user.Join(db.AccountMaping, item => item.User_code, ite => ite.zhanghao, (item, ite) => new { currid = item.Cuserid, gonghao = ite.gonghao, usertype = ite.UserType });
            var hrentity = hrmaping.FirstOrDefault(item => item.currid == Currid);
            if (hrentity != null)
            {
                dplEmployeeType.SelectedValue = hrentity.usertype;
                txtEmployeeNumber.Text = hrentity.gonghao;
            }

        }

        /// <summary>
        /// 根据工号查询员工详细信息
        /// </summary>
        void QueryInfo()
        {
            HREmployee EmployeeModel = new IAMEntities().HREmployee.FirstOrDefault(item => item.code == Employeecode && (item.leavePostsDate == null || item.leavePostsDate > DateTime.Now) == true);
            if (EmployeeModel != null)
            {
                var en = new AccountMapingDAL().GetOne(Employeecode, Unitity.SystemType.HR.ToString(), Unitity.UserType.员工.ToString());
                if (en != null && IsUpdate == false && dplEmployeeType.SelectedValue == Unitity.UserType.员工.ToString())
                {
                    PageStutas("2");
                    return;
                }
                txtName.Text = EmployeeModel.name;
                txtGangwei.Text = EmployeeModel.posts;
                txtPhoneNumber.Text = EmployeeModel.moblePhone;
                txtComeDate.Text = EmployeeModel.toPostsDate != null ? DateTime.Parse(EmployeeModel.toPostsDate.ToString()).ToShortDateString() : "";
                txtOutDate.Text = EmployeeModel.leavePostsDate != null ? DateTime.Parse(EmployeeModel.leavePostsDate.ToString()).ToShortDateString() : "";
                V_HrDepartment departmentModel = new IAMEntities().V_HrDepartment.FirstOrDefault(item => item.dept == EmployeeModel.dept);
                if (departmentModel != null)
                {
                    txtPartment.Text = departmentModel.name;
                    txtPrePartment.Text = departmentModel.shangjiName;
                }

                if (dplEmployeeType.SelectedValue == Unitity.UserType.员工.ToString()) //添加员工类时检查是否已存在ad账号
                {


                    string acc, cnname;
                    acc = cnname = string.Empty;
                    using (IAMEntities db = new IAMEntities())
                    {
                        var adentity = (from a in db.AccountMaping
                                        join b in db.AD_UserInfo on a.zhanghao equals b.Accountname
                                        where a.gonghao == Employeecode && a.UserType == "员工" && a.type == "AD"
                                        select new { Accountname = b.Accountname, CnName = b.CnName }).FirstOrDefault();
                        if (adentity != null)
                        {
                            acc = adentity.Accountname;
                            cnname = adentity.CnName;
                        }
                    }

                    if (acc != string.Empty && cnname != string.Empty)
                    {
                        txtEpolyeeEncode.Text = acc;
                        txtEmployeeName.Text = cnname;
                    }
                    else
                    {
                        PageStutas("3");
                        return;
                    }
                }

                PageStutas("y");

            }
            else
            {
                PageStutas("1");
            }
        }

        bool CheckValue(out string mess)
        {
            System.Text.StringBuilder stb = new System.Text.StringBuilder();
            bool istrue = true;
            if (dplEmployeeType.SelectedValue == "员工")
            {
                if (string.IsNullOrEmpty(txtEmployeeNumber.Text.Trim()))
                {
                    stb.Append("请填写员工工号");
                    istrue = false;
                }
                else if (string.IsNullOrEmpty(txtEmployeeName.Text.Trim()))
                {
                    stb.Append("请填写用户名称");
                    istrue = false;
                }
                else if (string.IsNullOrEmpty(txtEpolyeeEncode.Text.Trim()))
                {
                    stb.Append("请填写用户编码");
                    istrue = false;
                }
                else if (string.IsNullOrEmpty(txtPassword.Text.Trim()) && IsUpdate == false)
                {
                    stb.Append("请填写密码");
                    istrue = false;
                }

            }
            if (dplEmployeeType.SelectedValue == "其他")
            {
                if (string.IsNullOrEmpty(txtEmployeeNumber.Text.Trim()))
                {
                    stb.Append("请填写员工工号");
                    istrue = false;
                }
                else
                    if (string.IsNullOrEmpty(txtEmployeeName.Text.Trim()))
                    {
                        stb.Append("请填写用户名称");
                        istrue = false;
                    }
                    else if (string.IsNullOrEmpty(txtEmpDescription.Text.Trim()))
                    {
                        stb.Append("请填写用户描述");
                        istrue = false;
                    }
                    else if (string.IsNullOrEmpty(txtEpolyeeEncode.Text.Trim()))
                    {
                        stb.Append("请填写用户编码");
                        istrue = false;
                    }

                    else if (string.IsNullOrEmpty(txtPassword.Text.Trim()) && IsUpdate == false)
                    {
                        stb.Append("请填写密码");
                        istrue = false;
                    }
            }

            if (dplEmployeeType.SelectedValue == "系统")
            {
                
                if (string.IsNullOrEmpty(txtEpolyeeEncode.Text.Trim()))
                {
                    stb.Append("请填写用户编码");
                    istrue = false;
                }

                else if (string.IsNullOrEmpty(txtPassword.Text.Trim()) && IsUpdate == false)
                {
                    stb.Append("请填写密码");
                    istrue = false;
                }
            }

            mess = stb.ToString();
            return istrue;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            string mess = "";
            if (!CheckValue(out mess))
            {
                Response.Write("<script>alert('" + mess + "');</script>");
                return;
            }
            if (Create() > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('添加成功');window.close();", true);
            }
        }

        protected void btnOnLoadRoleInfo_click(object sender, EventArgs e)
        {
            QueryUserRoleInfo(false);
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmployeeNumber.Text.Trim()))
            {
                QueryInfo();
            }

            //if (dplEmployeeType.SelectedValue == "其他" && !string.IsNullOrEmpty(txtEmployeeNumber.Text.Trim()))
            //{
            //    PageStutas("y");
            //}
            //else
            //{
            //    PageStutas("NaN");
            //}
            //if (dplEmployeeType.SelectedValue == "系统")
            //{
            //    PageStutas("y");
            //}
            if (dplEmployeeType.SelectedValue == "系统" && string.IsNullOrEmpty(txtEmployeeNumber.Text.Trim()))
            {
                PageStutas("4");
            }
            else if (dplEmployeeType.SelectedValue != "系统" && string.IsNullOrEmpty(txtEmployeeNumber.Text.Trim()))
            {
                PageStutas("NaN");
            }

        }

        protected void lbtnDelete_Command(object sender, CommandEventArgs e)
        {
            List<V_HRSm_User_Role_new> vhrSmUserRole = new List<V_HRSm_User_Role_new>();
            vhrSmUserRole = (List<V_HRSm_User_Role_new>)ViewState["UserInfoList"];
            if (e.CommandArgument.ToString() != string.Empty)
            {
                vhrSmUserRole.FirstOrDefault(item => item.urPk_user_role == e.CommandArgument.ToString()).urDr = 1;

                ViewState["UserInfoList"] = vhrSmUserRole;
                txtUserInfo.Text = string.Empty;
                QueryUserRoleInfo(true);
            }

        }
    }
}