using COService.Application.DTOs;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des types de partenaires
/// </summary>
public interface ITypePartenaireService
{
    Task<TypePartenaireDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TypePartenaireDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IEnumerable<TypePartenaireDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<TypePartenaireDto>> GetActifsAsync(CancellationToken cancellationToken = default);
}
