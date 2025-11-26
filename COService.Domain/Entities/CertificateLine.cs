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
    /// Code HS
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
    /// Unité de mesure
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

