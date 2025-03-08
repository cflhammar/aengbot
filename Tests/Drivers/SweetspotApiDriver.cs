using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Sweetspot.External.Api;
using Sweetspot.External.Api.Models;
using Tests.Steps;

namespace Tests.Drivers;

public class SweeetSpotApiDriver
{
    private readonly SweetspotApiMock _serviceMock = new();

    private class SweetspotApiMock : ISweetSpotApi
    {
        public List<Booking>? Bookings = new();

        // public Task<VolvoIdResult> GetVolvoId(string userName, CancellationToken ct = default)
        // {
        //     ExistingVolvoIds.TryGetValue(userName, out var volvoId);
        //     var response = new VolvoIdResult
        //     {
        //         VolvoId = volvoId
        //     };
        //     return Task.FromResult(response);
        // }

        public Task<List<Booking>?> GetBookings(string courseId, DateTime fromTime, DateTime toTime,
            int numberOfPlayers)
        {
            return Task.FromResult(Bookings);
        }
    }

    // public string BaseUrl => "https://mockthislater/user-account-api/";
    // public string GetTokenUrl => "https://mockthislater/as/token.oauth2";
    // public string ClientId => "ajdmbw4_20";
    // public string ClientSecret => "SuperSecretSecret";

    public void AddMockSweetspotApi(IServiceCollection services)
    {
        services.RemoveAll(typeof(ISweetSpotApi));
        services.AddSingleton<ISweetSpotApi>(_serviceMock);
    }

    public void GivenSweetspotHasBookings(List<SweetspotApiSteps.StepBooking> bookings)
    {
        foreach (var booking in bookings)
        {
            _serviceMock.Bookings?.Add(new Booking
            {
                course = new Course
                {
                    uuid = booking.CourseId,
                },
                category = new Category
                {
                    name = booking.Status ?? "",
                    custom_name = booking.Event ?? ""
                },
                from = booking.FromTime,
                to = booking.ToTime,
                available_slots = booking.AvailableSlots
            });
        }
    }
}