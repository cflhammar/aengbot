using Aengbot.Handlers.Query;
using Aengbot.Models;
using Aengbot.Notification;
using Aengbot.Repositories;
using Sweetspot.External.Api;
using Sweetspot.External.Api.Models;

namespace Aengbot.Handlers.Command;

public interface ITriggerHandler : ICommandHandler
{
    Task<List<string>> Handle(CancellationToken ct);
}

public class TriggerHandler(
    ISweetSpotApi api,
    IGetSubscriptionsHandler getSubscriptionsHandler,
    IGetCoursesHandler getCoursesHandler,
    INotifier notifier,
    INotificationRepository notificationRepository)
    : ITriggerHandler
{
    public async Task<List<string>> Handle(CancellationToken ct)
    {
        var sent = new List<string>();
        var activeSubs = await getSubscriptionsHandler.Handle(ct);

        if (activeSubs.Subscriptions != null)
        {
            var emails = activeSubs.Subscriptions.Select(s => s.Email).Distinct().ToList();

            foreach (var email in emails)
            {
                var availableBookingsToNotify = new List<AengbotBooking>();

                // find available/not-notified bookings for each email/user and its subscriptions
                // fix logic, remove notified from subs?
                foreach (var sub in activeSubs.Subscriptions.Where(s => s.Email == email))
                {
                    var externalBookings =
                        await api.GetBookings(sub.CourseId, sub.FromTime, sub.ToTime, sub.NumberOfPlayers);
                    var availableBookings = externalBookings?
                        .Where(eb =>
                            IsPlayableNotFullAndWithinTimeInterval(eb, sub.FromTime, sub.ToTime, sub.NumberOfPlayers))
                        .Select(Map)
                        .ToList();

                    foreach (var aengbotBooking in availableBookings!)
                    {
                        if (!await notificationRepository.WasNotified(aengbotBooking.CourseId, aengbotBooking.Date,
                                sub.Email))
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
                    sent.Add(email);
                    foreach (var booking in notificationBookings)
                    {
                        await notificationRepository.AddNotified(booking.CourseId, booking.Date, email);
                    }
                }
            }
        }

        return sent;
    }

    private bool IsPlayableNotFullAndWithinTimeInterval(Booking booking, DateTime fromTime, DateTime toTime,
        int numberOfPlayers)
    {
        return booking.category.name != "Stängd" && booking.category.custom_name != "Tävling" &&
               booking.from.AddSeconds(1) >= fromTime && booking.from.AddSeconds(-1) <= toTime &&
               booking.available_slots >= numberOfPlayers;
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