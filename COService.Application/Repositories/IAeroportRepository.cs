using COService.Domain.Entities;

namespace COService.Application.Repositories;

/// <summary>
/// Repository pour les aéroports
/// Lecture seule - synchronisés depuis le microservice Référentiel
/// </summary>
public interface IAeroportRepository
{
    Task<Aeroport?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Aeroport?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<Aeroport>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Aeroport>> GetActifsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Aeroport>> GetByPaysAsync(Guid paysId, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);
}
