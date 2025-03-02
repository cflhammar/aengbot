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

    public async Task<Course?> Get(string courseId)
    {
        using var conn = sqlConnectionFactory.Create();
        var course = await conn.QueryFirstOrDefaultAsync<CourseDataModel>(
            sql: $"""
                  SELECT Id, Name FROM Courses
                  WHERE Id = @Id
                  """,
            param: new
            {
                Id = courseId 
            });
        return course != null ? Map(course) : null;
    }

    public async Task<bool> AddCourse(Course course)
    {
        using var conn = sqlConnectionFactory.Create();
        var affectedRows = await conn.ExecuteAsync(
            sql: $"""
                    INSERT INTO Courses (Id, Name) VALUES (@Id, @Name)
                  """,
            param: new
            {
                course.Id, course.Name
            });
        return affectedRows == 1;
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