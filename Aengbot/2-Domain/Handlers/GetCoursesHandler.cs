namespace Aengbot._2_Domain.Handlers;

public record GetCoursesResult(Dictionary<string,string>? Courses);

public class GetCoursesHandler(CoursesProvider coursesProvider)
{
    public GetCoursesResult Handle()
    {
        return new GetCoursesResult(coursesProvider.Courses);
    }
}
