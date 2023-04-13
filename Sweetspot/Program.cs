﻿// See https://aka.ms/new-console-template for more information

using System;
using System.Text.Json;
using Sweetspot;


var d = "2023-04-15";
var f = 8;
var t = 17;
Console.WriteLine("Letar efter starttid " + d + " > "  + f + "-" + t);


    HttpClient client = new HttpClient();
    var respone = await client.GetAsync($"https://platform.sweetspot.io/api/tee-times?course.uuid=6ec41746-b529-46bc-ac81-514095105d54&from%5Bafter%5D={d}T00%3A00%3A00%2B02%3A00&from%5Bbefore%5D={d}T23%3A59%3A59%2B02%3A00&limit=100&order%5Bfrom%5D=asc&page=1");
    var json = respone.Content.ReadAsStringAsync().Result;
    //var json = File.ReadAllText("../../../json.txt");

    var found = false;
    List<Booking>? bookings = JsonSerializer.Deserialize<List<Booking>>(json);

    bookings?.ForEach(b =>
    {
        Console.WriteLine(b.from + " > " + b.available_slots  + " > " + b.category.name);
        
        if (b.available_slots == 4 && 
            b.category.name != "Stängd" &&
            b.from.Hour < 17)
        {
            Console.WriteLine(b.from.Date + " > " +  b.@from.Hour + ":" + b.@from.Minute);
            var mailService = new EmailService();
            mailService.SendAsync("cflhammar@gmail.com");
            found = true;
        }
    });
    if (!found)
    {
        var current = DateTime.Now;
        Console.WriteLine("Ingen ledig tid just nu (" + current.Month + "/" + current.Day + " " + current.Hour + ":" + current.Minute + ")");
    }




