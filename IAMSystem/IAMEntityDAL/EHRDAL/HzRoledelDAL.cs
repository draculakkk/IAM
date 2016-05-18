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
        public List<BaseDataAccess.EHREntities.hz_Roledel> ReturnHzRoleDel()
        {
            try
            {
                OleDbDataReader reader = _sqlhelper.ExcuetReader(_sqlhelper.ConnectionString, System.Data.CommandType.Text, "select * from shacehr.view_sm_role");//先 使用 sm_role，之后在 使用view_sm_role
                List<EHREntities.hz_Roledel> list = new List<EHREntities.hz_Roledel>();
                while (reader.Read())
                {
                    EHREntities.hz_Roledel module = new EHREntities.hz_Roledel();
                    module.Pk_role = reader["Pk_role"].ToString();
                    module.Role_code = reader["Role_code"].ToString();
                    module.Role_name = reader["role_name"].ToString();
                    module.Pk_corp = reader["Pk_corp"].ToString();
                    module.Resource_type = reader["Resource_type"] != DBNull.Value ? Convert.ToInt32(reader["Resource_type"].ToString()) : 0;
                    module.Ts = reader["Ts"] != DBNull.Value ? reader["Ts"].ToString() : "";
                    module.Dr = reader["Dr"] != DBNull.Value ? Convert.ToInt32(reader["Dr"].ToString()) : 0;
                    list.Add(module);
                }
                return list;
            }
            catch (Exception ex)
            {
                new LogDAL().AddsysErrorLog(ex.ToString());
                return null;
            }
        }
    }
}
