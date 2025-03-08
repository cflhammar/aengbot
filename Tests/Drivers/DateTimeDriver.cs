using Aengbot.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;

namespace Tests.Drivers;

public class DateTimeDriver
{
    // This should be the same instance that gets registered in the ObjectContainer as well as in the web host
    // so that means that if we modify the date in the driver, it will be modified in the web host as well
    private readonly IDateService _serviceMock;

    public DateTimeDriver()
    {
        _serviceMock = Substitute.For<IDateService>();
        _serviceMock.UtcNow.Returns(DateTime.UtcNow);
    }

    public void GivenCurrentDateIs(DateTime dateTime)
    {
        _serviceMock.UtcNow.Returns(dateTime);
    }

    public void AddMockDateTimeProvider(IServiceCollection services)
    {
        services.RemoveAll(typeof(IDateService));
        services.AddSingleton(_serviceMock);
    }
}
