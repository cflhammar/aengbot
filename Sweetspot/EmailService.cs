
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Sweetspot;

public class EmailService 
{
    public async Task<bool> SendAsync(string email, string time)
    {
        
            var mail = new MimeMessage();
            mail.From.Add(new MailboxAddress("Test", "cflhammar@gmail.com"));
            mail.Sender = new MailboxAddress("Test", "cflhammar@gmail.com");
            mail.To.Add(MailboxAddress.Parse(email));
            var body = new BodyBuilder();
            mail.Subject = "Ledig tid!!  " + time;
            body.HtmlBody = "Kolla sweetspot!";
            mail.Body = body.ToMessageBody();
            using var smtp = new SmtpClient();

            // if (_settings.UseSSL)
            //{
            //   await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.SslOnConnect);
            // }
            //else if (_settings.UseStartTls)
            //{
            await smtp.ConnectAsync("smtp-relay.sendinblue.com", 587, SecureSocketOptions.StartTls);
            // }
            await smtp.AuthenticateAsync("cflhammar@gmail.com", "0GHbpB1YXV73EhQv");
            await smtp.SendAsync(mail);
            await smtp.DisconnectAsync(true);
            return true;
    
       
        }
    
}