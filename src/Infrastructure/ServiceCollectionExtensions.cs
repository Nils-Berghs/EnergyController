using System;
using Berghs.Nils.EnergyController.Domain.Devices;
using Berghs.Nils.EnergyController.Infrastructure.Devices;
using Microsoft.Extensions.DependencyInjection;

namespace Berghs.Nils.EnergyController.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .ConfigureDevices()
            .ConfigureMediator();

        return services;
    }

    private static IServiceCollection ConfigureDevices(this IServiceCollection services)
    {
        services.AddScoped<ISolarConverter, SunnyBoySolarConverter>();

        return services;
    }

    private static IServiceCollection ConfigureMediator(this IServiceCollection services)
    {
        return services.AddMediator(options =>
        {
            options.ServiceLifetime = ServiceLifetime.Scoped;
            // options.PipelineBehaviors =
            // [
            //     typeof(LoggingBehavior<,>),
            //     typeof(AuthorizationBehavior<,>),
            //     typeof(MyFluentValidationBehavior<,>),
            //     typeof(AnnotationValidationBehavior<,>),
            //     typeof(TransactionBehavior<,>),
            //     typeof(ResultLoggingBehavior<,>),
            // ];
        });
    }
}
