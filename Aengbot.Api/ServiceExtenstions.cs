using AengbotApi.Middleware;

namespace AengbotApi;

public static class ServiceExtensions
{
    public static void MapRestApi(this WebApplication app)
    {
        app.MapAengbotApi();
        app.UseMiddleware<ApiKeyMiddleware>();
    }
}