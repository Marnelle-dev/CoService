namespace COService.Domain.Entities;

/// <summary>
/// Entité représentant une unité statistique
/// Synchronisée depuis le microservice Référentiel Global
/// </summary>
public class UniteStatistique
{
    /// <summary>
    /// Identifiant unique de l'unité statistique
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Code unique de l'unité statistique
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Nom de l'unité statistique (ex: "kg", "m³", "pièce")
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
}
