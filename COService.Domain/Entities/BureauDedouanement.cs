namespace COService.Domain.Entities;

/// <summary>
/// Entité représentant un bureau de dédouanement
/// Synchronisée depuis le microservice Référentiel Global
/// </summary>
public class BureauDedouanement
{
    /// <summary>
    /// Identifiant unique du bureau de dédouanement
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Code unique du bureau
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Description du bureau
    /// </summary>
    public string Description { get; set; } = string.Empty;

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
