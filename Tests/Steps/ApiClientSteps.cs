using Reqnroll;
using Tests.Drivers;

namespace Tests.Steps;

[Binding]
public class ApiClientSteps(ApiClientDriver driver)
{
    [When(@"the request is made")]
    public async Task WhenTheRequestIsMade()
    {
        await driver.WhenARequestIsMade();   
    }

    [Then(@"the request is not successful with")]
    public void ThenTheRequestIsNotSuccessful(Table table)
    {
        var status = table.CreateInstance<ErrorStep>();
        driver.ThenTheRequestIsNotSuccessfulWith(status);
    }
    
    public record ErrorStep(int status, string message);
}