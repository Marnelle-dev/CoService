using COService.Domain.Entities;

namespace COService.Application.Repositories;

/// <summary>
/// Repository pour les pays
/// Synchronisés depuis le microservice Référentiel via RabbitMQ
/// </summary>
public interface IPaysRepository
{
    Task<Pays?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Pays?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<Pays>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Pays>> GetActifsAsync(CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<Pays> AddAsync(Pays pays, CancellationToken cancellationToken = default);
    void Update(Pays pays);
}
