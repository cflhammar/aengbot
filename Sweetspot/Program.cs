// See https://aka.ms/new-console-template for more information

using System;
using System.Text.Json;
using Sweetspot;

class Program
{
    

static async Task Main(string[] args)
{
    var d = "2023-04-15";
    var f = 8;
    var t = 17;
    Console.WriteLine("Letar efter starttid " + d + " > " + f + "-" + t);


    HttpClient client = new HttpClient();
    var respone = await client.GetAsync(
        $"https://platform.sweetspot.io/api/tee-times?course.uuid=6ec41746-b529-46bc-ac81-514095105d54&from%5Bafter%5D={d}T00%3A00%3A00%2B02%3A00&from%5Bbefore%5D={d}T23%3A59%3A59%2B02%3A00&limit=100&order%5Bfrom%5D=asc&page=1");
    var json = respone.Content.ReadAsStringAsync().Result;
    //var json = File.ReadAllText("../../../json.txt");

    var found = false;
    List<Booking>? bookings = JsonSerializer.Deserialize<List<Booking>>(json);

    bookings?.ForEach(async b =>
    {
        var t = b.from.Hour + 2;
        Console.WriteLine(b.from.Day + " .." + t + ":" + b.from.Minute  + " > " + b.available_slots + " > " + b.category.name);

        if (b.available_slots == 1 &&
            b.category.name != "Stängd" &&
            t < 17)
        {
            Console.WriteLine(b.from.Day + " > " + t + ":" + b.from.Minute);
            var mailService = new EmailService();
            await mailService.SendAsync("cflhammar@gmail.com");
            found = true;
        }
    });
    if (!found)
    {
        var current = DateTime.Now;
        Console.WriteLine("Ingen ledig tid just nu (" + current.Month + "/" + current.Day + " " + current.Hour + ":" +
                          current.Minute + ")");
    }
}



}
