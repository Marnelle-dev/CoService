using COService.Application.Repositories;
using COService.Domain.Entities;
using COService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace COService.Infrastructure.Repositories;

/// <summary>
/// Implémentation du repository pour les devises
/// </summary>
public class DeviseRepository : Repository<Devise>, IDeviseRepository
{
    public DeviseRepository(COServiceDbContext context) : base(context)
    {
    }

    public override async Task<Devise?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<Devise?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(d => d.Code == code, cancellationToken);
    }

    public override async Task<IEnumerable<Devise>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .OrderBy(d => d.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Devise>> GetActifsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(d => d.Actif)
            .OrderBy(d => d.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(d => d.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(d => d.Code == code, cancellationToken);
    }

    public new async Task<Devise> AddAsync(Devise devise, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(devise, cancellationToken);
        return devise;
    }

    public new void Update(Devise devise)
    {
        _dbSet.Update(devise);
    }
}
