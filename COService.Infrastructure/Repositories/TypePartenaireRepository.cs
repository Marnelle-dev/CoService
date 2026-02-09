using COService.Application.Repositories;
using COService.Domain.Entities;
using COService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace COService.Infrastructure.Repositories;

/// <summary>
/// Implémentation du repository pour les types de partenaires
/// </summary>
public class TypePartenaireRepository : Repository<TypePartenaire>, ITypePartenaireRepository
{
    public TypePartenaireRepository(COServiceDbContext context) : base(context)
    {
    }

    public override async Task<TypePartenaire?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<TypePartenaire?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(tp => tp.Code == code, cancellationToken);
    }

    public override async Task<IEnumerable<TypePartenaire>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .OrderBy(tp => tp.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TypePartenaire>> GetActifsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(tp => tp.Actif)
            .OrderBy(tp => tp.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(tp => tp.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(tp => tp.Code == code, cancellationToken);
    }
}
