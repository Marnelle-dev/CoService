using COService.Domain.Entities;

namespace COService.Application.Repositories;

/// <summary>
/// Repository pour les positions tarifaires (Codes HS)
/// Lecture seule - synchronisés depuis le microservice Référentiel
/// </summary>
public interface IPositionTarifaireRepository
{
    Task<PositionTarifaire?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PositionTarifaire?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<PositionTarifaire>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<PositionTarifaire>> GetActifsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<PositionTarifaire>> GetByCategorieAsync(Guid categorieId, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);
}
