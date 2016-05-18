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
    public partial class OtherCreate : BasePage
    {
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

        public bool IsAdmin = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OnLoaded();
                if (IsUpdate)
                {
                    EditQuery();
                    QueryGroup();
                    txtLoginName.ReadOnly = true;
                    QueryPage("yes");
                }
                else
                {
                    ViewState["userwork"] = new List<AD_UserWorkGroup>();
                    QueryPage("NaN");
                }
                ControlButton();
            }
            IsAdmin = base.ReturnUserRole.Admin;


        }

        void ControlButton()
        {
            if (base.ReturnUserRole.Admin == false)
            {
                btnQuery.Enabled = false;
                btnworkground.Disabled = true;

               
                Button1.Enabled = false;
            }
            else if (base.ReturnUserRole.AD == false && base.ReturnUserRole.Admin == false && base.ReturnUserRole.Leader == false)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('无权查看该页面');window.close();", true);
            }
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
                    txtDescription.Enabled = false;
                    txtDisk.Enabled = false;
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
                    txtDescription.Enabled = false;
                    txtDisk.Enabled = false;
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert(\"不存在该工号员工或该员工已离职 请核实后在添加\");</script>");
                    break;
                case "-1":
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
                    txtDescription.Enabled = false;
                    txtDisk.Enabled = false;
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert(\"不存在该工号员工类账号\\n请核实后在添加\");</script>");
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
                    txtDescription.Enabled = false;
                    txtDisk.Enabled = false;
                    ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert(\"该工号员工已拥有ad员工类账号 故不可在添加员工类账号\");</script>");
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
                    txtDescription.Enabled = true;
                    txtDisk.Enabled = true;
                    
                    break;
            }
        }

        void EditQuery()
        {
            int count;
            var mod = new ADUserInfoDAL().GetADUserInfo(null, null, null, out count).FirstOrDefault(item => item.Accountname == acc);
            if (mod != null)
            {
                var accmaping = new AccountMapingDAL().GetOneByzhanghao(mod.Accountname, "其他", "AD");
                if (accmaping == null)
                    accmaping = new AccountMapingDAL().GetOneByzhanghao(mod.Accountname,"员工","AD");
                if(accmaping==null)
                    accmaping = new AccountMapingDAL().GetOneByzhanghao(mod.Accountname, "系统", "AD");
                if (accmaping!=null)
                txtNumber.Text =accmaping.gonghao;
                Query();
                txtChineseName.Text = mod.CnName;
                txtDisplayName.Text = mod.DisplayName;
                txtLoginName.Text = mod.Accountname;
                txtPassword.Text = mod.PASSWORD;
                txtPhone.Text = mod.ADMobile;
                chkEnable.Checked = mod.ENABLE ? true : false;
                txtMobleNumber.Text = mod.ADTel;
                //tmp.parentDept = dplDepartment.SelectedValue;
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
                hiddenid.Value = mod.Id;
                var dpent = new AD_Department_WorkGroupDAL().GetList().FirstOrDefault(item => item.p2 == mod.Department && item.p1 == "False");
                if(dpent!=null)
                dplDepartment.SelectedValue =dpent.ID.ToString() ;

                txtEnableDate.Text = mod.expiryDate != null ? Convert.ToDateTime(mod.expiryDate.ToString()).ToString("yyyy-MM-dd") : "";
                editHRInfo();

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
               EmployeeModel= new IAMEntities().HREmployee.FirstOrDefault(item => item.code == Employeecode);
            }
            else
            {
                EmployeeModel=new IAMEntities().HREmployee.FirstOrDefault(item => item.code == Employeecode && (item.leavePostsDate == null || item.leavePostsDate > DateTime.Now) == true);
            }
            if (EmployeeModel != null)
            {
                var en = new AccountMapingDAL().GetOne(Employeecode, Unitity.SystemType.AD.ToString(), "员工");
                //if (en != null && IsUpdate == false)
                //{
                //    QueryPage("2");
                //    return;
                //}
                //else 
                if (en == null && IsUpdate == false)
                {
                    QueryPage("-1");
                    return;
                }

                if (!IsUpdate)
                {
                    if (string.IsNullOrEmpty(txtChineseName.Text) && string.IsNullOrEmpty(txtDisplayName.Text))
                    {
                        txtChineseName.Text = txtDisplayName.Text = EmployeeModel.name;
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
            dplZhiji.DataSource = new AD_Zhiji_WorkGroupDAL().GetList().Where(item => item.p1 == "False").OrderBy(item => item.Zhiji).ToList();
            dplZhiji.DataValueField = "WorkGroup";
            dplZhiji.DataTextField = "Zhiji";
            dplZhiji.DataBind();
            dplZhiji.SelectedValue = "";

            dplDepartment.DataSource = new AD_Department_WorkGroupDAL().GetList().OrderBy(item => item.ordercolumn).ToList();
            dplDepartment.DataValueField = "ID";
            dplDepartment.DataTextField = "p2";
            dplDepartment.DataBind();
            ListItem li = new ListItem("", "");
            li.Selected = true;
            dplDepartment.Items.Add(li);

        }

        AD_UserInfo AdEntity()
        {
            AD_UserInfo tmp = new AD_UserInfo();
            tmp.CnName = txtChineseName.Text;
            tmp.DisplayName = txtDisplayName.Text;
            tmp.Accountname = txtLoginName.Text;
            tmp.PASSWORD = txtPassword.Text;
            tmp.ADTel = txtMobleNumber.Text;
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
            tmp.Id = hiddenid.Value ;
            tmp.expiryDate = txtEnableDate.Text.Trim() == string.Empty ? (DateTime?)null : Convert.ToDateTime(txtEnableDate.Text);
            return tmp;
        }

        void QueryGroup()
        {
            List<AD_UserWorkGroup> list = new List<AD_UserWorkGroup>();

            if (ViewState["userwork"] == null)
            {
                using (IAMEntities db = new IAMEntities())
                {
                    list = db.AD_UserWorkGroup.Where(item => item.Uid == acc&&item.isdr!=1).ToList();
                }
                if (IsUpdate == false)
                {
                    ViewState["userwork"] = new List<AD_UserWorkGroup>();
                }
                else
                {
                    ViewState["userwork"] = list;
                }
            }
            else
            {
                list = (List<AD_UserWorkGroup>)ViewState["userwork"];
            }

            list = list.Where(item => item.isdr != 1).ToList();
           

            var defaultworkgroup = new AD_DefaultWorkGroupDAL().GetList().Where(item => item.IsDelete == false).FirstOrDefault().NAME;
            var defa = list.Where(item => item.GroupName == defaultworkgroup).ToList();

            var kekong = list.Where(item => item.GroupName != defaultworkgroup).ToList();

            DefaultGroupRepeater.DataSource = defa;
            DefaultGroupRepeater.DataBind();
          

            kekongGroupsRepeater.DataSource = kekong;
            kekongGroupsRepeater.DataBind();

        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        bool CheckValue(out string mess)
        {
            bool istrue = true;
            mess = "";
            if (string.IsNullOrEmpty(txtNumber.Text.Trim()))
            {
                istrue = false;
                mess = "工号";
            }
            else if (string.IsNullOrEmpty(txtChineseName.Text.Trim()))
            {
                istrue = false;
                mess = "cn名称";
            }
            else if (string.IsNullOrEmpty(txtDisplayName.Text.Trim()))
            {
                istrue = false;
                mess = "显示名";
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
            else if (string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                istrue = false;
                mess = "描述";
            }

            return istrue;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string mess = "";
            txtLoginName.ReadOnly = false;
            if (!CheckValue(out mess))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('" + mess + "字段不能为空');", true);
                return;
            }
            btnShengcheng_Click(btnShengcheng, new EventArgs());
            BLL.AddUserMail addmail = new BLL.AddUserMail();
            addmail.Actioner = base.UserInfo.adname;
            addmail.SystemName = Unitity.SystemType.AD.ToString();
            addmail.SystemType = "其他";
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
                        UserType = "其他"
                    };
                    new AccountMapingDAL().Add(tmp);
                    BLL.ActionLog.CreateLog(base.UserInfo.adname, tmp.zhanghao, Unitity.SystemType.AD.ToString(), "其他");
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

                    using (IAMEntities db = new IAMEntities())
                    {

                        db.ExecuteStoreCommand(stb.ToString());
                        db.SaveChanges();
                    }

                    addmail.UserInfo = new BLL.CompareEntity<AD_UserInfo>().ReturnCompareStringForMailAdd(AdEntity(), Unitity.SystemType.AD);
                    addmail.RoleString = BLL.CompareRoleList.AD((List<AD_UserWorkGroup>)ViewState["userwork"], AdEntity().Accountname);
                    new BLL.MailServices().CreateUserMail(addmail);

                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('添加成功');window.close();", true);
                }
            }
            else
            {
                int count;
                AD_UserInfo entity = new ADUserInfoDAL().GetADUserInfo(null, null, null, out count).FirstOrDefault(item => item.Accountname == txtLoginName.Text);
                AD_UserInfo old = entity;
                using (IAMEntities db = new IAMEntities())
                {
                    string sql = "DELETE FROM dbo.AD_UserWorkGroup WHERE Uid='" + entity.Accountname + "' AND (GroupName='" + entity.Department + "' or GroupName='" + entity.Job + "') ";
                    db.ExecuteStoreCommand(sql);
                }

                entity = AdEntity();

                if (new ADUserInfoDAL().UpdateUserInfo(entity) > 0)
                {
                    ADUserWorkGroupDAL _tmpservices = new ADUserWorkGroupDAL();

                    new BLL.ActionLog().EditLog<AD_UserInfo>(base.UserInfo.adname, entity.Accountname, Unitity.SystemType.AD.ToString(), "其他", old, entity);
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
                    stb.Append(@"update AccountMaping set usertype='其他' where gonghao='" + txtNumber.Text.Trim() + "' and type='AD' and zhanghao='" + acc + "'");
                    stb.Append(@"update AccountMaping set usertype='其他',gonghao='" + txtNumber.Text.Trim() + "'where type='AD' and zhanghao='" + acc + "'");
                    using (IAMEntities db = new IAMEntities())
                    {
                        db.ExecuteStoreCommand(stb.ToString());
                        db.SaveChanges();
                    }

                    addmail.UserInfo = new BLL.CompareEntity<AD_UserInfo>().ReturnCompareStringForMailUpdate(old, AdEntity(), Unitity.SystemType.AD);
                    addmail.RoleString = BLL.CompareRoleList.AD((List<AD_UserWorkGroup>)ViewState["userwork"], AdEntity().Accountname);
                    if (!chkEnable.Checked)
                    {
                        using (IAMEntities db = new IAMEntities())
                        {

                            //db.ExecuteStoreCommand("update dbo.AD_UserWorkGroup set isdr=1 where uid='" + AdEntity().Accountname + "'");
                            db.ExecuteStoreCommand(string.Format("delete dbo.AD_UserWorkGroup where uid='{0}'",AdEntity().Accountname));
                            db.SaveChanges();
                        }
                        new BLL.MailServices().DisabledUserMail(addmail);
                    }
                    else
                    {
                        new BLL.MailServices().UpdateUserMail(addmail);
                    }

                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('更新成功');window.close(true);", true);
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
                        Uid = txtLoginName.Text,
                        GroupName = a,
                        isdr = 2
                    };
                    uli.Add(n);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(updatepanel1,this.GetType(),"","alert('已存在该"+a+"工作组');",true);
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
            //描述
            if (txtLoginName.Text.Trim() == string.Empty)
            {
                QueryPage("yes");
                Response.Write("<script>alert('请先填写ad登陆名');</script>");
                return;
            }

            //描述 邮件数据库     
            if (!string.IsNullOrEmpty(dplDepartment.SelectedValue) && string.IsNullOrEmpty(txtMailDataBase.Text))
            {
                Guid id = new Guid(dplDepartment.SelectedValue);
                AD_Department_WorkGroup tmp = new AD_Department_WorkGroupDAL().GetList().FirstOrDefault(item => item.ID == id);
                txtMailDataBase.Text = tmp.EmailDataBase;
            }

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
            //职级组
            //if (!string.IsNullOrEmpty(dplZhiji.SelectedValue))
            //{
            //    if (uli.FirstOrDefault(item => item.GroupName == dplZhiji.SelectedValue) == null)
            //    {
            //        AD_UserWorkGroup tp = new AD_UserWorkGroup() { isdr = 2, ID = Guid.NewGuid(), GroupName = dplZhiji.SelectedValue, Uid = txtLoginName.Text.Trim(), memo = "no" };
            //        uli.Add(tp);

            //    }
            //}
            //可控组
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
            QueryGroup();
            Query();
        }
    }
}