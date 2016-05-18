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
    public partial class SAP_ParametersSettingManager : BasePage
    {
        SAP_ParametersSettingDAL _pamrsServices = new SAP_ParametersSettingDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bind(); 
            }
        }

        void bind()
        {
            repeaterSetting.DataSource = _pamrsServices.list().OrderBy(item => item.OrderColumn);
            repeaterSetting.DataBind();
        }

        protected void repeaterSetting_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Guid id = new Guid(e.CommandArgument.ToString());
            if (e.CommandName == "up")
            {
                
                var my = _pamrsServices.list().FirstOrDefault(item=>item.id==id);
                var upentity = _pamrsServices.Get_UpOne(e.CommandArgument.ToString());
                if (upentity == null)
                {
                    Response.Write("<script>alert('已在最上面');</script>");
                }
                else
                {
                    int m = my.OrderColumn;
                    int u = upentity.OrderColumn;
                    string sql = "update SAP_ParametersSetting set OrderColumn="+u+" where id='"+id.ToString()+"'";
                    sql += " update SAP_ParametersSetting set OrderColumn="+m+" where id='"+upentity.id.ToString()+"'";
                    using (IAMEntities db = new IAMEntities())
                    {
                        db.ExecuteStoreCommand(sql);
                        db.SaveChanges();
                    }
                }
            }
            else if (e.CommandName == "down")
            {
                var my = _pamrsServices.list().FirstOrDefault(item => item.id == id);
                var upentity = _pamrsServices.Get_DownOne(e.CommandArgument.ToString());

                if (upentity == null)
                {
                    Response.Write("<script>alert('已在最下方');</script>");
                }
                else
                {
                    int m = my.OrderColumn;
                    int u = upentity.OrderColumn;
                    string sql = "update SAP_ParametersSetting set OrderColumn=" + u + " where id='" + id.ToString() + "'";
                    sql += " update SAP_ParametersSetting set OrderColumn=" + m + " where id='" + upentity.id.ToString() + "'";
                    using (IAMEntities db = new IAMEntities())
                    {
                        db.ExecuteStoreCommand(sql);
                        db.SaveChanges();
                    }
                }
                
            }
            bind();
        }
    }
}