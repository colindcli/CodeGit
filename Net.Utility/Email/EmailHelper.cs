using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;

public class EmailHelper
{
    /// <summary>
    /// 
    /// </summary>
    public class Receiver
    {
        /// <summary>
        /// 收件人邮箱地址
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// 收件人显示名称
        /// </summary>
        public string DisplayName { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class Email
    {
        /// <summary>
        /// 收件人
        /// </summary>
        public List<Receiver> Emails { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Body { get; set; }
    }

    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="email"></param>
    /// <param name="displayName"></param>
    /// <param name="subject"></param>
    /// <param name="body"></param>
    /// <returns></returns>
    public static bool Send(string email, string displayName, string subject, string body)
    {
        return Send(new Email()
        {
            Emails = new List<Receiver>()
            {
                new Receiver()
                {
                    DisplayName = displayName,
                    EmailAddress = email
                }
            },
            Subject = subject,
            Body = body
        });
    }

    /// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="model"></param>
    public static bool Send(Email model)
    {
        var m = MailInfo;
        var smtp = new SmtpClient
        {
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = true,
            Host = m.SmtpServerAddress,
            Port = m.SmtpServerPortNumber,
            Credentials = new NetworkCredential(m.SmtpUserName, m.SmtpUserPassword),
            EnableSsl = m.EnableSsl
        };

        var success = true;
        model.Emails.ForEach(email =>
        {
            try
            {
                smtp.Send(new MailMessage(m.FromMailAddress, email.EmailAddress, model.Subject, model.Body)
                {
                    IsBodyHtml = true
                });
            }
            catch (Exception ex)
            {
                LogHelper.Fatal($"发送邮件失败：To:{email.EmailAddress};Subject:{model.Subject};Body:{model.Body}", ex);
                success = false;
            }
        });
        return success;
    }


    private class MailInfoModel
    {
        public string SmtpServerAddress { get; set; }
        public string SmtpUserName { get; set; }
        public string FromMailAddress { get; set; }
        public string SmtpUserPassword { get; set; }
        public int SmtpServerPortNumber { get; set; }
        public bool EnableSsl { get; set; }
    }

    private static string GetValue(string name)
    {
        return ConfigurationManager.AppSettings[name];
    }

    private static MailInfoModel MailInfoObject { get; set; }

    private static MailInfoModel MailInfo
    {
        get
        {
            if (MailInfoObject != null)
            {
                return MailInfoObject;
            }
            try
            {
                var model = new MailInfoModel()
                {
                    SmtpServerAddress = GetValue("Email.SmtpServerAddress"),
                    SmtpUserName = GetValue("Email.SmtpUserName"),
                    FromMailAddress = GetValue("Email.FromMailAddress"),
                    SmtpUserPassword = GetValue("Email.SmtpUserPassword"),
                    SmtpServerPortNumber = GetValue("Email.SmtpServerPortNumber") != null ? int.Parse(GetValue("Email.SmtpServerPortNumber")) : 25,
                    EnableSsl = GetValue("Email.EnableSsl") != null && bool.Parse(GetValue("Email.EnableSsl"))
                };
                MailInfoObject = model;
                return MailInfoObject;
            }
            catch (Exception ex)
            {
                LogHelper.Fatal(ex.Message, ex);
                return null;
            }
        }
    }

}
