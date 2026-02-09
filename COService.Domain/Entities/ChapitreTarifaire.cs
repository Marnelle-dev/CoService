namespace COService.Domain.Entities;

/// <summary>
/// Entité représentant un chapitre tarifaire
/// Synchronisée depuis le microservice Référentiel Global
/// </summary>
public class ChapitreTarifaire
{
    /// <summary>
    /// Identifiant unique du chapitre tarifaire
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Code unique du chapitre tarifaire
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Description du chapitre tarifaire
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Section tarifaire (FK vers SectionsTariffaires)
    /// </summary>
    public Guid? SectionTarifaireId { get; set; }

    /// <summary>
    /// Section tarifaire (navigation property)
    /// </summary>
    public SectionTarifaire? SectionTarifaire { get; set; }

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
    /// Divisions tarifaires de ce chapitre
    /// </summary>
    public ICollection<DivisionTarifaire> DivisionsTariffaires { get; set; } = new List<DivisionTarifaire>();
}
