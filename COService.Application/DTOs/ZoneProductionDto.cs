namespace COService.Application.DTOs;

/// <summary>
/// DTO représentant une zone de production
/// </summary>
public class ZoneProductionDto
{
    public Guid Id { get; set; }
    public Guid PartenaireId { get; set; }
    public string? PartenaireNom { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? CreeLe { get; set; }
    public string? CreePar { get; set; }
    public DateTime? ModifierLe { get; set; }
    public string? ModifiePar { get; set; }
}
