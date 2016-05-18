using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
   public class AD_Department_WorkGroupDAL:BaseFind<AD_Department_WorkGroup>
    {
       public int AddAd_Department_WorkGroup(AD_Department_WorkGroup entity)
       {
           return Add(entity);
       }

       public int UpdateAd_Department_WorkGroup(AD_Department_WorkGroup entity)
       {
           using (IAMEntities db = new IAMEntities())
           {
               db.AD_Department_WorkGroup.Attach(entity);
               db.ObjectStateManager.ChangeObjectState(entity,System.Data.EntityState.Modified);
              return db.SaveChanges();
           }
       }

       public int DeleteAd_Department_WorkGroup(Guid id)
       {
           using (IAMEntities db = new IAMEntities())
           {
               var mo = db.AD_Department_WorkGroup.FirstOrDefault(i=>i.ID==id);
               db.AD_Department_WorkGroup.Detach(mo);
               db.ObjectStateManager.ChangeObjectState(mo,System.Data.EntityState.Deleted);
               return db.SaveChanges();
           }
       }

       public AD_Department_WorkGroup GetOne(string id)
       {
           Guid _id = new Guid(id);
           return NonExecute<AD_Department_WorkGroup>(db => {
               return db.AD_Department_WorkGroup.FirstOrDefault(item=>item.ID==_id);
           });
       }

       /// <summary>
       /// 获取当前id 上一顺序 部门组
       /// </summary>
       /// <param name="id">id</param>
       /// <returns></returns>
       public AD_Department_WorkGroup Get_UpOne(string id)
       {
           IAMEntities db = new IAMEntities();
           string sql = @"SELECT TOP 1 * 
FROM dbo.AD_Department_WorkGroup
WHERE ordercolumn <(SELECT ordercolumn FROM dbo.AD_Department_WorkGroup WHERE ID='"+id+"') order BY ordercolumn DESC";
           var list = db.ExecuteStoreQuery<AD_Department_WorkGroup>(sql);
           return list.ToList().FirstOrDefault();
       }

       /// <summary>
       /// 获取当前id 后一顺序 部门组
       /// </summary>
       /// <param name="id">id</param>
       /// <returns></returns>
       public AD_Department_WorkGroup Get_DownOne(string id)
       {
           IAMEntities db = new IAMEntities();
           string sql = @"SELECT TOP 1 *
FROM dbo.AD_Department_WorkGroup
WHERE ordercolumn >(SELECT ordercolumn FROM dbo.AD_Department_WorkGroup WHERE ID='"+id+"') ORDER BY ordercolumn ASC";
           var list = db.ExecuteStoreQuery<AD_Department_WorkGroup>(sql);
           return list.ToList().FirstOrDefault();
       }

       public List<AD_Department_WorkGroup> GetList()
       {
           return NonExecute<List<AD_Department_WorkGroup>>(db => {
               return db.AD_Department_WorkGroup.ToList();
           });
       }


    }
}
