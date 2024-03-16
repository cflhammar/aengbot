using Aengbot.Models;
using Aengbot.Repositories;

namespace Aengbot.Handlers.Command;

public record AddSubscriptionCommand(
    string CourseId,
    string Date,
    string FromTime,
    string ToTime,
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
            Date = command.Date,
            FromTime = command.FromTime,
            ToTime = command.ToTime,
            NumberOfPlayers = command.NumberOfPlayers,
            Email = command.Email
        };
        
        repository.AddSubscription(subscription, ct);

        return true;
    }
}