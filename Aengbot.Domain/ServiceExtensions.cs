using Aengbot.Handlers.Command;
using Aengbot.Handlers.Query;
using Aengbot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aengbot;

public static class ServiceExtensions
{
    public static void AddDomainServices(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(ServiceExtensions).Assembly;

// Register command handlers
        services.Scan(scan => scan
            .FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableTo<ICommandHandler>())
            .AsMatchingInterface());

// Register query handlers
        services.Scan(scan => scan
            .FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableTo<IQueryHandler>())
            .AsMatchingInterface());
        
        services.Scan(scan => scan
            .FromAssemblies(assembly)
            .AddClasses(classes => classes.InNamespaceOf<DateService>(), publicOnly: false)
            .AsMatchingInterface());
    }
}