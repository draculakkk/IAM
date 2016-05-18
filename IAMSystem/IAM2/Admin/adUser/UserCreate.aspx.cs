using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseDataAccess;
using IAMEntityDAL;

namespace IAM.Admin.adUser
{
    public partial class UserCreate : BasePage
    {
        public bool IsAdmin = false;
        public class tmp : IEqualityComparer<AD_Computer>
        {
            public tmp()
            { }
            public bool Equals(AD_Computer x, AD_Computer y)
            {
                return x.NAME == y.NAME;
            }

            public int GetHashCode(AD_Computer a)
            {
                return 0;
            }
        }

        HREmployeeDAL _hrEmployeeServices = new HREmployeeDAL();
        ADUserInfoDAL _adUserInfoServices = new ADUserInfoDAL();
        HRDepartmentDAL _hrDepartmentServices = new HRDepartmentDAL();
        ADComputerDAL _adcomputerServices = new ADComputerDAL();
        IAMEntities db = new IAMEntities();
        AccountMapingDAL _accServicer = new AccountMapingDAL();
        string ComputerName
        {
            get;
            set;
        }

        bool IsUpdate
        {
            get
            {
                if (Request.QueryString["userid"] != null)
                    return true;
                else
                    return false;
            }
        }

        string acc
        {
            get
            {

                if (Request.QueryString["userid"] != null)
                    return Request.QueryString["userid"];
                else
                    return string.Empty;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OnLoaded();
                if (IsUpdate)
                {
                    //Request.QueryString["userid"];
                    EditQuery();
                    QueryGroup();
                    QueryPage("yes");
                    txtNumber.Attributes.Add("readonly","true");
                }
                else
                {
                    List<AD_UserWorkGroup> list = new List<AD_UserWorkGroup>();
                    ViewState["userwork"] = list;
                    QueryPage("NaN");
                }
                ControlButton();
                ControlReadOnly(true);
                
            }
            IsAdmin = base.ReturnUserRole.Admin;
        }

        /// <summary>
        /// 设置字段不可手动添加
        /// </summary>
        void ControlReadOnly(bool sta)
        {
            if (IsUpdate == true)
            {
                txtLoginName.Attributes.Add("readonly", "true");
            }
            txtChineseName.Attributes.Add("readonly", "true");

            txtDescription.Attributes.Add("readonly", "true");
            txtMailDataBase.Attributes.Add("readonly", "true");
            txtLyncNumber.Attributes.Add("readonly", "true");
            txtEmail.Attributes.Add("readonly", "true");
        }

        void QueryPage(string _sta)
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
                    txtChineseName.Enabled = false; ;
                    txtDisplayName.Enabled = false; ;
                    txtLoginName.Enabled = false; ;
                    txtPassword.Enabled = false; ;
                    txtPhone.Enabled = false; ;
                    txtMobleNumber.Enabled = false; ;
                    chkEnable.Enabled = false; ;
                    txtEnableDate.Enabled = false;
                    dplDepartment.Enabled = false;
                    dplZhiji.Enabled = false;
                    chkDisk.Enabled = false;
                    txtDiskNumber.Enabled = false;
                    txtMemo.Enabled = false;
                    break;
                case "1":
                    txtChineseName.Enabled = false; ;
                    txtDisplayName.Enabled = false; ;
                    txtLoginName.Enabled = false; ;
                    txtPassword.Enabled = false; ;
                    txtPhone.Enabled = false; ;
                    txtMobleNumber.Enabled = false; ;
                    chkEnable.Enabled = false; ;
                    txtEnableDate.Enabled = false;
                    dplDepartment.Enabled = false;
                    dplZhiji.Enabled = false;
                    chkDisk.Enabled = false;
                    txtDiskNumber.Enabled = false;
                    txtMemo.Enabled = false;
                    ClientScript.RegisterStartupScript(this.GetType(),"","<script>alert(\"不存在该工号员工或该员工已离职 请核实后在添加\");</script>");
                    break;
                case "2":
                    txtChineseName.Enabled = false; ;
                    txtDisplayName.Enabled = false; ;
                    txtLoginName.Enabled = false; ;
                    txtPassword.Enabled = false; ;
                    txtPhone.Enabled = false; ;
                    txtMobleNumber.Enabled = false; ;
                    chkEnable.Enabled = false; ;
                    txtEnableDate.Enabled = false;
                    dplDepartment.Enabled = false;
                    dplZhiji.Enabled = false;
                    chkDisk.Enabled = false;
                    txtDiskNumber.Enabled = false;
                    txtMemo.Enabled = false;
                    ClientScript.RegisterStartupScript(this.GetType(),"","<script>alert(\"该工号员工已拥有ad员工类账号 故不可在添加员工类账号\");</script>");
                    break;
                case "yes":
                    txtChineseName.Enabled = true; ;
                    txtDisplayName.Enabled = true; ;
                    txtLoginName.Enabled = true; ;
                    txtPassword.Enabled = true; ;
                    txtPhone.Enabled = true; ;
                    txtMobleNumber.Enabled = true; ;
                    chkEnable.Enabled = true; ;
                    txtEnableDate.Enabled = true;
                    dplDepartment.Enabled = true;
                    dplZhiji.Enabled = true;
                    chkDisk.Enabled = true;
                    txtDiskNumber.Enabled = true;
                    txtMemo.Enabled = true;
                    txtChineseName.Attributes.Add("readonly", "true");

