using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Reqnroll;

namespace Tests.Hooks;

[Binding]
public class BeforeTestRunHooks
{
    private static IContainer _container;


    [BeforeTestRun(Order = 1)]
    public static async Task RunTestContainer()
    {
        _container = new ContainerBuilder()
            .WithImage("mcr.microsoft.com/azure-sql-edge:1.0.7")
            .WithPortBinding(5551, 1433)
            .WithEnvironment("SA_PASSWORD", "TestPassword123")
            .WithEnvironment("ACCEPT_EULA", "Y")
            .Build();
        
        await _container.StartAsync();
    }
    
    // [BeforeTestRun(Order = 2)]
    // public static void MountTestAppsettings()
    // {
//     _webAppFactory = new WebApplicationFactory<Program>()
//     .WithWebHostBuilder(builder =>
//     {
//         builder.ConfigureAppConfiguration((context, config) =>
//         {
//             config.AddJsonFile("appsettings.Test.json");
//         });
//     });
// Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
    // }
    

    [AfterTestRun]
    public static async Task DisposeAsync()
    {
        if (_container != null)
        {
            await _container.DisposeAsync();
        }
        // _webAppFactory?.Dispose();
    }
}