namespace COService.Domain.Entities;

/// <summary>
/// Entité représentant une route nationale
/// Synchronisée depuis le microservice Référentiel Global
/// </summary>
public class RouteNationale
{
    /// <summary>
    /// Identifiant unique de la route nationale
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Code unique de la route
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Nom de la route nationale
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
    /// Tronçons de cette route
    /// </summary>
    public ICollection<Troncon> Troncons { get; set; } = new List<Troncon>();
}
