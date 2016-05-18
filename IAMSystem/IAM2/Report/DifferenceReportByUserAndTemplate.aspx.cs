using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseDataAccess;

namespace IAM.Report
{
    public partial class DifferenceReportByUserAndTemplate : BasePage
    {
        public class ItemModel
        {
            public string Group { get; set; }
            public string RoleName { get; set; }
            public string Company { get; set; }
            public string RoleName2 { get; set; }
            public string Company2 { get; set; }
            public string zhanghao { get; set; }
            public string zhanghao2 { get; set; }

        }

        public class PageModel
        {
            public List<ItemModel> SAP = new List<ItemModel>();
            public List<ItemModel> HR = new List<ItemModel>();
            public List<ItemModel> HEC = new List<ItemModel>();
            public List<ItemModel> TC = new List<ItemModel>();
            public List<ItemModel> AD = new List<ItemModel>();
            public List<ItemModel> ADComputer = new List<ItemModel>();
            public List<ItemModel> HEC2 = new List<ItemModel>();

            public List<ItemModel> SAPother = new List<ItemModel>();
            public List<ItemModel> HRother = new List<ItemModel>();
            public List<ItemModel> HECother = new List<ItemModel>();
            public List<ItemModel> TCother = new List<ItemModel>();
            public List<ItemModel> ADother = new List<ItemModel>();
            public List<ItemModel> ADComputerother = new List<ItemModel>();
            public List<ItemModel> HEC2other = new List<ItemModel>();

            public List<ItemModel> SAPsytem = new List<ItemModel>();
            public List<ItemModel> HRsytem = new List<ItemModel>();
            public List<ItemModel> HECsytem = new List<ItemModel>();
            public List<ItemModel> TCsytem = new List<ItemModel>();
            public List<ItemModel> ADsytem = new List<ItemModel>();
            public List<ItemModel> ADComputersystem = new List<ItemModel>();
            public List<ItemModel> HEC2System = new List<ItemModel>();

        }

