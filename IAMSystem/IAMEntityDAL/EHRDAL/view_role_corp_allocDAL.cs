using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;
using System.Data.OracleClient;
using System.Data.OleDb;

namespace IAMEntityDAL.EHRDAL
{
    public partial class view_bd_psndocDAL
    {
        public List<EHREntities.hz_r_c_allocdel> ReturnHzRoleCorpAlloc()
        {
            try
            {
                OleDbDataReader reader = _sqlhelper.ExcuetReader(_sqlhelper.ConnectionString, System.Data.CommandType.Text, "select * from shacehr.view_role_corp_alloc");
                List<EHREntities.hz_r_c_allocdel> list = new List<EHREntities.hz_r_c_allocdel>();
                while (reader.Read())
                {
                    EHREntities.hz_r_c_allocdel entity = new EHREntities.hz_r_c_allocdel();
                    entity.Dr = reader["Dr"] != DBNull.Value ? Convert.ToInt32(reader["Dr"].ToString()) : 0;
                    entity.Ts = reader["Ts"] != DBNull.Value ? reader["Ts"].ToString() : string.Empty;
                    entity.Pk_role_corp_alloc = reader["Pk_role_corp_alloc"].ToString();
                    entity.Pk_corp = reader["Pk_corp"].ToString();
                    entity.Pk_role = reader["Pk_role"].ToString();
                    list.Add(entity);
                }
                return list;
            }
            catch (Exception ex)
            {
                new LogDAL().AddsysErrorLog(ex.ToString());
                return new List<EHREntities.hz_r_c_allocdel>();
            }
        }
    }
}
