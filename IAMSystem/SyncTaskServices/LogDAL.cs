using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SyncTaskServices
{
   public class LogDAL
    {
       public static void LogAdd(string mess)
       {
           using (SqlConnection con = new SqlConnection())
           {
               con.ConnectionString = "Data Source=10.124.90.49;Initial CataLog=IAM;User ID=sa;Password=Iam12345;";
               con.Open();
               SqlCommand cmd = con.CreateCommand();
               cmd.CommandType = CommandType.Text;
               cmd.CommandText = string.Format("insert into dbo.Log(id,mess,type,createDate) values(newid(),'{0}',1,getdate())",mess);
               cmd.ExecuteNonQuery();
           }
       }
    }
}