        public PageModel Models = new PageModel();
        IAMEntities db = new IAMEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!base.ReturnUserRole.Admin && !base.ReturnUserRole.Leader)
                {
                    base.NoRole();
                }
            }
        }

        void bind()
        {
            if (string.IsNullOrEmpty(TextBox2.Text.Trim()) || string.IsNullOrEmpty(TextBox3.Text.Trim()))
            {
                Models = new PageModel();
                return;
            }
            string leftid = TextBox2.Text.Trim();
            string right = TextBox3.Text.Trim();

            //HEC模版和用户角色对比
            string hecsql = @"SELECT * 
FROM( 
(SELECT a.USER_NAME zhanghao2,b.ROLE_NAME RoleName2,c.COMPANY_FULL_NAME Company2 
FROM dbo.HEC_User_Info a INNER JOIN dbo.HEC_Role b ON a.ROLE_CODE=b.ROLE_CODE
     INNER JOIN dbo.HEC_Company_Info c ON a.COMPANY_CODE=c.COMPANY_CODE
     INNER JOIN dbo.AccountMaping m ON a.USER_NAME=m.zhanghao
WHERE m.gonghao=@gonghao AND type='HEC' AND UserType='{0}')s1 
FULL JOIN 

(SELECT RoleName,CompanyName Company,b.p2 zhanghao
FROM dbo.RoleTemplate a INNER JOIN dbo.RoleTemplateInfo b ON a.ID=b.TemplateID
WHERE a.TemplateName=@tempname AND b.p1='{1}' AND SystemName='HEC')s2 ON s1.RoleName2=s2.RoleName  AND s1.Company2=s2.Company
)";
            var heclist = db.ExecuteStoreQuery<ItemModel>(string.Format(hecsql, "员工", "员工"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@gonghao",right),
            new System.Data.SqlClient.SqlParameter("@tempname",leftid)
            });
            Models.HEC = heclist.ToList();


            var heclistother = db.ExecuteStoreQuery<ItemModel>(string.Format(hecsql, "其他", "其他"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@gonghao",right),
            new System.Data.SqlClient.SqlParameter("@tempname",leftid)
            });
            Models.HECother = heclistother.ToList();

            var heclistsytem = db.ExecuteStoreQuery<ItemModel>(string.Format(hecsql, "系统", "系统"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@gonghao",right),
            new System.Data.SqlClient.SqlParameter("@tempname",leftid)
            });
            Models.HECsytem = heclistsytem.ToList();

            var sqlhec2 = @"SELECT *
FROM (

SELECT a.User_CD zhanghao,b.COMPANY_CODE+'^'+ b.COMPANY_NAME Company,(b.UNIT_CODE+'^'+b.UNIT_NAME+'^'+b.POSITION_CODE+'^'+b.POSITION_NAME) AS RoleName 
FROM dbo.HEC_User a INNER JOIN dbo.HEC_User_Gangwei b ON a.User_CD=b.EMPLOYEE_CODE INNER JOIN dbo.AccountMaping m ON a.User_CD=m.zhanghao AND m.type='HEC'
WHERE m.gonghao=@gonghao AND m.UserType='{0}'
) s1 FULL JOIN 
(
SELECT a.ID,  a.RoleName RoleName2,a.SystemName,a.TemplateID,a.CompanyName Company2,a.p2 zhanghao2 
FROM dbo.RoleTemplateInfo a INNER JOIN dbo.RoleTemplate b ON a.TemplateID=b.ID 
	  WHERE b.TemplateName=@tempname AND SystemName='HEC2' and a.p1='{1}') s2
	  ON s1.Company = s2.Company2 AND s1.RoleName = s2.RoleName2";

            var hec2 = db.ExecuteStoreQuery<ItemModel>(string.Format(sqlhec2, "员工", "员工"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@gonghao",right),
            new System.Data.SqlClient.SqlParameter("@tempname",leftid)
            });
            Models.HEC2 = hec2.ToList();

            var hec2other = db.ExecuteStoreQuery<ItemModel>(string.Format(sqlhec2, "其他", "其他"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@gonghao",right),
            new System.Data.SqlClient.SqlParameter("@tempname",leftid)
            });
            Models.HEC2 = hec2other.ToList();

            var hec2system = db.ExecuteStoreQuery<ItemModel>(string.Format(sqlhec2, "系统", "系统"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@gonghao",right),
            new System.Data.SqlClient.SqlParameter("@tempname",leftid)
            });
            Models.HEC2 = hec2system.ToList();


            //SAP模版和用户角色对比
            string sapsql = @"SELECT DISTINCT s1.RoleName AS RoleName ,s2.ROLENAME2,s2.zhanghao2,s1.zhanghao
	  FROM (
	  (SELECT a.ID,  a.RoleName, a.SystemName,a.TemplateID ,a.CompanyName,a.p2 zhanghao FROM dbo.RoleTemplateInfo a INNER JOIN dbo.RoleTemplate b ON a.TemplateID=b.ID 
	  WHERE b.TemplateName=@tempname AND SystemName='SAP' AND a.p1='{1}') s1
	  FULL JOIN 

	  (
	  SELECT r.ROLENAME ROLENAME2,u.BAPIBNAME zhanghao2 FROM dbo.SAP_UserInfo u INNER JOIN dbo.SAP_User_Role r ON u.BAPIBNAME =r.BAPIBNAME
									   INNER JOIN dbo.AccountMaping m ON u.BAPIBNAME=m.zhanghao
									   WHERE m.gonghao=@gonghao AND m.type='SAP' AND m.UserType='{0}'
	  
	  ) s2

	  ON s1.RoleName=s2.ROLENAME2
	  )";
            var saplist = db.ExecuteStoreQuery<ItemModel>(string.Format(sapsql, "员工", "员工"),
                new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@tempname",leftid),
            new System.Data.SqlClient.SqlParameter("@gonghao",right)
            });

            Models.SAP = saplist.ToList();

            var saplistother = db.ExecuteStoreQuery<ItemModel>(string.Format(sapsql, "其他", "其他"),
             new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@tempname",leftid),
            new System.Data.SqlClient.SqlParameter("@gonghao",right)
            });

            Models.SAPother = saplistother.ToList();

            var saplistsytem = db.ExecuteStoreQuery<ItemModel>(string.Format(sapsql, "系统", "系统"),
                new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@tempname",leftid),
            new System.Data.SqlClient.SqlParameter("@gonghao",right)
            });

            Models.SAPsytem = saplistsytem.ToList();

            //HR模版和用户角色对比
            string hrsql = @"SELECT DISTINCT
        s1.RoleName AS RoleName ,
        s1.CompanyName AS Company ,
        s2. RoleName2 ,
        s2.Company2,
		s1.zhanghao,s2.zhanghao2
FROM    ( ( SELECT  a.ID ,
                    a.RoleName ,
                    a.SystemName ,
                    a.TemplateID ,
                    a.CompanyName,a.p2 zhanghao
            FROM    dbo.RoleTemplateInfo a
                    INNER JOIN dbo.RoleTemplate b ON a.TemplateID = b.ID
            WHERE   b.TemplateName = @tempid
                    AND SystemName = 'HR' AND a.p1='{0}'
          ) s1
          FULL JOIN ( SELECT    u.User_code zhanghao2 ,
                                r.role_name RoleName2 ,
                                c.UNTTNAME Company2
                      FROM      dbo.HRsm_user_role h
                                INNER JOIN dbo.HRSm_user u ON h.Cuserid = u.Cuserid
                                INNER JOIN dbo.AccountMaping m ON m.zhanghao = u.User_code
                                INNER JOIN dbo.HRCompany c ON h.Pk_corp = c.Pk_CORP
                                INNER JOIN dbo.HRsm_role r ON h.Pk_role = r.Pk_role
                      WHERE     m.type = 'HR'
                                AND UserType = '{1}'
                                AND m.gonghao = @gonghao
                    ) s2 ON s1.RoleName = s2.RoleName2
        )";
            var hrlist = db.ExecuteStoreQuery<ItemModel>(string.Format(hrsql, "员工", "员工"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@tempid",leftid),
            new System.Data.SqlClient.SqlParameter("@gonghao",right)
            });
            Models.HR = hrlist.ToList();

            var hrlistother = db.ExecuteStoreQuery<ItemModel>(string.Format(hrsql, "其他", "其他"),
                new System.Data.SqlClient.SqlParameter[]{
                new System.Data.SqlClient.SqlParameter("@tempid",leftid),
                new System.Data.SqlClient.SqlParameter("@gonghao",right)
                }
                );
            Models.HRother = hrlistother.ToList();

            var hrlistsystem = db.ExecuteStoreQuery<ItemModel>(string.Format(hrsql, "系统", "系统"),
                new System.Data.SqlClient.SqlParameter[]{
                new System.Data.SqlClient.SqlParameter("@tempid",leftid),
                new System.Data.SqlClient.SqlParameter("@gonghao",right)
                }
                );
            Models.HRsytem = hrlistsystem.ToList();

            //TC模版和用户角色对比
            string tcsql = @"SELECT DISTINCT
        a.RoleName,b.RoleName2,a.RoleID Company,b.[Group] Company2,b.zhanhgao2,a.zhanghao
