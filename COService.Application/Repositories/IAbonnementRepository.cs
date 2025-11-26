using COService.Domain.Entities;

namespace COService.Application.Repositories;

/// <summary>
/// Repository pour les abonnements
/// </summary>
public interface IAbonnementRepository
{
    Task<Abonnement?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Abonnement>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Abonnement>> GetByExportateurAsync(string exportateur, CancellationToken cancellationToken = default);
    Task<IEnumerable<Abonnement>> GetByPartenaireAsync(string partenaire, CancellationToken cancellationToken = default);
    Task<Abonnement> AddAsync(Abonnement abonnement, CancellationToken cancellationToken = default);
    void Update(Abonnement abonnement);
    void Remove(Abonnement abonnement);
}

