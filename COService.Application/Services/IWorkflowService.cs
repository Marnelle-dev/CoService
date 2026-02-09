using COService.Application.DTOs;

namespace COService.Application.Services;

/// <summary>
/// Service pour gérer les workflows de validation des certificats
/// </summary>
public interface IWorkflowService
{
    /// <summary>
    /// Soumet un certificat pour validation (Élaboré → Soumis)
    /// </summary>
    Task<CertificatOrigineDto> SoumettreCertificatAsync(Guid certificatId, string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Contrôle un certificat (Soumis → Contrôlé)
    /// </summary>
    Task<CertificatOrigineDto> ControleCertificatAsync(Guid certificatId, string userId, string password, CancellationToken cancellationToken = default);

    /// <summary>
    /// Approuve un certificat (Contrôlé → Approuvé)
    /// </summary>
    Task<CertificatOrigineDto> ApprouverCertificatAsync(Guid certificatId, string userId, string password, CancellationToken cancellationToken = default);

    /// <summary>
    /// Valide définitivement un certificat (Approuvé → Validé)
    /// </summary>
    Task<CertificatOrigineDto> ValiderCertificatAsync(Guid certificatId, string userId, string password, CancellationToken cancellationToken = default);

    /// <summary>
    /// Rejette un certificat (vers Rejeté)
    /// </summary>
    Task<CertificatOrigineDto> RejeterCertificatAsync(Guid certificatId, string userId, string password, string commentaire, CancellationToken cancellationToken = default);

    /// <summary>
    /// Demande une modification (Validé → Modification)
    /// </summary>
    Task<CertificatOrigineDto> DemanderModificationAsync(Guid certificatId, string userId, string commentaire, CancellationToken cancellationToken = default);

    /// <summary>
    /// Vérifie si une transition est valide pour un certificat donné
    /// </summary>
    Task<bool> EstTransitionValideAsync(Guid certificatId, string codeNouveauStatut, string userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Récupère les transitions possibles pour un certificat
    /// </summary>
    Task<List<string>> GetTransitionsPossiblesAsync(Guid certificatId, string userId, CancellationToken cancellationToken = default);
}
