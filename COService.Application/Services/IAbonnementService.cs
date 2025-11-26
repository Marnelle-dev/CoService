using COService.Application.DTOs;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des abonnements
/// </summary>
public interface IAbonnementService
{
    Task<AbonnementDto> CreerAbonnementAsync(CreerAbonnementDto dto, string? utilisateur = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<AbonnementDto>> GetAllAbonnementsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<AbonnementDto>> GetAbonnementsByExportateurAsync(string exportateur, CancellationToken cancellationToken = default);
    Task<IEnumerable<AbonnementDto>> GetAbonnementsByPartenaireAsync(string partenaire, CancellationToken cancellationToken = default);
    Task<AbonnementDto?> GetAbonnementByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<AbonnementDto> ModifierAbonnementAsync(Guid id, ModifierAbonnementDto dto, string? utilisateur = null, CancellationToken cancellationToken = default);
    Task SupprimerAbonnementAsync(Guid id, CancellationToken cancellationToken = default);
    Task<AbonnementDto> RattacherCertificatsAsync(Guid abonnementId, List<Guid> certificateIds, CancellationToken cancellationToken = default);
    Task<AbonnementDto> DetacherCertificatAsync(Guid abonnementId, Guid certificateId, CancellationToken cancellationToken = default);
}

