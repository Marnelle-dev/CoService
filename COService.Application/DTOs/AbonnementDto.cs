namespace COService.Application.DTOs;

/// <summary>
/// DTO pour un abonnement
/// </summary>
public class AbonnementDto
{
    public Guid Id { get; set; }
    public string Exportateur { get; set; } = string.Empty;
    public string Partenaire { get; set; } = string.Empty;
    public string? TypeCO { get; set; }
    public string? FactureNo { get; set; }
    public string? Formule { get; set; }
    public string? Numero { get; set; }
    public string? Statut { get; set; }
    public DateTime? CreeLe { get; set; }
    public string? CreePar { get; set; }
    public DateTime? ModifierLe { get; set; }
    public string? ModifiePar { get; set; }
    public List<CertificatOrigineDto> Certificats { get; set; } = new();
    public int NombreCertificats { get; set; }
}

/// <summary>
/// DTO pour créer un abonnement
/// </summary>
public class CreerAbonnementDto
{
    public string Exportateur { get; set; } = string.Empty;
    public string Partenaire { get; set; } = string.Empty;
    public string? TypeCO { get; set; }
    public string? FactureNo { get; set; }
    public string? Formule { get; set; }
    public string? Numero { get; set; }
    public string? Statut { get; set; }
    public List<Guid>? CertificateIds { get; set; } // IDs des certificats à rattacher
}

/// <summary>
/// DTO pour modifier un abonnement
/// </summary>
public class ModifierAbonnementDto
{
    public string? Exportateur { get; set; }
    public string? Partenaire { get; set; }
    public string? TypeCO { get; set; }
    public string? FactureNo { get; set; }
    public string? Formule { get; set; }
    public string? Numero { get; set; }
    public string? Statut { get; set; }
}

