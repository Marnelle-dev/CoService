namespace COService.Domain.Entities;

/// <summary>
/// Entité représentant un statut de certificat (référentiel)
/// </summary>
public class StatutCertificat
{
    /// <summary>
    /// Identifiant unique du statut
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Code unique du statut (ex: "ELABORE", "SOUMIS", "CONTROLE", etc.)
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Nom du statut (ex: "Élaboré", "Soumis", "Contrôlé", etc.)
    /// </summary>
    public string Nom { get; set; } = string.Empty;

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

    // Navigation properties
    /// <summary>
    /// Certificats ayant ce statut
    /// </summary>
    public ICollection<CertificatOrigine> Certificats { get; set; } = new List<CertificatOrigine>();
}
