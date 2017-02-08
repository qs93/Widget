using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

/// <summary>
/// 電郵操作
/// </summary>
public class EmailHelper
{
    public EmailHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 发送邮件 1：成功 0：地址為空 其它：為錯誤信息
    /// </summary>
    /// <param name="Subject">主题</param>
    /// <param name="Content">内容</param>
    /// <param name="accessoriesPath">附件路徑</param>
    /// <param name="emailAddress">收件人郵箱</param>
    /// <param name="nameaddress">收件人姓名</param>
    /// <param name="EnableSsl">是否使用SSL加密</param>
    public static string SendEmail2(string emailAddress, string subject, string content, string accessoriesPath)
    {
        string email = StringHelper.AppSettingsInStr("EmailFrom");//发件人邮箱
        string password = StringHelper.AppSettingsInStr("EmailPwd");//发件人邮箱密码
        string sMTP = StringHelper.AppSettingsInStr("SmtpServer");//发件人邮箱的SMTP服务器
        int port = StringHelper.AppSettingsInInt("EmailPort", 0);//SMTP服务器端口
        bool enableSsl = StringHelper.AppSettingsInBool("EnableSsl");
        string emailFormName = StringHelper.AppSettingsInStr("EmailFormName");
        if (emailAddress != null && emailAddress.Trim().Length <= 0)
        {
            return "0";
        }
        emailAddress = emailAddress.Trim().Replace("\r\n", ",");
        try
        {
            MailAddress senderAddr = new MailAddress(email, emailFormName);   //自定義發送郵件會發件人
            MailAddress receipiantAddr = new MailAddress(emailAddress, "GFOOD");
            using (MailMessage mail = new MailMessage(senderAddr, receipiantAddr))
            {
                mail.Subject = subject;
                mail.Body = content;
                mail.IsBodyHtml = true;
                mail.To.Clear();
                foreach (string Address in emailAddress.Split(','))
                {
                    if (Address.Trim() != "")
                    {
                        mail.To.Add(Address);
                    }
                }
                if (!string.IsNullOrEmpty(accessoriesPath))
                {
                    string[] fileCollection = accessoriesPath.Split('◆');
                    foreach (string f in fileCollection)
                    {
                        mail.Attachments.Add(new Attachment(f));
                    }
                }
                SmtpClient smtp = new SmtpClient(sMTP);
                smtp.Credentials = new NetworkCredential(email, password);
                smtp.Port = port;
                smtp.EnableSsl = enableSsl;
                smtp.Send(mail);
                return "1";
            }
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
}