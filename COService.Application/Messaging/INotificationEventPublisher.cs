using COService.Shared.Events;

namespace COService.Application.Messaging;

/// <summary>
/// Publisher pour les événements de notifications
/// </summary>
public interface INotificationEventPublisher
{
    /// <summary>
    /// Publie une demande de notification
    /// </summary>
    Task PublishNotificationDemandeAsync(NotificationDemandeEvent evt, CancellationToken cancellationToken = default);
}
