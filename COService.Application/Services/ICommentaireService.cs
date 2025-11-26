using COService.Application.DTOs;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des commentaires
/// </summary>
public interface ICommentaireService
{
    Task<CommentaireDto> CreerCommentaireAsync(Guid certificateId, CreerCommentaireDto dto, string? utilisateur = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<CommentaireDto>> GetCommentairesByCertificateIdAsync(Guid certificateId, CancellationToken cancellationToken = default);
    Task<CommentaireDto?> GetCommentaireByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<CommentaireDto> ModifierCommentaireAsync(Guid id, ModifierCommentaireDto dto, string? utilisateur = null, CancellationToken cancellationToken = default);
    Task SupprimerCommentaireAsync(Guid id, CancellationToken cancellationToken = default);
}

