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
    public partial class HRhzRoleManager :BasePage
    {
        HRsm_roleDAL hrsmroleservices = new HRsm_roleDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }


        void Bind()
        {
            int count = 0;
            repeater1Role.DataSource = hrsmroleservices.HRsmRoleList(out count);
            repeater1Role.DataBind();
            
        }

        private void updatepagerhtml()
        {
            AspNetPager1.CustomInfoHTML = string.Format("共{0}页,当前第{1}页", AspNetPager1.PageCount, AspNetPager1.CurrentPageIndex);
        }

        protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            Bind();
        }
    }
}