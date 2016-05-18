using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
   public class AuthorizeLogDAL:BaseQuery
    {
       
       public enum AuthRype
       {
           IAM = 0,
           HR = 1,
           EHC = 2,
           AD = 3,
           TC = 4,
           SAP = 5,
       }
       public List<BaseDataAccess.AuthorizeLog> ReturnAuthorizelog(int pagesize,int pageIndex,out int count)
       {
           List<AuthorizeLog> listAuthor = NonExecute<List<AuthorizeLog>>(db => {
               return db.AuthorizeLog.ToList();
           });

           count = listAuthor.Count;
           listAuthor = listAuthor.OrderByDescending(item => item.createtime).Skip((pageIndex - 1) * pagesize).Take(pagesize).ToList();
           return listAuthor;
       }

       private int AddAuthorizelog(AuthorizeLog entit)
       {
           return NonExecute(db => {
               if (entit.id == nid)
               {
                   entit.id = Guid.NewGuid();
               }
               db.AuthorizeLog.AddObject(entit);
           });
       }

       /// <summary>
       /// 授权日志
       /// </summary>
       /// <param name="actionuser">授权操作人</param>
       /// <param name="touser">目标用户</param>
       /// <param name="type">系统</param>
       /// <returns></returns>
       public Guid AuthorizeLog(string actionuser, string touser, AuthRype type)
       {
           var id = Guid.NewGuid();
           AddAuthorizelog(new AuthorizeLog() { id = id, actionUser = actionuser, rid = touser, type = (int)type, createtime = DateTime.Now });
           return id;
       }

       
    }
}
