using COService.Application.DTOs;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des exportateurs
/// Lecture seule - synchronisés depuis le microservice Enrolement
/// </summary>
public interface IExportateurService
{
    Task<ExportateurDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ExportateurDto?> GetByCodeAsync(string codeExportateur, CancellationToken cancellationToken = default);
    Task<IEnumerable<ExportateurDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ExportateurDto>> GetActifsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ExportateurDto>> GetByPartenaireAsync(Guid partenaireId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ExportateurDto>> GetByDepartementAsync(Guid departementId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ExportateurDto>> GetByTypeAsync(int typeExportateur, CancellationToken cancellationToken = default);
}
