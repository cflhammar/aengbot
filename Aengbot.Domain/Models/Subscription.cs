namespace Aengbot.Models;

public record Subscription
{
    public string CourseId { get; init; } = string.Empty;
    public string Date { get; init; } = string.Empty;
    public string FromTime { get; init; } = string.Empty;
    public string ToTime { get; init; } = string.Empty;
    public int NumberOfPlayers { get; init; }
    public string Email { get; init; } = string.Empty;
}