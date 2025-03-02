using Reqnroll;
using Tests.Drivers;

namespace Tests.Steps;

[Binding]
public class SubscriptionSteps(SubscriptionDriver subscriptionDriver, ApiClientDriver apiClientDriver)
{
    [Given(@"a user wants to subscribe to")]
    public void GivenAUserWantsToSubscribeTo(Table table)
    {
        var subscription = table.CreateInstance<SubscriptionStep>();
        subscriptionDriver.GivenAUserWantsToSubscribeTo(subscription);
    }

    [Given(@"a user subscribed to")]
    public async Task GivenAUserSubscribedTo(Table table)
    {
        var subscriptions = table.CreateSet<SubscriptionStep>();
        foreach (var subscription in subscriptions)
        {
            subscriptionDriver.GivenAUserWantsToSubscribeTo(subscription);
            await apiClientDriver.WhenARequestIsMade();
            await apiClientDriver.ThenRequestIsSuccessful();
        }
    }

    [Given(@"a user with email (.*) wants to get subscriptions")]
    public void GivenAUserWithEmailWantsToGetSubscriptions(string email)
    {
        subscriptionDriver.GivenAUserWithEmailWantsToGetSubscriptions(email);
    }

    [Then(@"the subscription are returned")]
    public async Task ThenTheSubscriptionAreReturned(Table table)
    {
        var subscriptions = table.CreateSet<SubscriptionStep>().ToList();
        await subscriptionDriver.ThenTheSubscriptionAreReturned(subscriptions);
    }

    public record SubscriptionStep(
        string CourseId,
        DateTime FromTime,
        DateTime ToTime,
        int NumberOfPlayers,
        string Email);
}