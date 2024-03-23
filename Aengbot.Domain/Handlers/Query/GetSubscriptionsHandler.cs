using Aengbot.Models;
using Aengbot.Repositories;

namespace Aengbot.Handlers.Query;

public record GetSubscriptionsResult(List<Subscription>? Courses);

public interface IGetSubscriptionsHandler : IQueryHandler
{
    Task<GetSubscriptionsResult> Handle(CancellationToken ct);
}

public class GetSubscriptionsHandler(ISubscriptionRepository repository) : IGetSubscriptionsHandler
{
    public async Task<GetSubscriptionsResult> Handle(CancellationToken ct)
    {
        var subs = await repository.GetSubscriptions(ct);
        return new GetSubscriptionsResult(subs);
    }
}