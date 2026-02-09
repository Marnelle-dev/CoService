using COService.Application.Repositories;
using COService.Domain.Entities;
using COService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace COService.Infrastructure.Repositories;

/// <summary>
/// Implémentation du repository pour les incoterms
/// </summary>
public class IncotermRepository : Repository<Incoterm>, IIncotermRepository
{
    public IncotermRepository(COServiceDbContext context) : base(context)
    {
    }

    public override async Task<Incoterm?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(i => i.Module)
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }

    public async Task<Incoterm?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(i => i.Module)
            .FirstOrDefaultAsync(i => i.Code == code, cancellationToken);
    }

    public override async Task<IEnumerable<Incoterm>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(i => i.Module)
            .OrderBy(i => i.Code)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Incoterm>> GetActifsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(i => i.Module)
            .Where(i => i.Actif)
            .OrderBy(i => i.Code)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Incoterm>> GetByModuleAsync(Guid moduleId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(i => i.Module)
            .Where(i => i.ModuleId == moduleId)
            .OrderBy(i => i.Code)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(i => i.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(i => i.Code == code, cancellationToken);
    }
}
