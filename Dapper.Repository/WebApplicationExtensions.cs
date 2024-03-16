using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Repository;

public static class WebApplicationExtensions
{
    public static IHost RunDbMigrations(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var dbMigrationRunner = scope.ServiceProvider.GetRequiredService<IDbMigrationRunner>();
        dbMigrationRunner.Run();
        return app;
    }
}
