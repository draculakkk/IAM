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
    public partial class sapUserCreate : BasePage
    {
        IAMEntities db = new IAMEntities();
        SAPUserInfoDAL _userInfoservices = new SAPUserInfoDAL();
        /// <summary>
        /// 获取当前用户的工号
        /// </summary>
        string _userCode;
        string UserCode
        {
            get
            {
                if (Request.QueryString["uid"] != null)
                {
                    _userCode = Request.QueryString["uid"];
                    return _userCode;
                }
                else
                {
                    _userCode = txtGongHao.Text;
                    return _userCode;
                }
            }
            set
            {
                _userCode = value;
            }
        }

        /// <summary>
        /// 判断是否是编辑
        /// </summary>
        bool IsUpdate
        {
            get
            {
                if (Request.QueryString["uid"] != null)
                    return true;
                else
                    return false;
            }
        }

        public bool IsAdmin = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnQuery.Enabled = base.ReturnUserRole.Admin;
                inputNew.Disabled = !base.ReturnUserRole.Admin;
                btnSave.Enabled = base.ReturnUserRole.Admin;
                if (!base.ReturnUserRole.Admin && !base.ReturnUserRole.SAP && !base.ReturnUserRole.Leader)
                {
                    base.NoRole();
                }

                if (IsUpdate)
                {
                    if (!string.IsNullOrEmpty(UserCode))
                    {
                        //btnQuery_Click(btnQuery, new EventArgs());
                        QueryInfo();
                        if (!string.IsNullOrEmpty(txtGongHao.Text.Trim()))
                        {
                            HREmployee EmployeeModel = new IAMEntities().HREmployee.FirstOrDefault(item => item.code == txtGongHao.Text.Trim() && (item.leavePostsDate == null || item.leavePostsDate > DateTime.Now) == true);
                            if (EmployeeModel != null)
                            {
                                var en = new AccountMapingDAL().GetOne(txtGongHao.Text.Trim(), Unitity.SystemType.SAP.ToString(), Unitity.UserType.员工.ToString());
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
                            }
                        }
                        PageStutas("4");
                    }
                }
                else
                {
                    PageStutas("NaN");
                    List<SAP_User_Role> list = new List<SAP_User_Role>();
                    ViewState["sapuserrole"] = list;

                }
                    QueryUserParameter();
                
            }
            IsAdmin = base.ReturnUserRole.Admin;
        }

        public void PageStutas(string _sta)
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
                    txtLastAndFirstName.Enabled = false;
                    txtDempartMent.Enabled = false;
                    ddlUserLanguage.Enabled = false;
                    txtMoblePhoneNumber.Enabled = false;
                    txtEmail.Enabled = false;
                    ddlUserType.Enabled = false;
                    ddlTypeId.Enabled = false;
                    txtStartDate.Enabled = false;
                    txtEndDate.Enabled = false;
                    dplDECIMAL_POINT_FORMAT.Enabled = false;
                    dplDATE_FORMAT.Enabled = false;
                    dplTIME_FORMAT.Enabled = false;
                    txtOUTPUT_EQUIMENT.Enabled = false;
                    dplNOWTIME_EQUIMENT.Enabled = false;
                    dplOUTPUTED_DELETE.Enabled = false;
                    dplUSER_TIMEZONE.Enabled = false;
                    txtSYSTEM_TIMEZONE.Enabled = false;
                    txtMemo.Enabled = false;
                    break;
                case "1"://离职或不存在
                    txtUserName.Enabled = false;
                    txtLastAndFirstName.Enabled = false;
                    txtDempartMent.Enabled = false;
                    ddlUserLanguage.Enabled = false;
                    txtMoblePhoneNumber.Enabled = false;
                    txtEmail.Enabled = false;
                    ddlUserType.Enabled = false;
                    ddlTypeId.Enabled = false;
                    txtStartDate.Enabled = false;
                    txtEndDate.Enabled = false;
                    dplDECIMAL_POINT_FORMAT.Enabled = false;
                    dplDATE_FORMAT.Enabled = false;
                    dplTIME_FORMAT.Enabled = false;
                    txtOUTPUT_EQUIMENT.Enabled = false;
                    dplNOWTIME_EQUIMENT.Enabled = false;
                    dplOUTPUTED_DELETE.Enabled = false;
                    dplUSER_TIMEZONE.Enabled = false;
                    txtSYSTEM_TIMEZONE.Enabled = false;
                    txtMemo.Enabled = false;
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert(\"不存在该工号员工或该员工已离职 请核实后在添加\");</script>");
                    break;
                case "2":
                    txtUserName.Enabled = false;
                    txtLastAndFirstName.Enabled = false;
                    txtDempartMent.Enabled = false;
                    ddlUserLanguage.Enabled = false;
                    txtMoblePhoneNumber.Enabled = false;
                    txtEmail.Enabled = false;
                    ddlUserType.Enabled = false;
                    ddlTypeId.Enabled = false;
                    txtStartDate.Enabled = false;
                    txtEndDate.Enabled = false;
                    dplDECIMAL_POINT_FORMAT.Enabled = false;
                    dplDATE_FORMAT.Enabled = false;
                    dplTIME_FORMAT.Enabled = false;
                    txtOUTPUT_EQUIMENT.Enabled = false;
                    dplNOWTIME_EQUIMENT.Enabled = false;
                    dplOUTPUTED_DELETE.Enabled = false;
                    dplUSER_TIMEZONE.Enabled = false;
                    txtSYSTEM_TIMEZONE.Enabled = false;
                    txtMemo.Enabled = false;
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert(\"该工号员工已拥有SAP员工类账号 故不可在添加员工类账号\");</script>");
                    break;
                case "3":
                    txtUserName.Enabled = false;
                    txtLastAndFirstName.Enabled = false;
                    txtDempartMent.Enabled = false;
                    ddlUserLanguage.Enabled = false;
                    txtMoblePhoneNumber.Enabled = false;
                    txtEmail.Enabled = false;
                    ddlUserType.Enabled = false;
                    ddlTypeId.Enabled = false;
                    txtStartDate.Enabled = false;
                    txtEndDate.Enabled = false;
                    dplDECIMAL_POINT_FORMAT.Enabled = false;
                    dplDATE_FORMAT.Enabled = false;
                    dplTIME_FORMAT.Enabled = false;
                    txtOUTPUT_EQUIMENT.Enabled = false;
                    dplNOWTIME_EQUIMENT.Enabled = false;
                    dplOUTPUTED_DELETE.Enabled = false;
                    dplUSER_TIMEZONE.Enabled = false;
                    txtSYSTEM_TIMEZONE.Enabled = false;
                    txtMemo.Enabled = false;
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert(\"未能在AD系统中检测到该工号所对应的AD员工类账号 故不可在添加员工类账号 如有疑问请联系AD系统管理员\");</script>");
                    break;
                case "4":
                    if (dplEmployeeType.SelectedValue == "员工")
                    {
                        txtUserName.Enabled = true;
                        txtLastAndFirstName.Enabled = true;
                        txtDempartMent.Enabled = true;
                        ddlUserLanguage.Enabled = true;
                        txtMoblePhoneNumber.Enabled = true;
                        txtEmail.Enabled = true;
                        ddlUserType.Enabled = true;
                        ddlTypeId.Enabled = true;
                        txtStartDate.Enabled = true;
                        txtEndDate.Enabled = true;
                        dplDECIMAL_POINT_FORMAT.Enabled = true;
                        dplDATE_FORMAT.Enabled = true;
                        dplTIME_FORMAT.Enabled = true;
                        txtOUTPUT_EQUIMENT.Enabled = true;
                        dplNOWTIME_EQUIMENT.Enabled = true;
                        dplOUTPUTED_DELETE.Enabled = true;
                        dplUSER_TIMEZONE.Enabled = true;
                        txtSYSTEM_TIMEZONE.Enabled = true;
                        txtMemo.Enabled = true;
                        txtUserName.Attributes.Add("readonly", "true");
                        txtDempartMent.Attributes.Add("readonly", "true");
                        txtMoblePhoneNumber.Attributes.Add("readonly", "true");
                        txtEmail.Attributes.Add("readonly", "true");
                    }
                    else if (dplEmployeeType.SelectedValue == "其他" || dplEmployeeType.SelectedValue == "系统")
                    {
                        txtUserName.Enabled = true;
                        txtLastAndFirstName.Enabled = true;
                        txtDempartMent.Enabled = true;
                        ddlUserLanguage.Enabled = true;
                        txtMoblePhoneNumber.Enabled = true;
                        txtEmail.Enabled = true;
                        ddlUserType.Enabled = true;
                        ddlTypeId.Enabled = true;
                        txtStartDate.Enabled = true;
                        txtEndDate.Enabled = true;
                        dplDECIMAL_POINT_FORMAT.Enabled = true;
                        dplDATE_FORMAT.Enabled = true;
                        dplTIME_FORMAT.Enabled = true;
                        txtOUTPUT_EQUIMENT.Enabled = true;
                        dplNOWTIME_EQUIMENT.Enabled = true;
                        dplOUTPUTED_DELETE.Enabled = true;
                        dplUSER_TIMEZONE.Enabled = true;
                        txtSYSTEM_TIMEZONE.Enabled = true;
                        txtMemo.Enabled = true;
                        txtUserName.Attributes.Remove("readonly");
                        txtDempartMent.Attributes.Remove("readonly");
                        txtMoblePhoneNumber.Attributes.Remove("readonly");
                        txtEmail.Attributes.Remove("readonly");
                    }
                    break;
                case "-2":
                    txtUserName.Enabled = false;
                    txtLastAndFirstName.Enabled = false;
                    txtDempartMent.Enabled = false;
                    ddlUserLanguage.Enabled = false;
                    txtMoblePhoneNumber.Enabled = false;
                    txtEmail.Enabled = false;
                    ddlUserType.Enabled = false;
                    ddlTypeId.Enabled = false;
                    txtStartDate.Enabled = false;
                    txtEndDate.Enabled = false;
                    dplDECIMAL_POINT_FORMAT.Enabled = false;
                    dplDATE_FORMAT.Enabled = false;
                    dplTIME_FORMAT.Enabled = false;
                    txtOUTPUT_EQUIMENT.Enabled = false;
                    dplNOWTIME_EQUIMENT.Enabled = false;
                    dplOUTPUTED_DELETE.Enabled = false;
                    dplUSER_TIMEZONE.Enabled = false;
                    txtSYSTEM_TIMEZONE.Enabled = false;
                    txtMemo.Enabled = false;
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert(\"该工号员工已拥有SAP员工类账号 故不可在添加员工类账号\");</script>");
                    break;
            }

        }

        void QueryHRInfo()
        {
            HREmployee EmployeeModel = new IAMEntities().HREmployee.FirstOrDefault(item => item.code == txtGongHao.Text.Trim() && (item.leavePostsDate == null || item.leavePostsDate > DateTime.Now) == true);
            if (EmployeeModel != null)
            {
                var en = new AccountMapingDAL().GetOne(txtGongHao.Text.Trim(), Unitity.SystemType.SAP.ToString(), Unitity.UserType.员工.ToString());
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
                    string acc, cnname, email, dempartMent, phoneNumber;
                    acc = email = phoneNumber = dempartMent = cnname = string.Empty;
                    using (IAMEntities db = new IAMEntities())
                    {
                        var adentity = (from a in db.AccountMaping
                                        join b in db.AD_UserInfo on a.zhanghao equals b.Accountname
                                        where a.gonghao == txtGongHao.Text.Trim() && a.UserType == "员工" && a.type == "AD"
                                        select new { Accountname = b.Accountname, DempartMent = b.Department, PhoneNumber = b.HRMoblePhone, CnName = b.CnName, Email = b.Email }).FirstOrDefault();
                        if (adentity != null)
                        {
                            cnname = adentity.CnName;
                            email = adentity.Email;
                            dempartMent = adentity.DempartMent;
                            phoneNumber = adentity.PhoneNumber;
                        }
                        else
                        {
                            PageStutas("3");
                            return;
                        }
                    }
                    txtUserName.Text = "shac" + txtGongHao.Text.Trim().Replace("-", "");
                    txtLastAndFirstName.Text = cnname;
                    txtDempartMent.Text = dempartMent;
                    txtEmail.Text = email;
                    txtMoblePhoneNumber.Text = phoneNumber;
                    txtStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }

                PageStutas("4");

            }
            else
            {
                PageStutas("1");
            }
        }

        void QueryInfo()
        {
            V_SAP_UserInfo sapUserInfo;
            if (!IsUpdate)
            {
                sapUserInfo = db.V_SAP_UserInfo.FirstOrDefault(item => item.bGONGHAO == txtGongHao.Text.Trim());
            }
            else
            {
                sapUserInfo = db.V_SAP_UserInfo.FirstOrDefault(item => item.bZHANGHAO == UserCode);
            }

            if (sapUserInfo == null)
                return;
            else
            {
                UserCode = sapUserInfo.aBAPIBNAME;
                txtGongHao.Text = sapUserInfo.bGONGHAO;
                dplEmployeeType.SelectedValue = sapUserInfo.bUserType;//IAM账号类型
            }

            SAP_UserInfo entity = db.SAP_UserInfo.FirstOrDefault(item => item.BAPIBNAME == UserCode);
            if (entity != null)
            {
                txtDempartMent.Text = entity.DEPARTMENT_NAME;
                //txtGongHao.Text = entity.BAPIBNAME;
                //tcUserType.Text = entity.USERTYPE;
                txtUserName.Text = entity.BAPIBNAME;
                txtLastAndFirstName.Text = entity.LASTNAME + entity.FIRSTNAME;
                ddlUserLanguage.SelectedValue = entity.LANGUAGE;
                txtMoblePhoneNumber.Text = entity.MOBLIE_NUMBER;
                txtEmail.Text = entity.E_MAIL;
                ddlUserType.SelectedValue = entity.USERTYPE;
                ddlTypeId.SelectedValue = entity.UCLASSTYPE;
                txtPassword1.Text = entity.PASSWORD;
                txtPassword2.Text = entity.PASSWORD2;
                txtStartDate.Text = entity.START_DATE;
                txtEndDate.Text = entity.END_DATE;
                txtMemo.Text = entity.p2;
                dplDECIMAL_POINT_FORMAT.SelectedValue = entity.DECIMAL_POINT_FORMAT;
                dplDATE_FORMAT.SelectedValue = entity.DATE_FORMAT;
                dplTIME_FORMAT.SelectedValue = entity.TIME_FORMAT;
                txtOUTPUT_EQUIMENT.Text = entity.OUTPUT_EQUIMENT;
                dplNOWTIME_EQUIMENT.SelectedValue = entity.NOWTIME_EQUIMENT;
                dplOUTPUTED_DELETE.SelectedValue = entity.OUTPUTED_DELETE;
                dplUSER_TIMEZONE.SelectedValue = entity.USER_TIMEZONE;
                txtSYSTEM_TIMEZONE.Text = entity.SYSTEM_TIMEZONE;
                QueryUserRole();

            }
        }

        void QueryUserParameter()
        {
            List<SAP_Parameters> list = new List<SAP_Parameters>();
            if (ViewState["sappar"] != null)
            {
                list = (List<SAP_Parameters>)ViewState["sappar"];
            }
            else
            {
                if (IsUpdate)
                {
                    list = new SAP_ParametersDAL().list().Where(item => item.BAPIBNAME == UserCode && item.isdr != 1).ToList();
                    ViewState["sappar"] = list;
                }
                else
                {
                    var lissetting = new SAP_ParametersSettingDAL().list().Where(item => item.isdr != 1).OrderBy(item => item.OrderColumn).ToList();
                    if (lissetting != null)
                    {
                        foreach (var i in lissetting)
                        {
                            SAP_Parameters tm = new SAP_Parameters();
                            tm.id = Guid.NewGuid();
                            tm.PARAMENTERID = i.ParameterId;
                            tm.PARAMENTERVALUE = "";
                            tm.PARAMETERTEXT = "";
                            tm.isdr = 2;
                            list.Add(tm);
                        }
                    }
                    ViewState["sappar"] = list;
                }
            }
            rptParameters.DataSource = list.Where(item => item.isdr != 1);
            rptParameters.DataBind();
        }

        void QueryUserRole()
        {
            List<SAP_User_Role> list = new List<SAP_User_Role>();
            if (ViewState["sapuserrole"] != null)
            {
                list = (List<SAP_User_Role>)ViewState["sapuserrole"];
                repeaterUserRole.DataSource = list.Where(item => item.isdr != 1);
                repeaterUserRole.DataBind();
            }
            else
            {

                list = db.SAP_User_Role.Where(item => item.BAPIBNAME == UserCode && item.isdr != 1).ToList();
                repeaterUserRole.DataSource = list.Where(item => item.isdr != 1);
                repeaterUserRole.DataBind();
                ViewState["sapuserrole"] = list;

            }
        }

        int Create()
        {     
                   
                    
            SAP_UserInfo moduleold = new SAPUserInfoDAL().GetOneTCUser(UserCode);
            SAP_UserInfo entity = new SAP_UserInfo();
            entity.DEPARTMENT_NAME = txtDempartMent.Text;
            //entity.BAPIBNAME = txtGongHao.Text;
            //entity.USERTYPE = tcUserType.Text.Trim();
            entity.BAPIBNAME = txtUserName.Text;
            entity.LASTNAME = entity.FIRSTNAME = txtLastAndFirstName.Text;
            entity.LANGUAGE = ddlUserLanguage.SelectedValue;
            entity.MOBLIE_NUMBER = txtMoblePhoneNumber.Text;
            entity.E_MAIL = txtEmail.Text;
            entity.USERTYPE = ddlUserType.SelectedValue;
            entity.UCLASSTYPE = ddlTypeId.SelectedValue;
            entity.PASSWORD = txtPassword1.Text;
            entity.PASSWORD2 = txtPassword2.Text;
            entity.START_DATE = string.IsNullOrEmpty(txtStartDate.Text) ? DateTime.Now.ToString("yyyy-MM-dd") : txtStartDate.Text;
            entity.END_DATE = txtEndDate.Text;
            entity.LOGIN_LANGUAGE = ddlUserLanguage.SelectedValue;
            entity.p2 = txtMemo.Text.Trim();
            entity.DECIMAL_POINT_FORMAT = dplDECIMAL_POINT_FORMAT.SelectedValue;
            entity.DATE_FORMAT = dplDATE_FORMAT.SelectedValue;
            entity.TIME_FORMAT = dplTIME_FORMAT.SelectedValue;
            entity.OUTPUT_EQUIMENT = txtOUTPUT_EQUIMENT.Text;
            entity.NOWTIME_EQUIMENT = dplNOWTIME_EQUIMENT.SelectedValue;
            entity.OUTPUTED_DELETE = dplOUTPUTED_DELETE.SelectedValue;
            entity.USER_TIMEZONE = dplUSER_TIMEZONE.SelectedValue;
            entity.SYSTEM_TIMEZONE = txtSYSTEM_TIMEZONE.Text;
            entity.PARAMENTERID = "";
            entity.PARAMENTERVALUE = "";
            entity.PARAMETERTEXT = "";

            var accountMaping = new AccountMaping()
            {
                type = Unitity.SystemType.SAP.ToString(),
                zhanghao = txtUserName.Text.Trim(),
                gonghao = txtGongHao.Text.Trim(),
                UserType = dplEmployeeType.SelectedValue.Trim(),
                id = Guid.NewGuid()
            };

            List<SAP_User_Role> listr = (List<SAP_User_Role>)ViewState["sapuserrole"];
            List<SAP_User_Role> listold = new List<SAP_User_Role>();
            listr.ForEach(item=>listold.Add(BLL.Extensions.Clone<SAP_User_Role>(item)));
            foreach (RepeaterItem i in repeaterUserRole.Items)
            {
                TextBox st = (TextBox)i.FindControl("rep_txtstartdate");
                TextBox en = (TextBox)i.FindControl("rep_enddate");
                HiddenField hid = (HiddenField)i.FindControl("rep_hiddenid");
                Guid id = new Guid(hid.Value);
                if (!string.IsNullOrEmpty(st.Text.Trim()))
                {
                    listr.FirstOrDefault(item => item.ID == id).START_DATE = st.Text.Trim();
                }

                if (!string.IsNullOrEmpty(en.Text.Trim()))
                {
                    listr.FirstOrDefault(item => item.ID == id).END_DATE = en.Text.Trim();
                }
            }

            List<SAP_Parameters> lispa = new List<SAP_Parameters>();
            foreach (RepeaterItem it in rptParameters.Items)
            {
                HiddenField id = (HiddenField)it.FindControl("hiddenid");
                TextBox txtwenben = (TextBox)it.FindControl("txtwenben");
                TextBox txtparvalue = (TextBox)it.FindControl("txtparametersvalue");
                TextBox txtparid = (TextBox)it.FindControl("txtparametersID");
                lispa.Add(new SAP_Parameters()
                {
                    id = new Guid(id.Value),
                    PARAMENTERID = txtparid.Text.Trim(),
                    PARAMENTERVALUE = txtparvalue.Text.Trim(),
                    PARAMETERTEXT = txtwenben.Text.Trim(),
                    isdr = 2,
                    BAPIBNAME = txtUserName.Text.Trim()
                });
            }

            try
            {
                List<SAP_Parameters> tmp = (List<SAP_Parameters>)ViewState["sappar"];
                foreach (var tim in tmp.Where(item => item.isdr == 1))
                {
                    lispa.Add(tim);
                }
            }
            catch
            { }

            //BLL.AddUserMail addmail = new BLL.AddUserMail();
            //addmail.Actioner = base.UserInfo.adname;
            //addmail.SystemName = Unitity.SystemType.SAP.ToString();
            //addmail.SystemType = dplEmployeeType.SelectedValue;
            //addmail.UserName = entity.BAPIBNAME;
            IAM2.SAP_BLL.SAP_MailService sapmail = new IAM2.SAP_BLL.SAP_MailService(entity, moduleold);
            sapmail.SAPParameters = lispa;
            sapmail.SAPRoleList = listr;
            if (_userInfoservices.CreateOrUpdate(entity, listr, lispa) > 0)
            {
                if (entity.END_DATE == DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    BLL.ActionLog.DisabledLog(base.UserInfo.adname, entity.BAPIBNAME, Unitity.SystemType.SAP.ToString(), dplEmployeeType.SelectedValue);
                    //addmail.UserInfo = new BLL.CompareEntity<SAP_UserInfo>().ReturnCompareStringForMailUpdate(moduleold, entity, Unitity.SystemType.SAP);
                    //new BLL.MailServices().DisabledUserMail(addmail);
                }

                if (IsUpdate)
                {
                    new BLL.ActionLog().EditLog<SAP_UserInfo>(base.UserInfo.adname, entity.BAPIBNAME, Unitity.SystemType.SAP.ToString(), dplEmployeeType.SelectedValue, moduleold, entity);
                    //addmail.UserInfo = new BLL.CompareEntity<SAP_UserInfo>().ReturnCompareStringForMailUpdate(moduleold, entity, Unitity.SystemType.SAP);
                    //addmail.RoleString = BLL.CompareRoleList.SapRoleMess(listr,listold, moduleold.BAPIBNAME) + "<br/>" + BLL.CompareRoleList.SapParemters(lispa, moduleold.BAPIBNAME);
                    //new BLL.MailServices().UpdateUserMail(addmail);
                    var oldacc = new AccountMapingDAL().GetOne(entity.BAPIBNAME, "SAP");
                    return new AccountMapingDAL().UpdateAccountMaping(accountMaping, oldacc);
                }
                else
                {
                    BLL.ActionLog.CreateLog(base.UserInfo.adname, entity.BAPIBNAME, Unitity.SystemType.SAP.ToString(), dplEmployeeType.SelectedValue);
                    //addmail.UserInfo = new BLL.CompareEntity<SAP_UserInfo>().ReturnCompareStringForMailAdd(entity, Unitity.SystemType.SAP);
                    //addmail.RoleString = BLL.CompareRoleList.SapRoleMess(listr,listold, entity.BAPIBNAME) + "<br/>" + BLL.CompareRoleList.SapParemters(lispa, entity.BAPIBNAME);
                    //new BLL.MailServices().CreateUserMail(addmail);
                    sapmail.CreateMail(base.UserInfo.adname, dplEmployeeType.SelectedValue);
                    return new AccountMapingDAL().Add(accountMaping);
                }

            }
            else
                return 0;
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if (IsUpdate)
            {
                QueryInfo();
            }
            if (!string.IsNullOrEmpty(txtGongHao.Text.Trim()))
            {
                QueryHRInfo();
                //IsReadOnlyNewBuild();
            }

            if (dplEmployeeType.SelectedValue == "系统" && string.IsNullOrEmpty(txtGongHao.Text.Trim()))
            {
                PageStutas("4");
            }
            else if (dplEmployeeType.SelectedValue != "系统" && string.IsNullOrEmpty(txtGongHao.Text.Trim()))
            {
                PageStutas("NaN");
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
                if (string.IsNullOrEmpty(this.txtDempartMent.Text))
                {
                    istrue = false;
                    stb.Append("请填写部门");
                }
                else if (string.IsNullOrEmpty(this.txtEmail.Text))
                {
                    istrue = false;
                    stb.Append("请填写EMail");
                }
                else if (string.IsNullOrEmpty(this.txtStartDate.Text))
                {
                    istrue = false;
                    stb.Append("请填写有效期从");
                }
                else if (string.IsNullOrEmpty(this.txtUserName.Text))
                {
                    istrue = false;
                    stb.Append("请填写小数点格式");
                }
                else if (string.IsNullOrEmpty(this.txtLastAndFirstName.Text))
                {
                    istrue = false;
                    stb.Append("请填写用户名");
                }
                else if (string.IsNullOrEmpty(this.txtGongHao.Text.Trim()))
                {
                    istrue = false;
                    stb.Append("请填写员工");
                }
            }
            if (dplEmployeeType.SelectedValue == "其他")
            {
                if (string.IsNullOrEmpty(this.txtStartDate.Text))
                {
                    istrue = false;
                    stb.Append("请填写有效期从");
                }
                else if (string.IsNullOrEmpty(this.txtUserName.Text))
                {
                    istrue = false;
                    stb.Append("请填写小数点格式");
                }
                else if (string.IsNullOrEmpty(this.txtLastAndFirstName.Text))
                {
                    istrue = false;
                    stb.Append("请填写用户名");
                }
                else if (string.IsNullOrEmpty(this.txtGongHao.Text.Trim()))
                {
                    istrue = false;
                    stb.Append("请填写员工");
                }
            }

            if (dplEmployeeType.SelectedValue == "系统")
            {

                if (string.IsNullOrEmpty(this.txtStartDate.Text))
                {
                    istrue = false;
                    stb.Append("请填写有效期从");
                }
                else if (string.IsNullOrEmpty(this.txtUserName.Text))
                {
                    istrue = false;
                    stb.Append("请填写小数点格式");
                }
                else if (string.IsNullOrEmpty(this.txtLastAndFirstName.Text))
                {
                    istrue = false;
                    stb.Append("请填写用户名");
                }
            }
            mess = stb.ToString();
            return istrue;

        }

        protected void btnOnLoadRoleInfo_ServerClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUserInfo.Text.Trim()))
            {
                string[] _value = txtUserInfo.Text.Trim().Split('^');
                SAP_User_Role tmp = new SAP_User_Role() { isdr = 2, ID = Guid.NewGuid(), ROLEID = _value[0], ROLENAME = _value[1], START_DATE = _value[2], END_DATE = _value[3], BAPIBNAME = txtUserName.Text.Trim() };
                List<SAP_User_Role> list = (List<SAP_User_Role>)ViewState["sapuserrole"];
                if (list != null)
                {
                    var ent = list.FirstOrDefault(item => item.ROLEID.Trim() == tmp.ROLEID.Trim() && item.ROLENAME.Trim() == tmp.ROLENAME.Trim() && item.isdr != 1);
                    if (ent == null)
                        list.Add(tmp);
                    else
                        ScriptManager.RegisterStartupScript(updatepanel1, this.GetType(), "", "<script>alert('已存在该" + tmp.ROLENAME + "角色');</script>", false);
                    ViewState["sapuserrole"] = list;
                }
                else
                {
                    list = new List<SAP_User_Role>();
                    var ent = list.FirstOrDefault(item => item.ROLEID == tmp.ROLEID && item.ROLENAME == tmp.ROLENAME && item.isdr != 1);
                    if (ent == null)
                        list.Add(tmp);
                    else
                        ScriptManager.RegisterStartupScript(updatepanel1, this.GetType(), "", "<script>alert('已存在该" + tmp.ROLENAME + "角色');</script>", false);

                    ViewState["sapuserrole"] = list;
                }
                txtUserInfo.Text = string.Empty;
                QueryUserRole();

            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {

            this.txtGongHao.ReadOnly = false;
            this.txtUserName.ReadOnly = false;
            this.txtMoblePhoneNumber.ReadOnly = false;
            this.txtDempartMent.ReadOnly = false;
            this.txtEmail.ReadOnly = false;
            string mess = "";
            if (!IsMustOptions(out mess) && !IsUpdate)
            {
                txtMoblePhoneNumber.ReadOnly = dplEmployeeType.SelectedValue == "员工" ? true : false;
                txtDempartMent.ReadOnly = dplEmployeeType.SelectedValue == "员工" ? true : false;
                txtEmail.ReadOnly = dplEmployeeType.SelectedValue == "员工" ? true : false;
                txtUserName.ReadOnly = dplEmployeeType.SelectedValue == "员工" ? true : false;
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + mess + "');", true);
                //btnQuery_Click(btnQuery,new EventArgs());
                return;
            }

            if (Create() > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('添加成功!');window.close();", true);
            }
        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "role")
            {
                List<SAP_User_Role> list = new List<SAP_User_Role>();
                list = (List<SAP_User_Role>)ViewState["sapuserrole"];
                if (e.CommandArgument.ToString() != string.Empty)
                {
                    Guid id = new Guid(e.CommandArgument.ToString());
                    list.FirstOrDefault(o => o.ID == id).isdr = 1;
                    ViewState["sapuserrole"] = list;
                    QueryUserRole();
                }
            }
            else if (e.CommandName == "par")
            {
                List<SAP_Parameters> list = new List<SAP_Parameters>();
                list = (List<SAP_Parameters>)ViewState["sappar"];
                if (e.CommandArgument.ToString() != string.Empty)
                {
                    Guid id = new Guid(e.CommandArgument.ToString());
                    list.FirstOrDefault(item => item.id == id).isdr = 1;
                    ViewState["sappar"] = list;
                    QueryUserParameter();
                }
            }
        }

        protected void Button2_ServerClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBox1.Text.Trim()))
            {
                string[] _value = TextBox1.Text.Trim().Split('^');

                SAP_Parameters tmp = new SAP_Parameters();
                tmp.id = Guid.NewGuid();
                tmp.PARAMENTERID = _value[0];
                tmp.PARAMENTERVALUE = _value[1];
                tmp.PARAMETERTEXT = _value[2];
                tmp.isdr = 2;

                List<SAP_Parameters> list = null;
                list = (List<SAP_Parameters>)ViewState["sappar"];

                if (list != null)
                {
                    list.Add(tmp);
                    ViewState["sappar"] = list;
                }
                else
                {
                    list = new List<SAP_Parameters>();
                    list.Add(tmp);
                    ViewState["sappar"] = list;
                }
                TextBox1.Text = string.Empty;
                QueryUserParameter();

            }
        }

    }
}