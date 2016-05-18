using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAMEntityDAL;
using BaseDataAccess;

namespace IAM.Admin
{
    public partial class AD_WorkGroupManager : BasePage
    {
        public class tmp : IEqualityComparer<AD_workGroup>
        {
            public bool Equals(AD_workGroup x, AD_workGroup y)
            {
                return x.NAME == y.NAME;
            }

            public int GetHashCode(AD_workGroup z)
            {
                return 0;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!base.ReturnUserRole.Admin && !base.ReturnUserRole.AD&&!base.ReturnUserRole.Leader)
                {
                    Response.Write("<script>alert('你无权限查看该页面');window.opener=null;window.open('','_self');window.close();</script>");
                    return;
                }

                List<AD_workGroup> list = new List<AD_workGroup>();
                using (IAMEntities db = new IAMEntities())
                {
                    list = db.AD_workGroup.ToList();
                }
                repeaterADWorkgroup.DataSource = list.Distinct(new tmp()).ToList() ;
                repeaterADWorkgroup.DataBind();
            }
        }
    }
}