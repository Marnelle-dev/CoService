namespace COService.Application.DTOs;

/// <summary>
/// DTO représentant un exportateur
/// </summary>
public class ExportateurDto
{
    public Guid Id { get; set; }
    public string CodeExportateur { get; set; } = string.Empty;
    public string Nom { get; set; } = string.Empty;
    public string? RaisonSociale { get; set; }
    public string? NIU { get; set; }
    public string? RCCM { get; set; }
    public string? CodeActivite { get; set; }
    public string? Adresse { get; set; }
    public string? Telephone { get; set; }
    public string? Email { get; set; }
    public bool Actif { get; set; }
    public Guid? PartenaireId { get; set; }
    public string? PartenaireNom { get; set; }
    public Guid? DepartementId { get; set; }
    public string? DepartementNom { get; set; }
    public int? TypeExportateur { get; set; }
    public DateTime? DerniereSynchronisation { get; set; }
    public DateTime? CreeLe { get; set; }
    public string? CreePar { get; set; }
    public DateTime? ModifierLe { get; set; }
    public string? ModifiePar { get; set; }
}
