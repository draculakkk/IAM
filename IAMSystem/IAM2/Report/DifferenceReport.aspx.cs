using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseDataAccess;

namespace IAM.Report
{
    public partial class DifferenceReport : BasePage
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

            public List<ItemModel> SAPother = new List<ItemModel>();
            public List<ItemModel> HRother = new List<ItemModel>();
            public List<ItemModel> HECother = new List<ItemModel>();
            public List<ItemModel> TCother = new List<ItemModel>();
            public List<ItemModel> ADother = new List<ItemModel>();
            public List<ItemModel> ADComputerOther = new List<ItemModel>();

            public List<ItemModel> SAPsytem = new List<ItemModel>();
            public List<ItemModel> HRsytem = new List<ItemModel>();
            public List<ItemModel> HECsytem = new List<ItemModel>();
            public List<ItemModel> TCsytem = new List<ItemModel>();
            public List<ItemModel> ADsytem = new List<ItemModel>();
            public List<ItemModel> ADComputerSystem = new List<ItemModel>();

            public List<ItemModel> HEC2 = new List<ItemModel>();
            public List<ItemModel> HEC2Other = new List<ItemModel>();
            public List<ItemModel> HEC2System = new List<ItemModel>();

        }

        public PageModel Models = new PageModel();
        IAMEntities db = new IAMEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBox2.Text.Trim()) && !string.IsNullOrEmpty(TextBox3.Text.Trim()))
            {
                if (!base.ReturnUserRole.Admin && !base.ReturnUserRole.Leader)
                {
                    base.NoRole();
                }
                //加载数据
                band();
            }
        }
        private void band()
        {
            if (string.IsNullOrEmpty(TextBox2.Text.Trim()) || string.IsNullOrEmpty(TextBox3.Text.Trim()))
            {
                Models = new PageModel();
                return;
            }
            string leftid = TextBox2.Text.Trim();
            string right = TextBox3.Text.Trim();

            //SAP数据比较
            var sapdata = db.ExecuteStoreQuery<ItemModel>(string.Format(@"SELECT s1.ROLENAME AS 'RoleName', s2.ROLENAME AS 'RoleName2',s1.BAPIBNAME zhanghao,s2.BAPIBNAME zhanghao2 FROM dbo.SAP_User_Role s1
FULL JOIN dbo.SAP_User_Role s2 ON s1.ROLENAME = s2.ROLENAME
WHERE 
s1.BAPIBNAME=(SELECT TOP 1 zhanghao FROM dbo.AccountMaping WHERE [type]='SAP' and gonghao='{0}' and usertype='员工')
AND s2.BAPIBNAME=(SELECT TOP 1 zhanghao FROM dbo.AccountMaping WHERE [type]='SAP' and gonghao='{1}' and usertype='员工')", leftid, right));
            Models.SAP = sapdata.ToList();

            var sapdataother = db.ExecuteStoreQuery<ItemModel>(string.Format(@"SELECT s1.ROLENAME AS 'RoleName', s2.ROLENAME AS 'RoleName2',s1.BAPIBNAME zhanghao,s2.BAPIBNAME zhanghao2 FROM dbo.SAP_User_Role s1
FULL JOIN dbo.SAP_User_Role s2 ON s1.ROLENAME = s2.ROLENAME
WHERE 
s1.BAPIBNAME=(SELECT TOP 1 zhanghao FROM dbo.AccountMaping WHERE [type]='SAP' and gonghao='{0}' and usertype='其他')
AND s2.BAPIBNAME=(SELECT TOP 1 zhanghao FROM dbo.AccountMaping WHERE [type]='SAP' and gonghao='{1}'and usertype='其他') ", leftid, right));
            Models.SAPother = sapdataother.ToList();

            var sapdatasystem = db.ExecuteStoreQuery<ItemModel>(string.Format(@"SELECT s1.ROLENAME AS 'RoleName', s2.ROLENAME AS 'RoleName2',s1.BAPIBNAME zhanghao,s2.BAPIBNAME zhanghao2 FROM dbo.SAP_User_Role s1
FULL JOIN dbo.SAP_User_Role s2 ON s1.ROLENAME = s2.ROLENAME
WHERE 
s1.BAPIBNAME=(SELECT TOP 1 zhanghao FROM dbo.AccountMaping WHERE [type]='SAP' and gonghao='{0}' and usertype='系统')
AND s2.BAPIBNAME=(SELECT TOP 1 zhanghao FROM dbo.AccountMaping WHERE [type]='SAP' and gonghao='{1}' and usertype='系统' )", leftid, right));
            Models.SAPsytem = sapdatasystem.ToList();

            //HR数据比较
            var hrdata = db.ExecuteStoreQuery<ItemModel>(string.Format(@"SELECT *
FROM (
(SELECT r.role_name RoleName1,c.UNTTNAME Company,a.User_code zhanghao  
FROM dbo.HRSm_user a INNER JOIN dbo.HRsm_user_role b ON a.Cuserid=b.Cuserid  INNER JOIN dbo.HRCompany c ON b.Pk_corp=c.Pk_CORP INNER JOIN dbo.AccountMaping m ON a.User_code=m.zhanghao INNER JOIN dbo.HRsm_role r ON b.Pk_role=r.Pk_role
WHERE m.gonghao='{0}' AND type='HR' AND m.UserType='员工'
)s1 
 FULL JOIN
(SELECT r.role_name RoleName2,c.UNTTNAME Company2,a.User_code zhanghao2  
FROM dbo.HRSm_user a INNER JOIN dbo.HRsm_user_role b ON a.Cuserid=b.Cuserid  INNER JOIN dbo.HRCompany c ON b.Pk_corp=c.Pk_CORP INNER JOIN dbo.AccountMaping m ON a.User_code=m.zhanghao INNER JOIN dbo.HRsm_role r ON b.Pk_role=r.Pk_role
WHERE m.gonghao='{1}' AND type='HR' AND m.UserType='员工') s2 ON s1.RoleName1=s2.RoleName2
)", leftid, right));
            Models.HR = hrdata.ToList();

            var hrdataother = db.ExecuteStoreQuery<ItemModel>(string.Format(@"SELECT *
FROM (
(SELECT r.role_name RoleName1,c.UNTTNAME Company,a.User_code zhanghao  
FROM dbo.HRSm_user a INNER JOIN dbo.HRsm_user_role b ON a.Cuserid=b.Cuserid  INNER JOIN dbo.HRCompany c ON b.Pk_corp=c.Pk_CORP INNER JOIN dbo.AccountMaping m ON a.User_code=m.zhanghao INNER JOIN dbo.HRsm_role r ON b.Pk_role=r.Pk_role
WHERE m.gonghao='{0}' AND type='HR' AND m.UserType='其他'
)s1 
 FULL JOIN
(SELECT r.role_name RoleName2,c.UNTTNAME Company2,a.User_code zhanghao2  
FROM dbo.HRSm_user a INNER JOIN dbo.HRsm_user_role b ON a.Cuserid=b.Cuserid  INNER JOIN dbo.HRCompany c ON b.Pk_corp=c.Pk_CORP INNER JOIN dbo.AccountMaping m ON a.User_code=m.zhanghao INNER JOIN dbo.HRsm_role r ON b.Pk_role=r.Pk_role
WHERE m.gonghao='{1}' AND type='HR' AND m.UserType='其他') s2 ON s1.RoleName1=s2.RoleName2
)", leftid, right));
            Models.HRother = hrdataother.ToList();

            var hrdatasytem = db.ExecuteStoreQuery<ItemModel>(string.Format(@"SELECT *
FROM (
(SELECT r.role_name RoleName1,c.UNTTNAME Company,a.User_code zhanghao  
FROM dbo.HRSm_user a INNER JOIN dbo.HRsm_user_role b ON a.Cuserid=b.Cuserid  INNER JOIN dbo.HRCompany c ON b.Pk_corp=c.Pk_CORP INNER JOIN dbo.AccountMaping m ON a.User_code=m.zhanghao INNER JOIN dbo.HRsm_role r ON b.Pk_role=r.Pk_role
WHERE m.gonghao='{0}' AND type='HR' AND m.UserType='系统'
)s1 
 FULL JOIN
(SELECT r.role_name RoleName2,c.UNTTNAME Company2,a.User_code zhanghao2  
FROM dbo.HRSm_user a INNER JOIN dbo.HRsm_user_role b ON a.Cuserid=b.Cuserid  INNER JOIN dbo.HRCompany c ON b.Pk_corp=c.Pk_CORP INNER JOIN dbo.AccountMaping m ON a.User_code=m.zhanghao INNER JOIN dbo.HRsm_role r ON b.Pk_role=r.Pk_role
WHERE m.gonghao='{1}' AND type='HR' AND m.UserType='系统') s2 ON s1.RoleName1=s2.RoleName2
)", leftid, right));
            Models.HRsytem = hrdatasytem.ToList();


            //HEC数据比较
            var hecdata = db.ExecuteStoreQuery<ItemModel>(string.Format(@"
SELECT *
FROM (
(
SELECT r.ROLE_NAME RoleName,c.COMPANY_FULL_NAME Company,a.User_CD zhanghao 
FROM dbo.HEC_User a INNER JOIN  dbo.HEC_User_Info b ON a.User_CD=b.[USER_NAME] INNER JOIN dbo.HEC_Role r ON b.ROLE_CODE=r.ROLE_CODE INNER JOIN dbo.HEC_Company_Info c ON b.COMPANY_CODE=c.COMPANY_CODE INNER JOIN dbo.AccountMaping m ON a.User_CD=m.zhanghao
WHERE m.gonghao='{0}' AND type='HEC' AND m.UserType='员工')s1
FULL JOIN
(SELECT r.ROLE_NAME RoleName2,c.COMPANY_FULL_NAME Company2,a.User_CD zhanghao2 
FROM dbo.HEC_User a INNER JOIN  dbo.HEC_User_Info b ON a.User_CD=b.[USER_NAME] INNER JOIN dbo.HEC_Role r ON b.ROLE_CODE=r.ROLE_CODE INNER JOIN dbo.HEC_Company_Info c ON b.COMPANY_CODE=c.COMPANY_CODE INNER JOIN dbo.AccountMaping m ON a.User_CD=m.zhanghao
WHERE m.gonghao='{1}' AND type='HEC' AND m.UserType='员工')s2 ON s1.RoleName=s2.RoleName2 AND s1.Company=s2.Company2
)", leftid, right));
            Models.HEC = hecdata.ToList();

            var hecdataother = db.ExecuteStoreQuery<ItemModel>(string.Format(@"
SELECT *
FROM (
(
SELECT r.ROLE_NAME RoleName,c.COMPANY_FULL_NAME Company,a.User_CD zhanghao 
FROM dbo.HEC_User a INNER JOIN  dbo.HEC_User_Info b ON a.User_CD=b.[USER_NAME] INNER JOIN dbo.HEC_Role r ON b.ROLE_CODE=r.ROLE_CODE INNER JOIN dbo.HEC_Company_Info c ON b.COMPANY_CODE=c.COMPANY_CODE INNER JOIN dbo.AccountMaping m ON a.User_CD=m.zhanghao
WHERE m.gonghao='{0}' AND type='HEC' AND m.UserType='其他')s1
FULL JOIN
(SELECT r.ROLE_NAME RoleName2,c.COMPANY_FULL_NAME Company2,a.User_CD zhanghao2 
FROM dbo.HEC_User a INNER JOIN  dbo.HEC_User_Info b ON a.User_CD=b.[USER_NAME] INNER JOIN dbo.HEC_Role r ON b.ROLE_CODE=r.ROLE_CODE INNER JOIN dbo.HEC_Company_Info c ON b.COMPANY_CODE=c.COMPANY_CODE INNER JOIN dbo.AccountMaping m ON a.User_CD=m.zhanghao
WHERE m.gonghao='{1}' AND type='HEC' AND m.UserType='其他')s2 ON s1.RoleName=s2.RoleName2 AND s1.Company=s2.Company2
)", leftid, right));
            Models.HECother = hecdataother.ToList();

            var hecdatasytem = db.ExecuteStoreQuery<ItemModel>(string.Format(@"SELECT *
FROM (
(
SELECT r.ROLE_NAME RoleName,c.COMPANY_FULL_NAME Company,a.User_CD zhanghao 
FROM dbo.HEC_User a INNER JOIN  dbo.HEC_User_Info b ON a.User_CD=b.[USER_NAME] INNER JOIN dbo.HEC_Role r ON b.ROLE_CODE=r.ROLE_CODE INNER JOIN dbo.HEC_Company_Info c ON b.COMPANY_CODE=c.COMPANY_CODE INNER JOIN dbo.AccountMaping m ON a.User_CD=m.zhanghao
WHERE m.gonghao='{0}' AND type='HEC' AND m.UserType='系统')s1
FULL JOIN
(SELECT r.ROLE_NAME RoleName2,c.COMPANY_FULL_NAME Company2,a.User_CD zhanghao2 
FROM dbo.HEC_User a INNER JOIN  dbo.HEC_User_Info b ON a.User_CD=b.[USER_NAME] INNER JOIN dbo.HEC_Role r ON b.ROLE_CODE=r.ROLE_CODE INNER JOIN dbo.HEC_Company_Info c ON b.COMPANY_CODE=c.COMPANY_CODE INNER JOIN dbo.AccountMaping m ON a.User_CD=m.zhanghao
WHERE m.gonghao='{1}' AND type='HEC' AND m.UserType='系统')s2 ON s1.RoleName=s2.RoleName2 AND s1.Company=s2.Company2
)", leftid, right));
            Models.HECsytem = hecdatasytem.ToList();

            var sqlHECGangwei = @"SELECT *
FROM (

SELECT a.User_CD zhanghao,b.COMPANY_CODE+'^'+ b.COMPANY_NAME Company,(b.UNIT_CODE+'^'+b.UNIT_NAME+'^'+b.POSITION_CODE+'^'+b.POSITION_NAME) AS RoleName 
FROM dbo.HEC_User a INNER JOIN dbo.HEC_User_Gangwei b ON a.User_CD=b.EMPLOYEE_CODE INNER JOIN dbo.AccountMaping m ON a.User_CD=m.zhanghao AND m.type='HEC'
WHERE m.gonghao=@gonghao AND m.UserType='{0}'
) s1 FULL JOIN 
(
SELECT a.User_CD zhanghao2,b.COMPANY_CODE+'^'+ b.COMPANY_NAME Company2,(b.UNIT_CODE+'^'+b.UNIT_NAME+'^'+b.POSITION_CODE+'^'+b.POSITION_NAME) AS RoleName2 
FROM dbo.HEC_User a INNER JOIN dbo.HEC_User_Gangwei b ON a.User_CD=b.EMPLOYEE_CODE INNER JOIN dbo.AccountMaping m ON a.User_CD=m.zhanghao AND m.type='HEC'
WHERE m.gonghao=@gonghao2 AND m.UserType='{1}') s2 ON s1.Company = s2.Company2 AND s1.RoleName = s2.RoleName2";




            //AD数据比较
            var adcdata = db.ExecuteStoreQuery<ItemModel>(string.Format(@"SELECT *
FROM (
(
SELECT b.GroupName RoleName,a.Accountname zhanghao 
FROM dbo.AD_UserInfo a INNER JOIN  dbo.AD_UserWorkGroup b ON a.Accountname=b.Uid INNER JOIN dbo.AccountMaping m ON a.Accountname=m.zhanghao
WHERE m.gonghao='{0}' AND m.type='AD' AND m.UserType='员工'
) s1
FULL JOIN 


(SELECT b.GroupName RoleName2,a.Accountname zhanghao2 
FROM dbo.AD_UserInfo a INNER JOIN  dbo.AD_UserWorkGroup b ON a.Accountname=b.Uid INNER JOIN dbo.AccountMaping m ON a.Accountname=m.zhanghao
WHERE m.gonghao='{1}' AND m.type='AD' AND m.UserType='员工'
) s2 ON s1.RoleName=s2.RoleName2
)
ORDER BY s1.RoleName", leftid, right));
            Models.AD = adcdata.ToList();

            //AD数据比较
            var adcdataother = db.ExecuteStoreQuery<ItemModel>(string.Format(@"SELECT *
FROM (
(
SELECT b.GroupName RoleName,a.Accountname zhanghao 
FROM dbo.AD_UserInfo a INNER JOIN  dbo.AD_UserWorkGroup b ON a.Accountname=b.Uid INNER JOIN dbo.AccountMaping m ON a.Accountname=m.zhanghao
WHERE m.gonghao='{0}' AND m.type='AD' AND m.UserType='其他'
) s1
FULL JOIN 


(SELECT b.GroupName RoleName2,a.Accountname zhanghao2 
FROM dbo.AD_UserInfo a INNER JOIN  dbo.AD_UserWorkGroup b ON a.Accountname=b.Uid INNER JOIN dbo.AccountMaping m ON a.Accountname=m.zhanghao
WHERE m.gonghao='{1}' AND m.type='AD' AND m.UserType='其他'
) s2 ON s1.RoleName=s2.RoleName2
)
ORDER BY s1.RoleName", leftid, right));
            Models.ADother = adcdataother.ToList();

            //AD数据比较
            var adcdatasystem = db.ExecuteStoreQuery<ItemModel>(string.Format(@"SELECT *
FROM (
(
SELECT b.GroupName RoleName,a.Accountname zhanghao 
FROM dbo.AD_UserInfo a INNER JOIN  dbo.AD_UserWorkGroup b ON a.Accountname=b.Uid INNER JOIN dbo.AccountMaping m ON a.Accountname=m.zhanghao
WHERE m.gonghao='{0}' AND m.type='AD' AND m.UserType='系统'
) s1
FULL JOIN 


(SELECT b.GroupName RoleName2,a.Accountname zhanghao2 
FROM dbo.AD_UserInfo a INNER JOIN  dbo.AD_UserWorkGroup b ON a.Accountname=b.Uid INNER JOIN dbo.AccountMaping m ON a.Accountname=m.zhanghao
WHERE m.gonghao='{1}' AND m.type='AD' AND m.UserType='系统'
) s2 ON s1.RoleName=s2.RoleName2
)
ORDER BY s1.RoleName", leftid, right));
            Models.ADsytem = adcdatasystem.ToList();

            string sqladc = @"SELECT * FROM(
(SELECT a.NAME zhanhgao,b.WorkGroup RoleName 
FROM dbo.AD_Computer a INNER JOIN dbo.AD_Computer_WorkGroups b ON a.NAME=b.ComputerName INNER JOIN dbo.AccountMaping m ON a.NAME=m.zhanghao
WHERE m.UserType='{0}' AND m.gonghao=@gonghao AND m.type='ADComputer') s1 
FULL JOIN 

(SELECT a.NAME zhanhgao2,b.WorkGroup RoleName2 
FROM dbo.AD_Computer a INNER JOIN dbo.AD_Computer_WorkGroups b ON a.NAME=b.ComputerName INNER JOIN dbo.AccountMaping m ON a.NAME=m.zhanghao
WHERE m.UserType='{1}' AND m.gonghao=@gonghao2 AND m.type='ADComputer'
)s2
ON s1.RoleName=s2.RoleName2
)
";

            var adcomputer = db.ExecuteStoreQuery<ItemModel>(string.Format(sqladc,"员工","员工"),new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@gonghao",leftid),
            new System.Data.SqlClient.SqlParameter("@gonghao2",right)
            });
            Models.ADComputer = adcomputer.ToList();

            var adcomputerother = db.ExecuteStoreQuery<ItemModel>(string.Format(sqladc,"其他","其他"),new System.Data.SqlClient.SqlParameter[]{
             new System.Data.SqlClient.SqlParameter("@gonghao",leftid),
            new System.Data.SqlClient.SqlParameter("@gonghao2",right)
            });
            Models.ADComputerOther = adcomputerother.ToList();

            var adcomputersystem = db.ExecuteStoreQuery<ItemModel>(string.Format(sqladc,"系统","系统"),new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@gonghao",leftid),
            new System.Data.SqlClient.SqlParameter("@gonghao2",right)
            });
            Models.ADComputerSystem = adcomputersystem.ToList();

            var hecgangwei = db.ExecuteStoreQuery<ItemModel>(string.Format(sqlHECGangwei,"员工","员工"),new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@gonghao",leftid),
            new System.Data.SqlClient.SqlParameter("@gonghao2",right)
            });
            Models.HEC2 = hecgangwei.ToList();

            var hecgangwei2 = db.ExecuteStoreQuery<ItemModel>(string.Format(sqlHECGangwei, "其他", "其他"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@gonghao",leftid),
            new System.Data.SqlClient.SqlParameter("@gonghao2",right)
            });
            Models.HEC2Other = hecgangwei2.ToList();

            var hecgangwei3 = db.ExecuteStoreQuery<ItemModel>(string.Format(sqlHECGangwei, "系统", "系统"), new System.Data.SqlClient.SqlParameter[]{
            new System.Data.SqlClient.SqlParameter("@gonghao",leftid),
            new System.Data.SqlClient.SqlParameter("@gonghao2",right)
            });
            Models.HEC2Other = hecgangwei3.ToList();

            



            //TC数据比较
            var TCdata = db.ExecuteStoreQuery<ItemModel>(string.Format(@"SELECT * 
FROM (

(SELECT a.UserID zhanghao,b.Memo RoleName FROM dbo.TC_UserInfo a INNER JOIN dbo.TC_UserGroupSetting b ON a.UserID=b.UserID JOIN dbo.AccountMaping m ON m.zhanghao=a.UserID 
WHERE m.gonghao='{0}' AND m.type='TC' AND m.UserType='员工') s1 FULL JOIN 


(SELECT a.UserID zhanghao2,b.Memo RoleName2 FROM dbo.TC_UserInfo a INNER JOIN dbo.TC_UserGroupSetting b ON a.UserID=b.UserID JOIN dbo.AccountMaping m ON m.zhanghao=a.UserID 
WHERE m.gonghao='{1}' AND m.type='TC' AND m.UserType='员工') s2 ON s1.RoleName=s2.RoleName2
)
ORDER BY s1.zhanghao",leftid,right) );
            Models.TC = TCdata.ToList();

            var TCdataother = db.ExecuteStoreQuery<ItemModel>(string.Format(@"SELECT * 
FROM (

(SELECT a.UserID zhanghao,b.Memo RoleName FROM dbo.TC_UserInfo a INNER JOIN dbo.TC_UserGroupSetting b ON a.UserID=b.UserID JOIN dbo.AccountMaping m ON m.zhanghao=a.UserID 
WHERE m.gonghao='{0}' AND m.type='TC' AND m.UserType='其他') s1 FULL JOIN 


(SELECT a.UserID zhanghao2,b.Memo RoleName2 FROM dbo.TC_UserInfo a INNER JOIN dbo.TC_UserGroupSetting b ON a.UserID=b.UserID JOIN dbo.AccountMaping m ON m.zhanghao=a.UserID 
WHERE m.gonghao='{1}' AND m.type='TC' AND m.UserType='其他') s2 ON s1.RoleName=s2.RoleName2
)
ORDER BY s1.zhanghao",leftid,right));
            Models.TCother = TCdataother.ToList();

            var TCdatasystem = db.ExecuteStoreQuery<ItemModel>(string.Format(@"SELECT * 
FROM (

(SELECT a.UserID zhanghao,b.Memo RoleName FROM dbo.TC_UserInfo a INNER JOIN dbo.TC_UserGroupSetting b ON a.UserID=b.UserID JOIN dbo.AccountMaping m ON m.zhanghao=a.UserID 
WHERE m.gonghao='{0}' AND m.type='TC' AND m.UserType='系统') s1 FULL JOIN 


(SELECT a.UserID zhanghao2,b.Memo RoleName2 FROM dbo.TC_UserInfo a INNER JOIN dbo.TC_UserGroupSetting b ON a.UserID=b.UserID JOIN dbo.AccountMaping m ON m.zhanghao=a.UserID 
WHERE m.gonghao='{1}' AND m.type='TC' AND m.UserType='系统') s2 ON s1.RoleName=s2.RoleName2
)
ORDER BY s1.zhanghao",leftid,right));
            Models.TCsytem = TCdatasystem.ToList();

        }


        protected void btnQuery_Click(object sender, EventArgs e)
        {
            band();
        }


        protected void btnOutput_Click(object sender, EventArgs e)
        {
            BLL.Untityone.outexcel(this, hidden1.Value);
        }
    }
}