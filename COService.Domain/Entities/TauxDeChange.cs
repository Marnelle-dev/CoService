namespace COService.Domain.Entities;

/// <summary>
/// Entité représentant un taux de change
/// Synchronisée depuis le microservice Référentiel Global
/// </summary>
public class TauxDeChange
{
    /// <summary>
    /// Identifiant unique du taux de change
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Devise (FK vers Devises)
    /// </summary>
    public Guid DeviseId { get; set; }

    /// <summary>
    /// Devise (navigation property)
    /// </summary>
    public Devise Devise { get; set; } = null!;

    /// <summary>
    /// Source du taux (ex: "Banque Centrale", "Marché")
    /// </summary>
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// Taux de change
    /// </summary>
    public decimal Taux { get; set; }

    /// <summary>
    /// Date de validité (format string)
    /// </summary>
    public string? ValideDe { get; set; }

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
