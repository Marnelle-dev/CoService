using COService.Domain.Enums;

namespace COService.Domain.Entities;

/// <summary>
/// Entité représentant un certificat d'origine
/// </summary>
public class CertificatOrigine
{
    /// <summary>
    /// Identifiant unique du certificat
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Numéro du certificat (unique)
    /// </summary>
    public string CertificateNo { get; set; } = string.Empty;

    /// <summary>
    /// Nom de l'exportateur
    /// </summary>
    public string Exportateur { get; set; } = string.Empty;

    /// <summary>
    /// Partenaire/CCI
    /// </summary>
    public string? Partenaire { get; set; }

    /// <summary>
    /// Pays de destination
    /// </summary>
    public string? PaysDestination { get; set; }

    /// <summary>
    /// Port de sortie
    /// </summary>
    public string? PortSortie { get; set; }

    /// <summary>
    /// Port côté Congo
    /// </summary>
    public string? PortCongo { get; set; }

    /// <summary>
    /// Référence au type de certificat
    /// </summary>
    public Guid? TypeId { get; set; }

    /// <summary>
    /// Type de certificat (EUR.1, Formule A, etc.)
    /// </summary>
    public CertificateType? Type { get; set; }

    /// <summary>
    /// Formule du certificat
    /// </summary>
    public string? Formule { get; set; }

    /// <summary>
    /// Mandataire (optionnel)
    /// </summary>
    public string? Mandataire { get; set; }

    /// <summary>
    /// Statut du dossier
    /// </summary>
    public StatutCertificat Statut { get; set; }

    /// <summary>
    /// Observation interne
    /// </summary>
    public string? Observation { get; set; }

    /// <summary>
    /// Nom du destinataire des produits
    /// </summary>
    public string? ProductsRecipientName { get; set; }

    /// <summary>
    /// Adresse du destinataire (ligne 1)
    /// </summary>
    public string? ProductsRecipientAddress1 { get; set; }

    /// <summary>
    /// Adresse du destinataire (ligne 2)
    /// </summary>
    public string? ProductsRecipientAddress2 { get; set; }

    /// <summary>
    /// Nom du navire
    /// </summary>
    public string? Navire { get; set; }

    /// <summary>
    /// Référence aux documents attachés (microservice Documents)
    /// </summary>
    public Guid? DocumentsId { get; set; }

    // Champs d'audit
    /// <summary>
    /// Date de création
    /// </summary>
    public DateTime CreeLe { get; set; }

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
    /// Lignes de produits du certificat
    /// </summary>
    public ICollection<CertificateLine> CertificateLines { get; set; } = new List<CertificateLine>();

    /// <summary>
    /// Validations du certificat
    /// </summary>
    public ICollection<CertificateValidation> CertificateValidations { get; set; } = new List<CertificateValidation>();

    /// <summary>
    /// Commentaires sur le certificat
    /// </summary>
    public ICollection<Commentaire> Commentaires { get; set; } = new List<Commentaire>();

    /// <summary>
    /// Référence à l'abonnement (optionnel, un certificat peut ne pas avoir d'abonnement)
    /// </summary>
    public Guid? AbonnementId { get; set; }

    /// <summary>
    /// Abonnement auquel ce certificat est rattaché
    /// </summary>
    public Abonnement? Abonnement { get; set; }
}

