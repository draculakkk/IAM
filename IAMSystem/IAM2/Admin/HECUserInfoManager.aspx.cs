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
    public partial class HECUserInfoManager : BasePage
    {
        HECUserInfoDAL _hecUserInfoServices = new HECUserInfoDAL();
        HECUserDAL _hecUserServices = new HECUserDAL();
        public bool IsAdmin = false;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // Bind();

                inputAddNew.Disabled = !base.ReturnUserRole.Admin;
                if (!base.ReturnUserRole.HEC&&!base.ReturnUserRole.Leader&&!base.ReturnUserRole.Admin)
                {

                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('你无权限查看当前页面');window.location.href='./hremployeemanager.aspx';", true);
                }
            }
            IsAdmin = base.ReturnUserRole.Admin;
        }

        void Bind()
        {
            string gonghao = txtgonghao.Text.Trim();
            string name = txtname.Text.Trim();
            string department = txtdepartment.Text.Trim();
            string gangwei = txtgangwei.Text.Trim();
            string hecname = txtUserName.Text.Trim();
            string leixing = dpltype.SelectedValue ;
            string jinyong = dpljinyong.SelectedValue;
            DateTime? startdate = txtStartdate.Text.Trim() == string.Empty ? (DateTime?)null : Convert.ToDateTime(txtStartdate.Text.Trim());
            DateTime? enddate = txtEndDate.Text.Trim() == string.Empty ? (DateTime?)null : Convert.ToDateTime(txtEndDate.Text.Trim());

            if (startdate != null && enddate == null)
            {
                Response.Write("<script>alert('请输入禁用时间至');</script>");
                return;
            }
            if (startdate == null && enddate != null)
            {
                Response.Write("<script>alert('请输入禁用时间从');</script>");
                return;
            }

            IAMEntities db = new IAMEntities();
            System.Text.StringBuilder stb = new System.Text.StringBuilder();
            stb.Append("select * from v_HEC_UserList where 1=1");
            List<System.Data.SqlClient.SqlParameter> parms = new List<System.Data.SqlClient.SqlParameter>();
            if (!string.IsNullOrEmpty(gonghao))
            {
                stb.Append(" and gonghao LIKE @gonghao");
                parms.Add(new System.Data.SqlClient.SqlParameter("@gonghao", "%" + gonghao + "%"));
            }

            if (!string.IsNullOrEmpty(name))
            {
                stb.Append(" and USER_NAME LIKE @uname");
                parms.Add(new System.Data.SqlClient.SqlParameter("@uname", "%" + name + "%"));
            }
            if (!string.IsNullOrEmpty(department))
            {
                stb.Append(" and department LIKE @depart");
                parms.Add(new System.Data.SqlClient.SqlParameter("@depart", "%" + department + "%"));
            }
            if (!string.IsNullOrEmpty(hecname))
            {
                stb.Append(" and User_CD LIKE @usercd");
                parms.Add(new System.Data.SqlClient.SqlParameter("@usercd", "%" + hecname + "%"));
            }
            //if (startdate != null)
            //{
            //    stb.Append(" and CONVERT(DATETIME,START_DATE)>@stardate");
            //        parms.Add(new System.Data.SqlClient.SqlParameter("@stardate",startdate));
            //}
            if (startdate != null&&enddate != null)
            {
                stb.Append(" AND CONVERT(DATETIME,END_DATE)>=@stardate and CONVERT(DATETIME,END_DATE)<=@enddate");
                parms.Add(new System.Data.SqlClient.SqlParameter("@enddate", enddate));
                parms.Add(new System.Data.SqlClient.SqlParameter("@stardate", startdate));
            }
            else
            {
                stb.Append(" and (END_DATE IS NULL OR END_DATE<>'' or CONVERT(DATETIME,END_DATE)<='1900-1-1') ");
            }
            if (!string.IsNullOrEmpty(dpljinyong.SelectedValue))
            {
                stb.Append(" and ISDISABLED="+dpljinyong.SelectedValue);
            }
            if (!string.IsNullOrEmpty(txtgangwei.Text.Trim()))
            {
                stb.Append(" and cposts like @post");
                parms.Add(new System.Data.SqlClient.SqlParameter("@post","%"+gangwei+"%"));
            }
            if (!string.IsNullOrEmpty(dpltype.SelectedValue))
            {
                stb.Append("and bUserType='"+dpltype.SelectedValue+"'");
            }
            var list = db.ExecuteStoreQuery<v_HEC_UserList>(stb.ToString(),parms.ToArray()).ToList();
            AspNetPager1.RecordCount = list.Count;
            AspNetPager1.PageSize = base.PageSize;
            list = list.OrderBy(item => item.User_CD).Skip((AspNetPager1.CurrentPageIndex - 1) * base.PageSize).Take(base.PageSize).ToList();
            repeater1HECUserInfo.DataSource = list; 
            repeater1HECUserInfo.DataBind();
            updatepagerhtml();
        }

        private void updatepagerhtml()
        {
            AspNetPager1.CustomInfoHTML = string.Format("共{0}页,当前第{1}页,共{2}条", AspNetPager1.PageCount, AspNetPager1.CurrentPageIndex, AspNetPager1.RecordCount);
        }

        protected void AspNetPager1_PageChanging(object sender, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            Bind();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Bind();
        }
    }
}