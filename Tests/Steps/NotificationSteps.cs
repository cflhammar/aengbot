using Reqnroll;
using Tests.Drivers;

namespace Tests.Steps;

[Binding]
public class NotificationSteps(NotificationsDriver notificationsDriver)
{
    [Then(@"notifications are sent to")]
    public void ThenNotificationsAreSentTo(Table table)
    {
        var sentNotifications = table.CreateSet<SentNotificationStep>();
        notificationsDriver.ThenNotificationsAreSentTo(sentNotifications.ToList());
    }

    public record SentNotificationStep()
    {
       public string Email { get; init; } = "";
       public string CourseName { get; init; } = "";
       public int AvailableSlots { get; init; }
       public DateTime TeeTime { get; init; }
    }
}