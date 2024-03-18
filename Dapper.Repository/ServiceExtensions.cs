using Aengbot.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Repository;

namespace Repository;

public static class ServiceExtensions
{
    public static void AddDapperServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
        services.AddScoped<IDbMigrationRunner, DbMigrationRunner>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        // services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddHealthChecks()
            .AddSqlServer(new SqlConnectionFactory(configuration).ConnectionString,
                tags: new[] { "StartupHealthCheck" }, timeout: TimeSpan.FromSeconds(3));
    }
}
