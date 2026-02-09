using COService.Application.Repositories;
using COService.Domain.Entities;
using COService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace COService.Infrastructure.Repositories;

/// <summary>
/// Implémentation du repository pour les partenaires
/// </summary>
public class PartenaireRepository : Repository<Partenaire>, IPartenaireRepository
{
    public PartenaireRepository(COServiceDbContext context) : base(context)
    {
    }

    public override async Task<Partenaire?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.TypePartenaire)
            .Include(p => p.Departement)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Partenaire?> GetByCodeAsync(string codePartenaire, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.TypePartenaire)
            .Include(p => p.Departement)
            .FirstOrDefaultAsync(p => p.CodePartenaire == codePartenaire, cancellationToken);
    }

    public override async Task<IEnumerable<Partenaire>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.TypePartenaire)
            .Include(p => p.Departement)
            .OrderBy(p => p.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Partenaire>> GetActifsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.TypePartenaire)
            .Include(p => p.Departement)
            .Where(p => p.Actif)
            .OrderBy(p => p.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Partenaire>> GetByTypeAsync(Guid typePartenaireId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.TypePartenaire)
            .Include(p => p.Departement)
            .Where(p => p.TypePartenaireId == typePartenaireId)
            .OrderBy(p => p.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Partenaire>> GetByDepartementAsync(Guid departementId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.TypePartenaire)
            .Include(p => p.Departement)
            .Where(p => p.DepartementId == departementId)
            .OrderBy(p => p.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByCodeAsync(string codePartenaire, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(p => p.CodePartenaire == codePartenaire, cancellationToken);
    }

    public new async Task<Partenaire> AddAsync(Partenaire partenaire, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(partenaire, cancellationToken);
        return partenaire;
    }

    public new void Update(Partenaire partenaire)
    {
        _dbSet.Update(partenaire);
    }
}
