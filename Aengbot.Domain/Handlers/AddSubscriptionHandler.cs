namespace Aengbot.Handlers;

public record AddSubscriptionCommand(string CourseId);

public class AddSubscriptionHandler
{
    public bool Handle(AddSubscriptionCommand command, CancellationToken ct)
    {
        Console.WriteLine("Adding subscription...");
        return true;
    }
}