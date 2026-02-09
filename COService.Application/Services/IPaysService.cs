using COService.Application.DTOs;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des pays
/// Lecture seule - synchronisés depuis le microservice Référentiel
/// </summary>
public interface IPaysService
{
    Task<PaysDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PaysDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<PaysDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<PaysDto>> GetActifsAsync(CancellationToken cancellationToken = default);
}
