namespace COService.Domain.Entities;

/// <summary>
/// Entité représentant un port (maritime ou fluvial)
/// Synchronisée depuis le microservice Référentiel Global
/// </summary>
public class Port
{
    /// <summary>
    /// Identifiant unique du port
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Code unique du port
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Nom du port
    /// </summary>
    public string Nom { get; set; } = string.Empty;

    /// <summary>
    /// Pays du port (FK vers Pays)
    /// </summary>
    public Guid? PaysId { get; set; }

    /// <summary>
    /// Pays (navigation property)
    /// </summary>
    public Pays? Pays { get; set; }

    /// <summary>
    /// Type de port (Maritime, Fluvial)
    /// </summary>
    public string? Type { get; set; }

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
