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
    public partial class ADInfo : BasePage
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["userid"] != null)
                {

                    QueryAdUserInfo(Request.QueryString["userid"]);
                    Query();
                    QueryAccUserMapping();
                }
                int count;
                ddlWorkGroup.DataSource = new ADWorkGroupDAL().GetADWorkGroupList(out count);
                ddlWorkGroup.DataTextField = "NAME";
                ddlWorkGroup.DataValueField = "NAME";
                ddlWorkGroup.DataBind();
                ListItem li = new ListItem("", "");
                li.Selected = true;
                ddlWorkGroup.Items.Add(li);
               
                btnQuery.Enabled = base.ReturnUserRole.Admin;
                btnComputer.Disabled = !base.ReturnUserRole.Admin;
                btnCreateGroup.Enabled = base.ReturnUserRole.Admin;
                Button1.Enabled = base.ReturnUserRole.Admin;
                if (base.ReturnUserRole.AD == false&&base.ReturnUserRole.Leader==false&&base.ReturnUserRole.Admin==false)
                {
                    ClientScript.RegisterStartupScript(this.GetType(),"","alert('你无权查看该页面');window.close();",true);
                    return;
                }
            }
        }

        /// <summary>
        /// 查询hr信息
        /// </summary>
        void Query()
        {
            string Employeecode = string.IsNullOrEmpty(txtNumber.Text) ? "" : txtNumber.Text.Trim();
            HREmployee EmployeeModel = new IAMEntities().HREmployee.FirstOrDefault(item => item.code == Employeecode && (item.leavePostsDate==null||item.leavePostsDate>DateTime.Now)==true);
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
                    txtPartment.Text = txtDepartMent.Text = departmentModel.name;
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

        /// <summary>
        /// 查询Ad信息
        /// </summary>
        /// <param name="UserId"></param>
        void QueryAdUserInfo(string UserId)
        {
            AD_UserInfo userEntity = db.AD_UserInfo.FirstOrDefault(item => item.UserID == UserId);
            AccountMaping accmapping = new AccountMaping();
            if (userEntity != null)
            {
                txtChineseName.Text = userEntity.CnName;
                txtDisplayName.Text = userEntity.DisplayName;
                txtLoginName.Text = userEntity.Accountname;
                txtPassword.Text = userEntity.PASSWORD;
                txtDescription.Text = userEntity.DESCRIPTION;
                txtDepartMent.Text = userEntity.Department;
                //txtPostLevel.Text = userEntity.Job;
                txtDisk.Text = userEntity.Drive;
                txtDiskNumber.Text = userEntity.PATH;
                txtEmail.Text = userEntity.Email;
                txtLyncNumber.Text = userEntity.Lync;
                //txtMailSize.Text = userEntity.Mailstorage.ToString();
                ComputerName = userEntity.Accountname;
                accmapping = db.AccountMaping.FirstOrDefault(item => item.zhanghao == UserId && item.type == "AD");
                txtNumber.Text = accmapping != null ? accmapping.gonghao : "";
                txtMemo.Text = userEntity.Memo;
                txtEnableDate.Text = userEntity.LeavePostsDate!=null?Convert.ToDateTime(userEntity.LeavePostsDate.ToString()).ToString("yyyy-MM-dd"):"";
                QueryComputer();
            }

        }

        /// <summary>
        /// 查询计算机信息
        /// </summary>
        void QueryComputer()
        {
            int count = 0;

            List<AD_Computer> computerlist = new List<AD_Computer>();
            if (ViewState["computerlist"] != null)
            {
                computerlist = (List<AD_Computer>)ViewState["computerlist"];
            }
            else
            {
                computerlist = _adcomputerServices.GetAdComputerList(out count).Where(item => item.NAME == ComputerName).ToList();
                ViewState["computerlist"] = computerlist;
            }

            repeaterComputer.DataSource = computerlist;
            repeaterComputer.DataBind();
        }

        /// <summary>
        /// 获取用户类型
        /// </summary>
        void QueryAccUserMapping()
        {
            AccountMaping accmap = _accServicer.GetOne(txtNumber.Text.Trim(), Unitity.SystemType.AD.ToString(), txtLoginName.Text.Trim());
            //dplEmployeeType.SelectedValue = accmap.UserType;
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        /// <summary>
        /// 添加计算机信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void btnCreateComputer_Click(object sender, EventArgs e)
        //{
        //    if (txtNumber.Text.Trim() != string.Empty)
        //        return;
        //    List<AD_Computer> computerlist = (List<AD_Computer>)ViewState["computerlist"];
        //    computerlist.Add(new AD_Computer() { DESCRIPTION = txtComputerDescritption.Text.Trim(), ID = Guid.NewGuid(), NAME = txtComputerName.Text });
        //    ViewState["computerlist"] = computerlist;
        //    QueryComputer();
        //}

        protected void btnCreateGroup_Click(object sender, EventArgs e)
        {
            AD_UserWorkGroup entity = new AD_UserWorkGroup() { ID = Guid.NewGuid(), GroupName = ddlWorkGroup.SelectedValue, Uid = txtLoginName.Text.Trim(), IsSync = false };
            if (new ADUserWorkGroupDAL().Add(entity) > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('添加成功');window.location.href=window.location;", true);
            }
        }

        int CreateOrUpdate()
        {
            AD_UserInfo entity = new AD_UserInfo();
            entity.NAME = txtName.Text;
            entity.Posts = txtGangwei.Text;
            entity.HRMoblePhone = txtPhoneNumber.Text;
            entity.ToPostsDate = Convert.ToDateTime(txtComeDate.Text);
            entity.LeavePostsDate = txtOutDate.Text.Trim() == string.Empty ? (DateTime?)null : Convert.ToDateTime(txtOutDate.Text);
            entity.dept = hiddenDepartMentId.Value;
            entity.parentDept = hiddenPreMentId.Value;
            entity.IsRevoke = chkPartmentOut.Checked;
            entity.LeavePostsDate = txtEnableDate.Text.Trim() == string.Empty ? (DateTime?)null : Convert.ToDateTime(txtEnableDate.Text);
            entity.CnName = txtChineseName.Text;
            entity.DisplayName = txtDisplayName.Text;
            entity.Accountname = txtLoginName.Text;
            entity.PASSWORD = txtPassword.Text;
            entity.DESCRIPTION = txtDescription.Text;
            entity.Department = txtDepartMent.Text;
            //entity.Job = txtPostLevel.Text;
            entity.Drive = txtDisk.Text;
            entity.PATH = txtDiskNumber.Text;
            entity.Email = txtEmail.Text;
            entity.Lync = txtLyncNumber.Text;
            //entity.Mailstorage = Convert.ToInt32(txtMailSize.Text);
            entity.Accountname = ComputerName;
            entity.UserID = txtNumber.Text.Trim();
            entity.Id = txtNumber.Text.Trim();
            entity.PASSWORD = txtPassword.Text;
            entity.Memo = txtMemo.Text.Trim();
            List<AD_Computer> computerlist = (List<AD_Computer>)ViewState["computerlist"];
            var accountMaping = new AccountMaping()
            {
                //UserType = dplEmployeeType.SelectedValue,
                gonghao = txtNumber.Text.Trim(),
                zhanghao = txtLoginName.Text.Trim(),
                type = Unitity.SystemType.AD.ToString(),
                id = Guid.NewGuid()
            };
            if (IsUpdate) { }
            //_accServicer.UpdateAccountMaping(accountMaping);
            else
                _accServicer.Add(accountMaping);

            return _adUserInfoServices.CreateOrUpdate(entity, computerlist);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (CreateOrUpdate() > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('添加成功');", true);
            }
        }

        protected void lbtnDelete_Command(object sender, CommandEventArgs e)
        {
            if (new ADUserWorkGroupDAL().DeleteAdUserWorkGroup(new Guid(e.CommandArgument.ToString())) > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('删除成功');window.location.href=window.location;", true);
            }
        }

        protected void lbtncomdel_Command(object sender, CommandEventArgs e)
        {

        }
    }
}