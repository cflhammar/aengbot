using DbUp;
using Microsoft.Extensions.Configuration;

namespace Repository;

public interface IDbMigrationRunner
{
    void Run();
}

public class DbMigrationRunner : IDbMigrationRunner
{
    private readonly string _connectionString;

    public DbMigrationRunner(IConfiguration configuration)
    {
        _connectionString = new SqlConnectionFactory(configuration).ConnectionString;
    }

    public void Run()
    {
        EnsureDatabase.For.PostgresqlDatabase(_connectionString);

        var upgradeEngine =
            DeployChanges.To
                .PostgresqlDatabase(_connectionString)
                .WithScriptsAndCodeEmbeddedInAssembly(typeof(DbMigrationRunner).Assembly)
                .LogToConsole()
                .LogScriptOutput()
                .Build();

#if DEBUG
        var scripts = upgradeEngine.GetScriptsToExecute();
#endif
        var result = upgradeEngine.PerformUpgrade();
        if (!result.Successful)
        {
            throw new Exception(result.Error.ToString());
        }
    }

}
