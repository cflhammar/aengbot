using Aengbot.Repositories;
using Dapper;

namespace Repository.Repository;

public class NotificationRepository(ISqlConnectionFactory sqlConnectionFactory) : INotificationRepository
{
    public async Task AddNotified(string courseId, DateTime teeTime, string email)
    {
        try
        {
            using var conn = sqlConnectionFactory.Create();
            await conn.ExecuteAsync(
                sql:
                $"""
                    INSERT INTO Notified (CourseId, TeeTime, Email)
                     VALUES (@CourseId, @TeeTime, @Email);
                 """,
                param: new
                {
                    CourseId = courseId,
                    TeeTime = teeTime,
                    Email = email
                }
            );
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> WasNotified(string courseId, DateTime teeTime, string email)
    {
        try
        {
            using var conn = sqlConnectionFactory.Create();
            var check = await conn.QuerySingleAsync<int>( 
                sql:
                $"""
                    SELECT COUNT(1)
                    FROM Notified
                    WHERE CourseId = @CourseId AND TeeTime = @Teetime AND Email = @Email
                 """,
                param: new
                {
                    CourseId = courseId,
                    TeeTime = teeTime,
                    Email = email
                }
            );

            return check > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}