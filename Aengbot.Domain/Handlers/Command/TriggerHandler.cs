using Aengbot.Handlers.Query;
using Aengbot.Models;
using Aengbot.Notification;
using Sweetspot.External.Api;
using Sweetspot.External.Api.Models;

namespace Aengbot.Handlers.Command;

public interface ITriggerHandler : ICommandHandler
{
    Task<bool> Handle(CancellationToken ct);
}

public class TriggerHandler(ISweetSpotApi api, IGetSubscriptionsHandler getSubscriptionsHandler, IGetCoursesHandler getCoursesHandler, INotifier notifier)
    : ITriggerHandler
{
    public async Task<bool> Handle(CancellationToken ct)
    {
        // figure out what courses and what dates here
        // for each subscription...
        var activeSubs = await getSubscriptionsHandler.Handle(ct);

        if (activeSubs.Subscriptions != null)
        {
            foreach (var sub in activeSubs.Subscriptions)
            {
                // get bookings (change to sub data)
                var bookings = await api.GetBookings(sub.CourseId, sub.FromTime, sub.ToTime, sub.NumberOfPlayers);
                    // "dummyId",
                    // new DateTime(2023, 04, 22, 05, 00, 00),
                    // new DateTime(2023, 04, 22, 18, 00, 00));
                var available = bookings?.Select(Map).ToList();
                
                // send notification
                if (available != null && available.Any())
                {
                    var courseName = await getCoursesHandler.GetCourseName(sub.CourseId);
                    notifier.Notify(available.Select(Map).ToList(), sub.Email, courseName);
                }
            }
        }

        return true;
    }
    
    
    private AengbotBooking Map(Booking booking)
    {
        return new AengbotBooking()
        {
            CourseId = booking.course.uuid,
            Date = booking.from,
            AvailableSlots = booking.available_slots
        };
    }
    
    private NotificationBooking Map(AengbotBooking booking)
    {
        return new NotificationBooking
        {
            CourseId = booking.CourseId,
            Date = booking.Date,
            AvailableSlots = booking.AvailableSlots
        };
    }
}

