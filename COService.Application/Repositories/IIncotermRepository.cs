using COService.Domain.Entities;

namespace COService.Application.Repositories;

/// <summary>
/// Repository pour les incoterms
/// Lecture seule - synchronisés depuis le microservice Référentiel
/// </summary>
public interface IIncotermRepository
{
    Task<Incoterm?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Incoterm?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<Incoterm>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Incoterm>> GetActifsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Incoterm>> GetByModuleAsync(Guid moduleId, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);
}
