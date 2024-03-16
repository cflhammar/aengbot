using Aengbot.Models;

namespace Aengbot.Repositories;

public interface ICourseRepository
{
    Task<List<Course>> GetCourses(CancellationToken ct);
}