using COService.Application.DTOs;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des positions tarifaires (Codes HS)
/// Lecture seule - synchronisés depuis le microservice Référentiel
/// </summary>
public interface IPositionTarifaireService
{
    Task<PositionTarifaireDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PositionTarifaireDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<PositionTarifaireDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<PositionTarifaireDto>> GetActifsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<PositionTarifaireDto>> GetByCategorieAsync(Guid categorieId, CancellationToken cancellationToken = default);
}
