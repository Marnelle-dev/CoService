namespace COService.Domain.Entities;

/// <summary>
/// Entité représentant un exportateur
/// Synchronisée depuis le microservice Enrolement
/// </summary>
public class Exportateur
{
    /// <summary>
    /// Identifiant unique de l'exportateur (depuis Enrolement)
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Code unique de l'exportateur
    /// </summary>
    public string CodeExportateur { get; set; } = string.Empty;

    /// <summary>
    /// Nom de l'entreprise
    /// </summary>
    public string Nom { get; set; } = string.Empty;

    /// <summary>
    /// Raison sociale
    /// </summary>
    public string? RaisonSociale { get; set; }

    /// <summary>
    /// Numéro d'Identification Unique
    /// </summary>
    public string? NIU { get; set; }

    /// <summary>
    /// Numéro RCCM
    /// </summary>
    public string? RCCM { get; set; }

    /// <summary>
    /// Code d'activité
    /// </summary>
    public string? CodeActivite { get; set; }

    /// <summary>
    /// Adresse complète
    /// </summary>
    public string? Adresse { get; set; }

    /// <summary>
    /// Numéro de téléphone
    /// </summary>
    public string? Telephone { get; set; }

    /// <summary>
    /// Adresse email
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Statut d'activation
    /// </summary>
    public bool Actif { get; set; } = true;

    /// <summary>
    /// Partenaire principal (FK vers Partenaires)
    /// </summary>
    public Guid? PartenaireId { get; set; }

    /// <summary>
    /// Partenaire principal (navigation property)
    /// </summary>
    public Partenaire? Partenaire { get; set; }

    /// <summary>
    /// Département (FK vers Departements)
    /// </summary>
    public Guid? DepartementId { get; set; }

    /// <summary>
    /// Département (navigation property)
    /// </summary>
    public Departement? Departement { get; set; }

    /// <summary>
    /// Type d'exportateur (ex: 3 pour exportateur spécial pouvant créer Formules A)
    /// </summary>
    public int? TypeExportateur { get; set; }

    /// <summary>
    /// Date de dernière synchronisation depuis Enrolement
    /// </summary>
    public DateTime? DerniereSynchronisation { get; set; }

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
    /// Certificats créés par cet exportateur
    /// </summary>
    public ICollection<CertificatOrigine> Certificats { get; set; } = new List<CertificatOrigine>();
}
