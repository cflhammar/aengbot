namespace Aengbot.Models;

public record Subscription
{
    public string CourseId { get; init; } = string.Empty;
    public DateTime FromTime { get; init; }
    public DateTime ToTime { get; init; }
    public int NumberOfPlayers { get; init; }
    public string Email { get; init; } = string.Empty;
}