using Aengbot.Models;
using Aengbot.Repositories;

namespace Aengbot.Handlers.Query;

public record GetCoursesResult(List<Course>? Courses);

public interface IGetCoursesHandler : IQueryHandler
{
    Task<GetCoursesResult> Handle(CancellationToken ct);
}

public class GetCoursesHandler(ICourseRepository repository) : IGetCoursesHandler
{
    public async Task<GetCoursesResult> Handle(CancellationToken ct)
    {
        var courses = await repository.GetCourses(ct);
        return new GetCoursesResult(courses);
    }
}
