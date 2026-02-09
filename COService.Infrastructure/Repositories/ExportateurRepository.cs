using COService.Application.Repositories;
using COService.Domain.Entities;
using COService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace COService.Infrastructure.Repositories;

/// <summary>
/// Implémentation du repository pour les exportateurs
/// </summary>
public class ExportateurRepository : Repository<Exportateur>, IExportateurRepository
{
    public ExportateurRepository(COServiceDbContext context) : base(context)
    {
    }

    public override async Task<Exportateur?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(e => e.Partenaire)
            .Include(e => e.Departement)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<Exportateur?> GetByCodeAsync(string codeExportateur, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(e => e.Partenaire)
            .Include(e => e.Departement)
            .FirstOrDefaultAsync(e => e.CodeExportateur == codeExportateur, cancellationToken);
    }

    public override async Task<IEnumerable<Exportateur>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(e => e.Partenaire)
            .Include(e => e.Departement)
            .OrderBy(e => e.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Exportateur>> GetActifsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(e => e.Partenaire)
            .Include(e => e.Departement)
            .Where(e => e.Actif)
            .OrderBy(e => e.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Exportateur>> GetByPartenaireAsync(Guid partenaireId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(e => e.Partenaire)
            .Include(e => e.Departement)
            .Where(e => e.PartenaireId == partenaireId)
            .OrderBy(e => e.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Exportateur>> GetByDepartementAsync(Guid departementId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(e => e.Partenaire)
            .Include(e => e.Departement)
            .Where(e => e.DepartementId == departementId)
            .OrderBy(e => e.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Exportateur>> GetByTypeAsync(int typeExportateur, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(e => e.Partenaire)
            .Include(e => e.Departement)
            .Where(e => e.TypeExportateur == typeExportateur)
            .OrderBy(e => e.Nom)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByCodeAsync(string codeExportateur, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(e => e.CodeExportateur == codeExportateur, cancellationToken);
    }

    public new async Task<Exportateur> AddAsync(Exportateur exportateur, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(exportateur, cancellationToken);
        return exportateur;
    }

    public new void Update(Exportateur exportateur)
    {
        _dbSet.Update(exportateur);
    }
}
