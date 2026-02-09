using COService.Application.Repositories;
using COService.Domain.Entities;
using COService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace COService.Infrastructure.Repositories;

/// <summary>
/// Implémentation du repository pour les pays
/// </summary>
public class PaysRepository : Repository<Pays>, IPaysRepository
{
    public PaysRepository(COServiceDbContext context) : base(context)
    {
    }

    public override async Task<Pays?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<Pays?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(p => p.Code == code, cancellationToken);
    }

    public override async Task<IEnumerable<Pays>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .OrderBy(p => p.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Pays>> GetActifsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.Actif)
            .OrderBy(p => p.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(p => p.Code == code, cancellationToken);
    }

    public new async Task<Pays> AddAsync(Pays pays, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(pays, cancellationToken);
        return pays;
    }

    public new void Update(Pays pays)
    {
        _dbSet.Update(pays);
    }
}
