// See https://aka.ms/new-console-template for more information
namespace Sweetspot;

static class Program
{
    
    static async Task Main(string[] args)
    {
        Dictionary<string, (string date, (int from, int to) interval)> subscriptions = new();
        //subscriptions.Add("6ec41746-b529-46bc-ac81-514095105d54",("2023-04-18",(6,18))); ängen
        subscriptions.Add("410fdd67-a108-4b3f-8058-1ff66fc061c2",("2023-05-18",(6,19)));


        foreach (var subscription in subscriptions)
        {
            var courseUid = subscription.Key;
            var date = subscription.Value.date;
            var interval = subscription.Value.interval;
       //     var availableTimes = await TimeFinder.GetAvailibleTimesAtCourseAndDate(courseUid, date, interval);
            var availableTimes = await TimeFinder.GetAvailibleTimesAtCourseAndTime(courseUid, date);
            if (availableTimes.Count > 0)
            {
                var mailService = new EmailService();
//                await mailService.SendAsync("cflhammar@gmail.com", availableTimes);
                await mailService.SendAsync("d.millner@gmail.com", availableTimes);
                Console.WriteLine("notification sent!");
            }
            else
            {
//                Console.WriteLine($"found nothing at {date} between {interval.from} - {interval.to} ({courseUid})");
                Console.WriteLine($"found nothing at {date} - 10:30 ({courseUid})");

            }
        }
    }
}