namespace COService.Domain.Entities;

/// <summary>
/// Type de certificat d'origine (EUR.1, Formule A, etc.)
/// </summary>
public class CertificateType
{
    /// <summary>
    /// Identifiant unique du type
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Désignation du type
    /// </summary>
    public string Designation { get; set; } = string.Empty;

    /// <summary>
    /// Code du type
    /// </summary>
    public string Code { get; set; } = string.Empty;

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
    /// Certificats de ce type
    /// </summary>
    public ICollection<CertificatOrigine> Certificats { get; set; } = new List<CertificatOrigine>();
}

