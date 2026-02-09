using COService.Domain.Entities;

namespace COService.Application.Repositories;

/// <summary>
/// Repository pour les départements
/// Lecture seule - synchronisés depuis le microservice Référentiel
/// </summary>
public interface IDepartementRepository
{
    Task<Departement?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Departement?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<Departement>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Departement>> GetActifsAsync(CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);
}
