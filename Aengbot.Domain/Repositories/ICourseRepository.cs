using Aengbot.Models;

namespace Aengbot.Repositories;

public interface ICourseRepository
{
    List<Course> GetCourses(CancellationToken ct);
}