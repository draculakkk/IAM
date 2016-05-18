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
    public partial class AD_ComputerCreate : BasePage
    {
        private string Id
        {
            get
            {
                if (Request.QueryString["id"] != null)
                    return Request.QueryString["id"];
                else
                {
                    return string.Empty;

                }
            }

        }

        public bool IsAdmin = false;

        ADComputerDAL _computerServices = new ADComputerDAL();
        ADWorkGroupDAL _workGroupServices = new ADWorkGroupDAL();
        AD_Computer_WorkGroupsDAL _adworkgroups = new AD_Computer_WorkGroupsDAL();
        IAMEntities db = new IAMEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    txtComputerName.Attributes.Add("readonly", "true");
                }

                if (Request.QueryString["type"] == null)
                {
                    dpltype.Items.Add(new ListItem("员工", "员工"));
                }
                else if (Request.QueryString["type"].ToString() != "yuangong")
                {
                    dpltype.Items.Add(new ListItem("员工", "员工"));
                }
                bindWorkGroup();
                bind();
                

            }

            if (base.ReturnUserRole.AD == false && base.ReturnUserRole.Admin == false && base.ReturnUserRole.Leader == false)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('无权查看该页面');window.location.href='./hrEmployeeManager.aspx';", true);
            }
            IsAdmin = base.ReturnUserRole.Admin;

            ControlButton();
        }

        void ControlButton()
        {
            if (base.ReturnUserRole.Admin == false)
            {
                btnYes.Enabled = false;
                btnAdd.Enabled = false;
                foreach (RepeaterItem i in repeaterComputerWorkGroups.Items)
                {
                    LinkButton lbtn = (LinkButton)i.FindControl("lbtnDelete");
                    lbtn.Enabled = false;
                }
            }
        }


        void bind()
        {
            PageComputerInfo pagemodule = null;
            int count = 0;
            //AD_Computer module = _computerServices.GetAdComputerList(out count).FirstOrDefault(item => item.NAME == Id);
            //if (module != null)
            //{
            //    pagemodule = new PageComputerInfo(); 
            //    txtComputerName.Value = module.NAME;
            //    txtDescription.Text = module.DESCRIPTION;
            //    txtMemo.Text = module.Memo;
            //    pagemodule.ComputerInfo = module;
            //}
            string gonghao = Request.QueryString["gonghao"];
            string name = Id;
            var module = from a in db.AD_Computer
                         join
                             b in db.AccountMaping on a.NAME equals b.zhanghao
                         where b.gonghao == gonghao && b.type == "ADComputer" && a.NAME == name
                         select new
                         {
                             NAME = a.NAME,
                             DESCRIPTION = a.DESCRIPTION,
                             Memo = a.Memo,
                             usertype = b.UserType,
                             enable=a.ENABLE
                         };
            if (module != null && module.Count() > 0)
            {
                txtComputerName.Value = module.ToList()[0].NAME;
                txtDescription.Text = module.ToList()[0].DESCRIPTION;
                txtMemo.Text = module.ToList()[0].Memo;
                dpltype.SelectedValue = module.ToList()[0].usertype;
                chkenable.Checked = module.ToList()[0].enable == 1 ? true : false;
            }

            List<AD_Computer_WorkGroups> worklist = new List<AD_Computer_WorkGroups>();

            if (ViewState["cworklist"] != null)
            {
                worklist = (List<AD_Computer_WorkGroups>)ViewState["cworklist"];
            }
            else
            {
                worklist = _adworkgroups.GetList().Where(item => item.ComputerName == Id && item.isdr != 1).ToList();
                ViewState["cworklist"] = worklist;
            }
            
            if (worklist != null && worklist.Count > 0)
            {
                repeaterComputerWorkGroups.DataSource = worklist.Where(item=>item.isdr!=1);
                repeaterComputerWorkGroups.DataBind();

            }

        }

        void bindWorkGroup()
        {
            int count = 0;
            ddlworklist.DataSource = _workGroupServices.GetADWorkGroupList(out count);
            ddlworklist.DataTextField = "NAME";
            ddlworklist.DataValueField = "NAME";
            ddlworklist.DataBind();
            ListItem li = new ListItem("", "");
            li.Selected = true;
            ddlworklist.Items.Add(li);
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtComputerName.Value.Trim() == string.Empty)
            {
                bind();
                return;
            }
            List<AD_Computer_WorkGroups> worklist = (List<AD_Computer_WorkGroups>)ViewState["cworklist"];
            var enti = worklist.FirstOrDefault(item => item.ComputerName == txtComputerName.Value.Trim() && item.WorkGroup == ddlworklist.SelectedValue && item.isdr != 1);
            if (enti == null)
            {
              
                worklist.Add(new AD_Computer_WorkGroups() { Id = Guid.NewGuid(), ComputerName = txtComputerName.Value, WorkGroup = ddlworklist.SelectedValue, isdr = 2 });
                bind();
            }
            else
            {
                Response.Write("<script>alert('"+ddlworklist.SelectedValue+"在该计算机工作组中已存在');</script>");
            }
            
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {

           if (string.IsNullOrEmpty(txtComputerName.Value))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请填写计算机名称');", true);
                return;
            }
           else if (Request.QueryString["gonghao"] == null&&dpltype.SelectedValue!="系统")
           {
               ClientScript.RegisterStartupScript(this.GetType(), "", "alert('无工号\\n只可新建系统类计算机');", true);
               return;
           }

            AD_Computer module = new AD_Computer();
            module.NAME = txtComputerName.Value;
            module.DESCRIPTION = txtDescription.Text;
            module.Memo = txtMemo.Text;
            module.ID = Guid.NewGuid();
            module.ENABLE = chkenable.Checked ? 1 : 0;

            if (Request.QueryString["id"] != null)
            {
                string sqlcomputer = "UPDATE dbo.AD_Computer SET DESCRIPTION=@des,Memo=@memo,ENABLE=@ENABLE WHERE NAME=@name ";
                db.ExecuteStoreCommand(sqlcomputer, new System.Data.SqlClient.SqlParameter[]{
                new System.Data.SqlClient.SqlParameter("@des",txtDescription.Text),
                new System.Data.SqlClient.SqlParameter("@memo",txtMemo.Text.Trim()),
                new System.Data.SqlClient.SqlParameter("@name",Id),
                new System.Data.SqlClient.SqlParameter("@ENABLE",module.ENABLE)
                });
                string sqlacc = "UPDATE dbo.AccountMaping SET UserType=@ut WHERE zhanghao=@z AND type='ADComputer'";
                db.ExecuteStoreCommand(sqlacc, new System.Data.SqlClient.SqlParameter[]{
                new System.Data.SqlClient.SqlParameter("@ut",dpltype.SelectedValue),
                new System.Data.SqlClient.SqlParameter("@z",Id)
                });
                string sqlwo = "";
                List<AD_Computer_WorkGroups> worklist = (List<AD_Computer_WorkGroups>)ViewState["cworklist"];
                foreach (var item in worklist)
                {
                    if (item.isdr == 2)
                        sqlwo += @" INSERT INTO dbo.AD_Computer_WorkGroups
         ( ComputerName, WorkGroup, Id, isdr )
 VALUES  ( '"+module.NAME+"','"+item.WorkGroup+"',newid(),2)";
                    else if (item.isdr == 1)
                    {
                        sqlwo += " update  AD_Computer_WorkGroups set isdr=1 where id='"+item.Id+"'";
                    }
                }
                if (!string.IsNullOrEmpty(sqlwo))
                {
                    db.ExecuteStoreCommand(sqlwo);
                    db.SaveChanges();
                    db.Dispose();
                }
                
            }
            else
            {
                int count;
                var adc = new ADComputerDAL().GetAdComputerList(out count).FirstOrDefault(item=>item.NAME==module.NAME);
                if (adc != null)
                {
                    Response.Write("<script>alert('计算机名称已存在 请重新填写计算机名称');</script>");
                    return;
                }


                new ADComputerDAL().Add(module);
                List<AD_Computer_WorkGroups> worklist = (List<AD_Computer_WorkGroups>)ViewState["cworklist"];
                foreach (var item in worklist.Where(item=>item.isdr!=1))
                {
                    AD_Computer_WorkGroupsDAL _s = new AD_Computer_WorkGroupsDAL();
                    item.ComputerName = module.NAME;
                    item.isdr = 0;
                    _s.Add(item);
                }
                new AccountMapingDAL().Add(new AccountMaping() { id = Guid.NewGuid(), type = Unitity.SystemType.ADComputer.ToString(), gonghao = Request.QueryString["gonghao"] == null ? "" : Request.QueryString["gonghao"], UserType = dpltype.SelectedValue, zhanghao = txtComputerName.Value.Trim() });
            }
            if (Request.QueryString["gonghao"] == null)
            {
                BLL.AddUserMail addmail = new BLL.AddUserMail();
                addmail.Actioner = base.UserInfo.adname;
                addmail.SystemName = Unitity.SystemType.ADComputer.ToString();
                addmail.SystemType = "系统";
                addmail.UserName = module.NAME;
                addmail.UserInfo = "";
                addmail.RoleString = BLL.CompareRoleList.ADComputerByCreateSystem(module.NAME);
                new BLL.MailServices().CreateUserMail(addmail);
            }
            else if (Request.QueryString["isupdate"] != null)
            {
                BLL.AddUserMail addmail = new BLL.AddUserMail();
                addmail.Actioner = base.UserInfo.adname;
                addmail.SystemName = Unitity.SystemType.ADComputer.ToString();
                addmail.SystemType = "系统";
                addmail.UserName = module.NAME;
                addmail.UserInfo = "";
                addmail.RoleString = BLL.CompareRoleList.ADComputerByCreateSystem(module.NAME);
                new BLL.MailServices().UpdateUserMail(addmail);
            }
            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('添加成功');window.close(true);", true);
            
        }

        protected void lbtnDelete_Command(object sender, CommandEventArgs e)
        {
            List<AD_Computer_WorkGroups> worklist = (List<AD_Computer_WorkGroups>)ViewState["cworklist"];
            Guid id=new Guid(e.CommandArgument.ToString());
            worklist.FirstOrDefault(item => item.Id == id).isdr=1;
            bind();

        }
    }
}