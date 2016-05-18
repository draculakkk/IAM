using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAMEntityDAL;
using BaseDataAccess;

namespace IAM.ConflictResolution
{
    public partial class UserConfilictByUser : BasePage
    {
        public class conflicttype
        {
            public Guid Id { get; set; }
            public string p2 { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { }
        }
        void Bind()
        {
            string systemtype = dplsystemtype.SelectedValue;
            var list = new Sys_UserName_ConflictResolutionDAL().ReturnList().Where(item => item.STATE == Convert.ToInt32(dlpsysType.SelectedValue)).ToList();
            if (!string.IsNullOrEmpty(systemtype))
                list = list.Where(item => item.SysType.Trim() == systemtype).ToList();
            list = list.Where(item => item.CollSysValue.Trim() == "源系统新增账号").ToList();
            AspNetPager1.RecordCount = list.Count;
            AspNetPager1.PageSize = base.PageSize;
            repeaterUserDeferences.DataSource = list.OrderByDescending(item => item.CreateTime).Skip((AspNetPager1.CurrentPageIndex - 1) * base.PageSize).Take(base.PageSize);
            repeaterUserDeferences.DataBind();
            updatepagerhtml();

        }

        private void updatepagerhtml()
        {
            AspNetPager1.CustomInfoHTML = string.Format("共{0}页,当前第{1}页,共{2}条", AspNetPager1.PageCount, AspNetPager1.CurrentPageIndex, AspNetPager1.RecordCount);
        }

        protected void AspNetPager1_PageChanging(object sender, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            Bind();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Bind();
        }

        List<conflicttype> ReturnCheckedItems()
        {
            List<conflicttype> jj = new List<conflicttype>();
            Repeater fdla = (Repeater)this.Page.Master.FindControl("ContentPlaceHolder1").FindControl("repeaterUserDeferences");

            for (int i = 0; i < fdla.Items.Count; i++)
            {
                System.Web.UI.HtmlControls.HtmlInputCheckBox checkbox = (System.Web.UI.HtmlControls.HtmlInputCheckBox)fdla.Items[i].FindControl("repcheckbox");
                HiddenField hip2 = (HiddenField)fdla.Items[i].FindControl("hiddenp2");
                if (checkbox.Checked == true)
                {
                    conflicttype cf = new conflicttype();
                    cf.Id = new Guid(checkbox.Value);
                    cf.p2 = hip2.Value;
                    jj.Add(cf);
                }
            }
            return jj;
        }

        protected void btniam_ServerClick(object sender, EventArgs e)
        {
            string sql = "";

            List<conflicttype> list = ReturnCheckedItems();
            foreach (var item in list)
            {
                sql += "update Sys_UserName_ConflictResolution set STATE=2,ApprovedTime=getdate(), remark='" + hiddenMemo.Value + "' where ID='" + item.Id.ToString() + "';";
            }
            int count = 0;
            using (IAMEntities db = new IAMEntities())
            {
                count = db.ExecuteStoreCommand(sql);
                db.SaveChanges();
            }
            if (count > 0)
            {
                Response.Write("<script>alert('操作成功');</script>"); 
            }
            Bind();
        }

        // //p2 user标记为账号字段冲突 role 标记为账号角色冲突
        protected void btnSystem_ServerClick(object sender, EventArgs e)
        {
            List<conflicttype> lis = ReturnCheckedItems();
            var listdata = new Sys_UserName_ConflictResolutionDAL().ReturnList().Where(item => item.STATE == Convert.ToInt32(dlpsysType.SelectedValue)).ToList();
            System.Text.StringBuilder stb = new System.Text.StringBuilder();
            foreach (var item in lis)
            {
                Sys_UserName_ConflictResolution mo = listdata.FirstOrDefault(j => j.ID == item.Id);
                stb.Append("update Sys_UserName_ConflictResolution set STATE=3,ApprovedTime=getdate(), remark='" + hiddenMemo.Value + "' where ID='" + item.Id.ToString() + "';");
                if (item.p2 == "role")
                {
                    stb.Append(" " + mo.Remark + ";");
                }
            }
            int count = 0;
            if (stb.ToString() != string.Empty)
            {
                try
                {
                    using (IAMEntities db = new IAMEntities())
                    {
                        count = db.ExecuteStoreCommand(stb.ToString());
                        db.SaveChanges();
                    }
                }
                catch
                { 
                
                }
               
            }
            Bind();
            if (count > 0)
            {
                Response.Write("<script>alert('操作成功');</script>");
            }
            
        }

        

        public string ReturnOpenLink(object _sysType, object _key)
        {
            return _key.ToString();
        }
    }
}