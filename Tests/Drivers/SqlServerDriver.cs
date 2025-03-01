using DotNet.Testcontainers.Builders;
using Testcontainers.MsSql;

namespace Tests.Drivers;

/// <summary>
/// Fixture for interacting with the MS SQL server container instance.
/// Note: There is one instance per test run.
/// </summary>
public static class SqlServerDriver
{
    private static MsSqlContainer? _dbContainer;

    public static async Task InitializeAsync()
    {
        _dbContainer = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2019-latest")
            .WithPortBinding(5551, 1433)
            .WithEnvironment("SA_PASSWORD", "TestPassword123")
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithPassword("TestPassword123")
            .WithName($"SQL-{Guid.NewGuid():N}")
            .Build();
        
        await _dbContainer.StartAsync();
    }

    public static async Task DisposeAsync()
    {
        await _dbContainer?.StopAsync()!;
    }
}