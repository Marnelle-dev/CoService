using AutoMapper;
using COService.Application.DTOs;
using COService.Application.Repositories;
using COService.Domain.Entities;
using COService.Shared.Constants;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des certificats d'origine
/// </summary>
public class CertificatOrigineService : ICertificatOrigineService
{
    private readonly ICertificatOrigineRepository _repository;
    private readonly ICertificateLineRepository _lineRepository;
    private readonly IStatutCertificatRepository _statutRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CertificatOrigineService(
        ICertificatOrigineRepository repository,
        ICertificateLineRepository lineRepository,
        IStatutCertificatRepository statutRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _lineRepository = lineRepository;
        _statutRepository = statutRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<CertificatOrigineDto> CreerCertificatAsync(CreerCertificatOrigineDto dto, string? utilisateur = null, CancellationToken cancellationToken = default)
    {
        // Vérifier si le numéro de certificat existe déjà
        if (await _repository.ExistsAsync(dto.CertificateNo, cancellationToken))
        {
            throw new InvalidOperationException($"Un certificat avec le numéro {dto.CertificateNo} existe déjà.");
        }

        var certificat = _mapper.Map<CertificatOrigine>(dto);
        certificat.CreePar = utilisateur;

        // Assigner le statut "Élaboré" par défaut lors de la création
        var statutElabore = await _statutRepository.GetByCodeAsync(StatutsCertificats.Elabore, cancellationToken);
        if (statutElabore == null)
        {
            throw new InvalidOperationException($"Le statut '{StatutsCertificats.Elabore}' est introuvable dans la base de données. Veuillez exécuter le script d'insertion des statuts.");
        }
        certificat.StatutCertificatId = statutElabore.Id;
        certificat.StatutCertificat = statutElabore;

        // Créer les lignes si fournies
        if (dto.CertificateLines.Any())
        {
            foreach (var ligneDto in dto.CertificateLines)
            {
                var ligne = _mapper.Map<CertificateLine>(ligneDto);
                ligne.CertificateId = certificat.Id;
                ligne.CreePar = utilisateur;
                certificat.CertificateLines.Add(ligne);
            }
        }

        await _repository.AddAsync(certificat, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CertificatOrigineDto>(certificat);
    }

    public async Task<IEnumerable<CertificatOrigineDto>> GetAllCertificatsAsync(CancellationToken cancellationToken = default)
    {
        var certificats = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<CertificatOrigineDto>>(certificats);
    }

    public async Task<CertificatOrigineDto?> GetCertificatByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var certificat = await _repository.GetByIdAsync(id, cancellationToken);
        return certificat == null ? null : _mapper.Map<CertificatOrigineDto>(certificat);
    }

    public async Task<CertificatOrigineDto?> GetCertificatByNoAsync(string certificateNo, CancellationToken cancellationToken = default)
    {
        var certificat = await _repository.GetByCertificateNoAsync(certificateNo, cancellationToken);
        return certificat == null ? null : _mapper.Map<CertificatOrigineDto>(certificat);
    }

    public async Task<CertificatOrigineDto> ModifierCertificatAsync(Guid id, ModifierCertificatOrigineDto dto, string? utilisateur = null, CancellationToken cancellationToken = default)
    {
        var certificat = await _repository.GetByIdAsync(id, cancellationToken);
        if (certificat == null)
        {
            throw new KeyNotFoundException($"Certificat avec l'ID {id} introuvable.");
        }

        _mapper.Map(dto, certificat);
        certificat.ModifiePar = utilisateur;
        certificat.ModifierLe = DateTime.UtcNow;

        _repository.Update(certificat);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CertificatOrigineDto>(certificat);
    }

    public async Task SupprimerCertificatAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var certificat = await _repository.GetByIdAsync(id, cancellationToken);
        if (certificat == null)
        {
            throw new KeyNotFoundException($"Certificat avec l'ID {id} introuvable.");
        }

        _repository.Remove(certificat);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<CertificatOrigineDto>> GetCertificatsByExportateurAsync(string exportateur, CancellationToken cancellationToken = default)
    {
        var certificats = await _repository.GetByExportateurAsync(exportateur, cancellationToken);
        return _mapper.Map<IEnumerable<CertificatOrigineDto>>(certificats);
    }

    public async Task<IEnumerable<CertificatOrigineDto>> GetCertificatsByStatutAsync(string statutNom, CancellationToken cancellationToken = default)
    {
        var certificats = await _repository.GetByStatutNomAsync(statutNom, cancellationToken);
        return _mapper.Map<IEnumerable<CertificatOrigineDto>>(certificats);
    }

    public async Task<IEnumerable<CertificatOrigineDto>> GetCertificatsByStatutIdAsync(Guid statutCertificatId, CancellationToken cancellationToken = default)
    {
        var certificats = await _repository.GetByStatutAsync(statutCertificatId, cancellationToken);
        return _mapper.Map<IEnumerable<CertificatOrigineDto>>(certificats);
    }

    public async Task<IEnumerable<CertificatOrigineDto>> GetCertificatsByPaysDestinationAsync(string paysDestination, CancellationToken cancellationToken = default)
    {
        var certificats = await _repository.GetByPaysDestinationAsync(paysDestination, cancellationToken);
        return _mapper.Map<IEnumerable<CertificatOrigineDto>>(certificats);
    }
}

