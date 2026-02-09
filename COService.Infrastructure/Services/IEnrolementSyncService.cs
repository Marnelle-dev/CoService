namespace COService.Infrastructure.Services;

/// <summary>
/// Interface pour le service de synchronisation avec Enrolement
/// </summary>
public interface IEnrolementSyncService
{
    /// <summary>
    /// Synchronise tous les partenaires depuis Enrolement
    /// </summary>
    Task SynchroniserPartenairesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Synchronise tous les exportateurs depuis Enrolement
    /// </summary>
    Task SynchroniserExportateursAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Synchronise un partenaire spécifique par son ID
    /// </summary>
    Task SynchroniserPartenaireAsync(Guid partenaireId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Synchronise un exportateur spécifique par son ID
    /// </summary>
    Task SynchroniserExportateurAsync(Guid exportateurId, CancellationToken cancellationToken = default);
}
