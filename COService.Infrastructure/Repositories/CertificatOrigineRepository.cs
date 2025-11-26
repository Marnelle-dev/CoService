using COService.Application.Repositories;
using COService.Domain.Entities;
using COService.Domain.Enums;
using COService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace COService.Infrastructure.Repositories;

/// <summary>
/// Implémentation du repository pour les certificats d'origine
/// </summary>
public class CertificatOrigineRepository : Repository<CertificatOrigine>, ICertificatOrigineRepository
{
    public CertificatOrigineRepository(COServiceDbContext context) : base(context)
    {
    }

    public override async Task<CertificatOrigine?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(c => c.CertificateLines)
            .Include(c => c.CertificateValidations)
            .Include(c => c.Commentaires)
            .Include(c => c.Abonnement)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<CertificatOrigine?> GetByCertificateNoAsync(string certificateNo, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(c => c.CertificateLines)
            .Include(c => c.CertificateValidations)
            .Include(c => c.Commentaires)
            .Include(c => c.Abonnement)
            .FirstOrDefaultAsync(c => c.CertificateNo == certificateNo, cancellationToken);
    }

    public async Task<IEnumerable<CertificatOrigine>> GetByExportateurAsync(string exportateur, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(c => c.Exportateur.Contains(exportateur))
            .OrderByDescending(c => c.CreeLe)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<CertificatOrigine>> GetByStatutAsync(StatutCertificat statut, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(c => c.Statut == statut)
            .OrderByDescending(c => c.CreeLe)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<CertificatOrigine>> GetByPaysDestinationAsync(string paysDestination, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(c => c.PaysDestination == paysDestination)
            .OrderByDescending(c => c.CreeLe)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string certificateNo, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(c => c.CertificateNo == certificateNo, cancellationToken);
    }
}

