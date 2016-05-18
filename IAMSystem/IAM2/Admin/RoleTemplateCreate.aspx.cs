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
    public partial class RoleTemplateCreate : BasePage
    {
        string TemplateName
        {
            get
            {
                return Request.QueryString["templatename"];
            }
           
        }

        List<RoleTemplateInfo> Templatelist = new List<RoleTemplateInfo>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!base.ReturnUserRole.Admin)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('你无权限查看该页面');", true);
                    return;
                }
                if (Request.QueryString["templatename"] != null)
                {
                    piliangdiv.Visible = false;
                    dangediv.Visible = true;
                }
                else
                {
                    piliangdiv.Visible = true;
                    dangediv.Visible = false;
                }
            }
        }

        void BindSAPUserRoleReport(string gonghao="")
        {
            List<V_Sap_UserRoleReport> SapUserRoleList = new List<V_Sap_UserRoleReport>();
            SapUserRoleList = new IAMEntityDAL.V_Sap_UserRoleReportDAL().GetV_Sap_UserRoleReport(Request.QueryString["templatename"] != null?TemplateName:gonghao);// db.V_Sap_UserRoleReport.Where(item => item.uBAPIBNAME == EmployeeCode).ToList();
            foreach (var item in SapUserRoleList)
            {
                if (item.urisdr != 1)
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
                        p1 = item.mUserType,
                        p2 = item.mzhanghao
                    };
                    Templatelist.Add(entity);
                }
            }
        }

        void BindHRUserRoleReport(string gonghao = "")
        {
            List<V_HRSm_User_Role> hrUserRoleList = new List<V_HRSm_User_Role>();
            int count = 0;
            List<HRCompany> companylist = new IAMEntityDAL.HRCompanyDAL().GetHrCompany(out count);
            hrUserRoleList = new IAMEntityDAL.V_HRSm_User_RoleDAL().GetV_HRSm_UserList(Request.QueryString["templatename"] != null ? TemplateName : gonghao); //db.V_HRSm_User_Role.Where(item=>item.UserName==EmployeeCode).ToList();
            foreach (var item in hrUserRoleList)
            {
                if (item.dr != 1)
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
                        p1 = item.mUserType,
                        p2 = item.mzhanghao
                    };
                    Templatelist.Add(entity);
                }
            }
        }

        void BindHECUserRoleReport(string gonghao = "")
        {
            List<V_HECUSER_Role> hecUserRoleLIst = new List<V_HECUSER_Role>();
            hecUserRoleLIst = new IAMEntityDAL.V_HECUSER_RoleDAL().Get_HECUser_RoleList(Request.QueryString["templatename"] != null ? TemplateName : gonghao);// db.V_HECUSER_Role.Where(item=>item.uEMPLOYEECODE==EmployeeCode).ToList();
            foreach (var item in hecUserRoleLIst)
            {
                if (item.isdr != 1)
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
                        p1 = item.mUserType,
                        p2 = item.mzhanghao
                    };
                    Templatelist.Add(entity);
                }
            }
        }

        void BindHECUserGangwei(string gonghao = "")
        {
            List<V_HEC_Gangwei> list = new List<V_HEC_Gangwei>();
            int count;
            list = new IAMEntityDAL.VHECGangWei().GetVHECGangwei(Request.QueryString["templatename"] != null ? TemplateName : gonghao, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "Y", int.MaxValue, 1, out count, true);
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
                    p1 = x.zhanghaoleixing,
                    p2 = x.zhanghao
                };
                Templatelist.Add(entity);
            }

        }

        void BindADUserRoleReport(string gonghao = "")
        {
            List<V_AD_UserWorkGroup> adUserRoleList = new List<V_AD_UserWorkGroup>();
            adUserRoleList = new IAMEntityDAL.V_AD_UserWorkGroupDAL().Get_V_AD_UserWorkGroupList(Request.QueryString["templatename"] != null ? TemplateName : gonghao);
            foreach (var item in adUserRoleList)
            {
                if (item.isdr != 1)
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
                        p1 = item.mUserType,
                        p2 = item.mzhanghao
                    };
                    Templatelist.Add(entity);
                }
            }
        }

        void BindADComputer(string gonghao = "")
        {
            List<V_AdcomputerWorkGroupInfo> adcomputerworkgroup = new List<V_AdcomputerWorkGroupInfo>();
            adcomputerworkgroup = new IAMEntityDAL.V_AdcomputerWorkGroupInfoDAL().GetList(Request.QueryString["templatename"] != null ? TemplateName : gonghao, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            foreach (var item in adcomputerworkgroup)
            {
                if (item.wcomputername == null || item.wworkgroup == null)
                {
                    continue;
                }
                if (item.wisdr != 1)
                {
                    RoleTemplateInfo entity = new RoleTemplateInfo()
                    {
                        ID = Guid.NewGuid(),
                        CompanyName = string.Empty,
                        StartDate = "",
                        EndDate = string.Empty,
                        SystemName = "ADComputer",
                        RoleName = item.wworkgroup,
                        RoleID = item.wcomputername,
                        p1 = item.bUserType,
                        p2 = item.bzhanghao
                    };
                    Templatelist.Add(entity);
                }
            }
        }

        void BindTCUserRoleReport(string gonghao = "")
        {
            List<V_TCReport> tcUserRoleList = new List<V_TCReport>();
            tcUserRoleList = new IAMEntityDAL.V_TCReportDAL().Get_V_TC_Repost_List_as_User(Request.QueryString["templatename"] != null ? TemplateName : gonghao, string.Empty, string.Empty, string.Empty, string.Empty, null, null, string.Empty, string.Empty, string.Empty, string.Empty);
            tcUserRoleList = tcUserRoleList.Where(item => item.urMemo != null).ToList();
            foreach (var item in tcUserRoleList)
            {
                if (item.isdr != 1)
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
                        p1 = item.mUserType,
                        p2 = item.mzhanghao
                    };
                    Templatelist.Add(entity);
                }
            }
        }

        protected void btncreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTemplateName.Text.Trim()))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('模版名称不能为空！');", true);
                return;
            }
            else if (!new RoleTemplateDAL().Exeits(txtTemplateName.Text.Trim()))
            {
                Response.Write("<script>alert('模版名称已存在！');</script>");
                return;
            }

            BindSAPUserRoleReport();
            BindHRUserRoleReport();
            BindHECUserRoleReport();
            BindHECUserGangwei();
            BindADUserRoleReport();
            BindTCUserRoleReport();
            BindADComputer();
            RoleTemplate entity = new RoleTemplate();
            entity.ID = Guid.NewGuid(); entity.TemplateName = txtTemplateName.Text;
            if (new RoleTemplateDAL().CreateRoleTemplate(entity, Templatelist, true) > 0)
            {
                BLL.ActionLog.CreateLog(base.UserInfo.adname, txtTemplateName.Text);
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('添加成功');window.close();", true);
            }
        }
        //TemplateName    EmployeeCode
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string ExtensionName = System.IO.Path.GetExtension(fileupload1.FileName);
                string filename = Guid.NewGuid().ToString() + ExtensionName;
                filename = Server.MapPath("../downloadFile/" + filename);
                string ExtensionTemplate = "xls,xlsx";
                bool istrue = false;
                ExtensionTemplate.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(x =>
                {
                    if ("." + x == ExtensionName)
                        istrue = true;
                });
                if (istrue)
                {
                    fileupload1.SaveAs(filename);
                    System.Data.DataTable dt = OLEDBExcelHelper.OLEDBExcelHelper.ReadExcle(filename);
                    System.Text.StringBuilder stb = new System.Text.StringBuilder();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Templatelist.Clear();
                        string gonghao = dt.Rows[i][0].ToString();
                        string tmplateName = dt.Rows[i][1].ToString();
                        BindSAPUserRoleReport(gonghao);
                        BindHRUserRoleReport(gonghao);
                        BindHECUserRoleReport(gonghao);
                        BindHECUserGangwei(gonghao);
                        BindADUserRoleReport(gonghao);
                        BindTCUserRoleReport(gonghao);
                        BindADComputer(gonghao);
                        RoleTemplate entity = new RoleTemplate();
                        entity.ID = Guid.NewGuid(); entity.TemplateName = tmplateName;
                        try
                        {
                            if (new RoleTemplateDAL().CreateRoleTemplate(entity, Templatelist, true) > 0)
                            {
                                BLL.ActionLog.CreateLog(base.UserInfo.adname, txtTemplateName.Text + "(批量添加类型，所使用工号为:" + gonghao + ")");
                            }
                        }
                        catch
                        {
                            stb.Append(string.Format("工号:{0}在添加失败,原因可能是模板名称已存在请检查\\n",gonghao));
                            continue;
                        }
                    }
                    if (string.IsNullOrEmpty(stb.ToString()))
                        Response.Write("<script>alert('全部添加成功');</script>");
                    else
                        Response.Write("<script>alert('部分添加成功，失败工号如下:\\n"+stb.ToString()+"');</script>");
                   
                }
                else
                {
                    Response.Write("<script>alert('文件不合法\\n请上传合法文件，例如: .xls,.xlsx');</script>");
                }
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('"+ex.Message+"');</script>");
            }

        }





    }
}