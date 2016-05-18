using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace IAM.BLL
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

    public class SendMail
    {
       static IAMSendMailServices.MessageWebService MailServices = new IAMSendMailServices.MessageWebService();
        public static void Send(MailInfo Model)
        {
            MailServices.SendMessage(Model.Authorizationid,Model.SendMode,Model.SendTime,Model.To,Model.Subject,Model.Body,Model.URLS);
        }
    }


}