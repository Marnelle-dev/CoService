namespace COService.Domain.Entities;

/// <summary>
/// Entité représentant une catégorie tarifaire
/// Synchronisée depuis le microservice Référentiel Global
/// </summary>
public class CategorieTarifaire
{
    /// <summary>
    /// Identifiant unique de la catégorie tarifaire
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Code unique de la catégorie tarifaire
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Description de la catégorie tarifaire
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Code de la division (string pour référence)
    /// </summary>
    public string? DivisionCode { get; set; }

    /// <summary>
    /// Division tarifaire (FK vers DivisionsTariffaires)
    /// </summary>
    public Guid? DivisionId { get; set; }

    /// <summary>
    /// Division tarifaire (navigation property)
    /// </summary>
    public DivisionTarifaire? Division { get; set; }

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
    /// Positions tarifaires de cette catégorie
    /// </summary>
    public ICollection<PositionTarifaire> PositionsTariffaires { get; set; } = new List<PositionTarifaire>();
}
