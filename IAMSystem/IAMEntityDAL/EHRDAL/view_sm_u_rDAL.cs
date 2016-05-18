using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using BaseDataAccess;
using System.Data.OleDb;

namespace IAMEntityDAL.EHRDAL
{
    public partial class view_bd_psndocDAL
    {
        public List<EHREntities.view_sm_u_r> ReturnHrsmUserRole()
        {
            try
            {
                OleDbDataReader reader = _sqlhelper.ExcuetReader(_sqlhelper.ConnectionString, System.Data.CommandType.Text, "select * from shacehr.view_sm_u_r");
                List<EHREntities.view_sm_u_r> list = new List<EHREntities.view_sm_u_r>();
                while (reader.Read())
                {
                    EHREntities.view_sm_u_r model = new EHREntities.view_sm_u_r();
                    model.Pk_user_role = reader["Pk_user_role"].ToString();
                    model.CuserId = reader["Cuserid"].ToString();
                    model.Pk_corp = reader["Pk_corp"].ToString();
                    model.Pk_role = reader["Pk_role"].ToString();
                    model.Ts = reader["Ts"].ToString();
                    model.Dr = reader["Dr"] != DBNull.Value ? Convert.ToInt32(reader["Dr"].ToString()) : 0;
                    list.Add(model);
                }
                return list;
            }
            catch (Exception ex)
            {
                new LogDAL().AddsysErrorLog(ex.ToString());
                return new List<EHREntities.view_sm_u_r>();

            }

        }
    }
}
