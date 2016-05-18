using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
   public class SAP_ParametersSettingDAL:BaseFind<SAP_ParametersSetting>
    {
       public int Newpdate(SAP_ParametersSetting entity)
       {
           using (IAMEntities db = new IAMEntities())
           {
               db.SAP_ParametersSetting.Attach(entity);
               db.ObjectStateManager.ChangeObjectState(entity,System.Data.EntityState.Modified);
               return db.SaveChanges();
           }
       }

       public List<SAP_ParametersSetting> list()
       {
           return NonExecute<List<SAP_ParametersSetting>>(db => {
               return db.SAP_ParametersSetting.ToList();
           });
       }


       public SAP_ParametersSetting Get_UpOne(string id)
       {
           string sql = @"SELECT TOP 1 * FROM dbo.SAP_ParametersSetting WHERE OrderColumn <(SELECT OrderColumn FROM dbo.SAP_ParametersSetting WHERE id='"+id+"') ORDER BY dbo.SAP_ParametersSetting.OrderColumn DESC";
           using (IAMEntities db = new IAMEntities())
           {
               var list = db.ExecuteStoreQuery<SAP_ParametersSetting>(sql);
               return list.ToList().FirstOrDefault();
           }
       }

       public SAP_ParametersSetting Get_DownOne(string id)
       {
           string sql = @"SELECT TOP 1 * 
FROM dbo.SAP_ParametersSetting
WHERE OrderColumn >(SELECT OrderColumn FROM dbo.SAP_ParametersSetting WHERE id='"+id+"') ORDER BY dbo.SAP_ParametersSetting.OrderColumn asc";
           using (IAMEntities db = new IAMEntities())
           {
               var list = db.ExecuteStoreQuery<SAP_ParametersSetting>(sql);
               return list.ToList().FirstOrDefault();
           }
       }
    }
}
