using Aengbot.Models;
using Aengbot.Repositories;
using Dapper;

namespace Repository.Repository;

public record CourseDataModel(
    string Id,
    string Name);

public class CourseRepository(ISqlConnectionFactory sqlConnectionFactory) : ICourseRepository
{
    public async Task<List<Course>> GetCourses(CancellationToken ct)
    {
        using var conn = sqlConnectionFactory.Create();
        var courses = await conn.QueryAsync<CourseDataModel?>(
            sql: $"""
                  SELECT Id, Name FROM Courses
                  """);

        return courses.Select(Map!).ToList();
    }

    public async Task<string> GetCourseName(string courseId)
    {
        using var conn = sqlConnectionFactory.Create();
        var courseName = await conn.QueryFirstAsync<string>(
            sql: $"""
                  SELECT Name FROM Courses
                  WHERE Id = @Id
                  """,
            param: new
            {
                Id = courseId
            });
        return courseName;
    }

    private static Course Map(CourseDataModel course)
    {
        return new Course()
        {
            Id = course.Id,
            Name = course.Name
        };
    }
}