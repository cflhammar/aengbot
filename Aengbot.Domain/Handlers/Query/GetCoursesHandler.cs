using Aengbot.Models;
using Aengbot.Repositories;

namespace Aengbot.Handlers.Query;

public record GetCoursesResult(List<Course>? Courses);

public interface IGetCoursesHandler : IQueryHandler
{
    GetCoursesResult Handle(CancellationToken ct);
}

public class GetCoursesHandler(ICourseRepository repository) : IGetCoursesHandler
{
    public GetCoursesResult Handle(CancellationToken ct)
    {
        var courses = repository.GetCourses(ct);
        return new GetCoursesResult(courses);
    }
}
