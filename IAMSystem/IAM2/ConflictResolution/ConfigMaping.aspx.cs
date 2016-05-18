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
    public partial class ConfigMaping : BasePage
    {
        IAMEntities db = new IAMEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }

        void Bind()
        {
            var lis = db.AccountMaping.Where(item => item.UserType == "" && item.gonghao == "").ToList();
            AspNetPager1.RecordCount = lis.Count;
            AspNetPager1.PageSize = base.PageSize;
            lis = lis.OrderBy(item => item.type).Skip((AspNetPager1.CurrentPageIndex - 1) * base.PageSize).Take(base.PageSize).ToList();
            this.repeater1.DataSource = lis;
            this.repeater1.DataBind();
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


        protected void btnupdate_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder stb = new System.Text.StringBuilder();
            System.Text.StringBuilder mes = new System.Text.StringBuilder();
            var count = 0;
            List<System.Data.SqlClient.SqlParameter> parms = new List<System.Data.SqlClient.SqlParameter>();
            foreach (RepeaterItem ite in repeater1.Items)
            {
                System.Web.UI.HtmlControls.HtmlInputCheckBox checkbox = (System.Web.UI.HtmlControls.HtmlInputCheckBox)ite.FindControl("repcheckbox");
                TextBox txtgonghao = (TextBox)ite.FindControl("txtgonghao");
                DropDownList dplusertype = (DropDownList)ite.FindControl("dplusertype");
                HiddenField hiddenzhanghao = (HiddenField)ite.FindControl("hiddenzhanghao");
                string p = "@gonghao" + count.ToString();
                if (checkbox.Checked && dplusertype.SelectedValue != "")
                {
                    if (dplusertype.SelectedValue != "系统" && txtgonghao.Text.Trim() != string.Empty)
                    {
                        stb.Append(string.Format(@"UPDATE dbo.AccountMaping SET gonghao=" + p + ",UserType='{0}' WHERE id='{1}' ", dplusertype.SelectedValue, checkbox.Value));
                        parms.Add(new System.Data.SqlClient.SqlParameter(p, txtgonghao.Text.Trim()));
                    }
                    else if (dplusertype.SelectedValue == "系统")
                    {
                        stb.Append(string.Format(@"UPDATE dbo.AccountMaping SET gonghao=" + p + ",UserType='{0}' WHERE id='{1}' ", dplusertype.SelectedValue, checkbox.Value));
                        parms.Add(new System.Data.SqlClient.SqlParameter(p, txtgonghao.Text.Trim()));
                    }
                    else
                    {
                        mes.Append(hiddenzhanghao.Value + "类型为" + dplusertype.SelectedValue + "工号不能为空 ");
                    }
                    count++; 
                }
                
            }

            if (count == 0)
            {
                Response.Write("<script>alert('请选择');</script>");
                return;
            }

            if (stb.ToString() != string.Empty)
            {
                count = 0;
                count = db.ExecuteStoreCommand(stb.ToString(), parms.ToArray());
                db.SaveChanges();
               
            }
            if (count > 0 && mes.ToString() == string.Empty)
            {
                Response.Write("<script>alert('Mapping关系配置成功');</script>");

            }
            else
            {
                Response.Write("<script>alert('"+mes.ToString()+" ');</script>");
            }
            Bind();

        }
    }
}