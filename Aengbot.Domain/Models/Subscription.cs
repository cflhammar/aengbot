namespace Aengbot.Models;

public record Subscription
{
    internal string CourseId { get; init; } = string.Empty;
    internal string Date { get; init; } = string.Empty;
    internal string FromTime { get; init; } = string.Empty;
    internal string ToTime { get; init; } = string.Empty;
    internal int NumberOfPlayers { get; init; }
    internal string Email { get; init; } = string.Empty;
}