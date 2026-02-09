using COService.Domain.Entities;

namespace COService.Application.Repositories;

/// <summary>
/// Repository pour les certificats d'origine
/// </summary>
public interface ICertificatOrigineRepository
{
    Task<CertificatOrigine?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<CertificatOrigine?> GetByCertificateNoAsync(string certificateNo, CancellationToken cancellationToken = default);
    Task<IEnumerable<CertificatOrigine>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<CertificatOrigine>> GetByExportateurAsync(string exportateur, CancellationToken cancellationToken = default);
    Task<IEnumerable<CertificatOrigine>> GetByExportateurIdAsync(Guid exportateurId, CancellationToken cancellationToken = default);
    Task<IEnumerable<CertificatOrigine>> GetByStatutAsync(Guid statutCertificatId, CancellationToken cancellationToken = default);
    Task<IEnumerable<CertificatOrigine>> GetByStatutNomAsync(string statutNom, CancellationToken cancellationToken = default);
    Task<IEnumerable<CertificatOrigine>> GetByPaysDestinationAsync(string paysDestination, CancellationToken cancellationToken = default);
    Task<IEnumerable<CertificatOrigine>> GetByPaysDestinationIdAsync(Guid paysDestinationId, CancellationToken cancellationToken = default);
    Task<CertificatOrigine> AddAsync(CertificatOrigine certificat, CancellationToken cancellationToken = default);
    void Update(CertificatOrigine certificat);
    void Remove(CertificatOrigine certificat);
    Task<bool> ExistsAsync(string certificateNo, CancellationToken cancellationToken = default);
}

