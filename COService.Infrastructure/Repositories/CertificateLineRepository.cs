using COService.Application.Repositories;
using COService.Domain.Entities;
using COService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace COService.Infrastructure.Repositories;

/// <summary>
/// Implémentation du repository pour les lignes de certificat
/// </summary>
public class CertificateLineRepository : Repository<CertificateLine>, ICertificateLineRepository
{
    public CertificateLineRepository(COServiceDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CertificateLine>> GetByCertificateIdAsync(Guid certificateId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(cl => cl.CertificateId == certificateId)
            .ToListAsync(cancellationToken);
    }
}

