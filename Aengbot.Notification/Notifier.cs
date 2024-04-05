namespace Aengbot.Notification;

public interface INotifier
{
    void Notify(List<NotificationBooking>? bookings, string email);
}

public class Notifier : INotifier
{
     public void Notify(List<NotificationBooking>? bookings, string email)
     {
          // Use email servvice
          if (bookings == null || bookings.Count == 0)
          {
               Console.WriteLine("No avaliable bookings found.");
               return;
          }
     }
}