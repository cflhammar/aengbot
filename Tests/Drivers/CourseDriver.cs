using System.Text;
using System.Text.Json;
using Tests.Steps;

namespace Tests.Drivers;

public class CourseDriver(ApiClientDriver httpClient)
{
    public async Task AddCourse(CourseSteps.CourseStep addCourse)
    {
        var request = new CourseRequest(
            addCourse.Id,
            addCourse.Name
        );

        var requestJson = JsonSerializer.Serialize(request,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "aengbot/courses/add")
        {
            Content = new StringContent(requestJson, Encoding.UTF8, "application/json")
        };
        
        httpClient.RequestMessage = httpRequest;
        await httpClient.WhenARequestIsMade();
        await httpClient.ThenRequestIsSuccessful();
    }

    private record CourseRequest(
        string Id,
        string Name);
}