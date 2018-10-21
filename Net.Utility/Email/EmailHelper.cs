using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailHelper
{
    public class SmtpClientModel
    {
        public string SmtpServerAddress { get; set; }
        public string SmtpUserName { get; set; }
        public string FromMailAddress { get; set; }
        public string SmtpUserPassword { get; set; }
        public int SmtpServerPortNumber { get; set; } = 25;
        public bool EnableSsl { get; set; } = false;
    }

    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="toEmail">接收邮箱地址</param>
    /// <param name="displayName">接收邮箱地址显示名称</param>
    /// <param name="subject">标题</param>
    /// <param name="body">内容</param>
    /// <param name="smtpClient"></param>
    /// <returns></returns>
    public static bool Send(string toEmail, string displayName, string subject, string body, SmtpClientModel smtpClient)
    {
        var smtp = new SmtpClient
        {
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = true,
            Host = smtpClient.SmtpServerAddress,
            Port = smtpClient.SmtpServerPortNumber,
            Credentials = new NetworkCredential(smtpClient.SmtpUserName, smtpClient.SmtpUserPassword),
            EnableSsl = smtpClient.EnableSsl,
        };

        try
        {
            var msg = new MailMessage(new MailAddress(smtpClient.FromMailAddress, displayName),
                new MailAddress(toEmail))
            {
                IsBodyHtml = true,
                Subject = subject,
                Body = body
            };
            smtp.Send(msg);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="toEmail">接收邮箱地址</param>
    /// <param name="displayName">接收邮箱地址显示名称</param>
    /// <param name="subject">标题</param>
    /// <param name="body">内容</param>
    /// <param name="smtpClient"></param>
    /// <returns></returns>
    public static Task<bool> SendAsynic(string toEmail, string displayName, string subject, string body, SmtpClientModel smtpClient)
    {
        return Task.Run(() => Send(toEmail, displayName, subject, body, smtpClient));
    }
}