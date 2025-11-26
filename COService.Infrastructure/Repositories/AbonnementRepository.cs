using COService.Application.Repositories;
using COService.Domain.Entities;
using COService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace COService.Infrastructure.Repositories;

/// <summary>
/// Implémentation du repository pour les abonnements
/// </summary>
public class AbonnementRepository : Repository<Abonnement>, IAbonnementRepository
{
    public AbonnementRepository(COServiceDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Abonnement>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(a => a.Certificats)
            .OrderByDescending(a => a.CreeLe)
            .ToListAsync(cancellationToken);
    }

    public override async Task<Abonnement?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(a => a.Certificats)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Abonnement>> GetByExportateurAsync(string exportateur, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(a => a.Certificats)
            .Where(a => a.Exportateur.Contains(exportateur))
            .OrderByDescending(a => a.CreeLe)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Abonnement>> GetByPartenaireAsync(string partenaire, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(a => a.Certificats)
            .Where(a => a.Partenaire.Contains(partenaire))
            .OrderByDescending(a => a.CreeLe)
            .ToListAsync(cancellationToken);
    }
}

