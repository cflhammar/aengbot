using Aengbot.Models;

namespace Repository.Repository;


public interface ISubscriptionRepository
{
    bool? AddSubscription(Subscription subscription, CancellationToken ct);
}


public class SubscriptionRepository : ISubscriptionRepository
{
    public bool? AddSubscription(Subscription subscription, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}

