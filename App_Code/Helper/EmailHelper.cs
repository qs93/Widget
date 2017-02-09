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
    /// <param name="email">收件人電郵(多個用,隔開)</param>
    /// <param name="subject">標題</param>
    /// <param name="content">內容</param>
    /// <param name="path">附件</param>
    /// <param name="isHtml">是否允許Html</param>
    /// <returns></returns>
    public static string Send(string email, string subject, string content, List<string> path, bool isHtml = true)
    {
        string sendEmail = StringHelper.AppSettingsInStr("SendEmail");//發件人郵箱
        string sendPwd = StringHelper.AppSettingsInStr("SendPwd");//發件人郵箱密碼
        string sendName = StringHelper.AppSettingsInStr("SendName");//發件人名稱
        string sendSmtp = StringHelper.AppSettingsInStr("SendSmtp");//發件人郵箱SMTP服務器
        int sendPort = StringHelper.AppSettingsInInt("SendPort", 0);//SMTP服務器端口
        bool enableSsl = StringHelper.AppSettingsInBool("EnableSsl");//是否使用SSL加密
        if (string.IsNullOrEmpty(email))
        {
            return "0";
        }
        //email = email.Trim().Replace("\r\n", ",");  //換行替換成,
        try
        {
            MailAddress senderAddr = new MailAddress(sendEmail, sendName);   //自定義發送郵件會發件人
            MailAddress receipiantAddr = new MailAddress(email, "GFOOD");
            using (MailMessage mail = new MailMessage(senderAddr, receipiantAddr))
            {
                mail.Subject = subject;
                mail.Body = content;
                mail.IsBodyHtml = isHtml;
                mail.To.Clear();
                foreach (string Address in email.Split(','))   //添加收件人電郵
                {
                    if (!string.IsNullOrEmpty(Address))
                    {
                        mail.To.Add(Address);
                    }
                }
                if (path.Count > 0)   //添加附件
                {
                    foreach (string f in path)
                    {
                        mail.Attachments.Add(new Attachment(f));
                    }
                }
                SmtpClient smtp = new SmtpClient(sendSmtp);
                smtp.Credentials = new NetworkCredential(sendEmail, sendPwd);
                smtp.Port = sendPort;
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