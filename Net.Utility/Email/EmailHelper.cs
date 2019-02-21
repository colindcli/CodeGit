//https://www.nuget.org/packages/DwrUtility
//Install-Package DwrUtility
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

/// <summary>
/// 发送邮件
/// </summary>
public class EmailHelper
{
    /// <summary>
    /// 
    /// </summary>
    public class SmtpModel
    {
        /// <summary>
        /// smtp服务，如：smtp.163.com
        /// </summary>
        public string SmtpServerAddress { get; set; }
        /// <summary>
        /// smtp用户名：xxx@163.com
        /// </summary>
        public string SmtpUserName { get; set; }
        /// <summary>
        /// smtp邮箱地址：xxx@163.com
        /// </summary>
        public string FromMailAddress { get; set; }
        /// <summary>
        /// smtp密码
        /// </summary>
        public string SmtpUserPassword { get; set; }
        /// <summary>
        /// 端口，默认25
        /// </summary>
        public int SmtpServerPortNumber { get; set; } = 25;
        /// <summary>
        /// 是否启用安全套接字层，默认false
        /// </summary>
        public bool EnableSsl { get; set; } = false;

        /// <summary>
        /// 日志(可选)
        /// </summary>
        public Action<Exception> Log { get; set; }
    }

    private SmtpModel Smtp { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="smtp"></param>
    public EmailHelper(SmtpModel smtp)
    {
        Smtp = smtp;
    }

    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="toEmail">接收邮箱地址</param>
    /// <param name="displayName">接收邮箱地址显示名称</param>
    /// <param name="subject">标题</param>
    /// <param name="body">内容</param>
    /// <returns></returns>
    public bool Send(string toEmail, string displayName, string subject, string body)
    {
        var smtp = new SmtpClient
        {
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = true,
            Host = Smtp.SmtpServerAddress,
            Port = Smtp.SmtpServerPortNumber,
            Credentials = new NetworkCredential(Smtp.SmtpUserName, Smtp.SmtpUserPassword),
            EnableSsl = Smtp.EnableSsl,
        };

        try
        {
            var msg = new MailMessage(new MailAddress(Smtp.FromMailAddress, displayName),
                new MailAddress(toEmail))
            {
                IsBodyHtml = true,
                Subject = subject,
                Body = body
            };
            smtp.Send(msg);
            return true;
        }
        catch (Exception ex)
        {
            Smtp.Log?.Invoke(ex);
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
    /// <returns></returns>
    public Task<bool> SendAsynic(string toEmail, string displayName, string subject, string body)
    {
        return Task.Run(() => Send(toEmail, displayName, subject, body));
    }
}