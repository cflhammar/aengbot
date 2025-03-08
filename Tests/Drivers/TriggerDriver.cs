namespace Tests.Drivers;

public class TriggerDriver(ApiClientDriver httpClient)
{
    public async Task WhenTheServiceIsTriggeredToSearch()
    {
        httpClient.RequestMessage = new HttpRequestMessage(HttpMethod.Get, $"aengbot/trigger");
        await httpClient.WhenARequestIsMade();
        await httpClient.ThenRequestIsSuccessful();
    }
}