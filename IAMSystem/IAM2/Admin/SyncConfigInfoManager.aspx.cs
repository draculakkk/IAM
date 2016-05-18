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
    public partial class SyncConfigInfo : System.Web.UI.Page
    {
        SyncConfigDAL _syncConfigDal = new SyncConfigDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        void Bind()
        {
            var Entity = _syncConfigDal.ReturnSyncConfigEntity("SyncConfigFirst");
            txtName.Text = Entity.asyncName;
            txtTime.Text = Entity.datetime.ToString("HH:mm:ss");
            hiddentime.Value = Entity.datetime.ToString("HH:mm:ss");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SyncConfig Entity = new SyncConfig() ;
            Entity.asyncName = txtName.Text;
            Entity.datetime = Convert.ToDateTime(txtTime.Text);
            if (_syncConfigDal.UpdateSyncConfig(Entity)>0)
            {
                Response.Write("<script>alert('更新成功');</script>");
            }
        }
        
    }
}