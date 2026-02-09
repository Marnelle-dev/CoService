using COService.Application.DTOs;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des statuts de certificats
/// </summary>
public interface IStatutCertificatService
{
    Task<IEnumerable<StatutCertificatDto>> GetAllStatutsAsync(CancellationToken cancellationToken = default);
    Task<StatutCertificatDto?> GetStatutByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<StatutCertificatDto?> GetStatutByCodeAsync(string code, CancellationToken cancellationToken = default);
}
