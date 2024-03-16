namespace Aengbot.Handlers;

public record AddSubscriptionCommand(
    string CourseId,
    string Date,
    string FromTime,
    string ToTime,
    int NumberOfPlayers,
    string Email);

public class AddSubscriptionHandler
{
    public bool Handle(AddSubscriptionCommand command, CancellationToken ct)
    {
        Console.WriteLine("Adding subscription...");
        return true;
    }
}