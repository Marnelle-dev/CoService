namespace COService.Domain.Entities;

/// <summary>
/// Entité représentant un tronçon
/// Synchronisée depuis le microservice Référentiel Global
/// </summary>
public class Troncon
{
    /// <summary>
    /// Identifiant unique du tronçon
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Code unique du tronçon
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Description du tronçon
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Corridor (FK vers Corridors)
    /// </summary>
    public Guid? CorridorId { get; set; }

    /// <summary>
    /// Corridor (navigation property)
    /// </summary>
    public Corridor? Corridor { get; set; }

    /// <summary>
    /// Route nationale (FK vers RoutesNationales)
    /// </summary>
    public Guid? RouteId { get; set; }

    /// <summary>
    /// Route nationale (navigation property)
    /// </summary>
    public RouteNationale? Route { get; set; }

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
