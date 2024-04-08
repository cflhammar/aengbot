using Aengbot.Handlers.Query;
using Aengbot.Models;
using Aengbot.Notification;
using Aengbot.Repositories;
using Sweetspot.External.Api;
using Sweetspot.External.Api.Models;

namespace Aengbot.Handlers.Command;

public interface ITriggerHandler : ICommandHandler
{
    Task<bool> Handle(CancellationToken ct);
}

public class TriggerHandler(ISweetSpotApi api, IGetSubscriptionsHandler getSubscriptionsHandler, IGetCoursesHandler getCoursesHandler, INotifier notifier, INotificationRepository notificationRepository)
    : ITriggerHandler
{
    public async Task<bool> Handle(CancellationToken ct)
    {
        var activeSubs = await getSubscriptionsHandler.Handle(ct);

        if (activeSubs.Subscriptions != null)
        {
            var emails = activeSubs.Subscriptions.Select(s => s.Email).Distinct().ToList();

            foreach (var email in emails)
            {
                var availableBookingsToNotify = new List<AengbotBooking>();
                
                // find available/not-notified bookings for each email/user and its subscriptions
                foreach (var sub in activeSubs.Subscriptions.Where(s => s.Email == email))
                {
                    var externalBookings = await api.GetBookings(sub.CourseId, sub.FromTime, sub.ToTime, sub.NumberOfPlayers);
                    var availableBookings = externalBookings?.Select(Map).ToList();
                    foreach (var aengbotBooking in availableBookings!)
                    {
                        if (!await notificationRepository.WasNotified(aengbotBooking.CourseId, aengbotBooking.Date, sub.Email))
                        {
                            aengbotBooking.CourseName = await getCoursesHandler.GetCourseName(aengbotBooking.CourseId);
                            availableBookingsToNotify.Add(aengbotBooking);
                        }
                    }
                }
                if (availableBookingsToNotify.Any())
                {
                    var notificationBookings = availableBookingsToNotify.Select(Map).ToList();
                    
                    notifier.Notify(notificationBookings, email);
                    foreach (var booking in notificationBookings)
                    {
                        await notificationRepository.AddNotified(booking.CourseId, booking.Date, email);
                    }
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
            CourseName = booking.CourseName,
            CourseId = booking.CourseId,
            Date = booking.Date,
            AvailableSlots = booking.AvailableSlots
        };
    }
}

