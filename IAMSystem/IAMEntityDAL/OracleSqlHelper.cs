using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;

namespace IAMEntityDAL
{
   public class OracleSqlHelper
    {
       string OracleConnectionString = "Data Source=(DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = {0})(PORT = {1})))" +
                "(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME ={2})));User Id={3}; Password={4}";
            string host = ConfigurationManager.AppSettings["Host"];
            string Port = ConfigurationManager.AppSettings["Port"];
            string ServiceName = ConfigurationManager.AppSettings["ServiceName"];
            string UserId = ConfigurationManager.AppSettings["UserId"];
            string Password = ConfigurationManager.AppSettings["Password"];
            string OracleConn = string.Empty;
            string connStr = "";
            public OracleSqlHelper()
            {
                connStr = string.Format(OracleConnectionString, host, Port, ServiceName, UserId, Password);
               // connStr = string.Format( "data source = {0};user id= {1}; password={2};persist security info=false",ServiceName,UserId,Password);
            }

            public OracleConnection Orconnection()
            {
                OracleConnection ocon = new OracleConnection();
                ocon.ConnectionString = connStr;
                if (ocon.State == ConnectionState.Closed)
                {
                    ocon.Open();
                   
                }
                else if (ocon.State == ConnectionState.Broken)
                {
                    ocon.Dispose();
                    ocon.Open();
                }
                else if(ocon.State==ConnectionState.Open)
                {
                    return ocon;
                }

                return ocon;
            }

            public DataSet ExcetDataset(string sqlString)
            {
                try
                {
                    using (OracleConnection con = Orconnection())
                    {
                        OracleCommand cmd = new OracleCommand();
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sqlString;
                        DataSet ds = new DataSet();
                        OracleDataAdapter da = new OracleDataAdapter(cmd);
                        da.Fill(ds);
                        return ds;

                    }
                }
                catch (OracleException ex)
                {
                  var id=  new LogDAL().AddsysErrorLog(ex.ToString());
                  throw new Exception("访问Oracle数据库出错;出错信息主键为："+id.ToString());
                }
            }


            public OracleDataReader ExcuetReader(string sqlString)
            {
                try
                {
                    using (OracleConnection con = Orconnection())
                    {
                        OracleCommand cmd = new OracleCommand();
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sqlString;
                        return cmd.ExecuteReader();                     

                    }
                }
                catch (OracleException ex)
                {
                    var id = new LogDAL().AddsysErrorLog(ex.ToString());
                    throw new Exception("访问Oracle数据库出错;出错信息主键为：" + id.ToString());
                }
            }

       

    }
}
