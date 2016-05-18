using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAM2.Report
{
    public partial class HECUserGangwei : IAM.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { }
        }

        void Bind()
        {
            string gonghao = txtgonghao.Text;
            string bumen = txtbumen.Text;
            string xingming = txtxingming.Text;
            string hrgangwei = txthrgangwei.Text;
            string heczhanghao = txtzhanghao.Text;
            string zhanghaoleixing = dplzhanghaoleixing.SelectedValue;
            string gongsi = txtgongsi.Text;
            string hecbumen = txthecbumen.Text;
            string hecgangwei = txthecgangwei.Text;
            string shifouzhuagangwei = ddlshifouzhugangwei.SelectedValue;
            string shifouqiyong = dplshifouqiyong.SelectedValue;
            int count;
            var lis = new IAMEntityDAL.VHECGangWei().GetVHECGangwei(gonghao, xingming, bumen, hrgangwei, heczhanghao, zhanghaoleixing, gongsi, hecbumen, hecgangwei, shifouzhuagangwei, shifouqiyong, base.PageSize, AspNetPager1.CurrentPageIndex, out count, Request.QueryString["user"] == "1" ? true : false);
            AspNetPager1.RecordCount = count;
            AspNetPager1.PageSize = base.PageSize;
            repeater1HECUserrole.DataSource = lis;
            repeater1HECUserrole.DataBind();
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