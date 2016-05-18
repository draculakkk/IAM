using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAM2.rolelist
{
    public partial class HRCompanyManager : IAM.BasePage
    {
        BaseDataAccess.IAMEntities db = new BaseDataAccess.IAMEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        void Bind()
        { 
            var list=db.HRCompany.Where(item=>1==1);
            AspNetPager1.RecordCount = list.Count();
            AspNetPager1.PageSize = 15;
            repeaterHrRole.DataSource = list.OrderBy(item => item.Pk_CORP).Skip((AspNetPager1.CurrentPageIndex - 1) * 15).Take(15).ToList();
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