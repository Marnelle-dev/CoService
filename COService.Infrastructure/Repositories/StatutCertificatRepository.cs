using COService.Application.Repositories;
using COService.Domain.Entities;
using COService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace COService.Infrastructure.Repositories;

/// <summary>
/// Implémentation du repository pour les statuts de certificats
/// </summary>
public class StatutCertificatRepository : Repository<StatutCertificat>, IStatutCertificatRepository
{
    public StatutCertificatRepository(COServiceDbContext context) : base(context)
    {
    }

    public async Task<StatutCertificat?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(s => s.Code == code, cancellationToken);
    }

    public async Task<bool> ExistsAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .AnyAsync(s => s.Code == code, cancellationToken);
    }
}
