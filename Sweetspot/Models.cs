namespace Sweetspot;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Category
{
    public string name { get; set; }
    public string description { get; set; }
    public string color { get; set; }
    public bool is_use_same_name { get; set; }
    public string custom_name { get; set; }
    public string custom_description { get; set; }
    public string display { get; set; }
    public string uuid { get; set; }
    public int id { get; set; }
}

public class Course
{
    public string uuid { get; set; }
    public int id { get; set; }
}

public class Booking
{
    public Course course { get; set; }
    public DateTime from { get; set; }
    public DateTime to { get; set; }
    public int available_slots { get; set; }
    public int max_slots { get; set; }
    public object price { get; set; }
    public object notes { get; set; }
    public bool is_prime_time { get; set; }
    public Category category { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
    public string uuid { get; set; }
    public int id { get; set; }
}
