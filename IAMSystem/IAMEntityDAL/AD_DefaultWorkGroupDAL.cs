using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
   public class AD_DefaultWorkGroupDAL :BaseFind<AD_DefaultWorkGroup>
    {
       /// <summary>
       /// 更新默认组
       /// </summary>
       /// <param name="entity"></param>
       /// <returns></returns>
       public int UpdateAdDefaultWorkGroup(AD_DefaultWorkGroup entity)
       {
           using (IAMEntities db = new IAMEntities())
           {
               db.AD_DefaultWorkGroup.Attach(entity);
               db.ObjectStateManager.ChangeObjectState(entity,System.Data.EntityState.Modified);
               return db.SaveChanges();
           }
       }

       /// <summary>
       /// 获取默认组列表
       /// </summary>
       /// <returns></returns>
       public List<AD_DefaultWorkGroup> GetList()
       {
           return NonExecute<List<AD_DefaultWorkGroup>>(db => {
               return db.AD_DefaultWorkGroup.ToList();
           });
       }

       /// <summary>
       /// 获取单个默认组
       /// </summary>
       /// <param name="id">id</param>
       /// <returns></returns>
       public AD_DefaultWorkGroup GetOne(Guid id)
       {
           return NonExecute<AD_DefaultWorkGroup>(db => {
               return db.AD_DefaultWorkGroup.FirstOrDefault(item=>item.Id==id);
           });
       }

       /// <summary>
       /// 删除默认组
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public int DeleteDefaultWorkGroup(Guid id)
       {
           using (IAMEntities db = new IAMEntities())
           {
               var modle = db.AD_DefaultWorkGroup.FirstOrDefault(item=>item.Id==id);
               db.DeleteObject(modle);
               db.SaveChanges();
              string sql = "DELETE dbo.AD_UserWorkGroup WHERE GroupName='"+modle.NAME+"'";
              return db.ExecuteStoreCommand(sql);
           }
       }
    }
}
