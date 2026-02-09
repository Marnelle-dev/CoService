namespace COService.Application.DTOs;

/// <summary>
/// DTO pour un certificat d'origine
/// </summary>
public class CertificatOrigineDto
{
    public Guid Id { get; set; }
    public string CertificateNo { get; set; } = string.Empty;
    public string Exportateur { get; set; } = string.Empty;
    public string? Partenaire { get; set; }
    public string? PaysDestination { get; set; }
    public string? PortSortie { get; set; }
    public string? PortCongo { get; set; }
    public string? Type { get; set; }
    public string? Formule { get; set; }
    public string? Mandataire { get; set; }
    public Guid? StatutCertificatId { get; set; }
    public string? StatutNom { get; set; }
    public string? Observation { get; set; }
    public Guid? CarnetAdresseId { get; set; }
    public string? Navire { get; set; }
    public Guid? DocumentsId { get; set; }
    public DateTime CreeLe { get; set; }
    public string? CreePar { get; set; }
    public DateTime? ModifierLe { get; set; }
    public string? ModifiePar { get; set; }
    public List<CertificateLineDto> CertificateLines { get; set; } = new();
    public List<CertificateValidationDto> CertificateValidations { get; set; } = new();
    public List<CommentaireDto> Commentaires { get; set; } = new();
    public Guid? AbonnementId { get; set; }
    public AbonnementDto? Abonnement { get; set; }
}

/// <summary>
/// DTO pour créer un certificat d'origine
/// </summary>
public class CreerCertificatOrigineDto
{
    public string CertificateNo { get; set; } = string.Empty;
    public Guid? ExportateurId { get; set; }
    public Guid? PartenaireId { get; set; }
    public Guid? PaysDestinationId { get; set; }
    public Guid? PortSortieId { get; set; }
    public Guid? PortCongoId { get; set; }
    public Guid? TypeId { get; set; }
    public string? Formule { get; set; }
    public string? Mandataire { get; set; }
    public string? Observation { get; set; }
    public Guid? CarnetAdresseId { get; set; }
    public string? Navire { get; set; }
    public Guid? DocumentsId { get; set; }
    public Guid? ZoneProductionId { get; set; }
    public Guid? BureauDedouanementId { get; set; }
    public Guid? ModuleId { get; set; }
    public Guid? DeviseId { get; set; }
    public List<CreerCertificateLineDto> CertificateLines { get; set; } = new();
}

/// <summary>
/// DTO pour modifier un certificat d'origine
/// </summary>
public class ModifierCertificatOrigineDto
{
    public Guid? ExportateurId { get; set; }
    public Guid? PartenaireId { get; set; }
    public Guid? PaysDestinationId { get; set; }
    public Guid? PortSortieId { get; set; }
    public Guid? PortCongoId { get; set; }
    public Guid? TypeId { get; set; }
    public string? Formule { get; set; }
    public string? Mandataire { get; set; }
    public string? Observation { get; set; }
    public Guid? CarnetAdresseId { get; set; }
    public string? Navire { get; set; }
    public Guid? DocumentsId { get; set; }
    public Guid? ZoneProductionId { get; set; }
    public Guid? BureauDedouanementId { get; set; }
    public Guid? ModuleId { get; set; }
    public Guid? DeviseId { get; set; }
}

