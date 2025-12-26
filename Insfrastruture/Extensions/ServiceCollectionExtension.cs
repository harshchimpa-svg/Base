using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddService();
    }

    public static void AddService(this IServiceCollection services)
    {
        services
            .AddTransient<IMediator, Mediator>();
    }
}
