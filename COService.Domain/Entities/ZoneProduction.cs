namespace COService.Domain.Entities;

/// <summary>
/// Entité représentant une zone de production
/// Gérée localement par COService (propre au domaine CO)
/// </summary>
public class ZoneProduction
{
    /// <summary>
    /// Identifiant unique de la zone de production
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Partenaire qui gère cette zone (FK vers Partenaires)
    /// </summary>
    public Guid PartenaireId { get; set; }

    /// <summary>
    /// Partenaire (navigation property)
    /// </summary>
    public Partenaire Partenaire { get; set; } = null!;

    /// <summary>
    /// Nom de la zone de production
    /// </summary>
    public string Nom { get; set; } = string.Empty;

    /// <summary>
    /// Description de la zone
    /// </summary>
    public string? Description { get; set; }

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
    /// Certificats associés à cette zone de production
    /// </summary>
    public ICollection<CertificatOrigine> Certificats { get; set; } = new List<CertificatOrigine>();
}
