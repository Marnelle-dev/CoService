namespace COService.Domain.Entities;

/// <summary>
/// Entité représentant un aéroport
/// Synchronisée depuis le microservice Référentiel Global
/// </summary>
public class Aeroport
{
    /// <summary>
    /// Identifiant unique de l'aéroport
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Code unique de l'aéroport (ex: "CDG", "FIH")
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Nom de l'aéroport
    /// </summary>
    public string Nom { get; set; } = string.Empty;

    /// <summary>
    /// Pays de l'aéroport (FK vers Pays)
    /// </summary>
    public Guid? PaysId { get; set; }

    /// <summary>
    /// Pays (navigation property)
    /// </summary>
    public Pays? Pays { get; set; }

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
}
