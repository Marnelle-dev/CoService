using AutoMapper;
using COService.Application.DTOs;
using COService.Application.Repositories;
using COService.Domain.Entities;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des commentaires
/// </summary>
public class CommentaireService : ICommentaireService
{
    private readonly ICommentaireRepository _repository;
    private readonly ICertificatOrigineRepository _certificatRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CommentaireService(
        ICommentaireRepository repository,
        ICertificatOrigineRepository certificatRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _certificatRepository = certificatRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<CommentaireDto> CreerCommentaireAsync(Guid certificateId, CreerCommentaireDto dto, string? utilisateur = null, CancellationToken cancellationToken = default)
    {
        // Vérifier que le certificat existe
        var certificat = await _certificatRepository.GetByIdAsync(certificateId, cancellationToken);
        if (certificat == null)
        {
            throw new KeyNotFoundException($"Certificat avec l'ID {certificateId} introuvable.");
        }

        var commentaire = _mapper.Map<Commentaire>(dto);
        commentaire.CertificateId = certificateId;
        commentaire.CreePar = utilisateur;

        await _repository.AddAsync(commentaire, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CommentaireDto>(commentaire);
    }

    public async Task<IEnumerable<CommentaireDto>> GetCommentairesByCertificateIdAsync(Guid certificateId, CancellationToken cancellationToken = default)
    {
        var commentaires = await _repository.GetByCertificateIdAsync(certificateId, cancellationToken);
        return _mapper.Map<IEnumerable<CommentaireDto>>(commentaires);
    }

    public async Task<CommentaireDto?> GetCommentaireByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var commentaire = await _repository.GetByIdAsync(id, cancellationToken);
        return commentaire == null ? null : _mapper.Map<CommentaireDto>(commentaire);
    }

    public async Task<CommentaireDto> ModifierCommentaireAsync(Guid id, ModifierCommentaireDto dto, string? utilisateur = null, CancellationToken cancellationToken = default)
    {
        var commentaire = await _repository.GetByIdAsync(id, cancellationToken);
        if (commentaire == null)
        {
            throw new KeyNotFoundException($"Commentaire avec l'ID {id} introuvable.");
        }

        _mapper.Map(dto, commentaire);
        commentaire.ModifiePar = utilisateur;
        commentaire.ModifierLe = DateTime.UtcNow;

        _repository.Update(commentaire);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CommentaireDto>(commentaire);
    }

    public async Task SupprimerCommentaireAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var commentaire = await _repository.GetByIdAsync(id, cancellationToken);
        if (commentaire == null)
        {
            throw new KeyNotFoundException($"Commentaire avec l'ID {id} introuvable.");
        }

        _repository.Remove(commentaire);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

