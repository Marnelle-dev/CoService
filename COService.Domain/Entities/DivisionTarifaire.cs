namespace COService.Domain.Entities;

/// <summary>
/// Entité représentant une division tarifaire
/// Synchronisée depuis le microservice Référentiel Global
/// </summary>
public class DivisionTarifaire
{
    /// <summary>
    /// Identifiant unique de la division tarifaire
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Code unique de la division tarifaire
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Description de la division tarifaire
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Chapitre tarifaire (FK vers ChapitresTariffaires)
    /// </summary>
    public Guid? ChapitreId { get; set; }

    /// <summary>
    /// Chapitre tarifaire (navigation property)
    /// </summary>
    public ChapitreTarifaire? Chapitre { get; set; }

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
    /// Catégories tarifaires de cette division
    /// </summary>
    public ICollection<CategorieTarifaire> CategoriesTariffaires { get; set; } = new List<CategorieTarifaire>();
}
