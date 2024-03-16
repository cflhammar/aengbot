using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Repository;
public class SqlConnectionFactory : ISqlConnectionFactory
{
    public string ConnectionString { get; }

    public SqlConnectionFactory(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("AengbotDb")
                               ?? throw new NullReferenceException("AppSetting QueryDb:ConnectionString was not provided.");
        ConnectionString = connectionString;
    }

    public IDbConnection Create()
        => new NpgsqlConnection(ConnectionString);
}

public interface ISqlConnectionFactory
{
    IDbConnection Create();
}
