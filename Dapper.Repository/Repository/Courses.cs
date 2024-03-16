using Aengbot.Models;

namespace Repository.Repository;

public interface ICourseRepository
{
    List<Course> GetCourses(CancellationToken ct);
}

public class CourseRepository : ICourseRepository
{
    public List<Course> GetCourses(CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}

