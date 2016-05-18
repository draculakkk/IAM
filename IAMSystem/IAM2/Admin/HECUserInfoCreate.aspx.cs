using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseDataAccess;
using IAMEntityDAL;
using System.Web.UI.HtmlControls;

namespace IAM.Admin
{    //V_HECUSER_Role.isdr=1 删除 2新增  0原始值
    public partial class HECUserInfoCreate : BasePage
    {
        HECUserDAL _hecuserServices = new HECUserDAL();

        HECUserInfoDAL _hecUserInfoServices = new HECUserInfoDAL();
        IAMEntities db = new IAMEntities();
        AccountMapingDAL _accountServices = new AccountMapingDAL();

        bool IsUpdate
        {
            get
            {
                if (Request.QueryString["usercd"] != null)
                    return true;
                else
                    return false;
            }
        }

        string UserCD
        {
            get
            {
                if (Request.QueryString["usercd"] != null)
                    return Request.QueryString["usercd"];
                else
                    return string.Empty;
            }
        }

        public bool IsAdmin = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                if (Request.QueryString["usercd"] != null)
                {
                    txtEmployeeNumber.Text = Request.QueryString["usercd"];
                    QueryHECinfo();
                    QueryUserInfo();
                    if (!string.IsNullOrEmpty(txtgonghao.Text.Trim()))
                    {
                        HREmployee EmployeeModel = new IAMEntities().HREmployee.FirstOrDefault(item => item.code == txtgonghao.Text.Trim());
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
                            }
                        }
                    }
                    PageStutas("y");
                }
                else
                {
                    List<V_HECUSER_Role> userInfoList = new List<V_HECUSER_Role>();
                    ViewState["UserInfoList"] = userInfoList;
                    PageStutas("NaN");
                }

                btnQuery.Enabled = base.ReturnUserRole.Admin;
                inputAddNewRole.Disabled = !base.ReturnUserRole.Admin;
                btnSave.Enabled = base.ReturnUserRole.Admin;
                if (!base.ReturnUserRole.Admin)
                {
                    foreach (RepeaterItem i in repeaterUserInfo.Items)
                    {
                        TextBox rep_txtstartdate = (TextBox)i.FindControl("rep_txtstartdate");
                        TextBox rep_enddate = (TextBox)i.FindControl("rep_enddate");
                        Button btnDelete = (Button)i.FindControl("btnDelete");
                        btnDelete.Enabled = rep_enddate.Enabled = rep_txtstartdate.Enabled = base.ReturnUserRole.Admin;
                    }
                }

                if (!base.ReturnUserRole.HEC && !base.ReturnUserRole.Admin && !base.ReturnUserRole.Leader)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('你无权限查看当前页面');window.location.href='./hremployeemanager.aspx';", true);
                }
            }

            IsAdmin = base.ReturnUserRole.Admin;
        }

        void PageStutas(string _stype)
        {
            txtName.Attributes.Add("readonly", "true");
            txtGangwei.Attributes.Add("readonly", "true");
            txtPartment.Attributes.Add("readonly", "true");
            txtPrePartment.Attributes.Add("readonly", "true");
            txtPhoneNumber.Attributes.Add("readonly", "true");
            txtComeDate.Attributes.Add("readonly", "true");
            txtOutDate.Attributes.Add("readonly", "true");
            switch (_stype)
            {
                case "NaN":
                    txtEmployeeNumber.Enabled = false;
                    txtEmployeeCode.Enabled = false;
                    txtUserChinesename.Enabled = false;
                    txtUserDescritpion.Enabled = false;
                    txtStartDate.Enabled = false;
                    txtEndDate.Enabled = false;
                    chkIsSuoding.Enabled = false;
                    txtDongjieTime.Enabled = false;
                    txtMemo.Enabled = false;
                    break;
                case "1":
                    txtEmployeeNumber.Enabled = false;
                    txtEmployeeCode.Enabled = false;
                    txtUserChinesename.Enabled = false;
                    txtUserDescritpion.Enabled = false;
                    txtStartDate.Enabled = false;
                    txtEndDate.Enabled = false;
                    chkIsSuoding.Enabled = false;
                    txtDongjieTime.Enabled = false;
                    txtMemo.Enabled = false;
                    Response.Write("<script>alert(\"不存在该工号员工或该员工已离职 请核实后在添加\");</script>");
                    break;
                case "2":
                    txtEmployeeNumber.Enabled = false;
                    txtEmployeeCode.Enabled = false;
                    txtUserChinesename.Enabled = false;
                    txtUserDescritpion.Enabled = false;
                    txtStartDate.Enabled = false;
                    txtEndDate.Enabled = false;
                    chkIsSuoding.Enabled = false;
                    txtDongjieTime.Enabled = false;
                    txtMemo.Enabled = false;
                    Response.Write("<script>alert(\"该工号员工已拥有HEC员工类账号 故不可在添加员工类账号\");</script>");
                    break;
                case "3":
                    txtEmployeeNumber.Enabled = false;
                    txtEmployeeCode.Enabled = false;
                    txtUserChinesename.Enabled = false;
                    txtUserDescritpion.Enabled = false;
                    txtStartDate.Enabled = false;
                    txtEndDate.Enabled = false;
                    chkIsSuoding.Enabled = false;
                    txtDongjieTime.Enabled = false;
                    txtMemo.Enabled = false;
                    Response.Write("<script>alert(\"未能在AD系统中检测到该工号所对应的HEC员工类账号\\n故不可在添加员工类账号\\n如有疑问请联系AD系统管理员\");</script>");
                    break;
                case "-2":
                    txtEmployeeNumber.Enabled = false;
                    txtEmployeeCode.Enabled = false;
                    txtUserChinesename.Enabled = false;
                    txtUserDescritpion.Enabled = false;
                    txtStartDate.Enabled = false;
                    txtEndDate.Enabled = false;
                    chkIsSuoding.Enabled = false;
                    txtDongjieTime.Enabled = false;
                    txtMemo.Enabled = false;
                    Response.Write("<script>alert(\"该工号员工已拥有HEC员工类账号\\n故不可在添加员工类账号\");</script>");
                    break;
                case "4":
                    txtEmployeeNumber.Enabled = true;
                    txtEmployeeCode.Enabled = true;
                    txtUserChinesename.Enabled = true;
                    txtUserDescritpion.Enabled = true;
                    txtStartDate.Enabled = true;
                    txtEndDate.Enabled = true;
                    chkIsSuoding.Enabled = true;
                    txtDongjieTime.Enabled = true;
                    txtMemo.Enabled = true;
                    if (dplEmployeeType.SelectedValue == Unitity.UserType.员工.ToString())
                    {
                        txtEmployeeNumber.Attributes.Add("readonly", "true");
                        txtEmployeeNumber.Attributes.Add("readonly", "true");
                    }
                    else
                    {
                        txtEmployeeNumber.Attributes.Remove("readonly");
                        txtEmployeeNumber.Attributes.Remove("readonly");
                    }
                    break;
                case "y":
                    txtEmployeeNumber.Enabled = true;
                    txtEmployeeCode.Enabled = true;
                    txtUserChinesename.Enabled = true;
                    txtUserDescritpion.Enabled = true;
                    txtStartDate.Enabled = true;
                    txtEndDate.Enabled = true;
                    chkIsSuoding.Enabled = true;
                    txtDongjieTime.Enabled = true;
                    txtMemo.Enabled = true;
                    if (dplEmployeeType.SelectedValue == Unitity.UserType.员工.ToString())
                    {
                        txtEmployeeNumber.Attributes.Add("readonly", "true");
                        txtEmployeeNumber.Attributes.Add("readonly", "true");
                    }
                    else
                    {
                        txtEmployeeNumber.Attributes.Remove("readonly");
                        txtEmployeeNumber.Attributes.Remove("readonly");
                    }
                    break;
            }
        }

        void QueryHECinfo()
        {
            HEC_User module = _hecuserServices.GetOneHECUser(UserCD);
            AccountMaping mapping = new AccountMapingDAL().GetOne(UserCD,Unitity.SystemType.HEC.ToString());
            if (module != null)    //根据usercd 获取hecuser详细信息
            {
                if (module.END_DATE == null)
                    module.END_DATE = new DateTime(2099, 12, 31);
                if (module.ISDISABLED == null || module.ISDISABLED == 0)
                    module.ISDISABLED = 0;
                txtUserDescritpion.Text = module.DESCRIPTION;
                txtgonghao.Text = mapping.gonghao;//module.USER_CODE;
                txtUserChinesename.Text = module.USER_NAME;
                txtStartDate.Text = Convert.ToDateTime(module.START_DATE).ToString("yyyy-MM-dd");
                txtEndDate.Text = module.END_DATE == null ? "2099-12-31" : Convert.ToDateTime(module.END_DATE).ToString("yyyy-MM-dd");
                txtEmployeeCode.Text = module.USER_CODE;
                chkIsSuoding.Checked = module.ISDISABLED == 1 ? true : false;
                txtMemo.Text = module.Memo;
                txtDongjieTime.Text = Convert.ToDateTime(module.DISABLED_DATE).ToString("yyyy-MM-dd");
                var accountmaping = _accountServices.GetOne(module.User_CD, Unitity.SystemType.HEC.ToString());
                if (accountmaping != null)
                {
                    dplEmployeeType.SelectedValue = accountmaping.UserType;
                    mappingId.Value = accountmaping.id.ToString();
                }
            }
        }

        void QueryHRInfo()
        {
            HREmployee EmployeeModel = new IAMEntities().HREmployee.FirstOrDefault(item => item.code == txtgonghao.Text.Trim() && (item.leavePostsDate == null || item.leavePostsDate > DateTime.Now) == true);
            if (EmployeeModel != null)
            {
                var en = new AccountMapingDAL().GetOne(txtgonghao.Text.Trim(), Unitity.SystemType.HEC.ToString(), Unitity.UserType.员工.ToString());
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
                                        join b in db.AD_UserInfo on a.gonghao equals b.Id
                                        where a.gonghao == txtgonghao.Text.Trim() && a.UserType == "员工" && a.type == "AD"
                                        select new { Accountname = b.Accountname, CnName = b.CnName }).FirstOrDefault();
                        if (adentity != null)
                        {
                            acc = adentity.Accountname;
                            cnname = adentity.CnName;
                        }
                    }

                    txtEmployeeNumber.Text = txtgonghao.Text;

                    if (acc != string.Empty && cnname != string.Empty)
                    {
                        txtUserChinesename.Text = cnname;
                        txtEmployeeNumber.Text = txtEmployeeCode.Text = txtgonghao.Text;
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

        void QueryUserGangwei(bool deleting=false)
        {
            List<HEC_User_Gangwei> listGangwei = new List<HEC_User_Gangwei>();
            if ((ViewState["UserGangWei"] == null || ((List<HEC_User_Gangwei>)ViewState["UserGangWei"]).Count <= 0) && deleting == false)
            {
                listGangwei = db.HEC_User_Gangwei.Where(x => x.EMPLOYEE_CODE == txtEmployeeNumber.Text.Trim()).ToList();
                ViewState["UserGangWei"] = listGangwei;
            }
            else
            {
                listGangwei = (List<HEC_User_Gangwei>)ViewState["UserGangWei"];
            }
            repeatergangwei.DataSource = listGangwei.Where(x=>x.isdelete!=1) ;
            repeatergangwei.DataBind();
        }

        void QueryUserInfo(bool deleting = false)
        {
            List<V_HECUSER_Role> userInfoList = new List<V_HECUSER_Role>();
            if ((ViewState["UserInfoList"] == null || ((List<V_HECUSER_Role>)ViewState["UserInfoList"]).Count == 0) && deleting == false)
            {
                userInfoList = db.V_HECUSER_Role.Where(itm => itm.uUSERNAME.Trim() == txtEmployeeNumber.Text.Trim() && itm.isdr != 1).ToList();
                ViewState["UserInfoList"] = userInfoList;
            }
            else
            {
                userInfoList = (List<V_HECUSER_Role>)ViewState["UserInfoList"];
            }

            if (txtUserInfo.Text != string.Empty)
            {
                string[] arrUserInfo = txtUserInfo.Text.Split('^');
                V_HECUSER_Role newmdule = new V_HECUSER_Role();
                newmdule.uID = new Guid(arrUserInfo[0]);
                newmdule.rROLECODE = arrUserInfo[1];
                newmdule.rROLENAME = arrUserInfo[2];
                newmdule.cCOMPANYCODE = arrUserInfo[5];
                newmdule.cCOMPNYFULLNAME = arrUserInfo[6];
                newmdule.rSTARTDATE = arrUserInfo[3];
                newmdule.rENDDATE = arrUserInfo[4];
                newmdule.isdr = 2;
                newmdule.uROLESTARTDATE = arrUserInfo[7];
                newmdule.uROLEENDDATE = arrUserInfo[8];

                var ent = userInfoList.FirstOrDefault(item => item.rROLECODE.Trim() == newmdule.rROLECODE.Trim() && item.rROLENAME.Trim() == newmdule.rROLENAME.Trim() && item.cCOMPANYCODE.Trim() == item.cCOMPANYCODE.Trim() && item.cCOMPNYFULLNAME.Trim() == newmdule.cCOMPNYFULLNAME.Trim() && item.isdr != 1);
                if (ent == null)
                    userInfoList.Add(newmdule);
                else
                    ScriptManager.RegisterStartupScript(updatepanel1, this.GetType(), "", "<script>alert('已存在" + newmdule.rROLENAME + "该角色');</script>", false);

                ViewState["UserInfoList"] = userInfoList;
            }
            repeaterUserInfo.DataSource = userInfoList.Where(item => item.isdr != 1);
            repeaterUserInfo.DataBind();
            QueryUserGangwei();
        }


        protected void btnOnLoadRoleInfo_ServerClick(object sender, EventArgs e)
        {
            QueryUserInfo();
        }


        protected void btnQuery_Click(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(txtgonghao.Text.Trim()))
            {
                QueryHRInfo();
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

        int Create()
        {
            HEC_User moduleold = _hecuserServices.GetOneHECUser(UserCD);
            if (moduleold != null)
            {
                if (moduleold.END_DATE == null)
                    moduleold.END_DATE = new DateTime(2099, 12, 31);
                if (moduleold.ISDISABLED == null || moduleold.ISDISABLED == 0)
                    moduleold.ISDISABLED = 0;
            }
            HEC_User module = new HEC_User();
            module.USER_TYPE = dplEmployeeType.SelectedValue;
            module.DESCRIPTION = txtUserDescritpion.Text;
            module.USER_NAME = txtUserChinesename.Text;
            module.START_DATE = string.IsNullOrEmpty(txtStartDate.Text) ? DateTime.Now : Convert.ToDateTime(txtStartDate.Text);
            module.END_DATE = string.IsNullOrEmpty(txtEndDate.Text) ? (DateTime?)null : Convert.ToDateTime(txtEndDate.Text);
            module.USER_CODE = txtEmployeeCode.Text;
            module.ISDISABLED = chkIsSuoding.Checked ? 1 : 0;
            module.User_CD = txtEmployeeNumber.Text.Trim();
            module.Memo = txtMemo.Text.Trim();
            if (chkIsSuoding.Checked)
            {
                module.DISABLED_DATE = string.IsNullOrEmpty(txtDongjieTime.Text.Trim()) ? DateTime.Now : Convert.ToDateTime(txtDongjieTime.Text);
                module.ISDISABLED = 1;
                module.DISABLED_DATE = string.IsNullOrEmpty(txtDongjieTime.Text.Trim()) ? DateTime.Now : Convert.ToDateTime(txtDongjieTime.Text);
            }
            else
            {
                module.ISDISABLED = 0;
                module.DISABLED_DATE = null;
                module.ISDISABLED = 0;
                module.DISABLED_DATE = (DateTime?)null;
            }
            module.createTime = DateTime.Now;

            AccountMaping accountMaping = new AccountMaping()
            {
                id = string.IsNullOrEmpty(mappingId.Value.Trim()) == true ? Guid.NewGuid() : new Guid(mappingId.Value),
                UserType = dplEmployeeType.SelectedValue.Trim(),
                type = Unitity.SystemType.HEC.ToString(),
                zhanghao = module.User_CD,
                gonghao = txtgonghao.Text.Trim()
            };
            int _r = _hecuserServices.UpdateOrCreate(module);
            List<V_HECUSER_Role> userlist = new List<V_HECUSER_Role>();
            userlist = (List<V_HECUSER_Role>)ViewState["UserInfoList"];
            List<V_HECUSER_Role> oldlist = new List<V_HECUSER_Role>();
            userlist.ForEach(x => oldlist.Add(BLL.Extensions.Clone<V_HECUSER_Role>(x)));
            foreach (RepeaterItem i in repeaterUserInfo.Items)
            {
                TextBox st = (TextBox)i.FindControl("rep_txtstartdate");
                TextBox end = (TextBox)i.FindControl("rep_enddate");
                HiddenField hidid = (HiddenField)i.FindControl("rep_hiddenid");
                Guid id = new Guid(hidid.Value);
                if (!string.IsNullOrEmpty(st.Text.Trim()))
                {
                    userlist.FirstOrDefault(item => item.uID == id).uROLESTARTDATE = st.Text.Trim();
                }
                else
                {
                    userlist.FirstOrDefault(item => item.uID == id).uROLESTARTDATE = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (!string.IsNullOrEmpty(end.Text.Trim()))
                {
                    userlist.FirstOrDefault(item => item.uID == id).uROLEENDDATE = end.Text.Trim();
                }
                else
                {
                    userlist.FirstOrDefault(item => item.uID == id).uROLEENDDATE = "";
                }

            }

            List<HEC_User_Gangwei> newgangweilist = new List<HEC_User_Gangwei>();
            newgangweilist = (List<HEC_User_Gangwei>)ViewState["UserGangWei"];
            List<HEC_User_Gangwei> oldgangweilist = new List<HEC_User_Gangwei>();
            newgangweilist.ForEach(x => oldgangweilist.Add(BLL.Extensions.Clone<HEC_User_Gangwei>(x)));
            foreach (RepeaterItem x in repeatergangwei.Items)
            {
                HtmlInputRadioButton enabledy = (HtmlInputRadioButton)x.FindControl("enabledY");
                HtmlInputRadioButton enabledn = (HtmlInputRadioButton)x.FindControl("enabledN");
                HtmlInputRadioButton primaryy = (HtmlInputRadioButton)x.FindControl("primarykeyY");
                HtmlInputRadioButton primaryn = (HtmlInputRadioButton)x.FindControl("primarykeyN");
                HiddenField hdid = (HiddenField)x.FindControl("hhdgangweiid");
                Guid iid=new Guid(hdid.Value);
                if (enabledy.Checked)
                    newgangweilist.FirstOrDefault(i => i.ID == iid).ENABLED_FLAG = "Y";
                if (enabledn.Checked)
                    newgangweilist.FirstOrDefault(i => i.ID == iid).ENABLED_FLAG = "N";
                if (primaryy.Checked)
                    newgangweilist.FirstOrDefault(i => i.ID == iid).PRIMARY_POSITION_FLAG = "Y";
                if (primaryn.Checked)
                    newgangweilist.FirstOrDefault(i => i.ID == iid).PRIMARY_POSITION_FLAG = "N";
                newgangweilist.FirstOrDefault(i => i.ID == iid).EMPLOYEE_CODE = txtEmployeeNumber.Text.Trim();
                newgangweilist.FirstOrDefault(i => i.ID == iid).EMPLOYEE_NAME = txtUserChinesename.Text.Trim();
            }

            if (chkIsSuoding.Checked)
            {
                module.END_DATE = DateTime.Now;
            }
            bool _s = new HECUserInfoDAL().AddRole(userlist, module);
            new HECUserGangweiDAL().PageAddUserGangwei(newgangweilist);
            BLL.AddUserMail addmail = new BLL.AddUserMail();
            addmail.Actioner = base.UserInfo.adname;
            addmail.SystemName = Unitity.SystemType.HEC.ToString();
            addmail.SystemType = dplEmployeeType.SelectedValue;
            addmail.UserName = module.User_CD;


            if (IsUpdate)
            {

                _accountServices.ChangeAcc(accountMaping);
                new BLL.ActionLog().EditLog<HEC_User>(base.UserInfo.adname, module.User_CD, Unitity.SystemType.HEC.ToString(), dplEmployeeType.SelectedValue, moduleold, module);
                addmail.UserInfo = new BLL.CompareEntity<HEC_User>().ReturnCompareStringForMailUpdate(moduleold, module, Unitity.SystemType.HEC);
                addmail.RoleString = BLL.CompareRoleList.HECRoleMess(userlist, oldlist, moduleold.User_CD) + BLL.CompareRoleList.HECGangWeiMess(newgangweilist,oldgangweilist,moduleold.User_CD) ;

                new BLL.MailServices().UpdateUserMail(addmail);
            }
            else
            {
                _accountServices.Add(accountMaping);
                BLL.ActionLog.CreateLog(base.UserInfo.adname, module.User_CD, Unitity.SystemType.HEC.ToString(), dplEmployeeType.SelectedValue);
                addmail.UserInfo = new BLL.CompareEntity<HEC_User>().ReturnCompareStringForMailAdd(module, Unitity.SystemType.HEC);
                addmail.RoleString = BLL.CompareRoleList.HECRoleMess(userlist, oldlist, module.User_CD)+BLL.CompareRoleList.HECGangWeiMess(newgangweilist,oldgangweilist,module.User_CD);
                new BLL.MailServices().CreateUserMail(addmail);
            }
            if (chkIsSuoding.Checked)
            {
                BLL.ActionLog.DisabledLog(base.UserInfo.adname, module.User_CD, Unitity.SystemType.HEC.ToString(), dplEmployeeType.SelectedValue);
                addmail.UserInfo = new BLL.CompareEntity<HEC_User>().ReturnCompareStringForMailUpdate(moduleold, module, Unitity.SystemType.HEC);
                new BLL.MailServices().DisabledUserMail(addmail);
            }
            if (_r > 0 && _s == true)
                return 1;
            else
                return 0;
        }

        bool CheckValue(out string mess)
        {
            bool istrue = true;
            System.Text.StringBuilder stb = new System.Text.StringBuilder();
            string usety = dplEmployeeType.SelectedValue;
            if (usety == "员工")
            {
                if (string.IsNullOrEmpty(txtEmployeeNumber.Text.Trim()))
                {
                    istrue = false;
                    stb.Append("账号不能为空");
                }
                else if (string.IsNullOrEmpty(txtEmployeeCode.Text.Trim()))
                {
                    istrue = false;
                    stb.Append("员工工号不能为空");
                }
                else if (string.IsNullOrEmpty(txtUserChinesename.Text.Trim()))
                {
                    istrue = false;
                    stb.Append("账号姓名不能为空");
                }
                else if (string.IsNullOrEmpty(txtStartDate.Text.Trim()))
                {
                    istrue = false;
                    stb.Append("有效期从不能为空");
                }
                else if (string.IsNullOrEmpty(txtgonghao.Text.Trim()))
                {
                    istrue = false;
                    stb.Append("员工工号不能为空");
                }

            }
            else if (usety == "其他")
            {
                if (string.IsNullOrEmpty(txtEmployeeNumber.Text.Trim()))
                {
                    istrue = false;
                    stb.Append("账号不能为空");
                }
                else if (string.IsNullOrEmpty(txtEmployeeCode.Text.Trim()))
                {
                    istrue = false;
                    stb.Append("员工工号不能为空");
                }
                else if (string.IsNullOrEmpty(txtUserDescritpion.Text.Trim()))
                {
                    istrue = false;
                    stb.Append("描述不能不空");
                }
                else if (string.IsNullOrEmpty(txtStartDate.Text.Trim()))
                {
                    istrue = false;
                    stb.Append("有效期从不能为空");
                }
                else if (string.IsNullOrEmpty(txtgonghao.Text.Trim()))
                {
                    istrue = false;
                    stb.Append("员工工号不能为空");
                }
            }
            else if (usety == "系统")
            {
                if (string.IsNullOrEmpty(txtEmployeeNumber.Text.Trim()))
                {
                    istrue = false;
                    stb.Append("账号不能为空");
                }


                else if (string.IsNullOrEmpty(txtUserDescritpion.Text.Trim()))
                {
                    istrue = false;
                    stb.Append("描述不能不空");
                }
                else if (string.IsNullOrEmpty(txtStartDate.Text.Trim()))
                {
                    istrue = false;
                    stb.Append("有效期从不能为空");
                }
            }
            mess = stb.ToString();
            return istrue;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<V_HECUSER_Role> userlist = new List<V_HECUSER_Role>();
            userlist = (List<V_HECUSER_Role>)ViewState["UserInfoList"];
            if (userlist == null || userlist.Count <= 0)
            {
                Response.Write("<script>alert('必须添加账号角色');</script>");
                return;
            }
            txtEmployeeNumber.ReadOnly = true;
            txtEmployeeCode.ReadOnly = true;
            txtUserChinesename.ReadOnly = true;
            string mess = "";
            if (!CheckValue(out mess))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + mess + "');", true);
                return;
            }

            Create();
            try
            {
                using (IAMEntities db = new IAMEntities())
                {
                    string sql = @"UPDATE dbo.HEC_User_Info   SET END_DATE= SUBSTRING(CONVERT(NVARCHAR(20),b.END_DATE,120),0,11) FROM dbo.HEC_User b WHERE b.User_CD='" + txtEmployeeNumber.Text.Trim() + "' AND dbo.HEC_User_Info.USER_NAME='" + txtEmployeeNumber.Text.Trim() + "' ";
                    db.ExecuteStoreCommand(sql);
                }
            }
            catch { }
            //if ( > 0)
            //{
            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('操作成功！');window.close();", true);
            //}
        }

        protected void btnDelete_click(object sender, CommandEventArgs e)
        {
            List<V_HECUSER_Role> userInfoList = new List<V_HECUSER_Role>();
            userInfoList = (List<V_HECUSER_Role>)ViewState["UserInfoList"];
            if (e.CommandArgument.ToString() != string.Empty)
            {
                Guid id = new Guid(e.CommandArgument.ToString());
                userInfoList.FirstOrDefault(item => item.uID == id).isdr = 1;
                ViewState["UserInfoList"] = userInfoList;
                txtUserInfo.Text = string.Empty;
                QueryUserInfo(true);
            }

        }

        protected void btnhecGangwei_ServerClick(object sender, EventArgs e)
        {
            string tmpgangwei = txthecgangwei.Text;
            if (!string.IsNullOrEmpty(tmpgangwei))
            {
                var list = (List<HEC_User_Gangwei>)ViewState["UserGangWei"];
                string[] StrGangWei = tmpgangwei.Split("^".ToArray(),StringSplitOptions.RemoveEmptyEntries);
                if (StrGangWei.Length >= 6)
                {
                    HEC_User_Gangwei tmp = new HEC_User_Gangwei();
                    tmp.ID = Guid.NewGuid();
                    tmp.COMPANY_CODE = StrGangWei[0];
                    tmp.COMPANY_NAME = StrGangWei[1];
                    tmp.UNIT_CODE = StrGangWei[2];
                    tmp.UNIT_NAME = StrGangWei[3];
                    tmp.POSITION_CODE = StrGangWei[4];
                    tmp.POSITION_NAME = StrGangWei[5];
                    tmp.ENABLED_FLAG = "Y";
                    tmp.PRIMARY_POSITION_FLAG = "Y";
                    tmp.isdelete = 2;
                    var en = list.FirstOrDefault(item=>item.COMPANY_CODE==tmp.COMPANY_CODE&&item.UNIT_CODE==tmp.UNIT_CODE&&item.POSITION_CODE==tmp.POSITION_CODE&&item.isdelete!=1);
                    if (en == null)
                    {
                        list.Add(tmp);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(updatepanel2, this.GetType(), "", "<script>alert('已存在" + tmp.COMPANY_NAME+"--"+tmp.UNIT_NAME+"--"+tmp.POSITION_CODE + "岗位');</script>", false);
                    }
                    ViewState["UserGangWei"] = list;
                }
            }
            QueryUserGangwei();
        }

        protected void btngangweidel_Command(object sender, CommandEventArgs e)
        {
            var list = (List<HEC_User_Gangwei>)ViewState["UserGangWei"];
            Guid id = new Guid(e.CommandArgument.ToString());
            list.FirstOrDefault(x => x.ID == id).isdelete = 1;
            ViewState["UserGangWei"] = list;
            QueryUserGangwei();
        }
    }
}