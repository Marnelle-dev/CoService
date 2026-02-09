using COService.Application.Repositories;
using COService.Domain.Entities;
using COService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace COService.Infrastructure.Repositories;

/// <summary>
/// Implémentation du repository pour les modules de transport
/// </summary>
public class ModuleRepository : Repository<Module>, IModuleRepository
{
    public ModuleRepository(COServiceDbContext context) : base(context)
    {
    }

    public override async Task<Module?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<Module?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(m => m.Code == code, cancellationToken);
    }

    public override async Task<IEnumerable<Module>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .OrderBy(m => m.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Module>> GetActifsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(m => m.Actif)
            .OrderBy(m => m.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(m => m.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(m => m.Code == code, cancellationToken);
    }
}
