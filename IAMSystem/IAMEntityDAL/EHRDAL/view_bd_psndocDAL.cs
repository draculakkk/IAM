using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data.OleDb;
using BaseDataAccess;

namespace IAMEntityDAL.EHRDAL
{
   public partial class view_bd_psndocDAL
    {
        SQLHelper _sqlhelper = new SQLHelper();
       public  List<EHREntities.view_bd_psndoc> ReturnView_bdpsndocList()
       {
           try
           {
               OleDbDataReader reader = _sqlhelper.ExcuetReader(_sqlhelper.ConnectionString, System.Data.CommandType.Text, "select * from shacehr.view_bd_psndoc");
               List<EHREntities.view_bd_psndoc> list = new List<EHREntities.view_bd_psndoc>();
               while (reader.Read())
               {
                   EHREntities.view_bd_psndoc entity = new EHREntities.view_bd_psndoc();
                   entity.Pk_psndoc = reader["Pk_psndoc"].ToString();
                   entity.Psncode = reader["psncode"].ToString();
                   entity.Jobcode = reader["jobcode"].ToString();
                   entity.Jobname = reader["jobname"].ToString();
                   entity.Pk_deptdoc = reader["Pk_deptdoc"].ToString();
                   entity.Psnname = reader["psnname"].ToString();
                   entity.Mobile = reader["mobile"].ToString();
                   entity.Indutydate = reader["indutydate"].ToString();
                   entity.Outdutydate = reader["outdutydate"] != DBNull.Value || reader["outdutydate"].ToString()!=string.Empty? Convert.ToDateTime(reader["outdutydate"].ToString()) : (DateTime?)null;
                   entity.psnlscope = reader["psnclscope"] != DBNull.Value ? Convert.ToInt32(reader["psnclscope"].ToString()) : 0;
                   entity.Pk_psncl = reader["pk_psncl"].ToString();
                   entity.Psnclasscode = reader["psnclasscode"].ToString();
                   entity.Psnclasscode = reader["psnclassname"].ToString();
                   entity.Pk_corp = reader["Pk_corp"].ToString();
                   list.Add(entity);

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
