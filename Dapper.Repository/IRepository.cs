using Aengbot.Models;

namespace Repository;

public interface IRepository
{
    List<Course>? GetCourses(CancellationToken ct);
    bool? AddSubscription(Subscription subscription, CancellationToken ct);
}