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
    public partial class AdUserInfoManager : BasePage
    {
        IAMEntities db = new IAMEntities();
        ADUserInfoDAL aduserservices = new ADUserInfoDAL();
        public bool IsAdmin = false;
        public class itemmodel
        {
            public string gonghao { get; set; }
            public string Department { get; set; }
            public string NAME { get; set; }
            public string Posts { get; set; }
            public DateTime? ToPostsDate { get; set; }
            public DateTime? LeavePostsDate { get; set; }
            public string Accountname { get; set; }
            public string UserID { get; set; }
            public Guid? bid { get; set; }
            public string bUserType { get; set; }
            public bool? ENABLE { get; set; }
            public DateTime? expiryDate { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //bind();
                btnAddNew.Disabled = !base.ReturnUserRole.Admin;
                if (!base.ReturnUserRole.AD&&!base.ReturnUserRole.Leader&&!base.ReturnUserRole.Admin)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('你无权查看该页面!');window.close();", true);
                }
            }
            IsAdmin = base.ReturnUserRole.Admin;
        }

        void bind()
        {
            string gonghao = txtgonghao.Text.Trim();
            string department = txtPartment.Text.Trim();
            string name = txtName.Text.Trim();
            string adzhanghao = txtUserName.Text.Trim();
            DateTime? startdate = txtStartDate.Text.Trim() == string.Empty ? (DateTime?)null : Convert.ToDateTime(txtStartDate.Text.Trim());
            DateTime? enddate = txtEndDate.Text.Trim() == string.Empty ? (DateTime?)null : Convert.ToDateTime(txtEndDate.Text.Trim());
            string gangwei = txtgangwei.Text.Trim();
            string type = dlptype.SelectedValue;
            string enable = dplEnable.SelectedValue;

            if (string.IsNullOrEmpty(txtStartDate.Text.Trim()) && !string.IsNullOrEmpty(txtEndDate.Text.Trim()))
            {
                Response.Write("<script>alert('必须填写失效日期从字段值');</script>"); return;
            }
            if (!string.IsNullOrEmpty(txtStartDate.Text.Trim()) &&string.IsNullOrEmpty(txtEndDate.Text.Trim()))
            {
                Response.Write("<script>alert('必须填写失效日期至字段值');</script>"); return;
            }

            IAMEntities db=new IAMEntities();
            string sql = @"SELECT DISTINCT MAX(gonghao) gonghao,MAX(d.name) Department,MAX(c.name) NAME,MAX(c.Posts) Posts, 
       a.Accountname,MAX(a.UserID) UserID,MAX(b.UserType) bUserType,b.id bid,a.ENABLE,MAX(a.expiryDate) expiryDate
FROM dbo.AD_UserInfo a 
		left JOIN dbo.AccountMaping b ON a.Accountname=b.zhanghao AND b.type='ad'
     LEFT JOIN dbo.HREmployee c ON c.code=b.gonghao
	 LEFT JOIN dbo.HRDepartment d ON c.dept=d.dept
	 GROUP BY a.Accountname,a.ENABLE,b.id
	 ";//c.leavePostsDate IS NULL WHERE 1=1 and (expiryDate IS NULL OR expiryDate<'1980-12-31') 
            var userlist = db.ExecuteStoreQuery<itemmodel>(sql).ToList();

            


            if (!string.IsNullOrEmpty(enable))
            {
                bool jj = enable == "1"?true:false;
                userlist = userlist.Where(item=>item.ENABLE==jj).ToList();   
            }
            if (!string.IsNullOrEmpty(gonghao))
            {
                userlist = userlist.Where(item=>item.gonghao!=null).ToList();
                userlist = userlist.Where(item => item.gonghao.Contains(gonghao)).ToList();
            }
            if (!string.IsNullOrEmpty(department))
            {
                userlist = userlist.Where(item=>item.Department!=null).ToList();
                userlist = userlist.Where(itme => itme.Department.Contains(department)).ToList();
            }
            if (!string.IsNullOrEmpty(name))
            {
                userlist = userlist.Where(item=>item.NAME!=null).ToList();
                userlist = userlist.Where(item => item.NAME.Contains(name)).ToList();
            }
            if (!string.IsNullOrEmpty(adzhanghao))
            {
                userlist = userlist.Where(item=>item.Accountname!=null).ToList();
                userlist = userlist.Where(item => item.UserID.Contains(adzhanghao) || item.Accountname.Contains(adzhanghao)).ToList();
            }
            if (!string.IsNullOrEmpty(gangwei))
            {
                userlist = userlist.Where(item=>item.Posts!=null).ToList();
                userlist = userlist.Where(item => item.Posts.Contains(gangwei)).ToList();
            }
            if (startdate != null && enddate != null)
            {
                userlist = userlist.Where(item => item.expiryDate != null).ToList();
                userlist = userlist.Where(item => item.expiryDate >= startdate && item.expiryDate <= enddate).ToList();
            }
            else
            {
                DateTime t=new DateTime(1980,12,31);
                userlist = userlist.Where(item => item.expiryDate <=t||item.expiryDate==null).ToList();
            }

            if (!string.IsNullOrEmpty(type))
            {
                userlist = userlist.Where(item => item.bUserType!=null).ToList();
                userlist = userlist.Where(item => item.bUserType.Trim()==type).ToList();
            }
            AspNetPager1.RecordCount = userlist.Count();
            AspNetPager1.PageSize = base.PageSize;

            userlist = userlist.OrderBy(item => item.Accountname).Skip((AspNetPager1.CurrentPageIndex - 1) * base.PageSize).Take(base.PageSize).ToList();
            repeaterUserInfo.DataSource = userlist;
            repeaterUserInfo.DataBind();
            updatepagerhtml();
        }

        private void updatepagerhtml()
        {
            AspNetPager1.CustomInfoHTML = string.Format("共{0}页,当前第{1}页,共{2}条", AspNetPager1.PageCount, AspNetPager1.CurrentPageIndex, AspNetPager1.RecordCount);
        }

        protected void AspNetPager1_PageChanging(object sender, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            bind();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            bind();
        }
    }
}