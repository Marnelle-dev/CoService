namespace COService.Domain.Entities;

/// <summary>
/// Entité représentant une devise
/// Synchronisée depuis le microservice Référentiel Global
/// </summary>
public class Devise
{
    /// <summary>
    /// Identifiant unique de la devise
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Code ISO de la devise (ex: "EUR", "USD", "XAF")
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Nom de la devise
    /// </summary>
    public string Nom { get; set; } = string.Empty;

    /// <summary>
    /// Symbole de la devise (ex: "€", "$", "FCFA")
    /// </summary>
    public string? Symbole { get; set; }

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
    /// Taux de change pour cette devise
    /// </summary>
    public ICollection<TauxDeChange> TauxDeChanges { get; set; } = new List<TauxDeChange>();
}
