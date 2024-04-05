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

public class TriggerHandler(ISweetSpotApi api, IGetSubscriptionsHandler getSubscriptionsHandler, INotifier notifier)
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
                var bookings = await api.GetBookings(
                    "dummyId",
                    new DateTime(2023, 04, 22, 05, 00, 00),
                    new DateTime(2023, 04, 22, 18, 00, 00));
                var aengbotBookins = bookings?.Select(Map).ToList();
                
                // filter times 
                var bookingsWithAvailableSpots = aengbotBookins?.Where(b => b.AvailableSlots >= sub.NumberOfPlayers).ToList();

                // send notification
                notifier.Notify(bookingsWithAvailableSpots?.Select(Map).ToList(), sub.Email);
            }


            // filter times 

            // send notification
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

