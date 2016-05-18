using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
   public class HECGangweiDAL:BaseFind<HEC_Gangwei_Info>
    {
       /// <summary>
       /// 更新
       /// </summary>
       /// <param name="entity"></param>
       /// <returns></returns>
       public int NewUpdate(HEC_Gangwei_Info entity)
       {
           using (IAMEntities db = new IAMEntities())
           {
               db.HEC_Gangwei_Info.Attach(entity);
               db.ObjectStateManager.ChangeObjectState(entity,System.Data.EntityState.Modified);
               return db.SaveChanges();
           }
       }

       /// <summary>
       /// 删除
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public int NewDelete(Guid id)
       {
           using (IAMEntities db = new IAMEntities())
           {
               var tmp = db.HEC_Gangwei_Info.FirstOrDefault(item=>item.ID==id);
               if (tmp != null)
               {
                   db.HEC_Gangwei_Info.DeleteObject(tmp);
                   db.ObjectStateManager.ChangeObjectState(tmp,System.Data.EntityState.Deleted);
                   return db.SaveChanges();
               }
               return 0;
           }
       }

       /// <summary>
       /// 获取列表
       /// </summary>
       /// <param name="pagesize"></param>
       /// <param name="pageindex"></param>
       /// <param name="count"></param>
       /// <returns></returns>
       public List<HEC_Gangwei_Info> GetList(int pagesize, int pageindex, out int count)
       {
           IAMEntities db = new IAMEntities();
           var list = db.HEC_Gangwei_Info.Where(imte=>1==1);
           count = list.Count();
           list = list.OrderBy(x => x.POSTITION_CODE).Skip((pageindex - 1) * pagesize).Take(pagesize);
           return list.ToList();
       }

       public List<HEC_Gangwei_Info> GetList(string UNIT_CODE)
       {
           IAMEntities db = new IAMEntities();
           var list = db.HEC_Gangwei_Info.Where(imte => 1 == 1);
           if (!string.IsNullOrEmpty(UNIT_CODE))
               list = list.Where(item=>item.UNIT_CODE==UNIT_CODE);         
           return list.ToList();
       }
       public List<HEC_Gangwei_Info> GetList(string UNIT_CODE,string CompanyCode)
       {
           IAMEntities db = new IAMEntities();
           var list = db.HEC_Gangwei_Info.Where(imte => 1 == 1);
           if (!string.IsNullOrEmpty(UNIT_CODE))
               list = list.Where(item => item.UNIT_CODE == UNIT_CODE&&item.Company_Code==CompanyCode);
           return list.ToList();
       }

       public void SyncGangwei(List<HEC_Gangwei_Info> SyncList)
       {
           IAMEntities db=new IAMEntities();
           foreach (var x in SyncList)
           {
               if (x.POSITION_NAME == "财务（戴晓琳）")
               {
                   string aa = "";
               }
               var tmp = db.HEC_Gangwei_Info.FirstOrDefault(item=>item.POSTITION_CODE==x.POSTITION_CODE&&item.POSITION_NAME==x.POSITION_NAME&&item.UNIT_CODE==x.UNIT_CODE&&item.ENABLED_FLAG==x.ENABLED_FLAG);
               if (tmp == null)
                   Add(x);
               else
               {
                   //if (!tmp.POSITION_NAME.Trim().Equals( x.POSITION_NAME.Trim()))
                   //{
                   //    new LogDAL().AddMasterDataModifyLog(Unitity.SystemType.HEC, tmp.POSITION_NAME, x.POSITION_NAME, "HEC_Gangwei_Info","岗位名称");
                   //}
                   //if (!tmp.UNIT_NAME.Trim().Equals(x.UNIT_NAME.Trim()))
                   //{
                   //    new LogDAL().AddMasterDataModifyLog(Unitity.SystemType.HEC, tmp.UNIT_NAME, x.UNIT_NAME, "HEC_Gangwei_Info", "部门名称");
                   //}

                   tmp.UNIT_CODE = x.UNIT_CODE;
                   tmp.UNIT_NAME = x.UNIT_NAME;
                   tmp.POSTITION_CODE = x.POSTITION_CODE;
                   tmp.POSITION_NAME = x.POSITION_NAME;
                   tmp.ENABLED_FLAG = x.ENABLED_FLAG;
                   //db.HEC_Gangwei_Info.Attach(tmp);
                   db.ObjectStateManager.ChangeObjectState(tmp, System.Data.EntityState.Modified);  
                   db.SaveChanges();
               }
           }
       }




    }
}
