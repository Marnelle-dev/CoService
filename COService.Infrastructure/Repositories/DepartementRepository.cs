using COService.Application.Repositories;
using COService.Domain.Entities;
using COService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace COService.Infrastructure.Repositories;

/// <summary>
/// Implémentation du repository pour les départements
/// </summary>
public class DepartementRepository : Repository<Departement>, IDepartementRepository
{
    public DepartementRepository(COServiceDbContext context) : base(context)
    {
    }

    public override async Task<Departement?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<Departement?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(d => d.Code == code, cancellationToken);
    }

    public override async Task<IEnumerable<Departement>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .OrderBy(d => d.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Departement>> GetActifsAsync(CancellationToken cancellationToken = default)
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
}
