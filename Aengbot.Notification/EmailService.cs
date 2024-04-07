// using MailKit.Net.Smtp;
// using MailKit.Security;
// using MimeKit;

using System.Net;
using System.Net.Mail;
using MailKit.Security;
using MimeKit;

namespace Aengbot.Notification;

public interface IEmailService
{
    void SendEmail(MailMessage message, string courseName);
}

public class EmailService : IEmailService
{
    public void SendEmail(MailMessage message, string courseName)
    {
        using var smtp = new SmtpClient();
        smtp.Host = "smtp-relay.sendinblue.com";
        smtp.Port = 587;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential("cflhammar@gmail.com", "");

        smtp.Send(message);
        Console.WriteLine($"Email sent to {message.To} for course: {courseName}");
    }
}