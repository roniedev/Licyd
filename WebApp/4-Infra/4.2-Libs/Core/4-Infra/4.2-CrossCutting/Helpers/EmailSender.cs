using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace Core;

/// <summary>
/// 
/// </summary>
public class EmailSender : IEmailSender
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpUsername;
    private readonly string _smtpPassword;

    /// <summary>
    /// 
    /// </summary>
    public EmailSender()
    {
        _smtpServer = "smtp-mail.outlook.com";
        _smtpPort = 587;
        _smtpUsername = "ronie.nogueira@outlook.com";
        _smtpPassword = "oeaiiqjkymdipqcl";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="to"></param>
    /// <param name="subject"></param>
    /// <param name="body"></param>
    /// <returns></returns>
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        using var client = new SmtpClient(_smtpServer, _smtpPort);
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
        client.EnableSsl = true;

        var message = new MailMessage(_smtpUsername, to, subject, body)
        {
            IsBodyHtml = true
        };

        await client.SendMailAsync(message);
    }
}