using COService.Application.DTOs;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des aéroports
/// Lecture seule - synchronisés depuis le microservice Référentiel
/// </summary>
public interface IAeroportService
{
    Task<AeroportDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<AeroportDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<AeroportDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<AeroportDto>> GetActifsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<AeroportDto>> GetByPaysAsync(Guid paysId, CancellationToken cancellationToken = default);
}
