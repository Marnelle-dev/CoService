using COService.Application.Messaging;
using COService.Shared.Events;
using Microsoft.Extensions.Logging;

namespace COService.Infrastructure.Messaging;

/// <summary>
/// Publisher pour les événements de notifications
/// </summary>
public class NotificationEventPublisher : INotificationEventPublisher
{
    private readonly IRabbitMQClient _rabbitMQClient;
    private readonly ILogger<NotificationEventPublisher> _logger;

    public NotificationEventPublisher(IRabbitMQClient rabbitMQClient, ILogger<NotificationEventPublisher> logger)
    {
        _rabbitMQClient = rabbitMQClient;
        _logger = logger;
    }

    public async Task PublishNotificationDemandeAsync(NotificationDemandeEvent evt, CancellationToken cancellationToken = default)
    {
        try
        {
            await _rabbitMQClient.PublishAsync("notification.demande", evt, cancellationToken);
            _logger.LogInformation(
                "Événement 'notification.demande' publié : Type={Type}, Destinataire={Destinataire}",
                evt.Type, evt.Destinataire);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la publication de l'événement 'notification.demande'");
            // Ne pas faire échouer l'opération principale si la publication échoue
        }
    }
}
