using COService.Domain.Entities;

namespace COService.Application.Repositories;

/// <summary>
/// Repository pour les exportateurs
/// Synchronisés depuis le microservice Enrolement
/// </summary>
public interface IExportateurRepository
{
    Task<Exportateur?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Exportateur?> GetByCodeAsync(string codeExportateur, CancellationToken cancellationToken = default);
    Task<IEnumerable<Exportateur>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Exportateur>> GetActifsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Exportateur>> GetByPartenaireAsync(Guid partenaireId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Exportateur>> GetByDepartementAsync(Guid departementId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Exportateur>> GetByTypeAsync(int typeExportateur, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByCodeAsync(string codeExportateur, CancellationToken cancellationToken = default);
    
    // Méthodes pour la synchronisation
    Task<Exportateur> AddAsync(Exportateur exportateur, CancellationToken cancellationToken = default);
    void Update(Exportateur exportateur);
}
