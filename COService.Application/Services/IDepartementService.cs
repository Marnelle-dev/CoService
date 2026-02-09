using COService.Application.DTOs;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des départements
/// Lecture seule - synchronisés depuis le microservice Référentiel
/// </summary>
public interface IDepartementService
{
    Task<DepartementDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<DepartementDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<DepartementDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<DepartementDto>> GetActifsAsync(CancellationToken cancellationToken = default);
}
