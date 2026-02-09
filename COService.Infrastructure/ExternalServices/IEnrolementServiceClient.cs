using COService.Application.DTOs;
using Refit;

namespace COService.Infrastructure.ExternalServices;

/// <summary>
/// Client API pour le microservice Enrolement
/// Utilise Refit pour les appels HTTP
/// </summary>
public interface IEnrolementServiceClient
{
    /// <summary>
    /// Récupère un partenaire par son ID
    /// </summary>
    [Get("/api/partenaires/{id}")]
    Task<PartenaireDto> GetPartenaireAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Récupère tous les partenaires
    /// </summary>
    [Get("/api/partenaires")]
    Task<List<PartenaireDto>> GetAllPartenairesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Récupère un partenaire par son code
    /// </summary>
    [Get("/api/partenaires/code/{code}")]
    Task<PartenaireDto?> GetPartenaireByCodeAsync(string code, CancellationToken cancellationToken = default);

    /// <summary>
    /// Récupère un exportateur par son ID
    /// </summary>
    [Get("/api/exportateurs/{id}")]
    Task<ExportateurDto> GetExportateurAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Récupère tous les exportateurs
    /// </summary>
    [Get("/api/exportateurs")]
    Task<List<ExportateurDto>> GetAllExportateursAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Récupère un exportateur par son code
    /// </summary>
    [Get("/api/exportateurs/code/{code}")]
    Task<ExportateurDto?> GetExportateurByCodeAsync(string code, CancellationToken cancellationToken = default);

    /// <summary>
    /// Récupère les exportateurs d'un partenaire
    /// </summary>
    [Get("/api/exportateurs/partenaire/{partenaireId}")]
    Task<List<ExportateurDto>> GetExportateursByPartenaireAsync(Guid partenaireId, CancellationToken cancellationToken = default);
}
