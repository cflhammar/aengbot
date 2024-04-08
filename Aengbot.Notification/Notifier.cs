using System.Net.Mail;
using MimeKit;

namespace Aengbot.Notification;

public interface INotifier
{
    void Notify(List<NotificationBooking> bookings, string email);
}

public class Notifier(IEmailService service) : INotifier
{
    public void Notify(List<NotificationBooking> bookings, string email)
    {
        var message = CreateMessage(bookings, email);
        service.SendEmail(message, string.Join(", ", bookings.Select(b => b.CourseName).Distinct()));
    }

    private MailMessage CreateMessage(List<NotificationBooking> bookings, string email)
    {
        var mail = new MailMessage();
        mail.From = new MailAddress("cflhammar@gmail.com", "Aengbot");
        mail.To.Add(email);
        mail.Subject = "Aengbot: Ledig tid!";
        mail.Body = StringifyBookings(bookings);
        return mail;
    }

    private string StringifyBookings(List<NotificationBooking> bookings)
    {
        string s = "";
        var courses = bookings.Select(b => b.CourseName).Distinct().ToList();
        foreach (var course in courses)
        {
            s += $"Lediga tider hittade på {course}: \n";
            foreach (var booking in bookings.Where(b => b.CourseName == course))
            {
                // adding 2 hours here to return date in swedish summertime
                s += booking.Date.AddHours(2).ToString("yyyy/MM/dd HH:mm") + "  (" + booking.AvailableSlots + ")" +
                     "\n";
            }
            s += $"-\n";
        }

        return s;
    }
}