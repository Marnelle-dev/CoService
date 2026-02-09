namespace COService.Application.Services;

/// <summary>
/// Service pour la génération de PDFs de certificats
/// </summary>
public interface IPDFGenerationService
{
    /// <summary>
    /// Génère un PDF de certificat d'origine standard (formule='CO')
    /// </summary>
    Task<byte[]> GenererPDFCertificatOrigineAsync(Guid certificatId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Génère un PDF de certificat d'origine pour Ouesso (selon type partenaire)
    /// </summary>
    Task<byte[]> GenererPDFCertificatOuessoAsync(Guid certificatId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Génère un PDF de Formule A (statut 15 requis)
    /// </summary>
    Task<byte[]> GenererPDFFormuleAAsync(Guid certificatId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Génère un PDF EUR.1 (formule='EUR-1')
    /// </summary>
    Task<byte[]> GenererPDFEUR1Async(Guid certificatId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Génère un PDF ALC (formule='CO+ALC')
    /// </summary>
    Task<byte[]> GenererPDFALCAsync(Guid certificatId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Génère un PDF CO+Formule A cargo (formule='B')
    /// </summary>
    Task<byte[]> GenererPDFFormuleACargoAsync(Guid certificatId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Détecte automatiquement le type de certificat et génère le PDF approprié
    /// </summary>
    Task<byte[]> GenererPDFParTypeAsync(Guid certificatId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Génère un QR Code pour un certificat
    /// </summary>
    Task<string> GenererQRCodeAsync(Guid certificatId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Ajoute une signature numérique au PDF
    /// </summary>
    Task<byte[]> AjouterSignatureAsync(byte[] pdfBytes, string userId, CancellationToken cancellationToken = default);
}
