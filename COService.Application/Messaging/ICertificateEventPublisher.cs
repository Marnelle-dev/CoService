using COService.Shared.Events;

namespace COService.Application.Messaging;

/// <summary>
/// Publisher pour les événements de certificats
/// </summary>
public interface ICertificateEventPublisher
{
    /// <summary>
    /// Publie l'événement de création d'un certificat
    /// </summary>
    Task PublishCertificatCreeAsync(CertificatCreeEvent evt, CancellationToken cancellationToken = default);

    /// <summary>
    /// Publie l'événement de changement de statut d'un certificat
    /// </summary>
    Task PublishCertificatStatutChangeAsync(CertificatStatutChangeEvent evt, CancellationToken cancellationToken = default);

    /// <summary>
    /// Publie l'événement de validation d'un certificat (pour facturation)
    /// </summary>
    Task PublishCertificatValideAsync(CertificatValideEvent evt, CancellationToken cancellationToken = default);

    /// <summary>
    /// Publie l'événement de rejet d'un certificat
    /// </summary>
    Task PublishCertificatRejeteAsync(CertificatRejeteEvent evt, CancellationToken cancellationToken = default);
}
