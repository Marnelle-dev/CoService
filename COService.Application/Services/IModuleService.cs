using COService.Application.DTOs;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des modules de transport
/// Lecture seule - synchronisés depuis le microservice Référentiel
/// </summary>
public interface IModuleService
{
    Task<ModuleDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ModuleDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<ModuleDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ModuleDto>> GetActifsAsync(CancellationToken cancellationToken = default);
}
