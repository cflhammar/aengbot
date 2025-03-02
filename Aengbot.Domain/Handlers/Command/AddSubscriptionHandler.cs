using System.Text.RegularExpressions;
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
    Task<List<string>> Handle(AddSubscriptionCommand command, CancellationToken ct);
}

public class AddSubscriptionHandler(ISubscriptionRepository repository, ICourseRepository courseRepository) : IAddSubscriptionHandler
{
    public async Task<List<string>> Handle(AddSubscriptionCommand command, CancellationToken ct)
    {
        var errors = await Validate(command, ct);
        if (errors.Count != 0) return errors;
        
        var subscription = new Subscription()
        {
            CourseId = command.CourseId,
            FromTime = command.FromTime,
            ToTime = command.ToTime,
            NumberOfPlayers = command.NumberOfPlayers,
            Email = command.Email
        };
        
        await repository.AddSubscription(subscription, ct);

        return [];
    }

    private async Task<List<string>> Validate(AddSubscriptionCommand command, CancellationToken ct)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(command.CourseId))
            errors.Add("CourseId is required");

        if (command.FromTime == default)
            errors.Add("FromTime is required");

        if (command.ToTime == default)
            errors.Add("ToTime is required");

        if (command.NumberOfPlayers is <= 0 or > 5)
            errors.Add("NumberOfPlayers must be greater than 0 and less than 5");

        if (string.IsNullOrWhiteSpace(command.Email))
            errors.Add("Email is required");
        
        var emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        if (!emailRegex.IsMatch(command.Email))
            errors.Add("Email is not in valid format");
        
        var course = await courseRepository.Get(command.CourseId);
        if (course == null)
            errors.Add("Course does not exist");

        return errors;
    }
}