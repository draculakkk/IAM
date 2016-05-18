using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseDataAccess;

namespace IAM.Admin
{
    public partial class SAPUserparmenterEdit : IAM.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<SAP_Parameters> list = new List<SAP_Parameters>();
                ViewState["sappar"] = list;
                List<SAP_User_Role> listrole = new List<SAP_User_Role>();
                ViewState["sapuserrole"] = listrole;
            }
        }
        void QueryUserParameter()
        {
            List<SAP_Parameters> list = new List<SAP_Parameters>();
            if (ViewState["sappar"] != null)
            {
                list = (List<SAP_Parameters>)ViewState["sappar"];
            }
            rptParameters.DataSource = list;
            rptParameters.DataBind();
        }
        protected void Button2_ServerClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBox1.Text.Trim()))
            {
                string[] _value = TextBox1.Text.Trim().Split('^');

                SAP_Parameters tmp = new SAP_Parameters();
                tmp.id = Guid.NewGuid();
                tmp.PARAMENTERID = _value[0];
                tmp.PARAMENTERVALUE = _value[1];
                tmp.PARAMETERTEXT = _value[2];
                tmp.isdr = 0;

                List<SAP_Parameters> list = null;
                list = (List<SAP_Parameters>)ViewState["sappar"];

                if (list != null)
                {
                    list.Add(tmp);
                    ViewState["sappar"] = list;
                }
                else
                {
                    list = new List<SAP_Parameters>();
                    list.Add(tmp);
                    ViewState["sappar"] = list;
                }
                TextBox1.Text = string.Empty;
                QueryUserParameter();
            }
        }

        void QueryUserRole()
        {
            List<SAP_User_Role> list = new List<SAP_User_Role>();
            if (ViewState["sapuserrole"] != null)
            {
                list = (List<SAP_User_Role>)ViewState["sapuserrole"];
                repeaterUserRole.DataSource = list;
                repeaterUserRole.DataBind();
            }
        }

        protected void btnOnLoadRoleInfo_ServerClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUserInfo.Text.Trim()))
            {
                string[] _value = txtUserInfo.Text.Trim().Split('^');
                SAP_User_Role tmp = new SAP_User_Role() { isdr = 0, ID = Guid.NewGuid(), ROLEID = _value[0], ROLENAME = _value[1], START_DATE = _value[2], END_DATE = _value[3], BAPIBNAME = "" };
                List<SAP_User_Role> list = (List<SAP_User_Role>)ViewState["sapuserrole"];
                if (list != null)
                {
                    var ent = list.FirstOrDefault(item => item.ROLEID.Trim() == tmp.ROLEID.Trim() && item.ROLENAME.Trim() == tmp.ROLENAME.Trim() && item.isdr != 1);
                    if (ent == null)
                        list.Add(tmp);
                    else
                        ScriptManager.RegisterStartupScript(updatepanel1, this.GetType(), "", "<script>alert('已存在该" + tmp.ROLENAME + "角色');</script>", false);
                    ViewState["sapuserrole"] = list;
                }
                else
                {
                    list = new List<SAP_User_Role>();
                    var ent = list.FirstOrDefault(item => item.ROLEID == tmp.ROLEID && item.ROLENAME == tmp.ROLENAME && item.isdr != 1);
                    if (ent == null)
                        list.Add(tmp);
                    else
                        ScriptManager.RegisterStartupScript(updatepanel1, this.GetType(), "", "<script>alert('已存在该" + tmp.ROLENAME + "角色');</script>", false);

                    ViewState["sapuserrole"] = list;
                }
                txtUserInfo.Text = string.Empty;
                QueryUserRole();

            }
        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "role")
            {
                List<SAP_User_Role> list = new List<SAP_User_Role>();
                list = (List<SAP_User_Role>)ViewState["sapuserrole"];
                if (e.CommandArgument.ToString() != string.Empty)
                {
                    Guid id = new Guid(e.CommandArgument.ToString());
                    list.Remove(list.FirstOrDefault(o => o.ID == id));
                    ViewState["sapuserrole"] = list;
                    QueryUserRole();
                }
            }
            else if (e.CommandName == "par")
            {
                List<SAP_Parameters> list = new List<SAP_Parameters>();
                list = (List<SAP_Parameters>)ViewState["sappar"];
                if (e.CommandArgument.ToString() != string.Empty)
                {
                    Guid id = new Guid(e.CommandArgument.ToString());
                    list.Remove(list.FirstOrDefault(item => item.id == id));
                    ViewState["sappar"] = list;
                    QueryUserParameter();
                }
            }
        }

        protected void Unnamed_ServerClick(object sender, EventArgs e)
        {
            List<SAP_Parameters> lispa = new List<SAP_Parameters>();
            foreach (RepeaterItem it in rptParameters.Items)
            {
                HiddenField id = (HiddenField)it.FindControl("hiddenid");
                TextBox txtwenben = (TextBox)it.FindControl("txtwenben");
                TextBox txtparvalue = (TextBox)it.FindControl("txtparametersvalue");
                TextBox txtparid = (TextBox)it.FindControl("txtparametersID");
                System.Web.UI.HtmlControls.HtmlInputRadioButton rdcreate = (System.Web.UI.HtmlControls.HtmlInputRadioButton)it.FindControl("rdcreate");
                System.Web.UI.HtmlControls.HtmlInputRadioButton rddelete =(System.Web.UI.HtmlControls.HtmlInputRadioButton)it.FindControl("rddelete");
                var n = new SAP_Parameters()
                {
                    id = new Guid(id.Value),
                    PARAMENTERID = txtparid.Text.Trim(),
                    PARAMENTERVALUE = txtparvalue.Text.Trim(),
                    PARAMETERTEXT = txtwenben.Text.Trim(),

                };
                if (rdcreate.Checked)
                    n.isdr = 2;
                if (rddelete.Checked)
                    n.isdr = 1;
                lispa.Add(n);
            }
            List<SAP_User_Role> listrold = (List<SAP_User_Role>)ViewState["sapuserrole"];
            List<SAP_User_Role> listr = new List<SAP_User_Role>();
            foreach (RepeaterItem it in repeaterUserRole.Items)
            {
                TextBox st = (TextBox)it.FindControl("rep_txtstartdate");
                TextBox en = (TextBox)it.FindControl("rep_enddate");
                HiddenField hid = (HiddenField)it.FindControl("rep_hiddenid");
                Guid id = new Guid(hid.Value);
                System.Web.UI.HtmlControls.HtmlInputRadioButton rdcreate = (System.Web.UI.HtmlControls.HtmlInputRadioButton)it.FindControl("rdcreate");
                System.Web.UI.HtmlControls.HtmlInputRadioButton rddelete = (System.Web.UI.HtmlControls.HtmlInputRadioButton)it.FindControl("rddelete");
                var entity = listrold.FirstOrDefault(item => item.ID == id);
                if (!string.IsNullOrEmpty(st.Text.Trim()))
                {
                    entity.START_DATE = st.Text.Trim();
                }

                if (!string.IsNullOrEmpty(en.Text.Trim()))
                {
                    entity.END_DATE = en.Text.Trim();
                }
                if (rdcreate.Checked)
                    entity.isdr = 2;
                if (rddelete.Checked)
                    entity.isdr = 1;
                listr.Add(entity);
            }

            string[] name = txtbapname1.Text.Trim().Split(new string[]{";"},StringSplitOptions.RemoveEmptyEntries);
            List<string> lisu = new List<string>();
            foreach (var x in name)
            {
                if (x == "")
                    continue;
                new IAMEntityDAL.SAP_ParametersDAL().IamNewAdd(lispa,x);
                new IAMEntityDAL.SAPUserRoleDAL().CreateOrUpdate(listr,x);
                new IAMEntityDAL.LogDAL().AdduserActionLog(base.UserInfo.adname,"sap批量添加参赛及权限",x+"添加了一些角色:"+string.Join(";",listr.Where(item=>item.isdr==2).Select(item=>item.ROLENAME).ToArray())+"删除了一些角色:"+string.Join(";",listr.Where(item=>item.isdr==1).Select(item=>item.ROLENAME).ToArray())+"添加了一些参数:"+string.Join(";",lispa.Where(item=>item.isdr==2).Select(item=>item.PARAMENTERID)).ToArray()+"删除了一些参数:"+string.Join(";",lispa.Where(item=>item.isdr==1).Select(item=>item.PARAMENTERID).ToArray()));
                lisu.Add(x);
            }

            BLL.SAPParmetersMail.SendMail(lisu, lispa, listr,base.UserInfo.adname);
            Response.Write("<script>alert('添加成功');window.location.href='./SAPUserparmenterEdit';</script>");
        }
    }
}