namespace COService.Application.DTOs;

/// <summary>
/// DTO représentant une position tarifaire (Code HS)
/// </summary>
public class PositionTarifaireDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid? CategorieCodeId { get; set; }
    public string? CategorieNom { get; set; }
    public bool Actif { get; set; }
    public DateTime? CreeLe { get; set; }
    public string? CreePar { get; set; }
    public DateTime? ModifierLe { get; set; }
    public string? ModifiePar { get; set; }
}
