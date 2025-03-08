using Reqnroll;
using Tests.Drivers;

namespace Tests.Steps;

[Binding]
public class TriggerSteps(TriggerDriver triggerDriver)
{
    [When(@"the service is triggered to search")]
    public async Task WhenTheServiceIsTriggeredToSearch()
    {
        await triggerDriver.WhenTheServiceIsTriggeredToSearch();   
    }
}