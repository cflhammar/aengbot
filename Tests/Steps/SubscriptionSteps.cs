using Reqnroll;
using Tests.Drivers;

namespace Tests.Steps;

[Binding]
public class SubscriptionSteps(SubscriptionDriver subscriptionDriver)
{
    [Given(@"a user subscribed to")]
    public async Task GivenAUserSubscribedTo(Table table)
    {
        var subscriptions = table.CreateSet<SubscriptionStep>();
        foreach (var subscription in subscriptions)
        {
            await subscriptionDriver.AddSubscription(subscription);    
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