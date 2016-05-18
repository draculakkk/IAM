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
    public partial class SAP_ParametersSettingAdd : BasePage
    {
        bool IsUpdate
        {
            get {
                if (Request.QueryString["id"] != null)
                    return true;
                else
                    return false;
            }
        }

        string querystring
        {
            get {
                if (Request.QueryString["id"] != null)
                    return Request.QueryString["id"];
                else
                    return string.Empty;
            }
        }
        SAP_ParametersSettingDAL _pamrsServices = new SAP_ParametersSettingDAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (IsUpdate)
                {
                    Guid id = new Guid(querystring);
                    var enti = _pamrsServices.list().FirstOrDefault(item=>item.id==id);
                    if (enti != null)
                    {
                        txtParmeterID.Text = enti.ParameterId;
                        chkFalse.Checked = enti.isdr == 1 ? true : false;
                        lblOrder.Text = enti.OrderColumn.ToString();
                    }
                }
                else
                lblOrder.Text = (_pamrsServices.list().Count + 1).ToString();
            }
            if (base.ReturnUserRole.Admin == false && base.ReturnUserRole.Leader==false)
            {
                base.NoRole();
            }
            btnYes.Enabled = base.ReturnUserRole.Admin;
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            if (IsUpdate)
            {
                string sql = string.Format("update dbo.SAP_ParametersSetting set ParameterId=@p,isdr={0}",chkFalse.Checked?1:0);

                if (chkFalse.Checked)
                {
                    sql += ",deletetime=getdate()";
                }
                sql += " where id='"+querystring+"'";
                IAMEntities db = new IAMEntities();
                int c = db.ExecuteStoreCommand(sql,new System.Data.SqlClient.SqlParameter("@p",txtParmeterID.Text.Trim()));
                if (c> 0)
                {
                    Response.Write("<script>alert('更新成功');window.parent.opener.Yes();window.close();</script>");
                }
            }
            else
            {
                string sql = string.Format(@"INSERT INTO dbo.SAP_ParametersSetting
        ( id ,
          ParameterId ,
          OrderColumn ,
          isdr ,
          CreateTime ,
          DeleteTime
        )
VALUES  ( NEWID() , -- id - uniqueidentifier
          '{0}' , -- ParameterId - nvarchar(500)
          {1}, -- OrderColumn - int
          {2} , -- isdr - int
          getdate() , -- CreateTime - datetime
          {3}  -- DeleteTime - datetime
        )
",txtParmeterID.Text.Trim(),lblOrder.Text,chkFalse.Checked?1:0,chkFalse.Checked?"'"+DateTime.Now.ToString()+"'":"NULL");
                IAMEntities db = new IAMEntities();
               int c= db.ExecuteStoreCommand(sql);
               db.SaveChanges();
               db.Dispose();
                
                if (c > 0)
                {
                    Response.Write("<script>alert('添加成功');window.parent.opener.Yes();window.close();</script>");
                }
            }
        }
    }
}