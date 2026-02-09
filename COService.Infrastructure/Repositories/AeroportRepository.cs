using COService.Application.Repositories;
using COService.Domain.Entities;
using COService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace COService.Infrastructure.Repositories;

/// <summary>
/// Implémentation du repository pour les aéroports
/// </summary>
public class AeroportRepository : Repository<Aeroport>, IAeroportRepository
{
    public AeroportRepository(COServiceDbContext context) : base(context)
    {
    }

    public override async Task<Aeroport?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(a => a.Pays)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<Aeroport?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(a => a.Pays)
            .FirstOrDefaultAsync(a => a.Code == code, cancellationToken);
    }

    public override async Task<IEnumerable<Aeroport>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(a => a.Pays)
            .OrderBy(a => a.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Aeroport>> GetActifsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(a => a.Pays)
            .Where(a => a.Actif)
            .OrderBy(a => a.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Aeroport>> GetByPaysAsync(Guid paysId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(a => a.Pays)
            .Where(a => a.PaysId == paysId)
            .OrderBy(a => a.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(a => a.Code == code, cancellationToken);
    }
}
