using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAM
{
    public partial class HRDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int countall, okcount;
            BaseDataAccess.IAMEntities db = new BaseDataAccess.IAMEntities();
            var tmp = db.HRSm_user.FirstOrDefault(item => item.Cuserid == "0001A1100000000002FP");
            var tmp1 = BLL.Extensions.Clone<BaseDataAccess.HRSm_user>(tmp);
            tmp1.Cuserid = "";
            new IAMEntityDAL.HRSm_userDAL().SyncHRsmUser(tmp1);
        }
    }
}