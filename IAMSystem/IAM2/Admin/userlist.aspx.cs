using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAMEntityDAL;
using BaseDataAccess;

namespace IAM.Admin
{
    public partial class userlist : System.Web.UI.Page
    {
        public class userinfo
        {
            public string Uname { get; set; }

            public string Pname { get; set; }
        }

        public class UserInfoBySystem
        {
            public List<userinfo> AD = new List<userinfo>();
            public List<userinfo> ADcomputer = new List<userinfo>();
            public List<userinfo> TC = new List<userinfo>();
            public List<userinfo> SAP = new List<userinfo>();
            public List<userinfo> HR = new List<userinfo>();
            public List<userinfo> HEC = new List<userinfo>();

        }

        public UserInfoBySystem module = new UserInfoBySystem();
        IAMEntities db = new IAMEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string systemtype = Request.QueryString["systype"];
                switch ((Unitity.SystemType)Enum.Parse(typeof(Unitity.SystemType), systemtype, true))
                {
                    case Unitity.SystemType.AD: BindAD(); break;
                    case Unitity.SystemType.ADComputer: BindADComputer(); break;
                    case Unitity.SystemType.HEC: BindHEC(); break;
                    case Unitity.SystemType.TC: BindTC(); break;
                    case Unitity.SystemType.HR: BindHR(); break;
                    case Unitity.SystemType.SAP: break;
                }
            }
        }

        void BindAD()
        {
            string sql = @"SELECT Accountname Uname,CnName Pname FROM dbo.AD_UserInfo WHERE 1=1";
            string name = txtusername.Text.Trim();
            string pname = txtname.Text.Trim();
            List<System.Data.SqlClient.SqlParameter> liparms = new List<System.Data.SqlClient.SqlParameter>();
            if (!string.IsNullOrEmpty(name))
            {
                sql += " and Accountname like @name";
                liparms.Add(new System.Data.SqlClient.SqlParameter("@name", "%" + name + "%"));
            }
            if (!string.IsNullOrEmpty(pname))
            {
                sql += " and CnName like @pname";
                liparms.Add(new System.Data.SqlClient.SqlParameter("@pname", "%" + pname + "%"));
            }
            var aduser = db.ExecuteStoreQuery<userinfo>(sql, liparms.ToArray());
            var pagelis = aduser.ToList();
            AspNetPager1.RecordCount = pagelis.Count;
            AspNetPager1.PageSize = 15;
            var pagelist = pagelis.OrderBy(item => item.Uname).Skip((AspNetPager1.CurrentPageIndex - 1) * 15).Take(15).ToList();
            repeateruser.DataSource = pagelist;
            repeateruser.DataBind();
            updatepagerhtml();
        }

        void BindADComputer()
        {
            string sql = @"SELECT NAME Uname,DESCRIPTION Pname FROM dbo.AD_Computer WHERE 1=1 AND ENABLE=1";
            string name = txtusername.Text.Trim();
            string pname = txtname.Text.Trim();
            List<System.Data.SqlClient.SqlParameter> liparms = new List<System.Data.SqlClient.SqlParameter>();
            if (!string.IsNullOrEmpty(name))
            {
                sql += " and NAME like @name";
                liparms.Add(new System.Data.SqlClient.SqlParameter("@name", "%" + name + "%"));
            }
            if (!string.IsNullOrEmpty(pname))
            {
                sql += " and DESCRIPTION like @pname";
                liparms.Add(new System.Data.SqlClient.SqlParameter("@pname", "%" + pname + "%"));
            }
            var aduser = db.ExecuteStoreQuery<userinfo>(sql, liparms.ToArray());
            var pagelis = aduser.ToList();
            AspNetPager1.RecordCount = pagelis.Count;
            AspNetPager1.PageSize = 15;
            var pagelist = pagelis.OrderBy(item => item.Uname).Skip((AspNetPager1.CurrentPageIndex - 1) * 15).Take(15).ToList();
            repeateruser.DataSource = pagelist;
            repeateruser.DataBind();
            updatepagerhtml();
        }

        void BindHEC()
        {
            string sql = @"SELECT User_CD Uname,USER_NAME Pname FROM dbo.HEC_User WHERE 1=1 AND ISDISABLED=0";
            string name = txtusername.Text.Trim();
            string pname = txtname.Text.Trim();
            List<System.Data.SqlClient.SqlParameter> liparms = new List<System.Data.SqlClient.SqlParameter>();
            if (!string.IsNullOrEmpty(name))
            {
                sql += " and User_CD like @name";
                liparms.Add(new System.Data.SqlClient.SqlParameter("@name", "%" + name + "%"));
            }
            if (!string.IsNullOrEmpty(pname))
            {
                sql += " and USER_NAME like @pname";
                liparms.Add(new System.Data.SqlClient.SqlParameter("@pname", "%" + pname + "%"));
            }
            var aduser = db.ExecuteStoreQuery<userinfo>(sql, liparms.ToArray());
            var pagelis = aduser.ToList();
            AspNetPager1.RecordCount = pagelis.Count;
            AspNetPager1.PageSize = 15;
            var pagelist = pagelis.OrderBy(item => item.Uname).Skip((AspNetPager1.CurrentPageIndex - 1) * 15).Take(15).ToList();
            repeateruser.DataSource = pagelist;
            repeateruser.DataBind();
            updatepagerhtml();
        }

        void BindTC()
        {
            string sql = @"SELECT UserID Uname,UserName Pname FROM dbo.TC_UserInfo WHERE 1=1 AND UserStatus=1";
            string name = txtusername.Text.Trim();
            string pname = txtname.Text.Trim();
            List<System.Data.SqlClient.SqlParameter> liparms = new List<System.Data.SqlClient.SqlParameter>();
            if (!string.IsNullOrEmpty(name))
            {
                sql += " and UserID like @name";
                liparms.Add(new System.Data.SqlClient.SqlParameter("@name", "%" + name + "%"));
            }
            if (!string.IsNullOrEmpty(pname))
            {
                sql += " and UserName like @pname";
                liparms.Add(new System.Data.SqlClient.SqlParameter("@pname", "%" + pname + "%"));
            }
            var aduser = db.ExecuteStoreQuery<userinfo>(sql, liparms.ToArray());
            var pagelis = aduser.ToList();
            AspNetPager1.RecordCount = pagelis.Count;
            AspNetPager1.PageSize = 15;
            var pagelist = pagelis.OrderBy(item => item.Uname).Skip((AspNetPager1.CurrentPageIndex - 1) * 15).Take(15).ToList();
            repeateruser.DataSource = pagelist;
            repeateruser.DataBind();
            updatepagerhtml();
        }

        void BindHR()
        {
            string sql = @"
SELECT User_code Uname,USER_name Pname,* FROM dbo.HRSm_user WHERE 1=1 AND Locked_tag='N'";
            string name = txtusername.Text.Trim();
            string pname = txtname.Text.Trim();
            List<System.Data.SqlClient.SqlParameter> liparms = new List<System.Data.SqlClient.SqlParameter>();
            if (!string.IsNullOrEmpty(name))
            {
                sql += " and User_code like @name";
                liparms.Add(new System.Data.SqlClient.SqlParameter("@name", "%" + name + "%"));
            }
            if (!string.IsNullOrEmpty(pname))
            {
                sql += " and USER_name like @pname";
                liparms.Add(new System.Data.SqlClient.SqlParameter("@pname", "%" + pname + "%"));
            }
            var aduser = db.ExecuteStoreQuery<userinfo>(sql, liparms.ToArray());
            var pagelis = aduser.ToList();
            AspNetPager1.RecordCount = pagelis.Count;
            AspNetPager1.PageSize = 15;
            var pagelist = pagelis.OrderBy(item => item.Uname).Skip((AspNetPager1.CurrentPageIndex - 1) * 15).Take(15).ToList();
            repeateruser.DataSource = pagelist; 
            repeateruser.DataBind();
            updatepagerhtml();
        }

        private void updatepagerhtml()
        {
            AspNetPager1.CustomInfoHTML = string.Format("共{0}页,当前第{1}页,共{2}", AspNetPager1.PageCount, AspNetPager1.CurrentPageIndex,AspNetPager1.RecordCount);
        }

        protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            string systemtype = Request.QueryString["systype"];
            switch ((Unitity.SystemType)Enum.Parse(typeof(Unitity.SystemType), systemtype, true))
            {
                case Unitity.SystemType.AD: BindAD(); break;
                case Unitity.SystemType.ADComputer: BindADComputer(); break;
                case Unitity.SystemType.HEC: BindHEC(); break;
                case Unitity.SystemType.TC: BindTC(); break;
                case Unitity.SystemType.HR: BindHR(); break;
                case Unitity.SystemType.SAP: break;
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string systemtype = Request.QueryString["systype"];
            switch ((Unitity.SystemType)Enum.Parse(typeof(Unitity.SystemType), systemtype, true))
            {
                case Unitity.SystemType.AD: BindAD(); break;
                case Unitity.SystemType.ADComputer: BindADComputer(); break;
                case Unitity.SystemType.HEC: BindHEC(); break;
                case Unitity.SystemType.TC: BindTC(); break;
                case Unitity.SystemType.HR: BindHR(); break;
                case Unitity.SystemType.SAP: break;
            }
        }
    }
}