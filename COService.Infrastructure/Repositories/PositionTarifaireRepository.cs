using COService.Application.Repositories;
using COService.Domain.Entities;
using COService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace COService.Infrastructure.Repositories;

/// <summary>
/// Implémentation du repository pour les positions tarifaires
/// </summary>
public class PositionTarifaireRepository : Repository<PositionTarifaire>, IPositionTarifaireRepository
{
    public PositionTarifaireRepository(COServiceDbContext context) : base(context)
    {
    }

    public override async Task<PositionTarifaire?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Categorie)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<PositionTarifaire?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Categorie)
            .FirstOrDefaultAsync(p => p.Code == code, cancellationToken);
    }

    public override async Task<IEnumerable<PositionTarifaire>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Categorie)
            .OrderBy(p => p.Code)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<PositionTarifaire>> GetActifsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Categorie)
            .Where(p => p.Actif)
            .OrderBy(p => p.Code)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<PositionTarifaire>> GetByCategorieAsync(Guid categorieId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Categorie)
            .Where(p => p.CategorieCodeId == categorieId)
            .OrderBy(p => p.Code)
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
}
