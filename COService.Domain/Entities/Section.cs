namespace COService.Domain.Entities;

/// <summary>
/// Entité représentant une section
/// Synchronisée depuis le microservice Référentiel Global
/// </summary>
public class Section
{
    /// <summary>
    /// Identifiant unique de la section
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Code unique de la section
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Description de la section
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
