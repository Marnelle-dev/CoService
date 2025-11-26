using COService.Domain.Entities;

namespace COService.Application.Repositories;

/// <summary>
/// Repository pour les types de certificats
/// </summary>
public interface ICertificateTypeRepository
{
    Task<CertificateType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<CertificateType?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<CertificateType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CertificateType> AddAsync(CertificateType certificateType, CancellationToken cancellationToken = default);
    void Update(CertificateType certificateType);
    void Remove(CertificateType certificateType);
    Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken = default);
}

