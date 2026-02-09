namespace COService.Domain.Entities;

/// <summary>
/// Entité représentant un partenaire (Chambre de Commerce)
/// Synchronisée depuis le microservice Enrolement
/// </summary>
public class Partenaire
{
    /// <summary>
    /// Identifiant unique du partenaire (depuis Enrolement)
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Code unique du partenaire
    /// </summary>
    public string CodePartenaire { get; set; } = string.Empty;

    /// <summary>
    /// Nom de la chambre de commerce
    /// </summary>
    public string Nom { get; set; } = string.Empty;

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
    /// Type de partenaire (FK vers TypesPartenaires)
    /// </summary>
    public Guid? TypePartenaireId { get; set; }

    /// <summary>
    /// Type de partenaire (navigation property)
    /// </summary>
    public TypePartenaire? TypePartenaire { get; set; }

    /// <summary>
    /// Département (FK vers Departements)
    /// </summary>
    public Guid? DepartementId { get; set; }

    /// <summary>
    /// Département (navigation property)
    /// </summary>
    public Departement? Departement { get; set; }

    /// <summary>
    /// Statut d'activation
    /// </summary>
    public bool Actif { get; set; } = true;

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
    /// Certificats créés par ce partenaire
    /// </summary>
    public ICollection<CertificatOrigine> Certificats { get; set; } = new List<CertificatOrigine>();

    /// <summary>
    /// Exportateurs associés à ce partenaire
    /// </summary>
    public ICollection<Exportateur> Exportateurs { get; set; } = new List<Exportateur>();

    /// <summary>
    /// Zones de production gérées par ce partenaire
    /// </summary>
    public ICollection<ZoneProduction> ZonesProductions { get; set; } = new List<ZoneProduction>();
}
