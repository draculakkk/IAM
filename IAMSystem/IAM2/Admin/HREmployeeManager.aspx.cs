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
    public partial class HREmployeeManager : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
            if (base.ReturnUserRole.EndUser == true&&base.ReturnUserRole.Admin==false&&base.ReturnUserRole.EHR==false&&base.ReturnUserRole.Leader==false)
            {
                base.NoRole();
            }
        }

        void Bind()
        {
            string gonghao = txtgonghao.Text.Trim();
            string name = txtname.Text.Trim();
            string department = txtdepartment.Text.Trim();
            string gangwei = txtgangwei.Text.Trim();
            string lizhiriqi = txtlizhidate.Text.Trim();
            string lizhiriqi2 = txtlizhidate2.Text.Trim();
            string shifoulizhi = dpllizhi.SelectedValue;
            if (string.IsNullOrEmpty(lizhiriqi) && !string.IsNullOrEmpty(lizhiriqi2))
            {
                Response.Write("<script>alert('必须填写离职日期从值');</script>"); return;
            }
            if (!string.IsNullOrEmpty(lizhiriqi) && string.IsNullOrEmpty(lizhiriqi2))
            {
                Response.Write("<script>alert('必须填写离职日期至值');</script>"); return;
            }
            IAMEntities db = new IAMEntities();
            var list = db.HREmployee.Join(db.HRDepartment, item => item.dept, itm => itm.dept, (item, itm) => new
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

            if (!string.IsNullOrEmpty(gonghao))
                list = list.Where(item => item.code.Contains(gonghao)).ToList() ;
            if (!string.IsNullOrEmpty(name))
                list = list.Where(itme=>itme.name.Contains(name)).ToList();
            if (!string.IsNullOrEmpty(department))
            {
                list = list.Where(item=>item.deptname!=null).ToList();
                list = list.Where(item=>item.deptname.Contains(department)).ToList();
            }

            if (!string.IsNullOrEmpty(gangwei))
            {
                list = list.Where(item=>item.posts!=null).ToList();
                list = list.Where(item=>item.posts.Contains(gangwei)).ToList();
            }
            if (!string.IsNullOrEmpty(shifoulizhi))
            {
                if(shifoulizhi.Equals("0"))//在职
                list = list.Where(item=>item.leavePostsDate==null).ToList();
                if (shifoulizhi.Equals("1")) //离职
                    list = list.Where(item => item.leavePostsDate != null).ToList();
            }
            if (!string.IsNullOrEmpty(lizhiriqi)&&!string.IsNullOrEmpty(lizhiriqi2))
            {
                DateTime d=Convert.ToDateTime(lizhiriqi);
                DateTime d2 = Convert.ToDateTime(lizhiriqi2);
                list = list.Where(item=>item.leavePostsDate!=null).ToList();
                list = list.Where(item => item.leavePostsDate >=d&&item.leavePostsDate<=d2).ToList();
            }

            AspNetPager1.RecordCount = list.Count;
            AspNetPager1.PageSize = base.PageSize;


            list = list.ToList().OrderBy(item => item.code).Skip((AspNetPager1.CurrentPageIndex - 1) * base.PageSize).Take(base.PageSize).ToList();
            repeater1HrEmployee.DataSource = list;
            repeater1HrEmployee.DataBind();
            updatepagerhtml();

        }

        protected void AspNetPager1_PageChanging(object sender, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            Bind();
        }

        private void updatepagerhtml()
        {
            AspNetPager1.CustomInfoHTML = string.Format("共{0}页,当前第{1}页,共{2}条", AspNetPager1.PageCount, AspNetPager1.CurrentPageIndex,AspNetPager1.RecordCount);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Bind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           
        }

    }
}