using COService.Application.Repositories;
using COService.Domain.Entities;
using COService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace COService.Infrastructure.Repositories;

/// <summary>
/// Implémentation du repository pour les bureaux de douane
/// </summary>
public class BureauDedouanementRepository : Repository<BureauDedouanement>, IBureauDedouanementRepository
{
    public BureauDedouanementRepository(COServiceDbContext context) : base(context)
    {
    }

    public override async Task<BureauDedouanement?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<BureauDedouanement?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(b => b.Code == code, cancellationToken);
    }

    public override async Task<IEnumerable<BureauDedouanement>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .OrderBy(b => b.Description)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<BureauDedouanement>> GetActifsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(b => b.Actif)
            .OrderBy(b => b.Description)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(b => b.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(b => b.Code == code, cancellationToken);
    }
}
