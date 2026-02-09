using COService.Application.DTOs;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des unités statistiques
/// Lecture seule - synchronisés depuis le microservice Référentiel
/// </summary>
public interface IUniteStatistiqueService
{
    Task<UniteStatistiqueDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<UniteStatistiqueDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<UniteStatistiqueDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<UniteStatistiqueDto>> GetActifsAsync(CancellationToken cancellationToken = default);
}
