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
}