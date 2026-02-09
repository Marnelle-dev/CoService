using COService.Domain.Entities;

namespace COService.Application.Repositories;

/// <summary>
/// Repository pour les ports
/// Synchronisés depuis le microservice Référentiel via RabbitMQ
/// </summary>
public interface IPortRepository
{
    Task<Port?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Port?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<Port>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Port>> GetActifsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Port>> GetByPaysAsync(Guid paysId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Port>> GetByTypeAsync(string type, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<Port> AddAsync(Port port, CancellationToken cancellationToken = default);
    void Update(Port port);
}
