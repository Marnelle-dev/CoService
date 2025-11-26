using COService.Application.Repositories;
using COService.Infrastructure.Data;

namespace COService.Infrastructure.Repositories;

/// <summary>
/// Implémentation de l'unité de travail
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly COServiceDbContext _context;

    public UnitOfWork(COServiceDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }
}

