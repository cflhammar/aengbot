namespace Aengbot.Models;

public record Subscription
{
    string CourseId { get; init; } = string.Empty;
    string Date { get; init; } = string.Empty;
    string FromTime { get; init; } = string.Empty;
    string ToTime { get; init; } = string.Empty;
    int NumberOfPlayers { get; init; }
    string Email { get; init; } = string.Empty;
}