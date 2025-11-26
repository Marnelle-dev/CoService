using COService.Domain.Entities;

namespace COService.Application.Repositories;

/// <summary>
/// Repository pour les lignes de certificat
/// </summary>
public interface ICertificateLineRepository
{
    Task<CertificateLine?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CertificateLine>> GetByCertificateIdAsync(Guid certificateId, CancellationToken cancellationToken = default);
    Task<CertificateLine> AddAsync(CertificateLine line, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<CertificateLine> lines, CancellationToken cancellationToken = default);
    void Update(CertificateLine line);
    void Remove(CertificateLine line);
    void RemoveRange(IEnumerable<CertificateLine> lines);
}

