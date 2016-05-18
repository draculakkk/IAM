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
    public partial class HrSmUserManager : BasePage
    {
        HRSm_userDAL _hrsmRoleServices = new HRSm_userDAL();
        public bool IsAdmin = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                inputAddNew.Disabled = !base.ReturnUserRole.Admin;
            }
            if (!base.ReturnUserRole.EHR && !base.ReturnUserRole.Admin && !base.ReturnUserRole.Leader)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('你无权限查看该页面');window.location.href='./hremployeemanager.aspx';", true);
            }
            IsAdmin = base.ReturnUserRole.Admin;
        }

        void Bind()
        {
            string gonghao = txtgonghao.Text.Trim();
            string name = txtname.Text.Trim();
            string dept = txtdepartment.Text.Trim();
            string hrname = txtUserName.Text.Trim();
            string logintype = txtLoginType.Text.Trim();
            string islock = dplLock.SelectedValue;
            string utype = dplType.SelectedValue;
            string gangwei = txtgangwei.Text.Trim();
            List<V_HRsm_UserInfo> list = new List<V_HRsm_UserInfo>();
            using (IAMEntities db = new IAMEntities())
            {
               list= db.V_HRsm_UserInfo.ToList();               
            }
            if (!string.IsNullOrEmpty(utype))
            {
                list = list.Where(item => item.bUserType == utype).ToList() ;
            }
            if (!string.IsNullOrEmpty(gonghao))
            {
                list = list.Where(item=>item.bgonghao!=null).ToList();
                list = list.Where(item => item.bgonghao.Contains(gonghao)).ToList();
            }
            if (!string.IsNullOrEmpty(name))
            {
                list = list.Where(item=>item.aUser_name!=null).ToList();
                list = list.Where(item => item.aUser_name.Contains(name)).ToList();
            }
            if (!string.IsNullOrEmpty(dept))
            {
                list = list.Where(item=>item.dname!=null).ToList();
                list = list.Where(item => item.dname.Contains(dept)).ToList();
            }
            if (!string.IsNullOrEmpty(hrname))
            {
                list = list.Where(item=>item.aUser_code!=null).ToList();
                list = list.Where(item => item.aUser_code.Contains(hrname)).ToList();
            }
            if (!string.IsNullOrEmpty(islock))
            {
                list = list.Where(item => item.aLocked_tag == islock).ToList();
            }
            if (!string.IsNullOrEmpty(logintype))
            {
                list = list.Where(item => item.aAuthen_type == logintype).ToList();
            }

            if (!string.IsNullOrEmpty(gangwei))
            {
                list = list.Where(item=>item.eposts!=null).ToList();
                list = list.Where(item=>item.eposts.Contains(gangwei)).ToList();
            }

            AspNetPager1.RecordCount = list.Count;
            AspNetPager1.PageSize = base.PageSize;
            list = list.OrderBy(item => item.aUser_code).Skip((AspNetPager1.CurrentPageIndex - 1) * base.PageSize).Take(base.PageSize).ToList();
            repeater1.DataSource = list;
            repeater1.DataBind();
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Bind();
        }


    }
}