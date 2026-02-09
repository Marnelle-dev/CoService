namespace COService.Domain.Entities;

/// <summary>
/// Entité représentant un incoterm
/// Synchronisée depuis le microservice Référentiel Global
/// </summary>
public class Incoterm
{
    /// <summary>
    /// Identifiant unique de l'incoterm
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Code de l'incoterm (ex: "FOB", "CIF", "EXW")
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Description de l'incoterm
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Module associé (FK vers Modules)
    /// </summary>
    public Guid? ModuleId { get; set; }

    /// <summary>
    /// Module (navigation property)
    /// </summary>
    public Module? Module { get; set; }

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
