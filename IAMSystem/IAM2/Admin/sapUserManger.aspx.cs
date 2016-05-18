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
    public partial class sapUserManger : BasePage
    {
        IAMEntities db = new IAMEntities();
        SAPUserInfoDAL _userServices = new SAPUserInfoDAL();
        public bool IsAdmin = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                inputAddNew.Disabled = !base.ReturnUserRole.Admin;
                if (!base.ReturnUserRole.Admin && !base.ReturnUserRole.SAP&&!base.ReturnUserRole.Leader)
                {
                    base.NoRole();
                }
                //Bind();
            }
            IsAdmin = base.ReturnUserRole.Admin;
        }


        void Bind()
        {


            IAMEntities db = new IAMEntities();
            string gonghao = txtgonghao.Text.Trim();
            string department = txtdepartment.Text.Trim();
            string name = txtname.Text.Trim();
            string gangwei = txtgangwei.Text.Trim();
            string sapname = txtusername.Text.Trim();
            string startdate = txtStartDate.Text.Trim();
            string enddate = txtEndDate.Text.Trim();
            string usertype = dplUserType.SelectedValue;
            string leixing = dplleixing.SelectedValue;

            string sql = " SELECT * FROM V_SAP_UserInfo where 1=1";
            List<System.Data.SqlClient.SqlParameter> parms = new List<System.Data.SqlClient.SqlParameter>();
            if (!string.IsNullOrEmpty(gonghao))
            {
                sql += " and bGONGHAO like @gonghao ";                                       
                parms.Add(new System.Data.SqlClient.SqlParameter("@gonghao","%" + gonghao + "%"));
            }
            if (!string.IsNullOrEmpty(department))
            {
                sql += " and dNAME like @dept";
                parms.Add(new System.Data.SqlClient.SqlParameter("@dept", "%" + department + "%"));
            }
            if (!string.IsNullOrEmpty(name))
            {
                sql += " and eNAME like @name";
                parms.Add(new System.Data.SqlClient.SqlParameter("@name", "%" + name + "%"));
            }

            if (!string.IsNullOrEmpty(gangwei))
            {
                sql += " and eposts like @eposts";
                parms.Add(new System.Data.SqlClient.SqlParameter("@eposts","%"+gangwei+"%"));
            }

            if (!string.IsNullOrEmpty(sapname))
            {
                sql += " and aBAPIBNAME like @aBAPIBNAME";
                parms.Add(new System.Data.SqlClient.SqlParameter("@aBAPIBNAME", "%" + sapname + "%"));
            }

            if (
                !string.IsNullOrEmpty(leixing))
            {
                sql += " and bUserType='"+leixing+"'";
            }

            if (!string.IsNullOrEmpty(startdate))
            {
                sql += " and aSTART_DATE like '%" + startdate + "%'";
                parms.Add(new System.Data.SqlClient.SqlParameter("@aBAPIBNAME", "%" + sapname + "%"));
            }
            if (!string.IsNullOrEmpty(enddate))
                sql += " and aEND_DATE like '%"+enddate+"%'"; 
            if(!string.IsNullOrEmpty(usertype))
                sql += " and aUSERTYPE like '%"+usertype+"%'"; //list.Where(item => item.aUSERTYPE.Contains(usertype)).ToList();

            var list = db.ExecuteStoreQuery<V_SAP_UserInfo>(sql,parms.ToArray());
            var lii = list.ToList();
            AspNetPager1.RecordCount = lii.Count;
            AspNetPager1.PageSize = base.PageSize;

            lii = lii.ToList().OrderBy(item => item.aBAPIBNAME).Skip((AspNetPager1.CurrentPageIndex - 1) * base.PageSize).Take(base.PageSize).ToList();

            repeaterSapUserInfo.DataSource = lii;
            repeaterSapUserInfo.DataBind();
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
    }
}