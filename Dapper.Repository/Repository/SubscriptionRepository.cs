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
                    INSERT INTO Subscriptions (CourseId, Date, FromTime, ToTime, NumberPlayers, Email)
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