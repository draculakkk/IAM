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
    public partial class SyncListManager : System.Web.UI.Page
    {
        //LogDAL _logServices = new LogDAL();
        int pagecount = 10;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
               
            }
        }

        void Bind()
        {
            
            this.repeater1log.DataSource = IAMEntityDAL.SyncTask.tasklist;
            this.repeater1log.DataBind();
        }
        /// <summary>
        /// 运行按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Unnamed_Click(object sender, EventArgs e)
        {
            var bt = (Button)sender;
            IAMEntityDAL.SyncTask.RunTask(bt.ToolTip);
        }

    }
}