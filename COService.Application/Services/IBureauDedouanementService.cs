using COService.Application.DTOs;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des bureaux de douane
/// Lecture seule - synchronisés depuis le microservice Référentiel
/// </summary>
public interface IBureauDedouanementService
{
    Task<BureauDedouanementDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<BureauDedouanementDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<BureauDedouanementDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<BureauDedouanementDto>> GetActifsAsync(CancellationToken cancellationToken = default);
}
