namespace COService.Domain.Entities;

/// <summary>
/// Entité représentant une position tarifaire
/// Synchronisée depuis le microservice Référentiel Global
/// </summary>
public class PositionTarifaire
{
    /// <summary>
    /// Identifiant unique de la position tarifaire
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Code unique de la position tarifaire (ex: code HS)
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Description de la position tarifaire
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Catégorie tarifaire (FK vers CategoriesTariffaires)
    /// </summary>
    public Guid? CategorieCodeId { get; set; }

    /// <summary>
    /// Catégorie tarifaire (navigation property)
    /// </summary>
    public CategorieTarifaire? Categorie { get; set; }

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
