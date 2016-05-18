using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseDataAccess;
using IAMEntityDAL;

namespace IAM.Admin.adUser
{
    public partial class ADWorkGroupManger : System.Web.UI.Page
    {
        string zhanghao
        {
            get {
                if (Request.QueryString["userid"] != null)
                    return Request.QueryString["userid"];
                else
                    return string.Empty;
            }
        }

        List<AD_UserWorkGroup> list
        {
            get {
                if (ViewState["list"] != null)
                    return (List<AD_UserWorkGroup>)ViewState["list"];
                else
                {
                    List<AD_UserWorkGroup> li = new List<AD_UserWorkGroup>();
                    using (IAMEntities db = new IAMEntities())
                    {
                        li = db.AD_UserWorkGroup.Where(item=>item.Uid==zhanghao).ToList();
                    }
                    return li;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                repeaterdepartment.DataSource = new AD_Department_WorkGroupDAL().GetList().OrderBy(item=>item.ordercolumn);
                repeaterdepartment.DataBind();
                int count;
                repeaterkekong.DataSource = new ADWorkGroupDAL().GetADWorkGroupList(out count);
                repeaterkekong.DataBind();
            }
        }

        public string ReturnChecked(object workname)
        {
            string name = workname.ToString();
            AD_UserWorkGroup entity = list.FirstOrDefault(item=>item.GroupName==name);
            if (entity != null)
                return "<input type=\"checkbox\" value=\"" + name + "\" checked=\"true\" /><span style=\"margin-left:5px;\">" + name + "</span></div>";
            else
                return "<input type=\"checkbox\" value=\"" + name + "\" /><span style=\"margin-left:5px;\">" + name + "</span></div>";
        }

        public string ReturnDescription(object center, object department, object keshi)
        {
            System.Text.StringBuilder stb=new System.Text.StringBuilder();
            if (center != null)
                stb.Append(center+" ");
            if (department != null)
                stb.Append(department+" ");
            if (keshi != null)
                stb.Append(keshi);
            return stb.ToString().Trim();
        }
        

    }
}