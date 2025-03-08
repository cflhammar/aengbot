using Aengbot.Models;

namespace Aengbot.Repositories;

public interface ISubscriptionRepository
{
    Task AddSubscription(Subscription subscription, CancellationToken ct);
    Task<List<Subscription>> GetAll(DateTime activeAt, CancellationToken ct);
    Task<List<Subscription>?> Get(string email, CancellationToken ct);
}