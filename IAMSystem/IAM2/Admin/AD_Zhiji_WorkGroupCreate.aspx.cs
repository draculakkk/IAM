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
    public partial class AD_Zhiji_WorkGroupCreate : BasePage
    {

        bool IsUpdate
        {
            get
            {
                if (Request.QueryString["name"] != null)
                    return true;
                else
                    return false;
            }
        }

        string IsZhijiName
        {
            get
            {
                if (IsUpdate)
                    return Server.UrlDecode(Request.QueryString["name"]);
                else
                    return string.Empty;
            }
        }

        string IsGroup
        {
            get
            {
                if (Request.QueryString["group"] != null)
                    return Server.UrlDecode(Request.QueryString["group"]);
                else
                    return string.Empty;
            }
        }

        //禁用为true，启用为false
        bool Isjinyong
        {
            get
            {
                if (Request.QueryString["jinyong"] != null)
                {
                    string tmp = Request.QueryString["jinyong"];
                    if (tmp == string.Empty)
                    {
                        return false;
                    }
                    bool ist = false;
                    bool.TryParse(tmp, out ist);
                    return ist;

                }
                else
                    return false;
            }
        }

        AD_Zhiji_WorkGroupDAL _adzhijiservices = new AD_Zhiji_WorkGroupDAL();


        public bool IsAdmin = false;
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                if (!base.ReturnUserRole.AD && !base.ReturnUserRole.Admin && !base.ReturnUserRole.Leader)
                {
                    base.NoRole();
                }
                txtZhiji.ReadOnly = IsUpdate;
                txtWorkGroup.Text = IsGroup;
                txtZhiji.Text = IsZhijiName;
                chkFalse.Checked = Isjinyong;

            }
            IsAdmin = base.ReturnUserRole.Admin;
        }




        AD_Zhiji_WorkGroup GetModule()
        {
            AD_Zhiji_WorkGroup module = new AD_Zhiji_WorkGroup();
            module.Zhiji = IsUpdate ? IsZhijiName : txtZhiji.Text.Trim();
            module.WorkGroup = txtWorkGroup.Text.Trim();
            module.p1 = chkFalse.Checked == true ? "True" : "False";//禁用为true，启用为false

            return module;
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            int count = 0;
            AD_Zhiji_WorkGroup itm = GetModule();
            if (IsUpdate)
            {
                count = _adzhijiservices.UpdateAd_zhiji_WorkGroup(itm);
                string mess = base.UserInfo.adname + "在" + DateTime.Now.ToString() + "编辑一职级工作组<br/>";
                mess += "职级名称:" + itm.Zhiji + "<br/>更新信息如下:<br/>工作组:" + IsGroup + "==>" + itm.WorkGroup + "<br/>";
                mess += "状态:" + Isjinyong == "False" ? "可用" : "禁用" + "==>" + itm.p1 == "False" ? "可用" : "禁用";
                new LogDAL().AdduserActionLog(base.UserInfo.adname, "编辑职级工作组信息", mess);
            }
            else
            {
                count = _adzhijiservices.AddAd_zhiji_WorkGroup(itm);
                new LogDAL().AdduserActionLog(base.UserInfo.adname, "添加职级工作组信息", base.UserInfo.adname + "在" + DateTime.Now.ToString() + "新建一职级工作组<br/>职级名称:" + itm.Zhiji);
            }

            if (count > 0)
                ClientScript.RegisterStartupScript(this.GetType(), "", "ActionMessage();", true);

        }
    }
}