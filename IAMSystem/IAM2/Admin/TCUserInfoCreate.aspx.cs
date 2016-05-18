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
    public partial class TCUserInfoCreate : BasePage
    {

        string mzhanghao
        {
            get
            {
                if (Request.QueryString["mzhanghao"] != null)
                {

                    return Request.QueryString["mzhanghao"];
                }
                else
                    return string.Empty;
            }
        }

        bool Isupdate
        {
            get
            {
                if (Request.QueryString["mzhanghao"] != null)
                    return true;
                else
                    return false;
            }
        }

        AccountMapingDAL _accountServices = new AccountMapingDAL();

        public bool IsAdmin = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnQuery.Enabled = base.ReturnUserRole.Admin;
                inputAdd.Disabled = !base.ReturnUserRole.Admin;
                btnCreate.Enabled = base.ReturnUserRole.Admin;
                if (!base.ReturnUserRole.Admin && !base.ReturnUserRole.TC && !base.ReturnUserRole.Leader)
                {
                    base.NoRole();
                }

                if (Request.QueryString["mzhanghao"] != null)
                {
                    Query();
                    QueryAccMapping();
                    if (!string.IsNullOrEmpty(txtgonghao.Text.Trim()))
                    {
                        QueryHRInfo();
                    }
                    PageStutas("y");
                }
                else
                {
                    PageStutas("NaN");
                }
            }

            IsAdmin = base.ReturnUserRole.Admin;
        }

        public void IsReadOnly()
        {
            //this.txtUserName.ReadOnly = true;
            //this.txtUserID.ReadOnly = true;
            //this.txtgonghao.ReadOnly = true;
            //this.txtEmail.ReadOnly = true;
            //this.txtPassword.ReadOnly = true;
            //this.dplEmployeeType.Enabled = false;
        }


        void PageStutas(string _sta)
        {
            txtName.Attributes.Add("readonly", "true");
            txtGangwei.Attributes.Add("readonly", "true");
            txtPartment.Attributes.Add("readonly", "true");
            txtPrePartment.Attributes.Add("readonly", "true");
            txtPhoneNumber.Attributes.Add("readonly", "true");
            txtComeDate.Attributes.Add("readonly", "true");
            txtOutDate.Attributes.Add("readonly", "true");
            switch (_sta)
            {
                case "NaN":
                    txtUserName.Enabled = false;
                    txtUserID.Enabled = false;
                    txtSystemName.Enabled = false;
                    txtPassword.Enabled = false;
                    dplLevel.Enabled = false;
                    txtEmail.Enabled = false;
                    txtDefaultGroup.Enabled = false;
                    txtDefaultRole.Enabled = false;
                    break;
                case "1"://离职或不存在
                    txtUserName.Enabled = false;
                    txtUserID.Enabled = false;
                    txtSystemName.Enabled = false;
                    txtPassword.Enabled = false;
                    dplLevel.Enabled = false;
                    txtEmail.Enabled = false;
                    txtDefaultGroup.Enabled = false;
                    txtDefaultRole.Enabled = false;
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert(\"不存在该工号员工或该员工已离职 请核实后在添加\");</script>");
                    break;
                case "2"://已存在tc员工类账号
                    txtUserName.Enabled = false;
                    txtUserID.Enabled = false;
                    txtSystemName.Enabled = false;
                    txtPassword.Enabled = false;
                    dplLevel.Enabled = false;
                    txtEmail.Enabled = false;
                    txtDefaultGroup.Enabled = false;
                    txtDefaultRole.Enabled = false;
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert(\"该工号员工已拥有TC员工类账号 故不可在添加员工类账号\");</script>");
                    break;
                case "3"://无ad账号
                    txtUserName.Enabled = false;
                    txtUserID.Enabled = false;
                    txtSystemName.Enabled = false;
                    txtPassword.Enabled = false;
                    dplLevel.Enabled = false;
                    txtEmail.Enabled = false;
                    txtDefaultGroup.Enabled = false;
                    txtDefaultRole.Enabled = false;
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert(\"未能在AD系统中检测到该工号所对应的AD员工类账号 故不可在添加员工类账号 如有疑问请联系AD系统管理员\");</script>");
                    break;
                case "4":
                    txtUserName.Enabled = true;
                    txtUserID.Enabled = true;
                    txtSystemName.Enabled = true;
                    txtPassword.Enabled = true;
                    dplLevel.Enabled = true;
                    txtEmail.Enabled = true;
                    txtDefaultGroup.Enabled = true;
                    txtDefaultRole.Enabled = true;
                    if (dplEmployeeType.SelectedValue == "员工")
                    {
                        txtUserID.Attributes.Add("readonly", "true");
                        txtUserName.Attributes.Add("readonly", "true");
                    }
                    break;
                case "-2"://用户名已存在
                    txtUserName.Enabled = false;
                    txtUserID.Enabled = false;
                    txtSystemName.Enabled = false;
                    txtPassword.Enabled = false;
                    dplLevel.Enabled = false;
                    txtEmail.Enabled = false;
                    txtDefaultGroup.Enabled = false;
                    txtDefaultRole.Enabled = false;
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script> alert(\"该工号员工已拥有TC员工类账号 故不可在添加员工类账号\");</script>");
                    break;
                case "y":
                    txtUserName.Enabled = true;
                    txtUserID.Enabled = true;
                    txtSystemName.Enabled = true;
                    txtPassword.Enabled = true;
                    dplLevel.Enabled = true;
                    txtEmail.Enabled = true;
                    txtDefaultGroup.Enabled = true;
                    txtDefaultRole.Enabled = true;
                    if (dplEmployeeType.SelectedValue == "员工")
                    {
                        txtUserID.Attributes.Add("readonly", "true");
                        txtUserName.Attributes.Add("readonly", "true");
                    }
                    else
                    {
                        txtUserID.Attributes.Remove("readonly");
                        txtUserName.Attributes.Remove("readonly");
                    }
                    break;
            }
        }

        /// <summary>
        /// 查询tc账号信息
        /// </summary>
        void Query()
        {
            V_TCUserInfo entity = new V_TCUserInfo();
            string mzhanggong = Isupdate ? mzhanghao : txtgonghao.Text.Trim();
            if (mzhanggong == string.Empty)
            {
                return;
            }
            if (Isupdate)
            {
                using (IAMEntities db = new IAMEntities())
                {
                    //entity = db.V_TCUserInfo.FirstOrDefault(item => item.mzhanghao == mzhanggong);
                    var lis = db.ExecuteStoreQuery<V_TCUserInfo>("SELECT  * FROM V_TCUserInfo WHERE mzhanghao='" + mzhanggong + "'");

                    try
                    {
                        var lii = lis.ToList();
                        entity = lii[0];
                    }
                    catch { }

                }
            }
            else
            {
                using (IAMEntities db = new IAMEntities())
                {
                    entity = db.V_TCUserInfo.FirstOrDefault(item => item.mgonghao == mzhanggong);
                }
            }
            if (entity != null)
            {
                txtUserID.Text = entity.uUserID;
               // if (!Isupdate)
                //{
                    txtUserName.Text = entity.uUserName;
                //}
                txtSystemName.Text = entity.uSystemName;
                txtEmail.Text = entity.mMailAddress;
                txtPassword.Text = entity.uPassword;
                dplLevel.SelectedValue = entity.uLicenseLevel.ToString();
                txtgonghao.Text = entity.mgonghao;
                dplEmployeeType.SelectedValue = entity.mUserType;
                txtMemo.Text = entity.uMemo;
                rdstatus.SelectedValue = entity.uUserStatus.ToString();
            }
            BindUserRole();
        }

        /// <summary>
        /// 查询tc用户角色信息
        /// </summary>
        void BindUserRole()
        {
            List<V_TCReport> listrole = new List<V_TCReport>();

            if (ViewState["tcrolelist"] != null)
            {
                listrole = (List<V_TCReport>)ViewState["tcrolelist"];
            }
            else
            {
                List<TC_UserGroupSetting> listsetting = new List<TC_UserGroupSetting>();
                using (IAMEntities db = new IAMEntities())
                {
                    listsetting = db.TC_UserGroupSetting.Where(item => item.UserID == mzhanghao && item.isdr != 1).ToList();
                }
                if (listsetting != null && listsetting.Count > 0)
                {
                    foreach (var it in listsetting)
                    {
                        V_TCReport tmp = new V_TCReport();
                        tmp.urMemo = it.Memo;
                        tmp.urp1 = it.p1;
                        tmp.urp2 = it.p2;
                        tmp.isdr = it.isdr;
                        tmp.uUserID = it.UserID;
                        tmp.urGroupAdmin = it.GroupAdmin;
                        tmp.urGroupDefaultRole = it.GroupDefaultRole;
                        tmp.urGroupOut = it.GroupOut;
                        tmp.urGroupStatus = it.GroupStatus;
                        tmp.urid = it.ID;
                        listrole.Add(tmp);
                    }
                }

                ViewState["tcrolelist"] = listrole;
            }
            repeaterUserRole.DataSource = listrole.Where(item => item.isdr != 1);
            repeaterUserRole.DataBind();
        }

        /// <summary>
        /// 查询人员类型
        /// </summary>
        void QueryAccMapping()
        {
            AccountMaping acmp = _accountServices.GetOne(txtgonghao.Text.Trim(), Unitity.SystemType.TC.ToString(), txtUserID.Text.Trim());
            if (acmp != null)
            {
                dplEmployeeType.SelectedValue = acmp.UserType;
            }
        }

        void QueryHRInfo()
        {
            HREmployee EmployeeModel = null;
            if (Isupdate)
            {
                EmployeeModel = new IAMEntities().HREmployee.FirstOrDefault(item => item.code == txtgonghao.Text.Trim());
            }
            else
            {
                EmployeeModel = new IAMEntities().HREmployee.FirstOrDefault(item => item.code == txtgonghao.Text.Trim() && (item.leavePostsDate == null || item.leavePostsDate > DateTime.Now) == true);
            }
            if (EmployeeModel != null)
            {
                var en = new AccountMapingDAL().GetOne(txtgonghao.Text.Trim(), Unitity.SystemType.TC.ToString(), Unitity.UserType.员工.ToString());
                if (en != null && Isupdate == false && dplEmployeeType.SelectedValue == Unitity.UserType.员工.ToString())
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
                    string acc, cnname, email;
                    acc = email = cnname = string.Empty;
                    using (IAMEntities db = new IAMEntities())
                    {
                        var adentity = (from a in db.AccountMaping
                                        join b in db.AD_UserInfo on a.zhanghao equals b.Accountname
                                        where a.gonghao == txtgonghao.Text.Trim() && a.UserType == "员工" && a.type == "AD"
                                        select new { Accountname = b.Accountname, CnName = b.CnName, Email = b.Email }).FirstOrDefault();
                        if (adentity != null)
                        {
                            acc = adentity.Accountname;
                            cnname = adentity.CnName;
                            email = adentity.Email;
                        }
                    }

                    if (acc != string.Empty && cnname != string.Empty && email != string.Empty)
                    {

                        txtUserID.Text = "shac" + txtgonghao.Text.Trim().Replace("-", string.Empty);
                        txtSystemName.Text = txtUserID.Text;
                        if (!Isupdate)
                        {
                            txtUserName.Text = cnname;
                        }
                        txtEmail.Text = email;
                        // txtEmployeeNumber.Text = txtEmployeeCode.Text = txtgonghao.Text;
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

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            //Query();
            if (!string.IsNullOrEmpty(txtgonghao.Text.Trim()))
            {
                QueryHRInfo();
                IsReadOnlyNewBuild();
            }
            if (dplEmployeeType.SelectedValue == "系统" && string.IsNullOrEmpty(txtgonghao.Text.Trim()))
            {
                PageStutas("4");
            }
            else if (dplEmployeeType.SelectedValue != "系统" && string.IsNullOrEmpty(txtgonghao.Text.Trim()))
            {
                PageStutas("NaN");
            }
        }

        public void IsReadOnlyNewBuild()
        {
            //this.txtUserID.ReadOnly = true;
            //this.txtEmail.ReadOnly = true;
        }

        protected void btnOnLoadRoleInfo_ServerClick(object sender, EventArgs e)
        {
            List<V_TCReport> listrole = (List<V_TCReport>)ViewState["tcrolelist"];
            if (listrole == null)
            {
                listrole = new List<V_TCReport>();
            }
            if (txtUserInfo.Text.Trim() != string.Empty)
            {
                string[] rolearr = txtUserInfo.Text.Trim().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (rolearr != null && rolearr.Length > 0)
                {
                    foreach (var it in rolearr)
                    {
                        string[] tmp = it.Split('^');
                        V_TCReport entity = new V_TCReport();

                        entity.urMemo = tmp[1] + "." + tmp[2];
                        entity.urp1 = tmp[2];
                        entity.urp2 = tmp[1];
                        entity.isdr = 2;
                        entity.uUserID = txtUserID.Text.Trim();
                        entity.urGroupAdmin = 0;
                        entity.urGroupDefaultRole = 0;
                        entity.urGroupOut = 0;
                        entity.urGroupStatus = 0;
                        entity.urid = Guid.NewGuid();

                        var ent = listrole.FirstOrDefault(item => item.urMemo == entity.urMemo && item.urp1 == entity.urp1 && item.urp2 == entity.urp2 && item.isdr != 1);
                        if (ent == null)
                            listrole.Add(entity);
                        else
                            Response.Write("<script>alert('已存在" + entity.urMemo + "该角色');</script>");
                    }
                }
            }
            ViewState["tcrolelist"] = listrole;
            BindUserRole();
        }

        int CreateOrUpdate()
        {
            TC_UserInfo moduleold = new TC_UserInfoDAL().GetOneTCUser(mzhanghao);

            if (dplLevel.SelectedValue == "0")
            {
                int tclicense =Convert.ToInt32( new KeyValueDAL().GetOne("TC License").VALUE);
                int count;
                int tccount = new IAMEntityDAL.TC_UserInfoDAL().GetTCUserInfolist(out count).Where(x=>x.LicenseLevel==0&&x.UserStatus==0).Count();
                if (Isupdate==false&&(tccount + 1) > tclicense)
                {
                    Response.Write("<script>alert('超出License数量，系统不允许添加');</script>");
                    return 0 ;
                }
                else if (Isupdate && moduleold.LicenseLevel == 1 && dplLevel.SelectedValue == "0"&&(tccount + 1) > tclicense)
                {
                    Response.Write("<script>alert('超出License数量，系统不允许添加');</script>");
                    return 0;
                }
            }

            

            TC_UserInfo entity = new TC_UserInfo();
            entity.UserID = txtUserID.Text;
            entity.UserName = txtUserName.Text;
            entity.SystemName = txtSystemName.Text;
            entity.mailAddress = txtEmail.Text;
            entity.PASSWORD = txtPassword.Text;
            entity.Memo = txtMemo.Text;
            entity.UserStatus = short.Parse(rdstatus.SelectedValue);
            entity.LicenseLevel = short.Parse(dplLevel.SelectedValue);
            var accountMaping = new AccountMaping()
            {
                UserType = dplEmployeeType.SelectedValue.Trim(),
                gonghao = txtgonghao.Text.Trim(),
                zhanghao = txtUserID.Text.Trim(),
                type = Unitity.SystemType.TC.ToString(),
                id = Guid.NewGuid()
            };
            BLL.AddUserMail addmail = new BLL.AddUserMail();
            addmail.Actioner = base.UserInfo.adname;
            addmail.SystemName = Unitity.SystemType.TC.ToString();
            addmail.SystemType = dplEmployeeType.SelectedValue;
            addmail.UserName = entity.UserID;
            var listreport = ViewState["tcrolelist"] as List<V_TCReport>;
            List<V_TCReport> old = new List<V_TCReport>();
            listreport.ForEach(x=>old.Add(BLL.Extensions.Clone<V_TCReport>(x)));

            foreach (RepeaterItem x in repeaterUserRole.Items)
            {
                
                System.Web.UI.HtmlControls.HtmlInputRadioButton r1 = (System.Web.UI.HtmlControls.HtmlInputRadioButton)x.FindControl("inputhuodong");
                System.Web.UI.HtmlControls.HtmlInputRadioButton r2 = (System.Web.UI.HtmlControls.HtmlInputRadioButton)x.FindControl("Radio1");
                HiddenField rep_hiddenid = (HiddenField)x.FindControl("rep_hiddenid");
                Guid id = new Guid(rep_hiddenid.Value);
                if (r1.Checked == true)
                    listreport.FirstOrDefault(item => item.urid == id).urGroupStatus = 1;
                if (r2.Checked == true)
                    listreport.FirstOrDefault(item => item.urid == id).urGroupStatus = 0;
            }


            if (new TC_UserInfoDAL().CreateOrUpdate(entity, listreport) > 0)
            {
                if (rdstatus.SelectedValue == "1")
                {

                    if (Isupdate)
                    {
                        addmail.UserInfo = addmail.UserInfo = new BLL.CompareEntity<TC_UserInfo>().ReturnCompareStringForMailUpdate(moduleold, entity, Unitity.SystemType.TC);
                    }
                    else
                    {
                        addmail.UserInfo = new BLL.CompareEntity<TC_UserInfo>().ReturnCompareStringForMailAdd(entity, Unitity.SystemType.TC);
                    }
                    new BLL.MailServices().DisabledUserMail(addmail);
                }

                if (Isupdate)
                {
                    new BLL.ActionLog().EditLog<TC_UserInfo>(base.UserInfo.adname, entity.UserID, Unitity.SystemType.TC.ToString(), dplEmployeeType.SelectedValue, moduleold, entity);
                    addmail.UserInfo = new BLL.CompareEntity<TC_UserInfo>().ReturnCompareStringForMailUpdate(moduleold, entity, Unitity.SystemType.TC);
                    addmail.RoleString = BLL.CompareRoleList.TCRoleMess(listreport,(List<V_TCReport>)old, entity.UserID);
                    var oldaccount = new AccountMapingDAL().GetOne(entity.UserID, "TC");
                    if (rdstatus.SelectedValue != "1")
                    {
                        new BLL.MailServices().UpdateUserMail(addmail);
                    }
                    return new AccountMapingDAL().UpdateAccountMaping(accountMaping, oldaccount);
                }
                else
                {
                    BLL.ActionLog.CreateLog(base.UserInfo.adname, entity.UserID, Unitity.SystemType.TC.ToString(), dplEmployeeType.SelectedValue);
                    addmail.RoleString = BLL.CompareRoleList.TCRoleMess(listreport,(List<V_TCReport>)old, entity.UserID);
                    addmail.UserInfo = new BLL.CompareEntity<TC_UserInfo>().ReturnCompareStringForMailAdd(entity, Unitity.SystemType.TC);
                   
                        new BLL.MailServices().CreateUserMail(addmail);
                    
                    return new AccountMapingDAL().Add(accountMaping);
                }


            }
            else
                return 0;
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            string mess = "";
            this.txtUserName.ReadOnly = false;
            this.txtUserID.ReadOnly = false;
            this.txtgonghao.ReadOnly = false;
            this.txtEmail.ReadOnly = false;
            this.txtPassword.ReadOnly = false;
            this.dplEmployeeType.Enabled = false;
            if (!IsMustOptions(out mess))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + mess + "');", true);
                return;
            }
            this.txtUserID.ReadOnly = false;
            this.txtEmail.ReadOnly = false;
            if (CreateOrUpdate() > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('操作成功！');window.close();", true);
            }
        }

        /// <summary>
        /// 必选项是否为空
        /// </summary>
        /// <returns></returns>
        protected bool IsMustOptions(out string mess)
        {
            bool istrue = true;
            System.Text.StringBuilder stb = new System.Text.StringBuilder();
            if (dplEmployeeType.SelectedValue == "员工")
            {
                if (string.IsNullOrEmpty(txtgonghao.Text))
                {
                    istrue = false;
                    stb.Append("请填写员工工号");
                }
                else if (string.IsNullOrEmpty(this.txtUserName.Text))
                {
                    istrue = false;
                    stb.Append("请填写人员姓名");
                }
                else if (string.IsNullOrEmpty(this.txtUserID.Text))
                {
                    istrue = false;
                    stb.Append("请填写用户ID");
                }
                else if (string.IsNullOrEmpty(this.txtSystemName.Text))
                {
                    istrue = false;
                    stb.Append("请填写操作系统名称");
                }
                else if (string.IsNullOrEmpty(this.txtPassword.Text) && !Isupdate)
                {
                    istrue = false;
                    stb.Append("请填写密码");
                }
                else if (string.IsNullOrEmpty(this.txtEmail.Text))
                {
                    istrue = false;
                    stb.Append("请填写Email");
                }
            }

            if (dplEmployeeType.SelectedValue == "其他")
            {
                if (string.IsNullOrEmpty(txtgonghao.Text))
                {
                    istrue = false;
                    stb.Append("请填写员工工号");
                }
                else if (string.IsNullOrEmpty(this.txtUserName.Text))
                {
                    istrue = false;
                    stb.Append("请填写人员姓名");
                }
                else if (string.IsNullOrEmpty(this.txtUserID.Text))
                {
                    istrue = false;
                    stb.Append("请填写用户ID");
                }
                else if (string.IsNullOrEmpty(this.txtSystemName.Text))
                {
                    istrue = false;
                    stb.Append("请填写操作系统名称");
                }
                else if (string.IsNullOrEmpty(this.txtPassword.Text) && !Isupdate)
                {
                    istrue = false;
                    stb.Append("请填写密码");
                }
                else if (string.IsNullOrEmpty(this.txtEmail.Text))
                {
                    istrue = false;
                    stb.Append("请填写Email");
                }
            }

            if (dplEmployeeType.SelectedValue == "系统")
            {
                if (string.IsNullOrEmpty(this.txtUserID.Text))
                {
                    istrue = false;
                    stb.Append("请填写用户ID");
                }
                else if (string.IsNullOrEmpty(this.txtSystemName.Text))
                {
                    istrue = false;
                    stb.Append("请填写操作系统名称");
                }
                else if (string.IsNullOrEmpty(this.txtPassword.Text) && !Isupdate)
                {
                    istrue = false;
                    stb.Append("请填写密码");
                }
            }
            mess = stb.ToString();
            return istrue;
        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            List<V_TCReport> tcrolelist = new List<V_TCReport>();
            tcrolelist = (List<V_TCReport>)ViewState["tcrolelist"];
            if (e.CommandArgument.ToString() != string.Empty)
            {
                Guid id = new Guid(e.CommandArgument.ToString());
                tcrolelist.FirstOrDefault(item => item.urid == id).isdr = 1;
                ViewState["tcrolelist"] = tcrolelist;
                txtUserInfo.Text = string.Empty;
                BindUserRole();
            }
        }



    }
}