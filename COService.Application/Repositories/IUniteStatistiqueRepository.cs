using COService.Domain.Entities;

namespace COService.Application.Repositories;

/// <summary>
/// Repository pour les unités statistiques
/// Lecture seule - synchronisés depuis le microservice Référentiel
/// </summary>
public interface IUniteStatistiqueRepository
{
    Task<UniteStatistique?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<UniteStatistique?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<UniteStatistique>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<UniteStatistique>> GetActifsAsync(CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);
}
