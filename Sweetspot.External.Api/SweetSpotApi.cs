using System.Text.Json;
using Sweetspot.External.Api.Models;

namespace Sweetspot.External.Api;

public interface ISweetSpotApi
{
    Task<List<Booking>?> GetBookings(string courseId, DateTime fromTime, DateTime toTime, int numberOfPlayers);
}

public class SweetSpotApi(HttpClient client) : ISweetSpotApi
{
    public async Task<List<Booking>?> GetBookings(string courseId, DateTime fromTime, DateTime toTime,
        int numberOfPlayers)
    {
        var date = fromTime.Date.ToString("yyyy-MM-dd");
        var url =
            $"https://platform.sweetspot.io/api/tee-times?course.uuid={courseId}&from%5Bafter%5D={date}T00%3A00%3A00%2B02%3A00&from%5Bbefore%5D={date}T23%3A59%3A59%2B02%3A00&limit=100&order%5Bfrom%5D=asc&page=1";
        var response = await client.GetAsync(url);
        var json = response.Content.ReadAsStringAsync().Result;
        List<Booking>? bookings = JsonSerializer.Deserialize<List<Booking>>(json);
        

        var playableBookings = bookings
            ?.Where(b => IsPlayableNotFullAndWithinTimeInterval(b, fromTime, toTime, numberOfPlayers)).ToList();
        return playableBookings;
    }

    private bool IsPlayableNotFullAndWithinTimeInterval(Booking booking, DateTime fromTime, DateTime toTime,
        int numberOfPlayers)
    {
        return booking.category.name != "Stängd" &&
               booking.from.AddSeconds(1) >= fromTime && booking.from.AddSeconds(-1) <= toTime &&
               booking.available_slots >= numberOfPlayers;
    }
}