FROM    ( ( SELECT  a.ID ,
                    a.RoleName ,
                    a.SystemName ,
                    a.RoleID ,
                    a.CompanyName,
					a.RoleName+a.RoleID AS memo,a.p2 zhanghao
            FROM    dbo.RoleTemplateInfo a
                    INNER JOIN dbo.RoleTemplate b ON a.TemplateID = b.ID
            WHERE   b.TemplateName = @tempid
                    AND SystemName = 'TC'
                    and a.p1='{0}'
          ) a
          FULL JOIN ( SELECT    u.UserID zhanhgao2,c.p1 [Group],c.p2 RoleName2, c.p2+c.p1 AS memo
                      FROM      dbo.TC_UserInfo u
                                INNER JOIN dbo.TC_UserGroupSetting c ON u.UserID=c.UserID
                                INNER JOIN dbo.AccountMaping m ON u.UserID = m.zhanghao
                                                              AND m.type = 'TC'
                      WHERE     m.gonghao = @gonghao AND m.UserType='{1}' AND type='TC'
                    ) b ON a.memo=b.memo
        )";
            var tclist = db.ExecuteStoreQuery<ItemModel>(string.Format(tcsql, "员工", "员工"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@tempid",leftid),
            new System.Data.SqlClient.SqlParameter("@gonghao",right)
            });
            Models.TC = tclist.ToList();

            var tclistother = db.ExecuteStoreQuery<ItemModel>(string.Format(tcsql, "其他", "其他"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@tempid",leftid),
            new System.Data.SqlClient.SqlParameter("@gonghao",right)
            });
            Models.TCother = tclistother.ToList();

            var tclistsystem = db.ExecuteStoreQuery<ItemModel>(string.Format(tcsql, "系统", "系统"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@tempid",leftid),
            new System.Data.SqlClient.SqlParameter("@gonghao",right)
            });
            Models.TCsytem = tclistsystem.ToList();



            //AD模版和用户角色对比
            string adsql = @"SELECT DISTINCT
        a.RoleName AS RoleName ,a.zhanghao,
        b.zhanghao2,
        b.GroupName AS RoleName2
FROM    ( ( SELECT  a.ID ,
                    a.RoleName ,
                    a.SystemName ,
                    a.TemplateID ,
                    a.CompanyName,a.p2 zhanghao
            FROM    dbo.RoleTemplateInfo a
                    INNER JOIN dbo.RoleTemplate b ON a.TemplateID = b.ID
            WHERE   b.TemplateName = @tempid
                    AND SystemName = 'AD'
					AND a.p1='{0}'
          ) a
          FULL JOIN ( SELECT    w.GroupName,u.Accountname zhanghao2
                      FROM      dbo.AD_UserInfo u
                                INNER JOIN dbo.AD_UserWorkGroup w ON u.Accountname = w.Uid
                                INNER JOIN dbo.AccountMaping m ON u.Accountname = m.zhanghao
                                                              AND m.type = 'AD'
                      WHERE     m.gonghao =@gonghao AND m.UserType='{1}' AND m.type='AD'
                    ) b ON a.RoleName = b.GroupName
        )";
            var adlist = db.ExecuteStoreQuery<ItemModel>(string.Format(adsql, "员工", "员工"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@tempid",leftid),
            new System.Data.SqlClient.SqlParameter("@gonghao",right)
            });
            Models.AD = adlist.ToList();

            var adlistother = db.ExecuteStoreQuery<ItemModel>(string.Format(adsql, "其他", "其他"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@tempid",leftid),
            new System.Data.SqlClient.SqlParameter("@gonghao",right)
            });
            Models.ADother = adlistother.ToList();

            var adlisthsystem = db.ExecuteStoreQuery<ItemModel>(string.Format(adsql, "其他", "其他"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@tempid",leftid),
            new System.Data.SqlClient.SqlParameter("@gonghao",right)
            });
            Models.ADsytem = adlisthsystem.ToList();


        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            bind();
        }

        protected void btnOutput_Click(object sender, EventArgs e)
        {
            BLL.Untityone.outexcel(this, hidden1.Value);
        }
    }
}