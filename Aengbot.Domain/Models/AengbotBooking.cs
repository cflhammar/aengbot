namespace Aengbot.Models;

public class AengbotBooking
{
    public string CourseId { get; set; }
    public DateTime Date { get; set; }
    public int AvailableSlots { get; set; }
}