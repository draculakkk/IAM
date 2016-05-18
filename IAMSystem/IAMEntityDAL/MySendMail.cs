using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAMEntityDAL
{
    public class MailInfo
    {
        /// <summary>
        /// 授权id
        /// </summary>
        public Guid Authorizationid { get; set; }

        /// <summary>
        /// 消息发送模式 默认为1
        /// </summary>
        public int SendMode { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public string[] To { get; set; }

        /// <summary>
        /// 邮件主题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 消息正文
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 资源链接url集合
        /// </summary>
        public string[] URLS { get; set; }
    }

    public class MySendMail
    {
        static SendMail.MessageWebService MailServices = new SendMail.MessageWebService();
        public static void Send(MailInfo Model)
        {
            string returns = "测试环境测试";
            try
            {
                returns = MailServices.SendMessage(Model.Authorizationid, Model.SendMode, Model.SendTime,
                    Model.To, Model.Subject, Model.Body, Model.URLS);
            }
            catch (Exception ex)
            {
                new LogDAL().AddsysErrorLog(ex.ToString());
                returns = ex.Message;
            }
            string sql = @"INSERT INTO dbo.MailInfo
        ( id ,
          Authorizationid ,
          SendMode ,
          SendTime ,
          [To] ,
          SUBJECT ,
          Body ,
          URLS
        )
VALUES  ( NEWID() , -- id - uniqueidentifier
          '" + Model.Authorizationid + "' ,1,'" + Model.SendTime + "' ,'" + string.Join(";", Model.To) + "' ,'" + Model.Subject + "' ,'" + Model.Body + "' ,'" + returns + "')";
            using (BaseDataAccess.IAMEntities db = new BaseDataAccess.IAMEntities())
            {
                db.ExecuteStoreCommand(sql);
                db.SaveChanges();
            }
        }
    }
}
