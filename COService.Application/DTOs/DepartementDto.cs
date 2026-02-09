namespace COService.Application.DTOs;

/// <summary>
/// DTO représentant un département
/// </summary>
public class DepartementDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Nom { get; set; } = string.Empty;
    public bool Actif { get; set; }
    public DateTime? CreeLe { get; set; }
    public string? CreePar { get; set; }
    public DateTime? ModifierLe { get; set; }
    public string? ModifiePar { get; set; }
}
