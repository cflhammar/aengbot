using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Reqnroll;
using Reqnroll.BoDi;
using Tests.Drivers;

namespace Tests.Hooks;

[Binding]
public class BeforeTestRunHooks
{
    private static IContainer _container;

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        //Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
        Environment.SetEnvironmentVariable("XAPIKEY", "1234");

        Task.WhenAll(
            SqlServerDriver.InitializeAsync()
        ).GetAwaiter().GetResult();
    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
        Task.WhenAll(
            SqlServerDriver.DisposeAsync()
        ).GetAwaiter().GetResult();
    }
}