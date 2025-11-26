namespace COService.Application.DTOs;

/// <summary>
/// DTO pour une validation de certificat
/// </summary>
public class CertificateValidationDto
{
    public Guid Id { get; set; }
    public Guid CertificateId { get; set; }
    public string Etape { get; set; } = string.Empty;
    public string RoleVisa { get; set; } = string.Empty;
    public string? VisaPar { get; set; }
    public string? Commentaire { get; set; }
    public DateTime? CreeLe { get; set; }
    public string? CreePar { get; set; }
    public DateTime? ModifierLe { get; set; }
    public string? ModifiePar { get; set; }
}

