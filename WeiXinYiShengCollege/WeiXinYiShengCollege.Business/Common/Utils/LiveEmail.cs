using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Management;
using System.Collections;

namespace Module.Utils
{
    public class LiveEmail
    {
        public LiveEmail(string emailServer,string fromEmail,string fromUserId,string fromPWD,int port)
        {

            _EmailServer = emailServer;
            _FromEmail = fromEmail;
            _FromUserID = fromUserId;
            _FromPWD = fromPWD;
            _Port = port;
        }

        private string _EmailServer = "";
        private string _FromEmail = "";
        private string _FromUserID = "";
        private string _FromPWD = "";
        private int _Port = 25;

        //附件地址
        private string _strAttachmentsPath = "";
        /// <summary>
        /// 附件地址
        /// </summary>
        public string strAttachmentsPath
        {
            get { return _strAttachmentsPath; }
            set { _strAttachmentsPath = value; }
        }

        
        private string _emailBody = "";
        /// <summary>
        /// 邮件正文
        /// </summary>
        public string emailBody
        {
            get { return _emailBody; }
            set { _emailBody = value; }
        }

        //执行过程是否成功
        private bool _isSucess = false;
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool isSucess
        {
            get { return _isSucess; }
            set { _isSucess = value; }      
        }

        /// <summary>
        /// 邮件的标题
        /// </summary>
        public string emailSubject = "";

        private string FromEmailAddress
        {
            get;
            set;
        }
        public string FromEmailUserName
        {
            get;
            set;
        }
        public string ToEmailAddress
        {
            get;
            set;
        }
        public string ToEmailUserName
        {
            get;
            set;
        }
        


        /// <summary>
        /// 发送邮件
        /// </summary>
        public string SendEmail()
        {
            string strReturn = "";
            MailMessage mail = new MailMessage();
            try
            {
                //邮件服务器地址
                string EmailServer = _EmailServer;//"smtp.com.cn";

                //发件人信箱
                //string fromEmail = "xinchunheka@.com.cn";
                string fromEmail = _FromEmail;
                
                //发件人信箱用户名
                //string fromID = "xinchunheka";
                string fromID = _FromUserID;
                //发件人信箱密码
                //string fromKey = "123654a";
                string fromKey = _FromPWD;
                MailAddress from = new MailAddress(fromEmail,FromEmailUserName); //邮件的发件人
                //设置邮件的标题
                mail.Subject =  emailSubject;
                //设置邮件的发件人
                //Pass:如果不想显示自己的邮箱地址，这里可以填符合mail格式的任意名称，真正发mail的用户不在这里设定，这个仅仅只做显示用
                mail.From = from;
                //设置邮件的收件人
                mail.To.Add(new MailAddress(ToEmailAddress, ToEmailUserName));
                //抄送
                //mail.CC.Add(new MailAddress(Address1, Name1));
                //mail.CC.Add(new MailAddress(Address2, Name2));

                //设置邮件的内容
                mail.Body = emailBody;
                //设置邮件的格式
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                //设置邮件的发送级别
                mail.Priority = MailPriority.Normal;

                //添加附件
                if (strAttachmentsPath != "")
                {
                    mail.Attachments.Add(new Attachment(strAttachmentsPath));
                    mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                }
                SmtpClient client = new SmtpClient();
                //设置用于 SMTP 事务的主机的名称，填IP地址也可以了
                client.Host = EmailServer;
                //设置用于 SMTP 事务的端口，默认的是 25
                client.Port = _Port;
                client.UseDefaultCredentials = false;
                //client.UseDefaultCredentials = true;
                //邮箱的用户名和密码
                client.Credentials = new System.Net.NetworkCredential(fromID, fromKey);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = false;
                //发送邮件
                client.Send(mail);
                mail.Dispose();
            }
            catch(Exception ex)
            {
                strReturn = ex.Message;
            }
            finally
            {
                mail.Dispose();
            }
            return strReturn;
        }
    }
}
