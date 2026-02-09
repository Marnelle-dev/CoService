using COService.Application.DTOs;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des partenaires (Chambres de Commerce)
/// Lecture seule - synchronisés depuis le microservice Enrolement
/// </summary>
public interface IPartenaireService
{
    Task<PartenaireDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PartenaireDto?> GetByCodeAsync(string codePartenaire, CancellationToken cancellationToken = default);
    Task<IEnumerable<PartenaireDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<PartenaireDto>> GetActifsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<PartenaireDto>> GetByTypeAsync(Guid typePartenaireId, CancellationToken cancellationToken = default);
    Task<IEnumerable<PartenaireDto>> GetByDepartementAsync(Guid departementId, CancellationToken cancellationToken = default);
}
