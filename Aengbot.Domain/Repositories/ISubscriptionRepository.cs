using Aengbot.Models;

namespace Aengbot.Repositories;

public interface ISubscriptionRepository
{
    Task AddSubscription(Subscription subscription, CancellationToken ct);
    Task<List<Subscription>> GetSubscriptions(CancellationToken ct);
}