using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Configuration;

namespace IAMEntityDAL
{
    public class SQLHelper
    {
        public string ConnectionString = "Provider=OraOLEDB.Oracle;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" + System.Configuration.ConfigurationManager.AppSettings["Host"] + ")(PORT=" + System.Configuration.ConfigurationManager.AppSettings["Port"] + "))(CONNECT_DATA=(SID=" + System.Configuration.ConfigurationManager.AppSettings["ServiceName"] + ")));User ID=" + System.Configuration.ConfigurationManager.AppSettings["UserId"] + ";Password=" + System.Configuration.ConfigurationManager.AppSettings["Password"] + ";DistribTX=0;Max Pool Size=1024;";

        public OleDbConnection Connection(string ConnectionStr)
        {
            OleDbConnection con = new OleDbConnection(ConnectionStr);
            if (con.State == ConnectionState.Open)
            {
                return con;
            }
            else if (con.State == ConnectionState.Broken)
            {
                con.Dispose();
                con.Open();
            }
            else if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }

        public OleDbDataReader ExcuetReader(string connectionStr, CommandType type, string sql, OleDbParameter[] parms)
        {
            OleDbConnection con = Connection(connectionStr);
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = type;
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(parms);
            return cmd.ExecuteReader();
        }

        public OleDbDataReader ExcuetReader(string connectionStr, CommandType type, string sql)
        {
            OleDbConnection con = Connection(connectionStr);
            OleDbCommand cmd = con.CreateCommand();
            cmd.CommandType = type;
            cmd.CommandText = sql;
            return cmd.ExecuteReader();
        }

        public DataSet ExcutDataSet(string connectionStr, CommandType type, string sql)
        {
            using (OleDbConnection con = Connection(connectionStr))
            {
                using (OleDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = type;
                    cmd.CommandText = sql;

                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds;
                }
            }
        }


        public DataSet ExcutDataSet(string connectionStr, CommandType type, string sql, OleDbParameter[] parms)
        {
            using (OleDbConnection con = Connection(connectionStr))
            {
                using (OleDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = type;
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parms);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds;
                }
            }
        }

        public int ExcuteNonQuery(string connectionStr, CommandType type, string sql, OleDbParameter[] parms)
        {
            using (OleDbConnection con = Connection(connectionStr))
            {
                using (OleDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = type;
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parms);
                    return cmd.ExecuteNonQuery();
                }
            }
        }


        public int ExcuteNonQuery(string connectionStr, CommandType type, string sql)
        {
            using (OleDbConnection con = Connection(connectionStr))
            {
                using (OleDbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = type;
                    cmd.CommandText = sql;                    
                    return cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
