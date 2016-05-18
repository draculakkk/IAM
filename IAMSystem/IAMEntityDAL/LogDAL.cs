using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseDataAccess;

namespace IAMEntityDAL
{
  public class LogDAL : BaseQuery
    {

      public enum LogType 
      {
          系统错误日志 = 0,
          普通操作日志 = 1,
          邮件发送日志 = 2,
          数据同步日志 = 3,
          主数据变更日志=4
      }

      /// <summary>
      /// 添加日志
      /// </summary>
      /// <param name="entity">Log 实体类</param>
      /// <returns></returns>
      private int AddLogEntity(Log entity)
      {
          return NonExecute(db =>
          {
              if (entity.id ==nid)
              {
                  entity.id = Guid.NewGuid();
              }
             db.AddObject("Log",entity);
          });
      }
      /// <summary>
      /// 添加一个制定类型日志
      /// </summary>
      /// <param name="type"></param>
      /// <param name="mess"></param>
      /// <returns></returns>
      private Guid AddLog(LogType type, string mess)
      {
          var id=Guid.NewGuid();
          AddLogEntity(new Log() { id = id, mess = mess, createDate = DateTime.Now, type= (int)type});
          return id;
      }
      /// <summary>
      /// 添加系统错误日志
      /// </summary>
      /// <param name="mess"></param>
      /// <returns></returns>
      public Guid AddsysErrorLog(string mess)
      {
          return AddLog(LogType.系统错误日志, mess);
      }
      /// <summary>
      /// 添加操作日志
      /// </summary>
      /// <param name="Actionuser">操作人</param>
      /// <param name="ActionName">操作类型</param>
      /// <param name="Actionmess">操作内容</param>
      /// <returns></returns>
      public Guid AdduserActionLog(string Actionuser, string ActionName, string Actionmess)
      {
          string mess = string.Format("操作人：{0}<br/>操作类型：{1}<br/>内容：<br>{2}", Actionuser, ActionName, Actionmess);
          return AddLog(LogType.普通操作日志, mess);
      }
      /// <summary>
      /// 添加邮件发送日志
      /// </summary>
      /// <param name="touser">收件人列表</param>
      /// <param name="ccuser">抄送人列表</param>
      /// <param name="body">邮件内容</param>
      /// <returns></returns>
      public Guid AddEmailLog(string touser, string ccuser, string body)
      {
          string mess = string.Format("ToUser：{0}<br/>CCUser：{1}<br/>内容：<br/>{2}", touser, ccuser, body);
          return AddLog(LogType.邮件发送日志, mess);
      }
      /// <summary>
      /// 同步日志
      /// </summary>
      /// <param name="TaskName">任务名</param>
      /// <param name="SucessItemCount">成功数量</param>
      /// <param name="ErrorItemCount">失败数量</param>
      /// <param name="useDate">耗时</param>
      /// <returns></returns>
      public Guid AddSyncLog(string TaskName, int SucessItemCount, int ErrorItemCount, TimeSpan useDate)
      {
          string mess = string.Format("任务名：{0}<br/>同步结果：总数{4}，成功{1}，失败{2}<br/>耗时：{3}",TaskName,SucessItemCount,ErrorItemCount,useDate.ToString(),SucessItemCount+ErrorItemCount);
          return AddLog(LogType.数据同步日志, mess);
      }
     
     


      /// <summary>
      /// 根据日志类型查询日志
      /// </summary>
      /// <param name="pagesize"></param>
      /// <param name="pageIndex"></param>
      /// <param name="logType">日志类型</param>
      /// <param name="count"></param>
      /// <returns></returns>
      public List<Log> GetLogList(int pagesize, int pageIndex, int? logType,DateTime ?startTime,DateTime? endTime,string neirong, out int count)
      {
          IAMEntities db = new IAMEntities();
          var list = db.Log.Where(x=>1==1);

          if (logType != null)
              list = list.Where(item => item.type == logType);
          if (startTime != null && endTime != null)
              list = list.Where(item => item.createDate >= startTime && item.createDate <= endTime);
          if (!string.IsNullOrEmpty(neirong))
              list = list.Where(item=>item.mess.Contains(neirong));
         

          count = list.Count();
         list= list.OrderByDescending(item=>item.createDate).Skip((pageIndex - 1) * pagesize).Take(pagesize);
          return list.ToList();
      }

      public Log GetLogOne(Guid id)
      {
          return NonExecute<Log>(db => {
              return db.Log.FirstOrDefault(item=>item.id==id);
          });
      }


      public Guid AddMasterDataModifyLog(Unitity.SystemType SystemType, string OldValue, string NewValue,string TableName,string ColumnName)
      {
          string message = string.Format("{0}系统中的{1}表字段\"{2}\"发生了名称变更,有原来的{3}变为{4}",SystemType.ToString(),TableName,ColumnName,OldValue,NewValue);
          return AddLog(LogType.主数据变更日志,message);
      }

    }
}
