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
    public partial class TaskEmailManager : BasePage
    {
        TaskEmailDAL taskemailservices = new TaskEmailDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!base.ReturnUserRole.Admin&&!base.ReturnUserRole.Leader)
                {
                    base.NoRole();
                }

                Bind();
            }
        }

        void Bind()
        { 
            var list=taskemailservices.GetList();
            if(ddlName.SelectedValue!=string.Empty)
                list=list.Where(ite=>ite.SystemName.Trim()==ddlName.SelectedValue.Trim()).ToList();
            repeaterTaskEmail.DataSource = list;
            repeaterTaskEmail.DataBind();
        }


        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Bind();
        }
    }
}