namespace COService.Application.DTOs;

/// <summary>
/// DTO pour un type de certificat
/// </summary>
public class CertificateTypeDto
{
    public Guid Id { get; set; }
    public string Designation { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public DateTime? CreeLe { get; set; }
    public string? CreePar { get; set; }
    public DateTime? ModifierLe { get; set; }
    public string? ModifiePar { get; set; }
    public int NombreCertificats { get; set; }
}

/// <summary>
/// DTO pour créer un type de certificat
/// </summary>
public class CreerCertificateTypeDto
{
    public string Designation { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}

/// <summary>
/// DTO pour modifier un type de certificat
/// </summary>
public class ModifierCertificateTypeDto
{
    public string? Designation { get; set; }
    public string? Code { get; set; }
}

