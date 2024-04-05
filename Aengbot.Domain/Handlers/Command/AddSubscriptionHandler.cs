using Aengbot.Models;
using Aengbot.Repositories;

namespace Aengbot.Handlers.Command;

public record AddSubscriptionCommand(
    string CourseId,
    DateTime FromTime,
    DateTime ToTime,
    int NumberOfPlayers,
    string Email);

public interface IAddSubscriptionHandler : ICommandHandler
{
    bool Handle(AddSubscriptionCommand command, CancellationToken ct);
}

public class AddSubscriptionHandler(ISubscriptionRepository repository) : IAddSubscriptionHandler
{
    public bool Handle(AddSubscriptionCommand command, CancellationToken ct)
    {
        var subscription = new Subscription()
        {
            CourseId = command.CourseId,
            FromTime = command.FromTime,
            ToTime = command.ToTime,
            NumberOfPlayers = command.NumberOfPlayers,
            Email = command.Email
        };
        
        repository.AddSubscription(subscription, ct);

        return true;
    }
}