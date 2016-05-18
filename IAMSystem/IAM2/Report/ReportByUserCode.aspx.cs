using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseDataAccess;

namespace IAM.Report
{
    public partial class ReportByUserCode : BasePage
    {
        string EmployeeCode
        {
            get
            {
                if (ReturnUserRole.EndUser == true)
                {
                    var amaping = new IAMEntityDAL.AccountMapingDAL().GetOne(base.UserInfo.adname, "AD");
                    if (amaping != null)
                        return amaping.gonghao;
                    else
                        return "";
                }
                else
                {
                    if (string.IsNullOrEmpty(txtNumber.Text))
                        return "";
                    else
                        return txtNumber.Text.Trim();
                }
            }
        }
        IAMEntities db = new IAMEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //if (!base.ReturnUserRole.Admin&&!base.ReturnUserRole.Leader&&!base.ReturnUserRole.EndUser)
                //{
                //    base.NoRole();
                //}
                if (Request.QueryString["gonghao"] != null)
                {
                    txtNumber.Text = Request.QueryString["gonghao"];
                    btnQuery_Click(btnQuery, new EventArgs());
                }
            }
        }

        void BindHREmployee()
        {

            var list = db.HREmployee.Where(item => item.code == EmployeeCode).Join(db.HRDepartment, item => item.dept, itm => itm.dept, (item, itm) => new
            {
                code = item.code,
                posts = item.posts,
                deptname = itm.name,
                name = item.name,
                moblephone = item.moblePhone,
                topostDate = item.toPostsDate,
                leavePostsDate = item.leavePostsDate,
                userScope = item.userScope,
                isSync = item.isSync,
                syncdate = item.syncDate
            }).ToList();
            repeater1HrEmployee.DataSource = list;
            repeater1HrEmployee.DataBind();
        }

        void BindSAPUserRoleReport()
        {
            List<V_Sap_UserRoleReport> SapUserRoleList = new List<V_Sap_UserRoleReport>();
            SapUserRoleList = new IAMEntityDAL.V_Sap_UserRoleReportDAL().GetV_Sap_UserRoleReport(EmployeeCode);// db.V_Sap_UserRoleReport.Where(item => item.uBAPIBNAME == EmployeeCode).ToList();
            repeater1SAPuserRole.DataSource = SapUserRoleList.OrderBy(item => item.uBAPIBNAME).ThenBy(item => item.urRoleID);
            repeater1SAPuserRole.DataBind();
        }

        void BindHRUserRoleReport()
        {
            List<V_HRSm_User_Role> hrUserRoleList = new List<V_HRSm_User_Role>();
            hrUserRoleList = new IAMEntityDAL.V_HRSm_User_RoleDAL().GetV_HRSm_UserList(EmployeeCode); //db.V_HRSm_User_Role.Where(item=>item.UserName==EmployeeCode).ToList();
            repeaterHRUserRole.DataSource = hrUserRoleList.OrderBy(item => item.UserName).ThenBy(item => item.role_name);
            repeaterHRUserRole.DataBind();
        }

        void BindHECUserRoleReport()
        {
            List<V_HECUSER_Role> hecUserRoleLIst = new List<V_HECUSER_Role>();
            hecUserRoleLIst = new IAMEntityDAL.V_HECUSER_RoleDAL().Get_HECUser_RoleList(EmployeeCode);// db.V_HECUSER_Role.Where(item=>item.uEMPLOYEECODE==EmployeeCode).ToList();
            repeater1HECUserrole.DataSource = hecUserRoleLIst.OrderBy(item => item.uUSERNAME).ThenBy(item => item.rROLECODE);
            repeater1HECUserrole.DataBind();
        }

        void BindHECUserGangWeiReport()
        {
            int count;
            var list = new IAMEntityDAL.VHECGangWei().GetVHECGangwei(EmployeeCode, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,"Y", int.MaxValue, 1, out count, true);
            repeater1.DataSource = list;
            repeater1.DataBind();
        }

        void BindADUserRoleReport()
        {
            List<V_AD_UserWorkGroup> adUserRoleList = new List<V_AD_UserWorkGroup>();
            adUserRoleList = new IAMEntityDAL.V_AD_UserWorkGroupDAL().Get_V_AD_UserWorkGroupList(EmployeeCode);
            repeaterAD.DataSource = adUserRoleList.OrderBy(item => item.uAccountname).ThenBy(item => item.uGroup);
            repeaterAD.DataBind();
        }

        void BindTCUserRoleReport()
        {
            List<V_TCReport> tcUserRoleList = new List<V_TCReport>();
            tcUserRoleList = new IAMEntityDAL.V_TCReportDAL().Get_V_TC_Repost_List_as_User(EmployeeCode, string.Empty, string.Empty, string.Empty, string.Empty, null, null, string.Empty, string.Empty, string.Empty, string.Empty);
            repeaterTCUserInfo.DataSource = tcUserRoleList.OrderBy(item => item.uUserID).ThenBy(item => item.urMemo);
            repeaterTCUserInfo.DataBind();
        }

        void BindADComputer()
        {

            List<V_AdcomputerWorkGroupInfo> adcomputerlist = new List<V_AdcomputerWorkGroupInfo>();
            adcomputerlist = new IAMEntityDAL.V_AdcomputerWorkGroupInfoDAL().GetList(EmployeeCode, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            repeaterADComputer.DataSource = adcomputerlist.OrderBy(item => item.aName).ThenBy(item => item.wworkgroup);
            repeaterADComputer.DataBind();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindHREmployee();
            BindHRUserRoleReport();
            BindHECUserRoleReport();
            BindHECUserGangWeiReport();
            BindADUserRoleReport();
            BindSAPUserRoleReport();
            BindTCUserRoleReport();
            BindADComputer();
        }
    }
}