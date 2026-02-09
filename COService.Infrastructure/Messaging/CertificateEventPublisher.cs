using COService.Application.Messaging;
using COService.Shared.Events;
using Microsoft.Extensions.Logging;

namespace COService.Infrastructure.Messaging;

/// <summary>
/// Publisher pour les événements de certificats
/// </summary>
public class CertificateEventPublisher : ICertificateEventPublisher
{
    private readonly IRabbitMQClient _rabbitMQClient;
    private readonly ILogger<CertificateEventPublisher> _logger;

    public CertificateEventPublisher(IRabbitMQClient rabbitMQClient, ILogger<CertificateEventPublisher> logger)
    {
        _rabbitMQClient = rabbitMQClient;
        _logger = logger;
    }

    public async Task PublishCertificatCreeAsync(CertificatCreeEvent evt, CancellationToken cancellationToken = default)
    {
        try
        {
            await _rabbitMQClient.PublishAsync("certificat.creé", evt, cancellationToken);
            _logger.LogInformation("Événement 'certificat.creé' publié pour le certificat {CertificatId}", evt.CertificatId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la publication de l'événement 'certificat.creé' pour {CertificatId}", evt.CertificatId);
            // Ne pas faire échouer l'opération principale si la publication échoue
        }
    }

    public async Task PublishCertificatStatutChangeAsync(CertificatStatutChangeEvent evt, CancellationToken cancellationToken = default)
    {
        try
        {
            await _rabbitMQClient.PublishAsync("certificat.statut.changé", evt, cancellationToken);
            _logger.LogInformation(
                "Événement 'certificat.statut.changé' publié pour le certificat {CertificatId} : {AncienStatut} → {NouveauStatut}",
                evt.CertificatId, evt.AncienStatut, evt.NouveauStatut);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la publication de l'événement 'certificat.statut.changé' pour {CertificatId}", evt.CertificatId);
        }
    }

    public async Task PublishCertificatValideAsync(CertificatValideEvent evt, CancellationToken cancellationToken = default)
    {
        try
        {
            await _rabbitMQClient.PublishAsync("certificat.validé", evt, cancellationToken);
            _logger.LogInformation(
                "Événement 'certificat.validé' publié pour le certificat {CertificatId} ({CertificateNo}) - FacturationService sera notifié",
                evt.CertificatId, evt.CertificateNo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la publication de l'événement 'certificat.validé' pour {CertificatId}", evt.CertificatId);
        }
    }

    public async Task PublishCertificatRejeteAsync(CertificatRejeteEvent evt, CancellationToken cancellationToken = default)
    {
        try
        {
            await _rabbitMQClient.PublishAsync("certificat.rejeté", evt, cancellationToken);
            _logger.LogInformation("Événement 'certificat.rejeté' publié pour le certificat {CertificatId}", evt.CertificatId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la publication de l'événement 'certificat.rejeté' pour {CertificatId}", evt.CertificatId);
        }
    }
}
