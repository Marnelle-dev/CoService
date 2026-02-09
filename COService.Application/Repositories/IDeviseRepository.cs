using COService.Domain.Entities;

namespace COService.Application.Repositories;

/// <summary>
/// Repository pour les devises
/// Synchronisés depuis le microservice Référentiel via RabbitMQ
/// </summary>
public interface IDeviseRepository
{
    Task<Devise?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Devise?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<Devise>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Devise>> GetActifsAsync(CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<Devise> AddAsync(Devise devise, CancellationToken cancellationToken = default);
    void Update(Devise devise);
}
