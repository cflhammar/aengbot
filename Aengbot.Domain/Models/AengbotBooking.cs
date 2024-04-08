namespace Aengbot.Models;

public class AengbotBooking
{
    public string CourseName { get; set; } = string.Empty;
    public string CourseId { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int AvailableSlots { get; set; }
}