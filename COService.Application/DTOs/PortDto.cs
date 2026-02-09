namespace COService.Application.DTOs;

/// <summary>
/// DTO représentant un port (maritime ou fluvial)
/// </summary>
public class PortDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Nom { get; set; } = string.Empty;
    public Guid? PaysId { get; set; }
    public string? PaysNom { get; set; }
    public string? Type { get; set; } // Maritime, Fluvial
    public bool Actif { get; set; }
    public DateTime? CreeLe { get; set; }
    public string? CreePar { get; set; }
    public DateTime? ModifierLe { get; set; }
    public string? ModifiePar { get; set; }
}
