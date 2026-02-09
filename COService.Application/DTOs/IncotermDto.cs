namespace COService.Application.DTOs;

/// <summary>
/// DTO représentant un incoterm
/// </summary>
public class IncotermDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid? ModuleId { get; set; }
    public string? ModuleNom { get; set; }
    public bool Actif { get; set; }
    public DateTime? CreeLe { get; set; }
    public string? CreePar { get; set; }
    public DateTime? ModifierLe { get; set; }
    public string? ModifiePar { get; set; }
}
