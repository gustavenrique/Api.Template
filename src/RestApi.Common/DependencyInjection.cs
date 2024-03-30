using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using RestApi.Common.Abstractions.Domain.Events;
using RestApi.Common.Configuration;
using RestApi.Common.Services;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RestApi.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddCommon(
        this IServiceCollection services,
        ConfigurationManager config
    ) =>
        services
            .AddKeyVault(config)
            .AddServices();

    /// <summary>
    /// Adiciona o key vault como um dos Configurations Providers
    /// </summary>
    /// <remarks>
    /// Deve necessariamente ser o primeiro método de setup chamado,
    /// para garantir que todos as próximas dependências sejam injetadas 
    /// com os dados de configuração corretos
    /// </remarks>
    public static IServiceCollection AddKeyVault(
        this IServiceCollection services,
        ConfigurationManager config
    )
    {
        var keyVault = config
            .GetRequiredSection("KeyVault")
            .Get<KeyVaultConfig>()!;

        bool rodandoLocal = keyVault is not { ClientId: null, TenantId: null, ClientSecret: null };

        if (string.IsNullOrEmpty(keyVault.Url))
        {
            return services;
        }

        if (rodandoLocal)
        {
            config.AddAzureKeyVault(
                new SecretClient(
                    new Uri(keyVault.Url),
                    new ClientSecretCredential(
                        keyVault.TenantId,
                        keyVault.ClientId,
                        keyVault.ClientSecret
                    )
                ),
                new AzureKeyVaultConfigurationOptions()
            );
        }
        else // se estiver rodando em cloud, usa managed identity
        {
            config.AddAzureKeyVault(
                new Uri(keyVault.Url),
                new DefaultAzureCredential()
            );
        }

        return services;
    }

    static IServiceCollection AddServices(this IServiceCollection services) =>
        services
            .AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
                config.Lifetime = ServiceLifetime.Singleton;
                config.NotificationPublisher = new TaskWhenAllPublisher();
                config.NotificationPublisherType = typeof(TaskWhenAllPublisher);
            })
            .AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
}