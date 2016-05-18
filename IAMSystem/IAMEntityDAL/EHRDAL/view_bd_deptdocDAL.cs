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
        public List<EHREntities.view_bd_deptdoc> ReturnViewbdDeptdoc()
        {
            try
            {
                List<EHREntities.view_bd_deptdoc> listviewdbdeptodc = new List<EHREntities.view_bd_deptdoc>();
                OleDbDataReader reader = _sqlhelper.ExcuetReader(_sqlhelper.ConnectionString, System.Data.CommandType.Text, "select * from shacehr.view_bd_deptdoc");
                while (reader.Read())
                {
                    EHREntities.view_bd_deptdoc module = new EHREntities.view_bd_deptdoc();
                    module.Pk_deptdoc = reader["Pk_deptdoc"].ToString();
                    module.deptcode = reader["deptcode"].ToString();
                    module.Deptname = reader["Deptname"].ToString();
                    module.pk_fathedept = reader["pk_fathedept"].ToString();
                    module.hrcanceled = reader["hrcanceled"].ToString();
                    module.canceled = reader["canceled"].ToString();
                    module.Pk_corp = reader["Pk_corp"].ToString();
                    module.Canceldate = reader["Canceldate"].ToString();
                    listviewdbdeptodc.Add(module);
                }
                return listviewdbdeptodc;
            }
            catch (Exception ex)
            {
                return new List<EHREntities.view_bd_deptdoc>();
                new LogDAL().AddsysErrorLog(ex.ToString());
            }
        }


        
    }
}
