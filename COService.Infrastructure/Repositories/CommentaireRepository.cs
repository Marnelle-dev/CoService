using COService.Application.Repositories;
using COService.Domain.Entities;
using COService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace COService.Infrastructure.Repositories;

/// <summary>
/// Implémentation du repository pour les commentaires
/// </summary>
public class CommentaireRepository : Repository<Commentaire>, ICommentaireRepository
{
    public CommentaireRepository(COServiceDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Commentaire>> GetByCertificateIdAsync(Guid certificateId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(c => c.CertificateId == certificateId)
            .OrderByDescending(c => c.CreeLe)
            .ToListAsync(cancellationToken);
    }
}

