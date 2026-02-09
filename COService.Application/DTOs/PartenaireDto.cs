namespace COService.Application.DTOs;

/// <summary>
/// DTO représentant un partenaire (Chambre de Commerce)
/// </summary>
public class PartenaireDto
{
    public Guid Id { get; set; }
    public string CodePartenaire { get; set; } = string.Empty;
    public string Nom { get; set; } = string.Empty;
    public string? Adresse { get; set; }
    public string? Telephone { get; set; }
    public string? Email { get; set; }
    public Guid? TypePartenaireId { get; set; }
    public string? TypePartenaireNom { get; set; }
    public Guid? DepartementId { get; set; }
    public string? DepartementNom { get; set; }
    public bool Actif { get; set; }
    public DateTime? DerniereSynchronisation { get; set; }
    public DateTime? CreeLe { get; set; }
    public string? CreePar { get; set; }
    public DateTime? ModifierLe { get; set; }
    public string? ModifiePar { get; set; }
}
