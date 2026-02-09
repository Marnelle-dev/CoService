using COService.Application.Repositories;
using COService.Domain.Entities;
using COService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace COService.Infrastructure.Repositories;

/// <summary>
/// Implémentation du repository pour les ports
/// </summary>
public class PortRepository : Repository<Port>, IPortRepository
{
    public PortRepository(COServiceDbContext context) : base(context)
    {
    }

    public override async Task<Port?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Pays)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Port?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Pays)
            .FirstOrDefaultAsync(p => p.Code == code, cancellationToken);
    }

    public override async Task<IEnumerable<Port>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Pays)
            .OrderBy(p => p.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Port>> GetActifsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Pays)
            .Where(p => p.Actif)
            .OrderBy(p => p.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Port>> GetByPaysAsync(Guid paysId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Pays)
            .Where(p => p.PaysId == paysId)
            .OrderBy(p => p.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Port>> GetByTypeAsync(string type, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Pays)
            .Where(p => p.Type == type)
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

    public new async Task<Port> AddAsync(Port port, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(port, cancellationToken);
        return port;
    }

    public new void Update(Port port)
    {
        _dbSet.Update(port);
    }
}
