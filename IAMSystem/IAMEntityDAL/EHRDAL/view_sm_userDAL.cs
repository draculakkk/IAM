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
       
       public List<EHREntities.view_sm_user> Returnview_sm_userList()
       {
           try
           {
               OleDbDataReader reader = _sqlhelper.ExcuetReader(_sqlhelper.ConnectionString, System.Data.CommandType.Text, "select * from shacehr.view_sm_user");
               List<EHREntities.view_sm_user> list = new List<EHREntities.view_sm_user>();
               while (reader.Read())
               {
                   EHREntities.view_sm_user item = new EHREntities.view_sm_user()
                   {
                       Able_time = reader["Able_time"] == DBNull.Value ? "" : reader["Able_time"].ToString(),
                       Authen_type = reader["Authen_type"] == DBNull.Value ? "" : reader["Authen_type"].ToString(),
                       Cuserid = reader["Cuserid"] == DBNull.Value ? "" : reader["Cuserid"].ToString(),
                       Disable_time = reader["Disable_time"] != DBNull.Value ? reader["Disable_time"].ToString() : "",
                       Dr = reader["Dr"] != DBNull.Value ? Convert.ToInt32(reader["Dr"].ToString()) : 0,
                       Isca = reader["Isca"] == DBNull.Value ? "" : reader["Isca"].ToString(),
                       KeyUser = reader["keyuser"] != DBNull.Value ? reader["keyuser"].ToString() : "",
                       Langcode = reader["Langcode"] == DBNull.Value ? "" : reader["Langcode"].ToString(),
                       Locked_tag = reader["Locked_tag"] == DBNull.Value ? "" : reader["Locked_tag"].ToString(),
                       PwdLevelCode = reader["Pwdlevelcode"] == DBNull.Value ? "" : reader["Pwdlevelcode"].ToString(),
                       Pwdparam = reader["pwdparam"] == DBNull.Value ? "" : reader["pwdparam"].ToString(),
                       Pwdtype = reader["pwdtype"] == DBNull.Value ? 0 : Convert.ToInt32(reader["pwdtype"].ToString()),
                       Ts = reader["Ts"] == DBNull.Value ? "" : reader["Ts"].ToString(),
                       User_code = reader["user_code"] == DBNull.Value ? "" : reader["user_code"].ToString(),
                       User_name = reader["user_name"] == DBNull.Value ? "" : reader["user_name"].ToString(),
                       User_note = reader["user_note"] == DBNull.Value ? "" : reader["user_note"].ToString(),
                       User_pagessword = reader["USER_PASSWORD"] == DBNull.Value ? "" : reader["USER_PASSWORD"].ToString()
                   };
                   list.Add(item);
               }
               return list;
           }
           catch (OleDbException ex)
           {
               new LogDAL().AddsysErrorLog(ex.ToString());
               throw ex;
           }
       }
    }
}