using COService.Application.Repositories;
using COService.Domain.Entities;
using COService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace COService.Infrastructure.Repositories;

/// <summary>
/// Implémentation du repository pour les unités statistiques
/// </summary>
public class UniteStatistiqueRepository : Repository<UniteStatistique>, IUniteStatistiqueRepository
{
    public UniteStatistiqueRepository(COServiceDbContext context) : base(context)
    {
    }

    public override async Task<UniteStatistique?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<UniteStatistique?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Code == code, cancellationToken);
    }

    public override async Task<IEnumerable<UniteStatistique>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .OrderBy(u => u.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<UniteStatistique>> GetActifsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(u => u.Actif)
            .OrderBy(u => u.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(u => u.Code == code, cancellationToken);
    }
}
