using COService.Application.Repositories;
using COService.Domain.Entities;
using COService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace COService.Infrastructure.Repositories;

/// <summary>
/// Implémentation du repository pour les types de certificats
/// </summary>
public class CertificateTypeRepository : Repository<CertificateType>, ICertificateTypeRepository
{
    public CertificateTypeRepository(COServiceDbContext context) : base(context)
    {
    }

    public override async Task<CertificateType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(ct => ct.Certificats)
            .FirstOrDefaultAsync(ct => ct.Id == id, cancellationToken);
    }

    public override async Task<IEnumerable<CertificateType>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(ct => ct.Certificats)
            .OrderBy(ct => ct.Code)
            .ToListAsync(cancellationToken);
    }

    public async Task<CertificateType?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(ct => ct.Certificats)
            .FirstOrDefaultAsync(ct => ct.Code == code, cancellationToken);
    }

    public async Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(ct => ct.Code == code, cancellationToken);
    }
}

