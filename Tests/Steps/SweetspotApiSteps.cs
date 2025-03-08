using Reqnroll;
using Tests.Drivers;

namespace Tests.Steps;

[Binding]
public class SweetspotApiSteps(SweeetSpotApiDriver sweetSpotApiDriver)
{
    // only supports one course and one date
    [Given(@"sweetspot has bookings")]
    public void GivenSweetspotHasBookings(Table table)
    {
        var bookings = table.CreateSet<StepBooking>();
        
        sweetSpotApiDriver.GivenSweetspotHasBookings(bookings.ToList());
    }
    
    public record StepBooking(
        string CourseId,
        DateTime FromTime,
        DateTime ToTime,
        int AvailableSlots,
        string? Status, 
        string? Event);
}