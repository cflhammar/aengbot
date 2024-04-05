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
                    INSERT INTO Subscriptions (CourseId, FromTime, ToTime, NumberPlayers, Email)
                     VALUES (@CourseId, @FromTime, @ToTime, @NumberOfPlayers, @Email);
                 """,
                param: new
                {
                    subscription.CourseId,
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

    public async Task<List<Subscription>> GetSubscriptions(CancellationToken ct)
    {
        try
        {
            using var conn = sqlConnectionFactory.Create();
            var subs = await conn.QueryAsync<SubscriptionDataModel?>( 
                sql:
                $"""
                    SELECT CourseId, FromTime, ToTime, NumberPlayers, Email
                    FROM Subscriptions
                    WHERE ToTime >= @ToTime
                 """,
                param: new
                {
                    ToTime = DateTime.Now
                }
            );
            
            return subs.Select(Map!).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private Subscription Map(SubscriptionDataModel dataModel)
    {
        return new Subscription()
        {
            CourseId = dataModel.CourseId,
            FromTime = dataModel.FromTime,
            ToTime = dataModel.ToTime,
            NumberOfPlayers = dataModel.NumberPlayers,
            Email = dataModel.Email
        };
    }

    public record SubscriptionDataModel(
        string CourseId,
        DateTime FromTime,
        DateTime ToTime,
        Int32 NumberPlayers,
        string Email
    );
}