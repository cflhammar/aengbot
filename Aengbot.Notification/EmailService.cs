// using MailKit.Net.Smtp;
// using MailKit.Security;
// using MimeKit;

namespace Aengbot.Notification;

public class EmailService 
{
    // public async Task SendAsync(string email, List<(int,int)> teeTimes)
    // {
    //     var mail = new MimeMessage();
    //     mail.From.Add(new MailboxAddress("TeeTime Finder", "cflhammar@gmail.com"));
    //     mail.Sender = new MailboxAddress("TeeTime Finder", "cflhammar@gmail.com");
    //     mail.To.Add(MailboxAddress.Parse(email));
    //     var body = new BodyBuilder();
    //     mail.Subject = "Ledig tid!!";
    //     body.HtmlBody = Stringify(teeTimes);
    //     mail.Body = body.ToMessageBody();
    //     using var smtp = new SmtpClient();
    //     
    //     await smtp.ConnectAsync("smtp-relay.sendinblue.com", 587, SecureSocketOptions.StartTls);
    //     await smtp.AuthenticateAsync("cflhammar@gmail.com", "0GHbpB1YXV73EhQv");
    //     await smtp.SendAsync(mail);
    //     await smtp.DisconnectAsync(true);
    // }
    //
    // private string Stringify(List<(int hour, int minute)> times)
    // {
    //     string s = "";
    //     times.ForEach(t =>
    //     {
    //         s += t.hour + ":" + t.minute + "\n";
    //     });
    //     return s;
    // }
}