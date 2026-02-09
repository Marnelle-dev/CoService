using COService.Application.Repositories;
using COService.Domain.Entities;
using COService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace COService.Infrastructure.Repositories;

/// <summary>
/// Implémentation du repository pour les zones de production
/// </summary>
public class ZoneProductionRepository : Repository<ZoneProduction>, IZoneProductionRepository
{
    public ZoneProductionRepository(COServiceDbContext context) : base(context)
    {
    }

    public override async Task<ZoneProduction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(z => z.Partenaire)
            .FirstOrDefaultAsync(z => z.Id == id, cancellationToken);
    }

    public override async Task<IEnumerable<ZoneProduction>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(z => z.Partenaire)
            .OrderBy(z => z.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ZoneProduction>> GetByPartenaireAsync(Guid partenaireId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(z => z.Partenaire)
            .Where(z => z.PartenaireId == partenaireId)
            .OrderBy(z => z.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(z => z.Id == id, cancellationToken);
    }
}
