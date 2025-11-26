using COService.Domain.Entities;

namespace COService.Application.Repositories;

/// <summary>
/// Repository pour les commentaires
/// </summary>
public interface ICommentaireRepository
{
    Task<Commentaire?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Commentaire>> GetByCertificateIdAsync(Guid certificateId, CancellationToken cancellationToken = default);
    Task<Commentaire> AddAsync(Commentaire commentaire, CancellationToken cancellationToken = default);
    void Update(Commentaire commentaire);
    void Remove(Commentaire commentaire);
}

