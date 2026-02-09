namespace COService.Domain.Entities;

/// <summary>
/// Entité représentant un mode de transport (Module)
/// Synchronisée depuis le microservice Référentiel Global
/// </summary>
public class Module
{
    /// <summary>
    /// Identifiant unique du module
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Code unique du module
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Nom du module (ex: "Aérien", "Maritime", "Fluvial", "Routier")
    /// </summary>
    public string Nom { get; set; } = string.Empty;

    /// <summary>
    /// Description du module
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
    /// Incoterms associés à ce module
    /// </summary>
    public ICollection<Incoterm> Incoterms { get; set; } = new List<Incoterm>();
}
