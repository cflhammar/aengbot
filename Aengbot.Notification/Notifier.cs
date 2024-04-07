using System.Net.Mail;
using MimeKit;

namespace Aengbot.Notification;

public interface INotifier
{
     void Notify(List<NotificationBooking> bookings, string email, string courseName);
}

public class Notifier(IEmailService service) : INotifier
{
     public void Notify(List<NotificationBooking> bookings, string email, string courseName)
     {
          var message = CreateMessage(bookings, email, courseName);
          service.SendEmail(message, courseName);
     }
     
     private MailMessage CreateMessage(List<NotificationBooking> bookings, string email, string courseName)
     {
          var mail = new MailMessage();
          mail.From = new MailAddress("cflhammar@gmail.com", "Aengbot");
          mail.To.Add(email);
          mail.Subject = "Aengbot: Ledig tid!";
          mail.Body = StringifyBookings(bookings, courseName);
          return mail;
     }
     
     private string StringifyBookings(List<NotificationBooking> bookings, string courseName)
     {
         string s = $"Lediga tider hittade på {courseName}: \n";
         bookings.ForEach(b =>
         {
             s += b.Date.ToString("yyyy/MM/dd HH:mm") + "(" + b.AvailableSlots + ")" +"\n";
         });
         return s;
     }
}