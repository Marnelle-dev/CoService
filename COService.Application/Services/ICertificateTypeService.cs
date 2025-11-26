using COService.Application.DTOs;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des types de certificats
/// </summary>
public interface ICertificateTypeService
{
    Task<CertificateTypeDto> CreerCertificateTypeAsync(CreerCertificateTypeDto dto, string? utilisateur = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<CertificateTypeDto>> GetAllCertificateTypesAsync(CancellationToken cancellationToken = default);
    Task<CertificateTypeDto?> GetCertificateTypeByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<CertificateTypeDto?> GetCertificateTypeByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<CertificateTypeDto> ModifierCertificateTypeAsync(Guid id, ModifierCertificateTypeDto dto, string? utilisateur = null, CancellationToken cancellationToken = default);
    Task SupprimerCertificateTypeAsync(Guid id, CancellationToken cancellationToken = default);
}

