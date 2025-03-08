using Aengbot.Models;
using Aengbot.Repositories;
using Aengbot.Services;

namespace Aengbot.Handlers.Query;

public record GetSubscriptionsResult(List<Subscription>? Subscriptions);

public interface IGetSubscriptionsHandler : IQueryHandler
{
    Task<GetSubscriptionsResult> Handle(CancellationToken ct);
    Task<GetSubscriptionsResult> Handle(string email, CancellationToken ct);
}

public class GetSubscriptionsHandler(ISubscriptionRepository repository, IDateService dateService) : IGetSubscriptionsHandler
{
    public async Task<GetSubscriptionsResult> Handle(CancellationToken ct)
    {
        var now = dateService.UtcNow;
        var subs = await repository.GetAll(activeAt: now ,ct);
        return new GetSubscriptionsResult(subs);
    }
    
    public async Task<GetSubscriptionsResult> Handle(string email, CancellationToken ct)
    {
        var subs = await repository.Get(email, ct);
        return new GetSubscriptionsResult(subs);
    }
}
