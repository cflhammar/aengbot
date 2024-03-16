using Microsoft.Extensions.DependencyInjection;

namespace AengbotApi;

public static class ServiceExtensions
{
    public static void MapRestApi(this WebApplication app)
    {
        app.MapAengbotApi();
    }
}