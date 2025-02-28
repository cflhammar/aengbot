using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace Tests.Drivers;

public class HttpClientDriver
{
    private static WebApplicationFactory<Program>? _webAppFactory;
    private HttpClient? _httpClient;
    private HttpResponseMessage? _apiResponse;
    public async Task WhenARequestIsMade(HttpRequestMessage request)
    {
        request.Headers.Add("Accept", "application/json");
        request.Headers.Add("XAPIKEY", "1234");
        if (_webAppFactory == null) CreateClientFactory();
        _httpClient ??= _webAppFactory!.CreateClient();
        _apiResponse = await _httpClient.SendAsync(request);
    }

    private void CreateClientFactory()
    {
        _webAppFactory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.Test.json");
                });
            });
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
    }
}