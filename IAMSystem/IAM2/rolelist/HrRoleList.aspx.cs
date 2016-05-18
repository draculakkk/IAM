using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAMEntityDAL;
using BaseDataAccess;


namespace IAM.rolelist
{
    public partial class HrRoleList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (!base.ReturnUserRole.EHR && !base.ReturnUserRole.Admin)
                {
                    base.NoRole();
                }
                Bind();
            }
        }

        void Bind()
        {
            int count;
            List<HRsm_role> list = new HRsm_roleDAL().HRsmRoleList(out count);
            AspNetPager1.PageSize = 17;
            AspNetPager1.RecordCount = count;
            repeaterHrRole.DataSource = list.OrderBy(item=>item.Role_code).ThenBy(item=>item.role_name).Skip((AspNetPager1.CurrentPageIndex-1)*17).Take(17).ToList();
            repeaterHrRole.DataBind();
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
    }
}