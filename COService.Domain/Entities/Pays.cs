namespace COService.Domain.Entities;

/// <summary>
/// Entité représentant un pays
/// Synchronisée depuis le microservice Référentiel Global
/// </summary>
public class Pays
{
    /// <summary>
    /// Identifiant unique du pays
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Code ISO du pays (ex: "FR", "CG", "US")
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Nom du pays
    /// </summary>
    public string Nom { get; set; } = string.Empty;

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
    /// Ports de ce pays
    /// </summary>
    public ICollection<Port> Ports { get; set; } = new List<Port>();

    /// <summary>
    /// Aéroports de ce pays
    /// </summary>
    public ICollection<Aeroport> Aeroports { get; set; } = new List<Aeroport>();
}
