using Reqnroll;
using Reqnroll.BoDi;

namespace Tests.Hooks;

[Binding]
public class BeforeScenarioHooks
{
    [BeforeScenario]
    public static Task BeforeScenario()
    {
        // take 50 first chars of string
        var cleanScenarioName = TestContext.CurrentContext.Test.Name.Replace(" ", "_");
        string dbName;
        dbName = cleanScenarioName.Length > 50 ? cleanScenarioName.Substring(0, 50) : cleanScenarioName;

        Environment.SetEnvironmentVariable("ConnectionStrings:AengbotDb",
            $"Server=localhost,5551;Database={dbName};User=sa;Password=TestPassword123;TrustServerCertificate=true");
        return Task.CompletedTask;
    }
}