using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Abstractions.Events;
using SharedKernel.Services;

namespace SharedKernel;

public static class DependencyInjection
{
    public static IServiceCollection AddSharedKernel(this IServiceCollection services) =>
        services
            .AddServices();

    private static IServiceCollection AddServices(this IServiceCollection services) =>
        services
            .AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

                config.NotificationPublisher = new TaskWhenAllPublisher();
                config.NotificationPublisherType = typeof(TaskWhenAllPublisher);
            })
            .AddSingleton<IEventDispatcher, EventDispatcher>();
}