using System.Text.Json;
using System.Xml.Schema;

namespace Sweetspot;

public class TimeFinder
{
    
    public static async Task<List<(int,int)>> GetAvailibleTimesAtCourseAndDate(string courseUid, string date, (int from,int to) hourInterval)
    {
        HttpClient client = new HttpClient();
        var url =
            $"https://platform.sweetspot.io/api/tee-times?course.uuid={courseUid}&from%5Bafter%5D={date}T00%3A00%3A00%2B02%3A00&from%5Bbefore%5D={date}T23%3A59%3A59%2B02%3A00&limit=100&order%5Bfrom%5D=asc&page=1";
        var response = await client.GetAsync(url);
        var json = response.Content.ReadAsStringAsync().Result;
        List<Booking>? bookings = JsonSerializer.Deserialize<List<Booking>>(json);
        
        var availbileTimes = new List<(int h, int m)>();
        bookings?.ForEach(async b =>
        {
            var teeTimeHour = b.from.Hour;
            var teeTimeMinute = b.from.Minute;

            if (b.available_slots == 4 &&
                b.category.name != "Stängd" &&
                teeTimeHour > hourInterval.from &&
                teeTimeHour < hourInterval.to)
            {
                availbileTimes.Add((teeTimeHour, teeTimeMinute));
            }
        });

        return availbileTimes;
    }
    
    public static async Task<List<(int,int)>> GetAvailibleTimesAtCourseAndTime(string courseUid, string date)
    {
        HttpClient client = new HttpClient();
        string url =
            $"https://platform.sweetspot.io/api/tee-times?course.uuid={courseUid}&from%5Bafter%5D={date}T00%3A00%3A00%2B02%3A00&from%5Bbefore%5D={date}T23%3A59%3A59%2B02%3A00&limit=100&order%5Bfrom%5D=asc&page=1";
        var response = await client.GetAsync(url);
        var json = response.Content.ReadAsStringAsync().Result;
        List<Booking>? bookings = JsonSerializer.Deserialize<List<Booking>>(json);
        
        var availbileTimes = new List<(int h, int m)>();
        bookings?.ForEach(async b =>
        {
            var teeTimeHour = b.from.Hour;
            var teeTimeMinute = b.from.Minute;
            
            Console.WriteLine(teeTimeHour + "-" + teeTimeMinute + " : " + b.available_slots);

            if (b.available_slots == 1 &&
                b.category.name != "Stängd" &&
                teeTimeHour == 12 &&
                teeTimeMinute == 10 )
            {
                availbileTimes.Add((teeTimeHour, teeTimeMinute));
            }
        });

        return availbileTimes;
    }
}