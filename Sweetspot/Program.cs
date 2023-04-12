// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using Sweetspot;

Console.WriteLine("Hello, World!");

HttpClient client = new HttpClient();

var respone = await client.GetAsync("https://platform.sweetspot.io/api/tee-times?course.uuid=6ec41746-b529-46bc-ac81-514095105d54&from%5Bafter%5D=2023-04-15T00%3A00%3A00%2B02%3A00&from%5Bbefore%5D=2023-04-15T23%3A59%3A59%2B02%3A00&limit=100&order%5Bfrom%5D=asc&page=1");
var json = respone.Content.ReadAsStringAsync().Result;
//var json = File.ReadAllText("../../../json.txt");

List<Booking>? bookings = JsonSerializer.Deserialize<List<Booking>>(json);

bookings?.ForEach(b =>
{
    if (b.available_slots == 4 && 
        b.category.name != "Stängd" &&
        b.from.Hour < 17)
    {
        Console.WriteLine(b.from.Date + " > " +  b.@from.Hour + ":" + b.@from.Minute);
    }
});
