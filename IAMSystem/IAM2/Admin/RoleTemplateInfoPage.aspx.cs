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
    public partial class RoleTemplateInfoPage : BasePage
    {
        Guid TemplateId
        {
            get
            {
                if (Request.QueryString["id"] != null)
                    return new Guid(Request.QueryString["id"]);
                else
                    return Guid.Empty;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!base.ReturnUserRole.Admin && !base.ReturnUserRole.Leader)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('你无权限查看该页面');", true);
                    return;
                }
                Bind();
            }
        }
        List<RoleTemplateInfo> Templatelist = new List<RoleTemplateInfo>();
        void Bind()
        {
            List<RoleTemplateInfo> listInfo = new List<RoleTemplateInfo>();
            using (IAMEntities db = new IAMEntities())
            {
                listInfo = db.RoleTemplateInfo.Where(item => item.TemplateID == TemplateId).ToList();
                lblTemplateName.Text = db.RoleTemplate.FirstOrDefault(item => item.ID == TemplateId).TemplateName;
            }

            repeater1SAPuserRole.DataSource = listInfo.Where(item => item.SystemName == "SAP").ToList().OrderBy(item => item.p1).ThenBy(item => item.p2);
            repeater1SAPuserRole.DataBind();

            repeater1HECUserrole.DataSource = listInfo.Where(item => item.SystemName == "HEC").ToList().OrderBy(item => item.p1).ThenBy(item => item.p2);
            repeater1HECUserrole.DataBind();

            repeater1.DataSource = listInfo.Where(x => x.SystemName == "HEC2").ToList().OrderBy(x => x.p1).ThenBy(x => x.p2);
            repeater1.DataBind();

            repeaterAD.DataSource = listInfo.Where(item => item.SystemName == "AD").ToList().OrderBy(item => item.p1).ThenBy(item => item.p2);
            repeaterAD.DataBind();

            repeaterComputer.DataSource = listInfo.Where(item => item.SystemName == "ADComputer").ToList().OrderBy(item => item.p1).ThenBy(item => item.p2);
            repeaterComputer.DataBind();

            repeaterHRUserRole.DataSource = listInfo.Where(item => item.SystemName == "HR").ToList().OrderBy(item => item.p1).ThenBy(item => item.p2);
            repeaterHRUserRole.DataBind();

            repeaterTC.DataSource = listInfo.Where(item => item.SystemName == "TC").ToList().OrderBy(item => item.p1).ThenBy(item => item.p2);
            repeaterTC.DataBind();
        }


        protected void btnsapDelete_Command(object sender, CommandEventArgs e)
        {
            Guid id = new Guid(e.CommandArgument.ToString());
            RoleTemplateInfo entity = new RoleTemplateInfo() { ID = id };
            if (new IAMEntityDAL.RoleTemplateInfoDAL().DeleteRoleTemplateInfo(entity) > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('删除成功');window.location.href=window.location.href;", true);
            }
        }
        void BindSAPUserRoleReport()
        {
            string bapiname = txtUsername.Text.Trim();
            List<V_Sap_UserRoleReport> SapUserRoleList = new List<V_Sap_UserRoleReport>();
            SapUserRoleList = new IAMEntityDAL.V_Sap_UserRoleReportDAL().GetV_Sap_UserRoleReport(string.Empty);// db.V_Sap_UserRoleReport.Where(item => item.uBAPIBNAME == EmployeeCode).ToList();
            SapUserRoleList = SapUserRoleList.Where(item => item.uBAPIBNAME == bapiname && item.urisdr != 1).ToList();
            foreach (var item in SapUserRoleList)
            {
                RoleTemplateInfo entity = new RoleTemplateInfo()
                {
                    ID = Guid.NewGuid(),
                    CompanyName = string.Empty,
                    RoleID = item.srROLEID,
                    RoleName = item.srROLENAME,
                    StartDate = item.srSTATEDATE,
                    EndDate = item.srENDDATE,
                    SystemName = "SAP",
                    p1 = dplUserType111.SelectedValue,
                    p2 = item.mzhanghao
                };
                Templatelist.Add(entity);
            }
        }

        void BindHRUserRoleReport()
        {
            string hrname = txtUsername.Text;
            List<V_HRSm_User_Role> hrUserRoleList = new List<V_HRSm_User_Role>();
            int count = 0;
            List<HRCompany> companylist = new IAMEntityDAL.HRCompanyDAL().GetHrCompany(out count);
            hrUserRoleList = new IAMEntityDAL.V_HRSm_User_RoleDAL().GetV_HRSm_UserList(string.Empty); //db.V_HRSm_User_Role.Where(item=>item.UserName==EmployeeCode).ToList();
            hrUserRoleList = hrUserRoleList.Where(item => item.mzhanghao == hrname && item.dr != 1).ToList();
            foreach (var item in hrUserRoleList)
            {
                var companyentity = companylist.FirstOrDefault(a => a.Pk_CORP == item.CompanyKey);
                string companyname = string.Empty;
                if (companyentity != null)
                    companyname = companyentity.UNTTNAME;
                RoleTemplateInfo entity = new RoleTemplateInfo()
                {
                    ID = Guid.NewGuid(),
                    CompanyName = companyname,
                    RoleID = item.hrrRoleCode,
                    RoleName = item.role_name,
                    SystemName = "HR",
                    EndDate = "",
                    StartDate = "",
                    p1 = dplUserType111.SelectedValue,
                    p2 = item.mzhanghao
                };
                Templatelist.Add(entity);
            }
        }

        void BindHECUserRoleReport()
        {
            string hecname = txtUsername.Text;
            List<V_HECUSER_Role> hecUserRoleLIst = new List<V_HECUSER_Role>();
            hecUserRoleLIst = new IAMEntityDAL.V_HECUSER_RoleDAL().Get_HECUser_RoleList(string.Empty);// db.V_HECUSER_Role.Where(item=>item.uEMPLOYEECODE==EmployeeCode).ToList();
            hecUserRoleLIst = hecUserRoleLIst.Where(item => item.uUSERNAME == hecname && item.isdr != 1).ToList();
            foreach (var item in hecUserRoleLIst)
            {
                RoleTemplateInfo entity = new RoleTemplateInfo()
                {
                    ID = Guid.NewGuid(),
                    CompanyName = item.cCOMPNYFULLNAME,
                    StartDate = item.cSTARTDATE,
                    EndDate = item.cENDDATE,
                    SystemName = "HEC",
                    RoleName = item.rROLENAME,
                    RoleID = item.rROLECODE,
                    p1 = dplUserType111.SelectedValue,
                    p2 = item.mzhanghao
                };
                Templatelist.Add(entity);
            }
        }
        void BindHECGangWeiReport()
        {
            string hecname = txtUsername.Text;
            List<V_HEC_Gangwei> list = new List<V_HEC_Gangwei>();
            int count;
            list = new IAMEntityDAL.VHECGangWei().GetVHECGangwei(hecname, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Y", int.MaxValue, 1, out count, true);
            foreach (var x in list)
            {
                RoleTemplateInfo entity = new RoleTemplateInfo()
                {
                    ID = Guid.NewGuid(),
                    CompanyName = x.COMPANY_CODE + "^" + x.COMPANY_NAME,
                    StartDate = "",
                    EndDate = "",
                    SystemName = "HEC2",
                    RoleName = x.UNIT_CODE + "^" + x.UNIT_NAME + "^" + x.POSITION_CODE + "^" + x.POSITION_NAME,
                    RoleID = x.POSITION_CODE,
                    p1 = dplUserType111.SelectedValue,
                    p2 = x.zhanghao
                };
                Templatelist.Add(entity);
            }
        }

        void BindADUserRoleReport()
        {
            string aduname = txtUsername.Text;
            List<V_AD_UserWorkGroup> adUserRoleList = new List<V_AD_UserWorkGroup>();
            adUserRoleList = new IAMEntityDAL.V_AD_UserWorkGroupDAL().Get_V_AD_UserWorkGroupList(string.Empty);
            adUserRoleList = adUserRoleList.Where(item => item.uAccountname == aduname && item.isdr != 1).ToList();
            foreach (var item in adUserRoleList)
            {
                RoleTemplateInfo entity = new RoleTemplateInfo()
                {
                    ID = Guid.NewGuid(),
                    CompanyName = string.Empty,
                    StartDate = "",
                    EndDate = item.uexpirydate != null ? Convert.ToDateTime(item.uexpirydate.ToString()).ToString("yyyy-MM-dd") : string.Empty,
                    SystemName = "AD",
                    RoleName = item.uwGroupName,
                    RoleID = item.uwGroupName,
                    p1 = dplUserType111.SelectedValue,
                    p2 = item.mzhanghao
                };
                Templatelist.Add(entity);
            }
        }

        void BindADComputer()
        {
            string adname = txtUsername.Text;
            List<V_AdcomputerWorkGroupInfo> adcomputerworkgroup = new List<V_AdcomputerWorkGroupInfo>();
            adcomputerworkgroup = new IAMEntityDAL.V_AdcomputerWorkGroupInfoDAL().GetList(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            adcomputerworkgroup = adcomputerworkgroup.Where(item => item.aName == adname && item.wisdr != 1).ToList();
            foreach (var item in adcomputerworkgroup)
            {
                if (item.wcomputername == null || item.wworkgroup == null)
                {
                    continue;
                }
                RoleTemplateInfo entity = new RoleTemplateInfo()
                {
                    ID = Guid.NewGuid(),
                    CompanyName = string.Empty,
                    StartDate = "",
                    EndDate = string.Empty,
                    SystemName = "ADComputer",
                    RoleName = item.wworkgroup,
                    RoleID = item.wcomputername,
                    p1 = dplUserType111.SelectedValue,
                    p2 = item.bzhanghao
                };
                Templatelist.Add(entity);
            }
        }

        void BindTCUserRoleReport()
        {
            string tcname = txtUsername.Text;
            List<V_TCReport> tcUserRoleList = new List<V_TCReport>();
            tcUserRoleList = new IAMEntityDAL.V_TCReportDAL().Get_V_TC_Repost_List_as_User(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, null, null, string.Empty, string.Empty, string.Empty, string.Empty);
            tcUserRoleList = tcUserRoleList.Where(item => item.uUserID == tcname && item.isdr != 1).ToList();
            foreach (var item in tcUserRoleList)
            {
                RoleTemplateInfo entity = new RoleTemplateInfo()
                {
                    ID = Guid.NewGuid(),
                    CompanyName = string.Empty,
                    StartDate = "",
                    EndDate = string.Empty,
                    SystemName = "TC",
                    RoleName = item.urp2,
                    RoleID = item.urp1,
                    p1 = dplUserType111.SelectedValue,
                    p2 = item.mzhanghao
                };
                Templatelist.Add(entity);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text.Trim()))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('模版名称不能为空！');", true);
                return;
            }

            string syst = dplSystemType.SelectedValue;
            switch ((Unitity.SystemType)Enum.Parse(typeof(Unitity.SystemType), syst, true))
            {
                case Unitity.SystemType.AD:
                    BindADUserRoleReport();
                    break;
                case Unitity.SystemType.ADComputer:
                    BindADComputer();
                    break;
                case Unitity.SystemType.HEC:
                    BindHECUserRoleReport();
                    BindHECGangWeiReport();
                    break;
                case Unitity.SystemType.HR:
                    BindHRUserRoleReport();
                    break;
                case Unitity.SystemType.SAP:
                    BindSAPUserRoleReport();
                    break;
                case Unitity.SystemType.TC:
                    BindTCUserRoleReport();
                    break;
            }

            Guid tempid = TemplateId;
            if (new RoleTemplateInfoDAL().CreateRoleTemplateInfo(Templatelist, tempid, true) > 0)
            {
                BLL.ActionLog.CreateLog(base.UserInfo.adname, "将" + dplSystemType.SelectedValue + "系统中" + txtUsername.Text + "账号权限添加进模版中<br/>模版名为:" + lblTemplateName.Text + "");
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('添加成功');window.location.href = window.location.href;", true);
            }
        }

    }
}