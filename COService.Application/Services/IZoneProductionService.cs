using COService.Application.DTOs;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des zones de production
/// Gérées localement par COService
/// </summary>
public interface IZoneProductionService
{
    Task<ZoneProductionDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ZoneProductionDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ZoneProductionDto>> GetByPartenaireAsync(Guid partenaireId, CancellationToken cancellationToken = default);
    Task<ZoneProductionDto> CreerZoneProductionAsync(CreerZoneProductionDto dto, string? utilisateur = null, CancellationToken cancellationToken = default);
    Task<ZoneProductionDto> ModifierZoneProductionAsync(Guid id, ModifierZoneProductionDto dto, string? utilisateur = null, CancellationToken cancellationToken = default);
    Task SupprimerZoneProductionAsync(Guid id, CancellationToken cancellationToken = default);
}

/// <summary>
/// DTO pour créer une zone de production
/// </summary>
public class CreerZoneProductionDto
{
    public Guid PartenaireId { get; set; }
    public string Nom { get; set; } = string.Empty;
    public string? Description { get; set; }
}

/// <summary>
/// DTO pour modifier une zone de production
/// </summary>
public class ModifierZoneProductionDto
{
    public string? Nom { get; set; }
    public string? Description { get; set; }
}
