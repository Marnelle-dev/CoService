using AutoMapper;
using COService.Application.DTOs;
using COService.Application.Repositories;
using COService.Domain.Entities;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des lignes de certificat
/// </summary>
public class CertificateLineService : ICertificateLineService
{
    private readonly ICertificateLineRepository _repository;
    private readonly ICertificatOrigineRepository _certificatRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CertificateLineService(
        ICertificateLineRepository repository,
        ICertificatOrigineRepository certificatRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _certificatRepository = certificatRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<CertificateLineDto> CreerLigneAsync(Guid certificateId, CreerCertificateLineDto dto, string? utilisateur = null, CancellationToken cancellationToken = default)
    {
        // Vérifier que le certificat existe
        var certificat = await _certificatRepository.GetByIdAsync(certificateId, cancellationToken);
        if (certificat == null)
        {
            throw new KeyNotFoundException($"Certificat avec l'ID {certificateId} introuvable.");
        }

        var ligne = _mapper.Map<CertificateLine>(dto);
        ligne.CertificateId = certificateId;
        ligne.CreePar = utilisateur;

        await _repository.AddAsync(ligne, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CertificateLineDto>(ligne);
    }

    public async Task<IEnumerable<CertificateLineDto>> GetLignesByCertificateIdAsync(Guid certificateId, CancellationToken cancellationToken = default)
    {
        var lignes = await _repository.GetByCertificateIdAsync(certificateId, cancellationToken);
        return _mapper.Map<IEnumerable<CertificateLineDto>>(lignes);
    }

    public async Task<CertificateLineDto?> GetLigneByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var ligne = await _repository.GetByIdAsync(id, cancellationToken);
        return ligne == null ? null : _mapper.Map<CertificateLineDto>(ligne);
    }

    public async Task<CertificateLineDto> ModifierLigneAsync(Guid id, ModifierCertificateLineDto dto, string? utilisateur = null, CancellationToken cancellationToken = default)
    {
        var ligne = await _repository.GetByIdAsync(id, cancellationToken);
        if (ligne == null)
        {
            throw new KeyNotFoundException($"Ligne avec l'ID {id} introuvable.");
        }

        _mapper.Map(dto, ligne);
        ligne.ModifiePar = utilisateur;
        ligne.ModifierLe = DateTime.UtcNow;

        _repository.Update(ligne);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CertificateLineDto>(ligne);
    }

    public async Task SupprimerLigneAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var ligne = await _repository.GetByIdAsync(id, cancellationToken);
        if (ligne == null)
        {
            throw new KeyNotFoundException($"Ligne avec l'ID {id} introuvable.");
        }

        _repository.Remove(ligne);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

