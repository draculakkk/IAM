using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAMEntityDAL;
using BaseDataAccess;
using System.Data;

namespace IAM.ConflictResolution
{
    public partial class HRUserResolution : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!base.ReturnUserRole.Admin)
                {
                    base.NoRole();
                }
                Bind();
            }
        }

        void Bind()
        {
        
            repeaterUserDeferences.DataSource = new sys_HRUserResolutionDAL().ReturnList().Where(item =>{
                if (dlpsysType.SelectedValue == "") return true;
                else return item.state == dlpsysType.SelectedValue;
            } ).ToList();
            repeaterUserDeferences.DataBind();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Bind();
        }

        List<Guid> ReturnCheckedItems()
        {
            List<Guid> jj = new List<Guid>();
            Repeater fdla = (Repeater)this.Page.Master.FindControl("ContentPlaceHolder1").FindControl("repeaterUserDeferences");
            for (int i = 0; i < fdla.Items.Count; i++)
            {
                System.Web.UI.HtmlControls.HtmlInputCheckBox checkbox = (System.Web.UI.HtmlControls.HtmlInputCheckBox)fdla.Items[i].FindControl("repcheckbox");
                if (checkbox.Checked == true)
                {
                    jj.Add(new Guid(checkbox.Value));
                }
            }
            return jj;
        }

        protected void btniam_ServerClick(object sender, EventArgs e)
        {
            string sql="";
            List<Guid> list = ReturnCheckedItems();
            foreach (var item in list)
            {
                sql += "update [sys_HRUserResolution] set [state]='已处理',[updatetime]=getdate() where ID='" + item.ToString() + "';";
            }
            int count = 0;
            using (IAMEntities db = new IAMEntities())
            {
               count= db.ExecuteStoreCommand(sql);
                db.SaveChanges();
            }
            if (count > 0)
            {
                Response.Write("<script>alert('更新成功');</script>");
            }
            Bind();
        }

        public string ReturnOpenLink(object _sysType,object _key)
        {
            switch (_sysType.ToString().ToUpper().Trim())
            {
                case "HR": return "OpenPage('HREmployeeCreate.aspx?id=" + _key + "');";
                case "SAP": return "OpenPage('sapusercreate.aspx?uid="+_key+"');";
                case "AD": return "OpenPage('../admin/ADInfoManager.aspx?userid="+_key+" ');";
                case "TC": return "OpenPage('TCUserInfoCreate.aspx?mzhanghao="+_key+"');";
                case "HEC": return "OpenPage('HECUserInfoCreate.aspx?usercd="+_key+"');";
                default: return string.Empty;
            }
        }
      

        protected void btnsystemhtml_Click1(object sender, EventArgs e)
        {
            DataTable dt = null;
            dt = new sys_HRUserResolutionDAL().ReturnList().Where(item =>
            {
                if (dlpsysType.SelectedValue == "") return true;
                else return item.state == dlpsysType.SelectedValue;
            }).ToList().Select(x => new
            {
                工号 = x.cardNo,
                姓名 = x.name,
                字段 = x.porpert,
                源值 = x.oldvalue,
                现值 = x.newvalue,
                产生时间 = x.createtime,
                处理时间 = x.updatetime,
                状态 = x.state
            }).ToList().ToDataTable();
            string file = "downloadFile/HRUserResolution_" + Guid.NewGuid() + ".xlsx";
            ExcelHelper.ReturnExcelExport(System.Web.HttpContext.Current.Server.MapPath("Template/ExcelTemplate/HRUserResolution.xlsx"),
                System.Web.HttpContext.Current.Server.MapPath(file),
                dt);
            Response.Redirect("~/" + file);
        }

    }
}