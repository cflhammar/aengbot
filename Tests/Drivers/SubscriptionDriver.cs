using System.Text;
using System.Text.Json;
using Tests.Steps;

namespace Tests.Drivers;

public class SubscriptionDriver(HttpClientDriver httpClient)
{
    public async Task AddSubscription(SubscriptionSteps.AddSubscriptionStep addSubscriptionStep)
    {
        var request = new SubscribeRequest(
            addSubscriptionStep.Email,
            addSubscriptionStep.CourseId,
            addSubscriptionStep.FromTime,
            addSubscriptionStep.ToTime,
            addSubscriptionStep.NumberOfPlayers
        );

        var requestJson = JsonSerializer.Serialize(request,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "aengbot/subscribe")
        {
            Content = new StringContent(requestJson, Encoding.UTF8, "application/json")
        };
        
        await httpClient.WhenARequestIsMade(httpRequest);
    }

    private record SubscribeRequest(
        string Email,
        string CourseId,
        DateTime FromTime,
        DateTime ToTime,
        int NumberOfPlayers);
}