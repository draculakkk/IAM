using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAM.Admin
{
    public partial class SelectSapUserList :System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bind();
            }
        }

        void bind()
        {
            int count;
            string username = txtusername.Text;
            string name = txtname.Text;
            var list = new IAMEntityDAL.SAPUserInfoDAL().GetSapUserInfo(int.MaxValue, int.MaxValue
                , null, null, null, out count);
            if (!string.IsNullOrEmpty(username))
                list = list.Where(item => item.BAPIBNAME.Contains(username)).ToList();
            if (!string.IsNullOrEmpty(name))
                list = list.Where(item => item.LASTNAME.Contains(name)).ToList();

            repeaterSapUserInfo.DataSource = list; 
            repeaterSapUserInfo.DataBind();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            bind();
        }
    }
}