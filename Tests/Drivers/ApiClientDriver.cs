using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Newtonsoft.Json;
using Reqnroll;
using Tests.Steps;

namespace Tests.Drivers;

[Binding]
public class ApiClientDriver(WebApplicationFactory<Program> clientFactory)
{
    private HttpResponseMessage? _apiResponse;
    public HttpRequestMessage? RequestMessage;

    public async Task WhenARequestIsMade()
    {
        var request = RequestMessage;
        request!.Headers.Add("Accept", "application/json");
        request.Headers.Add("XAPIKEY", "1234");
        var httpClient = clientFactory.CreateClient();
        _apiResponse = await httpClient.SendAsync(request);
        RequestMessage = null;
    }

    public async Task ThenRequestIsSuccessful()
    {
        _apiResponse.Should()
            .NotBeNull("there should be an API response but it was null, was a request made and awaited?");
        var response = await _apiResponse!.Content.ReadAsStringAsync();
        _apiResponse.IsSuccessStatusCode.Should()
            .BeTrue($"Request failed, HTTP {(int?)_apiResponse.StatusCode} {_apiResponse.StatusCode}: {response}");
    }
    
    public async Task ThenTheRequestIsNotSuccessfulWith(ApiClientSteps.ErrorStep expectedError)
    {
        _apiResponse.Should()
            .NotBeNull("there should be an API response but it was null, was a request made and awaited?");
        _apiResponse!.IsSuccessStatusCode.Should().BeFalse();
        _apiResponse.StatusCode.Should().Be((HttpStatusCode)expectedError.status, $"Request failed with HTTP {_apiResponse.StatusCode}");
        var response = await GetResponseContent<List<string>>();
        response.Should().Contain(expectedError.message);
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
            return contentObject!;
        }
        catch (JsonSerializationException jsonEx)
        {
            throw new Exception(
                $"Cannot deserialize response data of type {typeof(T).Name} from content: {responseContent}", jsonEx);
        }
    }
}