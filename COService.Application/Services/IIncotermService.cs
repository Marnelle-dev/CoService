using COService.Application.DTOs;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des incoterms
/// Lecture seule - synchronisés depuis le microservice Référentiel
/// </summary>
public interface IIncotermService
{
    Task<IncotermDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IncotermDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<IncotermDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<IncotermDto>> GetActifsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<IncotermDto>> GetByModuleAsync(Guid moduleId, CancellationToken cancellationToken = default);
}
