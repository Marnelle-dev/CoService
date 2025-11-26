using COService.Application.DTOs;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des lignes de certificat
/// </summary>
public interface ICertificateLineService
{
    Task<CertificateLineDto> CreerLigneAsync(Guid certificateId, CreerCertificateLineDto dto, string? utilisateur = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<CertificateLineDto>> GetLignesByCertificateIdAsync(Guid certificateId, CancellationToken cancellationToken = default);
    Task<CertificateLineDto?> GetLigneByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<CertificateLineDto> ModifierLigneAsync(Guid id, ModifierCertificateLineDto dto, string? utilisateur = null, CancellationToken cancellationToken = default);
    Task SupprimerLigneAsync(Guid id, CancellationToken cancellationToken = default);
}

