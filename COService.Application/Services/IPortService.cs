using COService.Application.DTOs;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des ports
/// Lecture seule - synchronisés depuis le microservice Référentiel
/// </summary>
public interface IPortService
{
    Task<PortDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PortDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<PortDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<PortDto>> GetActifsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<PortDto>> GetByPaysAsync(Guid paysId, CancellationToken cancellationToken = default);
    Task<IEnumerable<PortDto>> GetByTypeAsync(string type, CancellationToken cancellationToken = default);
}
