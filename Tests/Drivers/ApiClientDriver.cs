using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Newtonsoft.Json;
using Reqnroll;

namespace Tests.Drivers;

[Binding]
public class ApiClientDriver(WebApplicationFactory<Program> clientFactory)
{
    private HttpResponseMessage? _apiResponse;


    public async Task WhenARequestIsMade(HttpRequestMessage request)
    {
        request.Headers.Add("Accept", "application/json");
        request.Headers.Add("XAPIKEY", "1234");
        var httpClient = clientFactory.CreateClient();
        _apiResponse = await httpClient.SendAsync(request);
    }

    public async Task ThenRequestIsSuccessful()
    {
        _apiResponse.Should()
            .NotBeNull("there should be an API response but it was null, was a request made and awaited?");
        var response = await _apiResponse!.Content.ReadAsStringAsync();
        _apiResponse.IsSuccessStatusCode.Should()
            .BeTrue($"Request failed, HTTP {(int?)_apiResponse.StatusCode} {_apiResponse.StatusCode}: {response}");
    }

    public async Task<T> GetResponseContent<T>()
    {
        _apiResponse.Should()
            .NotBeNull("there should be an API response but it was null, was a request made and awaited?");
        var responseContent = await _apiResponse!.Content.ReadAsStringAsync();
        try
        {
            var contentObject = JsonConvert.DeserializeObject<T>(responseContent);
            contentObject.Should().NotBeNull();
            return contentObject;
        }
        catch (JsonSerializationException jsonEx)
        {
            throw new Exception(
                $"Cannot deserialize response data of type {typeof(T).Name} from content: {responseContent}", jsonEx);
        }
    }
}