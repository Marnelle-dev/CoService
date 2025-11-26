using COService.Domain.Enums;

namespace COService.Domain.Entities;

/// <summary>
/// Validation/Visa d'un certificat d'origine
/// </summary>
public class CertificateValidation
{
    /// <summary>
    /// Identifiant unique de la validation
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Référence au certificat
    /// </summary>
    public Guid CertificateId { get; set; }

    /// <summary>
    /// Étape du workflow
    /// </summary>
    public EtapeValidation Etape { get; set; }

    /// <summary>
    /// Rôle du validateur
    /// </summary>
    public RoleValidateur RoleVisa { get; set; }

    /// <summary>
    /// Nom de l'agent qui a validé
    /// </summary>
    public string? VisaPar { get; set; }

    /// <summary>
    /// Commentaire de validation
    /// </summary>
    public string? Commentaire { get; set; }

    // Champs d'audit
    /// <summary>
    /// Date de création
    /// </summary>
    public DateTime? CreeLe { get; set; }

    /// <summary>
    /// Utilisateur créateur
    /// </summary>
    public string? CreePar { get; set; }

    /// <summary>
    /// Date de dernière modification
    /// </summary>
    public DateTime? ModifierLe { get; set; }

    /// <summary>
    /// Utilisateur ayant modifié
    /// </summary>
    public string? ModifiePar { get; set; }

    // Navigation property
    /// <summary>
    /// Certificat parent
    /// </summary>
    public CertificatOrigine CertificatOrigine { get; set; } = null!;
}

