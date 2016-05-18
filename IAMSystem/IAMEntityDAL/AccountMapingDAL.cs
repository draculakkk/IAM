using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
   public class AccountMapingDAL:BaseFind<AccountMaping>
    {

       public List<AccountMaping> GetList(string gonghao, Unitity.SystemType type)
       {
           IAMEntities db = new IAMEntities();
           string sql = "";
           switch (type)
           {
               case Unitity.SystemType.AD:
                   sql = @"SELECT a.*
FROM dbo.AccountMaping a RIGHT JOIN dbo.AD_UserInfo b ON a.zhanghao=b.Accountname 
WHERE b.ENABLE=1 AND a.gonghao=@gonghao AND (a.type='AD' OR a.type='ADComputer')";
                   break;
               case Unitity.SystemType.ADComputer:
                   sql = @"SELECT a.*
FROM dbo.AccountMaping a RIGHT JOIN dbo.AD_UserInfo b ON a.zhanghao=b.Accountname 
WHERE b.ENABLE=1 AND a.gonghao=@gonghao AND (a.type='AD' OR a.type='ADComputer')";
                   break;
               case Unitity.SystemType.HR:
                   sql = @"SELECT b.* FROM dbo.HRSm_user a INNER JOIN dbo.AccountMaping b ON a.User_code=b.zhanghao
WHERE b.type='HR'AND a.Locked_tag='N'AND b.gonghao=@gonghao";
                   break;
               case Unitity.SystemType.HEC:
                   sql = @"SELECT b.*
FROM dbo.HEC_User a INNER JOIN dbo.AccountMaping b ON a.User_CD=b.zhanghao
WHERE b.type='HEC' AND  (END_DATE IS NULL OR END_DATE>GETDATE()) AND b.gonghao=@gonghao";
                   break;
               case Unitity.SystemType.SAP:
                   sql = @"SELECT b.*
FROM dbo.SAP_UserInfo a INNER JOIN dbo.AccountMaping b ON a.BAPIBNAME=b.zhanghao AND a.END_DATE<>'0000-00-00'                                                   WHERE b.type='SAP' AND (END_DATE IS NULL or CONVERT(DATETIME2,a.END_DATE)>GETDATE() ) AND b.gonghao=@gonghao";
                   break;
               case Unitity.SystemType.TC:
                   sql = @"SELECT b.* 
FROM dbo.TC_UserInfo a INNER JOIN dbo.AccountMaping b ON a.UserID=b.zhanghao
WHERE b.type='TC' AND UserStatus=0 AND b.gonghao=@gonghao";
                   break;
           }
           if (string.IsNullOrEmpty(sql))
               return null;
           else
           {
              var list = db.ExecuteStoreQuery<AccountMaping>(sql, new System.Data.SqlClient.SqlParameter[] { new System.Data.SqlClient.SqlParameter("@gonghao", gonghao) });
              return list.ToList();
           }
       }

       /// <summary>
       /// 更改用户类型
       /// </summary>
       /// <param name="entity"></param>
       /// <returns></returns>
       public int UpdateAccountMaping(AccountMaping newentity,AccountMaping old) 
       {
           string sql = string.Format("UPDATE dbo.AccountMaping SET UserType='{0}',gonghao='{1}' WHERE type='{2}'AND( gonghao='{3}'OR gonghao IS NULL) AND zhanghao='{4}' and userType='{5}'", newentity.UserType, newentity.gonghao, old.type, old.gonghao, old.zhanghao,old.UserType);
           using (IAMEntities db = new IAMEntities())
           {
               return db.ExecuteStoreCommand(sql);
           }
       }

       public int ChangeAcc(AccountMaping entity)
       {
         using(IAMEntities db=new IAMEntities())
         {
             db.AccountMaping.Attach(entity);
             db.ObjectStateManager.ChangeObjectState(entity,System.Data.EntityState.Modified);
             return db.SaveChanges();
         }
       }

       /// <summary>
       /// 用户账号转移
       /// </summary>
       /// <param name="oldid"></param>
       /// <param name="newgonghao"></param>
       /// <returns></returns>
       public int UserTransfer(Guid oldid, string newgonghao,Unitity.SystemType type)
       {
           string sql = string.Format("UPDATE dbo.AccountMaping SET gonghao='{0}' where ID='{1}'",newgonghao,oldid);
           if (!string.IsNullOrEmpty(newgonghao))
           {
               var one = GetList(newgonghao,Unitity.SystemType.AD);
               if (one.Where(item => item.type == "AD" && item.UserType == "员工").Count()==0)
                   throw new Exception("目标工号在AD系统中无员工类账号\\n故不可转移");
           }
           using (IAMEntities db = new IAMEntities())
           {
               return db.ExecuteStoreCommand(sql);
           }
       }

       /// <summary>
       /// 查询唯一性账号
       /// </summary>
       /// <param name="gonghao">工号</param>
       /// <param name="type">系统类型</param>
       /// <param name="zhanghao">账号</param>
       /// <param name="usertype">用户类型</param>
       /// <returns></returns>
       public AccountMaping GetOne(string gonghao, string type, string zhanghao,string usertype)
       {
           return NonExecute<AccountMaping>(db => {
               return db.AccountMaping.FirstOrDefault(itme=>itme.type==type&&itme.gonghao==gonghao&&itme.zhanghao==zhanghao&&itme.UserType==usertype);
           });
       }

       /// <summary>
       /// 查询唯一性员工账号
       /// </summary>
       /// <param name="gonghao">工号</param>
       /// <param name="type">系统类型</param>
       /// <param name="usertype">用户类型</param>
       /// <returns></returns>
       public AccountMaping GetOne(string gonghao, string type, string usertype)
       {
           return NonExecute<AccountMaping>(db =>
           {
               return db.AccountMaping.FirstOrDefault(itme => itme.type == type && itme.gonghao == gonghao && itme.UserType == usertype);
           });
       }


       public AccountMaping GetOne(string zhanghao, string type)
       {
           return NonExecute<AccountMaping>(db =>
           {
               return db.AccountMaping.FirstOrDefault(itme => itme.zhanghao == zhanghao && itme.type == type);
           });
       }

       /// <summary>
       /// 查询系统中唯一账号 AD独用
       /// </summary>
       /// <param name="zhanghao">账号</param>
       /// <param name="type">系统类型</param>
       /// <returns></returns>
       public AccountMaping GetOneByzhanghao(string zhanghao, string usertype,string systemtype)
       {
           return NonExecute<AccountMaping>(db =>
           {
               return db.AccountMaping.FirstOrDefault(itme => itme.UserType.Trim() == usertype && itme.zhanghao == zhanghao && itme.type == systemtype);
           });
       }

       /// <summary>
       /// 查询系统唯一账号
       /// </summary>
       /// <param name="id">pk</param>
       /// <returns></returns>
       public AccountMaping GetOne(Guid id)
       {
           return NonExecute<AccountMaping>(db => {
               return db.AccountMaping.FirstOrDefault(item=>item.id==id);
           });
       }

       /// <summary>
       /// 根据工号查询其拥有的各个系统账号
       /// </summary>
       /// <param name="gonghao"></param>
       /// <returns></returns>
       public List<AccountMaping> GetList(string gonghao)
       {
           return NonExecute<List<AccountMaping>>(db => {
               return db.AccountMaping.Where(item=>item.gonghao==gonghao).ToList();
           });
       }
    }
}
