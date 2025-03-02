using System.Text;
using System.Text.Json;
using FluentAssertions;
using Tests.Steps;

namespace Tests.Drivers;

public class SubscriptionDriver(ApiClientDriver httpClient)
{
    public async Task AddSubscription(SubscriptionSteps.SubscriptionStep subscriptionStep)
    {
        var request = new SubscribeRequest(
            subscriptionStep.Email,
            subscriptionStep.CourseId,
            subscriptionStep.FromTime,
            subscriptionStep.ToTime,
            subscriptionStep.NumberOfPlayers
        );

        var requestJson = JsonSerializer.Serialize(request,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "aengbot/subscriptions/add")
        {
            Content = new StringContent(requestJson, Encoding.UTF8, "application/json")
        };

        httpClient.RequestMessage = httpRequest;
        await httpClient.WhenARequestIsMade();
        await httpClient.ThenRequestIsSuccessful();
    }

    public void GivenAUserWithEmailWantsToGetSubscriptions(string email)
    {
        httpClient.RequestMessage = new HttpRequestMessage(HttpMethod.Get, $"aengbot/subscriptions/{email}");
    }

    public async Task ThenTheSubscriptionAreReturned(List<SubscriptionSteps.SubscriptionStep> expected)
    {
        var response = await httpClient.GetResponseContent<SubscriptionResult>();
        response.subscriptions.Select(subscription =>
                new SubscriptionSteps.SubscriptionStep(
                    Email: subscription.Email, 
                    CourseId: subscription.CourseId,
                    FromTime: subscription.FromTime, 
                    ToTime: subscription.ToTime,
                    NumberOfPlayers: subscription.NumberOfPlayers))
            .ToList()
            .Should().BeEquivalentTo(expected);
    }

    private record SubscriptionResult(List<SubscribeRequest> subscriptions);

    private record SubscribeRequest(
        string Email,
        string CourseId,
        DateTime FromTime,
        DateTime ToTime,
        int NumberOfPlayers);
}