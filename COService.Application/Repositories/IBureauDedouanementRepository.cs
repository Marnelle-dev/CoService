using COService.Domain.Entities;

namespace COService.Application.Repositories;

/// <summary>
/// Repository pour les bureaux de douane
/// Lecture seule - synchronisés depuis le microservice Référentiel
/// </summary>
public interface IBureauDedouanementRepository
{
    Task<BureauDedouanement?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<BureauDedouanement?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<BureauDedouanement>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<BureauDedouanement>> GetActifsAsync(CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);
}
