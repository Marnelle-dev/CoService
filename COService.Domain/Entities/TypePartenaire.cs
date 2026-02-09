namespace COService.Domain.Entities;

/// <summary>
/// Entité représentant un type de partenaire (Chambre de Commerce)
/// </summary>
public class TypePartenaire
{
    /// <summary>
    /// Identifiant unique du type de partenaire
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Code unique du type
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Nom du type
    /// </summary>
    public string Nom { get; set; } = string.Empty;

    /// <summary>
    /// Description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Statut d'activation
    /// </summary>
    public bool Actif { get; set; } = true;

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
    /// Partenaires de ce type
    /// </summary>
    public ICollection<Partenaire> Partenaires { get; set; } = new List<Partenaire>();
}
