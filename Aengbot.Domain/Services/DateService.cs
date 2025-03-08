namespace Aengbot.Services;

public interface IDateService
{
    DateTime UtcNow { get; }
}

public class DateService : IDateService
{
    public DateTime UtcNow => DateTime.UtcNow;
}