                    txtDescription.Attributes.Add("readonly", "true");
                    txtMailDataBase.Attributes.Add("readonly", "true");
                    break;
            }
        }


        void ControlButton()
        {
            if (base.ReturnUserRole.Admin == false)
            {
                btnQuery.Enabled = false;
                btnworkground.Disabled = true;
                btncomputer.Disabled = true;
              
                Button1.Enabled = false;
            }
            else if (base.ReturnUserRole.AD == false && base.ReturnUserRole.Admin == false && base.ReturnUserRole.Leader == false)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('无权查看该页面');window.close();", true);
            }
        }

        void EditQuery()
        {
            int count;
            var mod = new ADUserInfoDAL().GetADUserInfo(null, null, null, out count).FirstOrDefault(item => item.Accountname == acc);
            if (mod != null)
            {
                quanjuentity = mod;
                var accmaping = new AccountMapingDAL().GetOneByzhanghao(mod.Accountname, "员工", "AD");
                if (accmaping != null)
                    txtNumber.Text = accmaping.gonghao;
                //txtNumber.Text = mod.Id;
                Query();
                txtChineseName.Text = mod.CnName;
                txtDisplayName.Text = mod.DisplayName;
                txtLoginName.Text = mod.Accountname;
                txtPassword.Text = mod.PASSWORD;
                txtPhone.Text = mod.ADMobile;
                chkEnable.Checked = mod.ENABLE ? true : false;
                txtMobleNumber.Text = mod.ADTel;
                //tmp.parentDept = dplDepartment.SelectedValue;
                //dplZhiji.SelectedItem.Text = mod.Job;
                dplZhiji.SelectedValue = new AD_Zhiji_WorkGroupDAL().GetList().FirstOrDefault(item => item.Zhiji == mod.Job).WorkGroup;
                chkDisk.Checked = mod.EnableDrive ? true : false;
                txtDisk.Text = mod.Drive;
                txtDiskNumber.Text = mod.PATH;
                txtMemo.Text = mod.Memo;
                txtEmail.Text = mod.Email;
                txtLyncNumber.Text = mod.Lync;
                txtMailDataBase.Text = mod.EmailDatabase;
                txtLoginName.Text = mod.UserID;
                txtGangwei.Text = mod.Posts;
                txtDescription.Text = mod.DESCRIPTION;
                var dpent = new AD_Department_WorkGroupDAL().GetList().FirstOrDefault(item => item.p2 == mod.Department && item.p1 == "False");
                if (dpent != null)
                    dplDepartment.SelectedValue = dpent.ID.ToString();
                txtEnableDate.Text = mod.expiryDate != null ? Convert.ToDateTime(mod.expiryDate.ToString()).ToString("yyyy-MM-dd") : "";

                //ad计算机
                bindComputer();
                editHRInfo();
            }
        }

        void bindComputer()
        {
            using (IAMEntities db = new IAMEntities())
            {
                var list = from a in db.AD_Computer
                           join
                               b in db.AccountMaping on a.NAME equals b.zhanghao
                           where b.gonghao == txtNumber.Text.Trim() && b.type == "ADComputer" && a.ENABLE == 1 && (a.IsDelete == false || a.IsDelete == null)
                           select new
                           {
                               NAME = a.NAME,
                               DESCRIPTION = a.DESCRIPTION,
                               ExpiryDate = a.ExpiryDate,
                               p1 = b.UserType,
                               ENABLE = a.ENABLE

                           };
                repeaterComputer.DataSource = list;
                repeaterComputer.DataBind();
            }
        }

        public string ComputerEditString(string name)
        {
            if (name.ToUpper() == txtLoginName.Text.Trim().ToUpper())
            {
                return string.Empty;
            }
            else
            {
                return string.Format("[<a href=\"javascript:AddComputer('{0}');\">编辑</a>]", name);
            }
        }

        void editHRInfo()
        {
            string Employeecode = string.IsNullOrEmpty(txtNumber.Text) ? "" : txtNumber.Text.Trim();
            HREmployee EmployeeModel = new IAMEntities().HREmployee.FirstOrDefault(item => item.code == Employeecode && (item.leavePostsDate == null || item.leavePostsDate > DateTime.Now) == true);
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
                    hiddenDepartMentId.Value = EmployeeModel.dept;
                    txtPrePartment.Text = departmentModel.shangjiName;
                    chkPartmentOut.Checked = (bool)departmentModel.isRevoke;
                    chkPartmentClose.Checked = (bool)departmentModel.isSealed;
                    txtPartmentOutDate.Text = departmentModel.revokeDate != null ? DateTime.Parse(departmentModel.revokeDate.ToString()).ToShortDateString() : "";
                    //QueryAdUserInfo(Employeecode);
                    hiddenDepartMentId.Value = departmentModel.dept;
                    hiddenPreMentId.Value = departmentModel.parentDept;
                }
            }
        }



        void Query()
        {
            string Employeecode = string.IsNullOrEmpty(txtNumber.Text) ? "" : txtNumber.Text.Trim();
            HREmployee EmployeeModel = null;
            if (IsUpdate)
            {
                EmployeeModel = new IAMEntities().HREmployee.FirstOrDefault(item => item.code ==Employeecode);
            }
            else
            {
               EmployeeModel= new IAMEntities().HREmployee.FirstOrDefault(item => item.code == Employeecode && (item.leavePostsDate == null || item.leavePostsDate > DateTime.Now) == true);
            }
            if (EmployeeModel != null)
            {
                var en = new AccountMapingDAL().GetOne(Employeecode, Unitity.SystemType.AD.ToString(), "员工");
                if (en != null && IsUpdate == false)
                {
                    QueryPage("2");
                    return;
                }
                if (IsUpdate == false || string.IsNullOrEmpty(txtChineseName.Text) || string.IsNullOrEmpty(txtDisplayName.Text))
                {
                    if (string.IsNullOrEmpty(txtChineseName.Text) && string.IsNullOrEmpty(txtDisplayName.Text))
                    {
                        txtName.Text = txtChineseName.Text = txtDisplayName.Text = EmployeeModel.name;
                    }
                    
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
                    hiddenDepartMentId.Value = EmployeeModel.dept;
                    txtPrePartment.Text = departmentModel.shangjiName;
                    chkPartmentOut.Checked = (bool)departmentModel.isRevoke;
                    chkPartmentClose.Checked = (bool)departmentModel.isSealed;
                    txtPartmentOutDate.Text = departmentModel.revokeDate != null ? DateTime.Parse(departmentModel.revokeDate.ToString()).ToShortDateString() : "";
                    //QueryAdUserInfo(Employeecode);
                    hiddenDepartMentId.Value = departmentModel.dept;
                    hiddenPreMentId.Value = departmentModel.parentDept;
                }
                QueryPage("yes");
            }
            else
            {
                QueryPage("1");
            }
        }

        void OnLoaded()
        {
            dplZhiji.DataSource = new AD_Zhiji_WorkGroupDAL().GetList().Where(item => item.p1 == "False").OrderBy(item=>item.Zhiji).ToList();
            dplZhiji.DataValueField = "WorkGroup";
            dplZhiji.DataTextField = "Zhiji";
            dplZhiji.DataBind();
            dplZhiji.SelectedValue = "";

            dplDepartment.DataSource = new AD_Department_WorkGroupDAL().GetList().Where(item => item.p1 == "False").OrderBy(item => item.ordercolumn);
            dplDepartment.DataValueField = "ID";
            dplDepartment.DataTextField = "p2";
            dplDepartment.DataBind();
            ListItem li = new ListItem("", "");
            li.Selected = true;
            dplDepartment.Items.Add(li);

            int count;
            repeaterComputer.DataSource = new ADComputerDAL().GetAdComputerList(out count).Where(item => item.NAME == txtLoginName.Text.Trim());
            repeaterComputer.DataBind();

        }

        AD_UserInfo AdEntity()
        {
            AD_UserInfo tmp = new AD_UserInfo();
            tmp.CnName = txtChineseName.Text;
            tmp.DisplayName = txtDisplayName.Text;
            tmp.Accountname = txtLoginName.Text;
            tmp.PASSWORD = txtPassword.Text;
            tmp.ADTel = txtMobleNumber.Text.Trim();
            tmp.ENABLE = chkEnable.Checked ? true : false;
            tmp.ADMobile = txtPhone.Text.Trim();
            //tmp.parentDept = dplDepartment.SelectedValue;
            tmp.Job = dplZhiji.SelectedItem.Text;
            tmp.EnableDrive = chkDisk.Checked ? true : false;
            tmp.Drive = txtDisk.Text.Trim();
            tmp.PATH = txtDiskNumber.Text.Trim();
            tmp.Memo = txtMemo.Text;
            tmp.Email = txtEmail.Text.Trim();
            tmp.Lync = txtLyncNumber.Text.Trim();
            tmp.EmailDatabase = txtMailDataBase.Text;
            tmp.UserID = txtLoginName.Text;
            tmp.Posts = txtGangwei.Text;
            tmp.DESCRIPTION = txtDescription.Text;
            tmp.Department = dplDepartment.SelectedItem.Text;
            tmp.Mailstorage = 0;
            tmp.Id = txtNumber.Text.Trim();
            tmp.expiryDate = txtEnableDate.Text.Trim() == string.Empty ? (DateTime?)null : Convert.ToDateTime(txtEnableDate.Text);
            return tmp;
        }

        AD_UserInfo quanjuentity = new AD_UserInfo();
        void QueryGroup()
        {
            List<AD_UserWorkGroup> list = new List<AD_UserWorkGroup>();
            if (ViewState["userwork"] == null)
            {
                using (IAMEntities db = new IAMEntities())
                {
                    list = db.AD_UserWorkGroup.Where(item => item.Uid == acc&&item.isdr!=1).ToList();
                }
                ViewState["userwork"] = list;
            }
            else
            {
                list = (List<AD_UserWorkGroup>)ViewState["userwork"];
                int count;
                quanjuentity = new ADUserInfoDAL().GetADUserInfo(null, null, null, out count).FirstOrDefault(item => item.Accountname == acc);
            }
            list = list.Where(item => item.isdr != 1).ToList();
            var defaultworkgroup = new AD_DefaultWorkGroupDAL().GetList().Where(item => item.IsDelete == false).FirstOrDefault().NAME;
            string n = dplDepartment.SelectedItem.Text;
            string[] narr = n.Split(new string[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
            if (narr.Length == 3)
                n = narr[2];
            if (narr.Length == 2)
                n = narr[1];
            if (narr.Length == 1)
                n = narr[0];

            //if (IsUpdate)
            //{
            var zhiji = list.Where(item => item.GroupName == dplZhiji.SelectedValue).ToList();
            zhijiGroupsRepeater.DataSource = zhiji;
            zhijiGroupsRepeater.DataBind();
            // }

            // if (IsUpdate)
            // {

            var bumen = list.Where(item => item.GroupName == n).ToList();
            DefaultPartmentRepeater.DataSource = bumen;
            DefaultPartmentRepeater.DataBind();
            // }

            //if (IsUpdate)
            //{

            var defa = list.Where(item => item.GroupName == defaultworkgroup).ToList();
            DefaultGroupRepeater.DataSource = defa;
            DefaultGroupRepeater.DataBind();
            // }

            //if (IsUpdate)
            //{
            var kekong = list.Where(item => item.GroupName != dplZhiji.SelectedValue && item.GroupName != defaultworkgroup && item.GroupName != n && item.isdr != 1).ToList();
            kekongWorkgroupsRepeater.DataSource = kekong;
            kekongWorkgroupsRepeater.DataBind();
            // }
            //else
            // {
            //kekongWorkgroupsRepeater.DataSource = list.Where(item => item.isdr == 2).ToList();
            //kekongWorkgroupsRepeater.DataBind();
            //}
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        bool checkValue(out string mess)
        {
            bool istrue = true;
            mess = "";
            if (string.IsNullOrEmpty(txtChineseName.Text.Trim()))
            {
                istrue = false;
                mess = "CN";
            }
            else if (string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                istrue = false;
                mess = "描述";
            }
            else if (string.IsNullOrEmpty(txtLoginName.Text.Trim()))
            {
                istrue = false;
                mess = "登陆名";
            }
            else if (string.IsNullOrEmpty(txtPassword.Text.Trim()) && IsUpdate == false)
            {
                istrue = false;
                mess = "密码";
            }
            else if (string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                istrue = false;
                mess = "邮件";
            }
            else if (string.IsNullOrEmpty(txtLyncNumber.Text.Trim()))
            {
                istrue = false;
                mess = "Lync";
            }
            else if (string.IsNullOrEmpty(txtMailDataBase.Text.Trim()))
            {
                istrue = false;
                mess = "邮件数据库";
            }
            else if (string.IsNullOrEmpty(txtNumber.Text.Trim()))
            {
                istrue = false;
                mess = "员工工号";
            }
            return istrue;
        }

        bool ValuedateForDisabled(out string mess)
        {
            mess = "";
            bool istrue = true;
            if (!chkEnable.Checked)
            {
                    var hr = new AccountMapingDAL().GetList(txtNumber.Text.Trim(),Unitity.SystemType.HR);
                    var ad = new AccountMapingDAL().GetList(txtNumber.Text.Trim(), Unitity.SystemType.AD).Where(item=>item.type=="AD");
                    var adc = new AccountMapingDAL().GetList(txtNumber.Text.Trim(), Unitity.SystemType.ADComputer).Where(item=>item.type=="ADComputer");
                    var hec = new AccountMapingDAL().GetList(txtNumber.Text.Trim(), Unitity.SystemType.HEC);
                    var sap = new AccountMapingDAL().GetList(txtNumber.Text.Trim(), Unitity.SystemType.SAP);
                    var tc = new AccountMapingDAL().GetList(txtNumber.Text.Trim(), Unitity.SystemType.TC);
                    if (hr != null && hr.Count() > 0)
                    {
                        istrue = false;
                        mess = "HR系统、";
                        chkEnable.Checked = true;
                    }
                    if (ad != null && ad.Count() > 1)
                    {
                        istrue = false;
                        mess += "ad系统、";
                        chkEnable.Checked = true;
                    }
                    if (adc != null && adc.Count() > 1)
                    {
                        istrue = false;
                        mess += "计算机系统、";
                        chkEnable.Checked = true;
                    }
                    if (hec != null && hec.Count() > 0)
                    {
                        istrue = false;
                        mess += "HEC系统、";
                        chkEnable.Checked = true;
                    }
                    if (sap != null && sap.Count() > 0)
                    {
                        istrue = false;
                        mess += "SAP系统、";
                    }
                    if (tc != null && tc.Count() > 0)
                    {
                        istrue = false;
                        mess += "TC系统、";
                        chkEnable.Checked = true;
                    }
                
            }
            return istrue;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ControlReadOnly(false);
            txtEmail.Enabled = true;
            txtLyncNumber.Enabled = true;
            txtLoginName.ReadOnly = false;
            string mess = "";
            if (!checkValue(out mess))
            {
                Response.Write("<script>alert('请填写" + mess + "字段');</script>");
                ControlReadOnly(true);
                return;
            }

            if (!ValuedateForDisabled(out mess))
            {
                Response.Write("<script>alert('无法禁用该账号\\n因为该账号下还存在" + mess + "账号\\n请先将这些系统中账号禁用或转移');</script>");
                return;
            }

            btnShengcheng_Click(btnShengcheng, new EventArgs());

            BLL.AddUserMail addmail = new BLL.AddUserMail();
            addmail.Actioner = base.UserInfo.adname;
            addmail.SystemName = Unitity.SystemType.AD.ToString();
            addmail.SystemType = "员工";
            addmail.UserName = AdEntity().Accountname;
            if (IsUpdate == false)
            {
                if (new ADUserInfoDAL().Add(AdEntity()) > 0)
                {
                    AccountMaping tmp = new AccountMaping()
                    {
                        id = Guid.NewGuid(),
                        gonghao = txtNumber.Text,
                        zhanghao = txtLoginName.Text,
                        type = "AD",
                        UserType = "员工"
                    };
                    new AccountMapingDAL().Add(tmp);
                    ADUserWorkGroupDAL _tmpservices = new ADUserWorkGroupDAL();
                    BLL.ActionLog.CreateLog(base.UserInfo.adname, tmp.zhanghao, Unitity.SystemType.AD.ToString(), "员工");
                    var list = (List<AD_UserWorkGroup>)ViewState["userwork"];
                    var addlis = list.Where(item => item.isdr == 2).ToList();
                    System.Text.StringBuilder stb = new System.Text.StringBuilder();
                    foreach (var i in addlis)
                    {
                        stb.Append(@"INSERT INTO dbo.AD_UserWorkGroup
        ( ID ,
          Uid ,
          GroupName ,          
          isdr
        )
VALUES  ( NEWID() ,'" + txtLoginName.Text.Trim() + "' ,'" + i.GroupName + "' ,0) \n");
                    }

                    if (stb.ToString().Trim() != string.Empty)
                    {
                        using (IAMEntities db = new IAMEntities())
                        {

                            db.ExecuteStoreCommand(stb.ToString());
                            db.SaveChanges();
                        }
                    }
                    addmail.UserInfo = new BLL.CompareEntity<AD_UserInfo>().ReturnCompareStringForMailAdd(AdEntity(), Unitity.SystemType.AD);
                    addmail.RoleString = BLL.CompareRoleList.AD((List<AD_UserWorkGroup>)ViewState["userwork"], AdEntity().Accountname) + BLL.CompareRoleList.ADComputer(txtNumber.Text.Trim());
                    new BLL.MailServices().CreateUserMail(addmail);
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('添加成功');window.close();", true);
                }
            }
            else
            {
                int count;
                AD_UserInfo entity = new ADUserInfoDAL().GetADUserInfo(null, null, null, out count).FirstOrDefault(item => item.Accountname == txtLoginName.Text);
                AD_UserInfo old = entity;
                //using (IAMEntities db = new IAMEntities())
                //{
                //    string sql = "DELETE FROM dbo.AD_UserWorkGroup WHERE Uid='" + entity.Accountname + "' AND (GroupName='" + entity.Job + "') ";//GroupName='" + entity.Department + "' or 
                //    db.ExecuteStoreCommand(sql);
                //    db.SaveChanges();
                //}

                entity = AdEntity();
                if (new ADUserInfoDAL().UpdateUserInfo(entity) > 0)
                {

                    new BLL.ActionLog().EditLog<AD_UserInfo>(base.UserInfo.adname, entity.Accountname, Unitity.SystemType.AD.ToString(), "员工", old, entity);

                    var list = (List<AD_UserWorkGroup>)ViewState["userwork"];
                    var addlis = list.Where(item => item.isdr == 2).ToList();
                    var dellis = list.Where(item => item.isdr == 1).ToList();
                    System.Text.StringBuilder stb = new System.Text.StringBuilder();
                    foreach (var i in addlis)
                    {
                        stb.Append(@"INSERT INTO dbo.AD_UserWorkGroup
        ( ID ,
          Uid ,
          GroupName ,          
          isdr
        )
VALUES  ( NEWID() ,'" + acc + "' ,'" + i.GroupName + "' ,0) \n");
                    }

                    foreach (var i in dellis)
                    {
                        stb.Append(@"update dbo.AD_UserWorkGroup set isdr=1 where ID='" + i.ID + "'\n");
                    }

                    if (stb.ToString().Trim() != string.Empty)
                    {
                        using (IAMEntities db = new IAMEntities())
                        {

                            db.ExecuteStoreCommand(stb.ToString());
                            db.SaveChanges();
                        }
                    }
                    addmail.UserInfo = new BLL.CompareEntity<AD_UserInfo>().ReturnCompareStringForMailUpdate(old, AdEntity(), Unitity.SystemType.AD);
                    addmail.RoleString = BLL.CompareRoleList.AD((List<AD_UserWorkGroup>)ViewState["userwork"], AdEntity().Accountname) + BLL.CompareRoleList.ADComputer(txtNumber.Text.Trim());
                    if (!chkEnable.Checked)
                    {
                        using (IAMEntities db = new IAMEntities())
                        {

                            //db.ExecuteStoreCommand("update dbo.AD_UserWorkGroup set isdr=1 where uid='" + AdEntity().Accountname + "' UPDATE AD_Computer SET ENABLE=0  where NAME='" + AdEntity().Accountname + "' ");
                            db.ExecuteStoreCommand("delete dbo.AD_UserWorkGroup where uid='" + AdEntity().Accountname + "' UPDATE AD_Computer SET ENABLE=0  where NAME='" + AdEntity().Accountname + "' ");
                            db.SaveChanges();
                        }
                        new BLL.MailServices().DisabledUserMail(addmail);
                    }
                    else
                    {
                        using (IAMEntities db = new IAMEntities())
                        {

                            db.ExecuteStoreCommand("UPDATE AD_Computer SET ENABLE=1  where NAME='" + AdEntity().Accountname + "' ");
                            db.SaveChanges();
                        }
                        new BLL.MailServices().UpdateUserMail(addmail);
                    }

                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('更新成功');window.close();", true);

                }
            }
        }

        protected void dplDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void btnOnLoadRoleInfo_ServerClick(object sender, EventArgs e)
        {
            if (hiddkekonggroups.Value == string.Empty)
            {
                return;
            }
            List<string> list = hiddkekonggroups.Value.Split(new string[] { "^" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<AD_UserWorkGroup> uli = (List<AD_UserWorkGroup>)ViewState["userwork"];
            foreach (string a in list)
            {
                var en = uli.FirstOrDefault(item=>item.GroupName==a&&item.isdr!=1);
                if (en == null)
                {
                    AD_UserWorkGroup n = new AD_UserWorkGroup()
                    {
                        ID = Guid.NewGuid(),
                        Uid = quanjuentity.Accountname,
                        GroupName = a,
                        isdr = 2
                    };
                    uli.Add(n);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(updatepanel1,this.GetType(),"","alert('已存在"+a+"该工作组');",true);
                }
            }

            ViewState["userwork"] = uli;
            QueryGroup();
        }

        protected void lbtnDelete_Command(object sender, CommandEventArgs e)
        {
            List<AD_UserWorkGroup> uli = (List<AD_UserWorkGroup>)ViewState["userwork"];
            Guid id = new Guid(e.CommandArgument.ToString());
            foreach (var a in uli)
            {
                uli.FirstOrDefault(item => item.ID == id).isdr = 1;
            }
            ViewState["userwork"] = uli;
            QueryGroup();
        }

        protected void btnShengcheng_Click(object sender, EventArgs e)
        {
            if (!chkEnable.Checked)
            {
                return;
            }
            if (txtLoginName.Text.Trim() == string.Empty)
            {
                Response.Write("<script>alert('请先填写ad登陆名');</script>");
                return;
            }

            if (dplDepartment.SelectedItem.Text == string.Empty)
            {
                Response.Write("<script>alert('请选择部门');</script>");
                return;
            }

            //计算机
            int count;
            var jj = new ADComputerDAL().GetAdComputerList(out count).FirstOrDefault(itme => itme.NAME.ToUpper() == txtLoginName.Text.ToUpper());
            if (jj == null)
            {
                AD_Computer tmpc = new AD_Computer();
                tmpc.ID = Guid.NewGuid();
                tmpc.DESCRIPTION = dplDepartment.SelectedItem.Text+"  "+txtChineseName.Text.Trim();
                tmpc.NAME = txtLoginName.Text;
                tmpc.IsDelete = false;
                tmpc.p1 = "员工";
                tmpc.ENABLE = 1;
                new ADComputerDAL().Add(tmpc);
                new AccountMapingDAL().Add(new AccountMaping() { id = Guid.NewGuid(), type = "ADComputer", UserType = "员工", gonghao = txtNumber.Text.Trim(), zhanghao = txtLoginName.Text.Trim() });
                
                List<AD_Computer> lico = new List<AD_Computer>();
                lico.Add(tmpc);
                repeaterComputer.DataSource = lico;
                repeaterComputer.DataBind();
            }
            else
            {
                List<AD_Computer> lico = new List<AD_Computer>();
                jj.DESCRIPTION = dplDepartment.SelectedItem.Text + "  " + txtChineseName.Text.Trim();
                
                new ADComputerDAL().UpdateADComputer(jj);
                lico.Add(jj);
                //repeaterComputer.DataSource = lico;
                //repeaterComputer.DataBind();
                bindComputer();
            }
            //描述 邮件数据库                    
            Guid id = new Guid(dplDepartment.SelectedValue);
            AD_Department_WorkGroup tmp = new AD_Department_WorkGroupDAL().GetList().FirstOrDefault(item => item.ID == id);
            txtDescription.Text = (tmp.Center + "  " + tmp.Department + "  " + tmp.KeShi).Trim() + "  " + txtChineseName.Text;
            txtMailDataBase.Text = tmp.EmailDataBase;
            //默认组
            List<AD_UserWorkGroup> uli = (List<AD_UserWorkGroup>)ViewState["userwork"];
            string na = new AD_DefaultWorkGroupDAL().GetList().Where(item => item.IsDelete == false).FirstOrDefault().NAME;
            if (uli.FirstOrDefault(item => item.GroupName == na) == null)
            {
                AD_UserWorkGroup tp1 = new AD_UserWorkGroup()
                {
                    memo = "no",
                    ID = Guid.NewGuid(),
                    GroupName = na,
                    Uid = txtLoginName.Text.Trim()
                    ,
                    isdr = 2
                };
                uli.Add(tp1);
            }

            //默认部门组
            string n = dplDepartment.SelectedItem.Text;
            if (!string.IsNullOrEmpty(n))
            {
                string[] narr = n.Split(new string[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
                if (narr.Length == 3)
                    n = narr[2];
                if (narr.Length == 2)
                    n = narr[1];
                if (narr.Length == 1)
                    n = narr[0];
                if (uli.FirstOrDefault(item => item.GroupName == n) == null)
                {
                    AD_UserWorkGroup tp2 = new AD_UserWorkGroup()
                    {
                        memo = "no",
                        ID = Guid.NewGuid(),
                        GroupName = n,
                        Uid = txtLoginName.Text.Trim(),
                        isdr = 2
                    };
                    uli.Add(tp2);
                }
            }
            else
            {
                Response.Write("<script>alert('请先填写ad登陆名');</script>");
                return;
            }
            //默认职级组
            if (!string.IsNullOrEmpty(dplZhiji.SelectedValue))
            {
                if (uli.FirstOrDefault(item => item.GroupName == dplZhiji.SelectedValue) == null)
                {
                    AD_UserWorkGroup tp = new AD_UserWorkGroup() { isdr = 2, ID = Guid.NewGuid(), GroupName = dplZhiji.SelectedValue, Uid = txtLoginName.Text.Trim(), memo = "no" };
                    uli.Add(tp);

                }
            }
            QueryGroup();
            Query();
        }

        protected void lbtncomputerdelete_Command(object sender, CommandEventArgs e)
        {
            string nam = e.CommandArgument.ToString();
            using (IAMEntities db = new IAMEntities())
            {
                db.ExecuteStoreCommand("update ad_computer set IsDelete=1 ,DeleteDatetime=getdate() where NAME='" + nam + "'");
                db.SaveChanges();
            }
            bindComputer();
        }
    }
}