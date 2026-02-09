namespace COService.Domain.Entities;

/// <summary>
/// Ligne de produit dans un certificat d'origine
/// </summary>
public class CertificateLine
{
    /// <summary>
    /// Identifiant unique de la ligne
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Référence au certificat
    /// </summary>
    public Guid CertificateId { get; set; }

    /// <summary>
    /// Référence à la position tarifaire (Code HS)
    /// </summary>
    public Guid? PositionTarifaireId { get; set; }

    /// <summary>
    /// Position tarifaire (Code HS)
    /// </summary>
    public PositionTarifaire? PositionTarifaire { get; set; }

    /// <summary>
    /// Code HS (conservé pour compatibilité, mais devrait utiliser PositionTarifaireId)
    /// </summary>
    public string? HSCode { get; set; }

    /// <summary>
    /// Nature du produit
    /// </summary>
    public string? LineNatureOfProduct { get; set; }

    /// <summary>
    /// Quantité
    /// </summary>
    public string? LineQuantity { get; set; }

    /// <summary>
    /// Référence à l'unité statistique
    /// </summary>
    public Guid? UniteStatistiqueId { get; set; }

    /// <summary>
    /// Unité statistique
    /// </summary>
    public UniteStatistique? UniteStatistique { get; set; }

    /// <summary>
    /// Unité de mesure (conservé pour compatibilité, mais devrait utiliser UniteStatistiqueId)
    /// </summary>
    public string? LineUnits { get; set; }

    /// <summary>
    /// Poids brut
    /// </summary>
    public string? LineGrossWeight { get; set; }

    /// <summary>
    /// Poids net
    /// </summary>
    public string? LineNetWeight { get; set; }

    /// <summary>
    /// Valeur FOB
    /// </summary>
    public string? LineFOBValue { get; set; }

    /// <summary>
    /// Référence à la devise
    /// </summary>
    public Guid? DeviseId { get; set; }

    /// <summary>
    /// Devise
    /// </summary>
    public Devise? Devise { get; set; }

    /// <summary>
    /// Référence à l'incoterm
    /// </summary>
    public Guid? IncotermId { get; set; }

    /// <summary>
    /// Incoterm
    /// </summary>
    public Incoterm? Incoterm { get; set; }

    /// <summary>
    /// Volume
    /// </summary>
    public string? LineVolume { get; set; }

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

    // Navigation property
    /// <summary>
    /// Certificat parent
    /// </summary>
    public CertificatOrigine CertificatOrigine { get; set; } = null!;
}

