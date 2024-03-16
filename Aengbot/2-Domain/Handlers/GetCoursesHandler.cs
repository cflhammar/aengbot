namespace Aengbot._2_Domain.Handlers;

public record GetCoursesResult(Dictionary<string,string>? Courses);

public interface IGetCoursesHandler
{
    GetCoursesResult? Handle(CancellationToken ct);
}

public class GetCoursesHandler(CoursesProvider coursesProvider) : IGetCoursesHandler
{
    public GetCoursesResult? Handle(CancellationToken ct)
    {
        return new GetCoursesResult(coursesProvider.Courses);
    }
}
