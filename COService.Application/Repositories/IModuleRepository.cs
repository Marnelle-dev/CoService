using COService.Domain.Entities;

namespace COService.Application.Repositories;

/// <summary>
/// Repository pour les modules de transport
/// Lecture seule - synchronisés depuis le microservice Référentiel
/// </summary>
public interface IModuleRepository
{
    Task<Module?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Module?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<Module>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Module>> GetActifsAsync(CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);
}
