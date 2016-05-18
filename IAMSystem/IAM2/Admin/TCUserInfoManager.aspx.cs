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
    public partial class TCUserInfoManager : BasePage
    {
        public bool IsAdmin = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                inputAddNew.Disabled = !base.ReturnUserRole.Admin;
                if (!base.ReturnUserRole.Admin && !base.ReturnUserRole.TC&&!base.ReturnUserRole.Leader)
                {
                    base.NoRole();
                }
                //Bind();
            }
            IsAdmin = base.ReturnUserRole.Admin;
        }

        void Bind()
        {

            List<V_TCUserInfo> list = new List<V_TCUserInfo>();
            System.Text.StringBuilder stb = new System.Text.StringBuilder();
            stb.Append("SELECT * FROM V_TCUserInfo WHERE 1=1");
            string gonghao = txtgonghao.Text.Trim();
            string name = txtname.Text.Trim();
            string department = txtdepartment.Text.Trim();
            string tcusername = txtuserName.Text.Trim();
            string xukejibie = dplxukejibie.SelectedValue ;
            string type = dpltype.SelectedValue;
            string userstatus = dpljinyong.SelectedValue;
            string gangwei = txtgangwei.Text.Trim();
            List<System.Data.SqlClient.SqlParameter> parms=new List<System.Data.SqlClient.SqlParameter>();
            if (!string.IsNullOrEmpty(gonghao))
            {
                stb.Append(string.Format(" and ecode like @gonghao"));
                parms.Add(new System.Data.SqlClient.SqlParameter("@gonghao","%"+gonghao+"%"));
            }

            if (!string.IsNullOrEmpty(name))
            {
                stb.Append(string.Format(" and ename like @ename"));
                parms.Add(new System.Data.SqlClient.SqlParameter("@ename", "%" + name + "%"));

            }

            if (!string.IsNullOrEmpty(department))
            {
                stb.Append(string.Format(" and dname like @dept"));
                parms.Add(new System.Data.SqlClient.SqlParameter("@dept", "%" + department + "%"));
            }

            if (!string.IsNullOrEmpty(tcusername))
            {
                stb.Append(string.Format(" and mzhanghao like @zhanghao", "%" + tcusername + "%"));
                parms.Add(new System.Data.SqlClient.SqlParameter("@zhanghao", "%" + tcusername + "%"));
            }

            if (!string.IsNullOrEmpty(type))
            {
                stb.Append(string.Format(" and mUserType='{0}'",type));
            }

            if (!string.IsNullOrEmpty(xukejibie))
                stb.Append(string.Format(" and uLicenseLevel = '{0}'", xukejibie));
           
            if(!string.IsNullOrEmpty(userstatus))
            stb.Append(string.Format(" and uUserStatus={0}", userstatus));

            using (IAMEntities db = new IAMEntities())
            {
                list = db.ExecuteStoreQuery<V_TCUserInfo>(stb.ToString(),parms.ToArray()).ToList<V_TCUserInfo>();
            }

            AspNetPager1.RecordCount = list.Count;
            AspNetPager1.PageSize = base.PageSize;
            list = list.OrderBy(item => item.uUserID).Skip((AspNetPager1.CurrentPageIndex - 1) * base.PageSize).Take(base.PageSize).ToList();
            
            repeaterTCUserInfo.DataSource = list;
            repeaterTCUserInfo.DataBind();
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