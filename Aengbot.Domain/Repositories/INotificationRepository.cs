namespace Aengbot.Repositories;

public interface INotificationRepository
{
    Task AddNotified(string courseId, DateTime date, string email);
    Task<bool> WasNotified(string courseId, DateTime teeTime, string email);
}