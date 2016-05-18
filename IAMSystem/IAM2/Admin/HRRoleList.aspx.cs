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
    public partial class HRRoleList : BasePage
    {
        List<V_HRSM_ROLE_COMPANY> hr_roleCompany_allc
        {
            get {
                if (ViewState["RoleCompany"] != null)
                {
                    return (List<V_HRSM_ROLE_COMPANY>)ViewState["RoleCompany"];
                }
                else
                {
                    List<V_HRSM_ROLE_COMPANY> list = new List<V_HRSM_ROLE_COMPANY>();
                    list = new IAMEntities().V_HRSM_ROLE_COMPANY.ToList();
                    ViewState["RoleCompany"] = list;
                    return list;
                }
            }
        }

        HRsm_roleDAL roleServices = new HRsm_roleDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Bind();
                btnYes.Disabled = !base.ReturnUserRole.Admin;
                if (!base.ReturnUserRole.EHR && !base.ReturnUserRole.Admin&&!base.ReturnUserRole.Leader)
                {
                    ClientScript.RegisterStartupScript(this.GetType(),"","alert('你无权限查看该页面');window.close();",true);
                }
            }
        }

        void Bind()
        {
            int count = 0;
            List<HRsm_role> rolelist = roleServices.HRsmRoleList(out count);
            repeaterRoleComputer.DataSource = rolelist.OrderBy(item=>item.Role_code);
            repeaterRoleComputer.DataBind();
        }

       public string BindCompany(string roleid)
        {
            List<V_HRSM_ROLE_COMPANY> tmp = hr_roleCompany_allc.Where(item => item.caPK_role == roleid).ToList();
            System.Text.StringBuilder stb = new System.Text.StringBuilder();
            if (tmp != null)
            {
                foreach (var item in tmp)
                {
                    stb.Append(" <div style=\"float:left;margin-left:10px;margin-bottom:5px;margin-top:5px;\">");
                    stb.Append("<input type=\"checkbox\" value=\""+item.cpk_corp+"^"+item.rPK_ROLES+"^"+item.rROLE_CODE+"^"+item.rROLENAME+"^"+item.cUNTTNAME+"^"+item.rRESOURCE_TYPE+";\" /><span style=\"margin-left:5px;\">"+item.cUNTTNAME+"<input type=\"hidden\" value=\""+item.rPK_ROLES+"\"/></span></div>");
                }
            }
            return stb.ToString();
        }
    }
}