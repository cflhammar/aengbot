using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Reqnroll;
using Reqnroll.BoDi;

namespace Tests.Drivers;

[Binding]
public class ApiHostDriver
{
    private readonly IObjectContainer _objectContainer;
    private readonly WebApplicationFactory<Program> _factory;

    public ApiHostDriver(IObjectContainer objectContainer, SweeetSpotApiDriver sweetSpotApiDriver, DateTimeDriver dateTimeDriver)
    {
        _objectContainer = objectContainer;

        _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureAppConfiguration(config =>
            {
                //
            });
            
            // do this here instead?
            // builder.UseEnvironment("AcceptanceTest");
            // builder.UseSetting("AzureAd:ClientId", "TestID");
            
            builder.ConfigureTestServices(services =>
            {
                dateTimeDriver.AddMockDateTimeProvider(services);
                sweetSpotApiDriver.AddMockSweetspotApi(services);
            });
            
        });
    }
    
    [BeforeScenario]
    public void BeforeScenario()
    {
        _objectContainer.RegisterInstanceAs(_factory);
    }
}