namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des notifications
/// Publie des événements RabbitMQ vers le microservice Notification
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Envoie une notification de changement de statut
    /// </summary>
    Task EnvoyerNotificationChangementStatutAsync(Guid certificatId, string ancienStatut, string nouveauStatut, CancellationToken cancellationToken = default);

    /// <summary>
    /// Envoie une notification de rejet
    /// </summary>
    Task EnvoyerNotificationRejetAsync(Guid certificatId, string commentaire, CancellationToken cancellationToken = default);

    /// <summary>
    /// Envoie une notification de validation
    /// </summary>
    Task EnvoyerNotificationValidationAsync(Guid certificatId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Envoie une notification de soumission
    /// </summary>
    Task EnvoyerNotificationSoumissionAsync(Guid certificatId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Envoie une notification de contrôle
    /// </summary>
    Task EnvoyerNotificationControleAsync(Guid certificatId, bool approuve, CancellationToken cancellationToken = default);

    /// <summary>
    /// Envoie une notification d'approbation
    /// </summary>
    Task EnvoyerNotificationApprobationAsync(Guid certificatId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Envoie une notification pour Formule A
    /// </summary>
    Task EnvoyerNotificationFormuleAAsync(Guid certificatId, string statut, CancellationToken cancellationToken = default);
}
