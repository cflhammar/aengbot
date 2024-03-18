using Aengbot.Models;
using Aengbot.Repositories;
using Dapper;

namespace Repository.Repository;

public class SubscriptionRepository(ISqlConnectionFactory sqlConnectionFactory) : ISubscriptionRepository
{
    public async Task AddSubscription(Subscription subscription, CancellationToken ct)
    {
        try
        {
            using var conn = sqlConnectionFactory.Create();
            await conn.ExecuteAsync(
                sql:
                $"""
                    INSERT INTO aengbot.subscriptions (course_id, date, from_time, to_time, number_players, email)
                     VALUES (@CourseId, @Date, @FromTime, @ToTime, @NumberOfPlayers, @Email);
                 """,
                param: new
                {
                    subscription.CourseId,
                    subscription.Date,
                    subscription.FromTime,
                    subscription.ToTime,
                    subscription.NumberOfPlayers,
                    subscription.Email
                }
            );
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}