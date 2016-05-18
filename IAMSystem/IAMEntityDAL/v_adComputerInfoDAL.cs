using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;
using System.Data;
using System.Data.SqlClient;

namespace IAMEntityDAL
{
    public class v_adComputerInfoDAL : BaseFind<v_adComputerInfo>
    {
        DateTime GetTime(object o)
        {
            try
            {
                string t = o.ToString();
                DateTime t1=new DateTime();
                DateTime.TryParse(t,out t1);
                return t1;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }                 

        public List<v_adComputerInfo> GetList(string gonghao,string name, string department,string gangwei,
            string computername,string usertype,string jinyong)
        {
            List<v_adComputerInfo> list = new List<v_adComputerInfo>();
            List<SqlParameter> parms=new List<SqlParameter>();
            System.Text.StringBuilder stb=new StringBuilder();
            stb.Append("select * from v_adComputerInfo where 1=1");
            if (!string.IsNullOrEmpty(gonghao))
            {
                stb.Append(" and bgonghao like @gonghao");
                parms.Add(new SqlParameter("@gonghao", "%" + gonghao + "%"));
            }

            if (!string.IsNullOrEmpty(name))
            {
                stb.Append(" and ename like @ename");
                parms.Add(new SqlParameter("@ename","%"+name+"%"));
            }

            if (!string.IsNullOrEmpty(department))
            {
                stb.Append(" and pname like @pname");
                parms.Add(new SqlParameter("@pname","%"+department+"%"));
            }

            if (!string.IsNullOrEmpty(gangwei))
            {
                stb.Append(" and eposts like @ep");
                parms.Add(new SqlParameter("@ep",gangwei));
            }

            if (!string.IsNullOrEmpty(computername))
            {
                stb.Append(" and aName like @aname");
                parms.Add(new SqlParameter("@aname","%"+computername+"%"));
            }
            if (!string.IsNullOrEmpty(usertype))
            {
                stb.Append(string.Format(" and bUserType='{0}'",usertype));
            }

            if (!string.IsNullOrEmpty(jinyong))
            {
                stb.Append(string.Format(" and aenable="+jinyong));
            }
            string constr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["comString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = stb.ToString();
                cmd.Parameters.AddRange(parms.ToArray());
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    v_adComputerInfo t = new v_adComputerInfo();
                    t.aID = new Guid(reader["aID"].ToString());
                    t.aDESCRIPTION = reader["aDESCRIPTION"].ToString();
                    t.aExpiryDate = GetTime(reader["aExpiryDate"]);
                    t.aName = reader["aName"].ToString();
                    t.bgonghao = reader["bgonghao"].ToString();
                    t.bid = reader["bid"]==DBNull.Value || reader["bid"]==null? (Guid?)null:new Guid(reader["bid"].ToString());
                    t.btype = reader["btype"].ToString();
                    t.bUserType = reader["bUserType"].ToString();
                    t.bzhanghao = reader["bzhanghao"].ToString();
                    t.ecode = reader["ecode"].ToString();
                    t.eleavePostsDate = GetTime(reader["eleavePostsDate"]);
                    t.emoblePhone = reader["emoblePhone"].ToString();
                    t.ename = reader["ename"].ToString();
                    t.ePk_psndoc = reader["ePk_psndoc"].ToString();
                    t.etoPostsDate = GetTime(reader["etoPostsDate"].ToString());
                    t.eposts = reader["eposts"].ToString();
                    t.euserScope = reader["euserScope"].ToString();
                    t.pdept = reader["pdept"].ToString();
                    t.pname = reader["pname"].ToString();
                    t.aenable = reader["aenable"]!=null?Convert.ToInt32(reader["aenable"].ToString()):(int?)null;                    
                    list.Add(t);
                }
            }
            return list;
        }
    }
}
