using Aengbot.Models;

namespace Aengbot.Repositories;

public interface ISubscriptionRepository
{
    bool? AddSubscription(Subscription subscription, CancellationToken ct);
}