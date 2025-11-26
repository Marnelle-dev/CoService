namespace COService.Domain.Entities;

/// <summary>
/// Abonnement créé par la chambre de commerce pour un exportateur
/// Un abonnement peut contenir plusieurs certificats d'origine
/// </summary>
public class Abonnement
{
    /// <summary>
    /// Identifiant unique de l'abonnement
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Exportateur bénéficiaire de l'abonnement
    /// </summary>
    public string Exportateur { get; set; } = string.Empty;

    /// <summary>
    /// Partenaire/Chambre de commerce qui crée l'abonnement
    /// </summary>
    public string Partenaire { get; set; } = string.Empty;

    /// <summary>
    /// Type de certificat d'origine (EUR.1, Formule A, etc.)
    /// </summary>
    public string? TypeCO { get; set; }

    /// <summary>
    /// Numéro de facture
    /// </summary>
    public string? FactureNo { get; set; }

    /// <summary>
    /// Formule
    /// </summary>
    public string? Formule { get; set; }

    /// <summary>
    /// Numéro d'abonnement
    /// </summary>
    public string? Numero { get; set; }

    /// <summary>
    /// Statut de facturation
    /// </summary>
    public string? Statut { get; set; }

    /// <summary>
    /// Référence au certificat principal (optionnel)
    /// </summary>
    public Guid? CertificateId { get; set; }

    /// <summary>
    /// Certificat principal de l'abonnement
    /// </summary>
    public CertificatOrigine? Certificate { get; set; }

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
    /// Certificats d'origine rattachés à cet abonnement
    /// </summary>
    public ICollection<CertificatOrigine> Certificats { get; set; } = new List<CertificatOrigine>();
}

