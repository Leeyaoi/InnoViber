using InnoViber.Domain.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace InnoViber.Domain.DI;

public static class DomainLayerDependencies
{
    public static void RegisterDomainDependencies(this IServiceCollection services)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
    }
}
