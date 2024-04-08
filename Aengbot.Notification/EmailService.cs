using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace Aengbot.Notification;

public interface IEmailService
{
    void SendEmail(MailMessage message, string courseName);
}

public class EmailService(IConfiguration config) : IEmailService
{
    public void SendEmail(MailMessage message, string courseName)
    {
        using var smtp = new SmtpClient();
        smtp.Host = "in-v3.mailjet.com";
        smtp.Port = 587;
        smtp.UseDefaultCredentials = false;
        var user = config.GetSection("MailServiceUsername").Value ?? throw new Exception("Email Service username not found"); 
        var pw = config.GetSection("MailServicePassword").Value ?? throw new Exception("Email Service password not found");
        smtp.Credentials = new NetworkCredential(user, pw);
        smtp.EnableSsl = true;
        
        smtp.Send(message);
        Console.WriteLine($"Email sent to {message.To} for course: {courseName}");
    }
}