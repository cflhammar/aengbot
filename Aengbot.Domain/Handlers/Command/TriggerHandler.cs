namespace Aengbot.Handlers.Command;

public interface ITriggerHandler : ICommandHandler
{
    bool Handle(CancellationToken ct);
}

public class TriggerHandler : ITriggerHandler
{
    public bool Handle(CancellationToken ct)
    {
        return true;
    }
}