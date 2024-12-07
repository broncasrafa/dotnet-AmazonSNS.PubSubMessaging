using Microsoft.Extensions.DependencyInjection;
using CloudNotes.Domain.Interfaces.Events;
using CloudNotes.Infra.Notification.SNS;

namespace CloudNotes.Infra.Notification.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureNotifications(this IServiceCollection services)
    {
        services.AddTransient<IEventPublisher, SNSEventPublisher>();


        return services;
    }
}
