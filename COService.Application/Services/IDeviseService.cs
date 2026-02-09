using COService.Application.DTOs;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des devises
/// Lecture seule - synchronisés depuis le microservice Référentiel
/// </summary>
public interface IDeviseService
{
    Task<DeviseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<DeviseDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<DeviseDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<DeviseDto>> GetActifsAsync(CancellationToken cancellationToken = default);
}
