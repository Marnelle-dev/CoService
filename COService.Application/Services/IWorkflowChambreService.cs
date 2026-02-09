using COService.Application.DTOs;

namespace COService.Application.Services;

/// <summary>
/// Interface pour les services de workflow spécifiques à une chambre de commerce
/// </summary>
internal interface IWorkflowChambreService
{
    Task<CertificatOrigineDto> SoumettreCertificatAsync(Guid certificatId, string userId, CancellationToken cancellationToken = default);
    Task<CertificatOrigineDto> ControleCertificatAsync(Guid certificatId, string userId, string password, CancellationToken cancellationToken = default);
    Task<CertificatOrigineDto> ApprouverCertificatAsync(Guid certificatId, string userId, string password, CancellationToken cancellationToken = default);
    Task<CertificatOrigineDto> ValiderCertificatAsync(Guid certificatId, string userId, string password, CancellationToken cancellationToken = default);
    Task<CertificatOrigineDto> RejeterCertificatAsync(Guid certificatId, string userId, string password, string commentaire, CancellationToken cancellationToken = default);
    Task<CertificatOrigineDto> DemanderModificationAsync(Guid certificatId, string userId, string commentaire, CancellationToken cancellationToken = default);
    Task<bool> EstTransitionValideAsync(Guid certificatId, string codeNouveauStatut, string userId, CancellationToken cancellationToken = default);
    Task<List<string>> GetTransitionsPossiblesAsync(Guid certificatId, string userId, CancellationToken cancellationToken = default);
}
