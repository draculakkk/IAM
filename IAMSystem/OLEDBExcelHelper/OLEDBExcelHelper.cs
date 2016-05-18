using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.OleDb;
using System.Data;

namespace OLEDBExcelHelper
{
    public class OLEDBExcelHelper
    {
        public static System.Data.DataTable ReadExcle(string filepath)
        {
            DataTable dt=new DataTable();
            if (!string.IsNullOrEmpty(filepath))
            {
                string ConnectionStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath + ";Extended Properties='Excel 8.0;'";
                using (OleDbConnection con = new OleDbConnection(ConnectionStr))
                {
                    System.Threading.Thread.Sleep(1000);
                    con.Open();
                    DataTable tmpdt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });
                    string firstSheet = tmpdt.Rows[0][2].ToString();
                    string sql = "select * from ["+firstSheet+"]";
                    OleDbCommand cmd = con.CreateCommand();
                    cmd.CommandText = sql;
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                }
                return dt;
            }
            return null;
        }

        public static bool Write(string filepath, string newfilepath, DataTable DataSource)
        {
            if (DataSource != null)
            {
                File.Copy(filepath, newfilepath);
                string ConnectionStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + newfilepath + ";Extended Properties='Excel 8.0;'";
                using (OleDbConnection con = new OleDbConnection(ConnectionStr))
                {
                    try
                    {
                        System.Threading.Thread.Sleep(1000);
                        con.Open();
                    }
                    catch (Exception ex)
                    { 
                    
                    }
                    DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });
                    string firstSheet = dt.Rows[0][2].ToString();

                    for (int i = 0; i < DataSource.Rows.Count; i++)
                    {
                        System.Text.StringBuilder stb = new StringBuilder();
                        stb.Append("insert into [" + firstSheet + "](");
                        stb.Append("[" + DataSource.Columns[0].ColumnName + "]");
                        string value = "'" + DataSource.Rows[i][0] + "'";
                        for (int j = 1; j < DataSource.Columns.Count; j++)
                        {
                            stb.Append(",[" + DataSource.Columns[j].ColumnName + "]");
                            value += ",'" + DataSource.Rows[i][j] + "'";
                        }

                        stb.Append(")values(");

                        stb.Append(value + ")");
                        string sql = stb.ToString();
                        OleDbCommand cmd = new OleDbCommand(sql, con);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                   


                }
                return true;
            }
            else
            {
                File.Copy(filepath, newfilepath);
                return false;
            }
        }





    }
}
