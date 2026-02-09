using COService.Domain.Entities;

namespace COService.Application.Repositories;

/// <summary>
/// Repository pour les partenaires (Chambres de Commerce)
/// Synchronisés depuis le microservice Enrolement
/// </summary>
public interface IPartenaireRepository
{
    Task<Partenaire?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Partenaire?> GetByCodeAsync(string codePartenaire, CancellationToken cancellationToken = default);
    Task<IEnumerable<Partenaire>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Partenaire>> GetActifsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Partenaire>> GetByTypeAsync(Guid typePartenaireId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Partenaire>> GetByDepartementAsync(Guid departementId, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByCodeAsync(string codePartenaire, CancellationToken cancellationToken = default);
    
    // Méthodes pour la synchronisation
    Task<Partenaire> AddAsync(Partenaire partenaire, CancellationToken cancellationToken = default);
    void Update(Partenaire partenaire);
}
