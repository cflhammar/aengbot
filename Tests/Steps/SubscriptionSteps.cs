using Reqnroll;
using Tests.Drivers;

namespace Tests.Steps;

[Binding]
public class SubscriptionSteps(SubscriptionDriver subscriptionDriver)
{  
    [Given(@"a user subscribed to")]
    public async Task GivenAUserSubscribedTo(Table table)
    {
        var addSubscriptionStep = table.CreateInstance<AddSubscriptionStep>();
        await subscriptionDriver.AddSubscription(addSubscriptionStep);
    }

    public record AddSubscriptionStep(
        string CourseId,
        DateTime FromTime,
        DateTime ToTime,
        int NumberOfPlayers,
        string Email);
}