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
    public partial class LogList :BasePage
    {
        LogDAL _logServices = new LogDAL();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                if (!base.ReturnUserRole.Admin&&!base.ReturnUserRole.Leader)
                {
                    ClientScript.RegisterStartupScript(this.GetType(),"","alert('你无权限查看该页面');",true);
                    return;
                }
                txtTimeStart.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                txtTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //Bind();
            }
        }

        void Bind()
        {
            int? logtype = null;
            DateTime? StartTime = null;
            DateTime? EndTime = null;
            if (dplType.SelectedValue != string.Empty)
                logtype = Convert.ToInt32(dplType.SelectedValue);
            if (txtTimeEnd.Text != string.Empty && txtTimeStart.Text != string.Empty)
            {
                StartTime = Convert.ToDateTime(txtTimeStart.Text);
                EndTime = Convert.ToDateTime(txtTimeEnd.Text+" 23:59:59");
            }
            int count=0;
            string mess = txtmess.Text.Trim();
            AspNetPager1.AlwaysShow = true;
            AspNetPager1.PageSize = base.PageSize;
            List<Log> listLog = _logServices.GetLogList(base.PageSize, AspNetPager1.CurrentPageIndex, logtype, StartTime, EndTime,mess, out count);
            AspNetPager1.RecordCount = count;
            updatepagerhtml();
            this.repeater1log.DataSource = listLog;
            this.repeater1log.DataBind();
        }

        protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            Bind();
        }

        private void updatepagerhtml() {
            AspNetPager1.CustomInfoHTML = string.Format("共{0}页,当前第{1}页", AspNetPager1.PageCount, AspNetPager1.CurrentPageIndex);
        }

        public string Getmess(string mess)
        {
            string aa= HttpContext.Current.Server.UrlEncode(mess);
            return aa;
        }


        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Bind();
        }
    }
}