using AutoMapper;
using COService.Application.DTOs;
using COService.Application.Repositories;
using COService.Domain.Entities;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des abonnements
/// </summary>
public class AbonnementService : IAbonnementService
{
    private readonly IAbonnementRepository _repository;
    private readonly ICertificatOrigineRepository _certificatRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public AbonnementService(
        IAbonnementRepository repository,
        ICertificatOrigineRepository certificatRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _certificatRepository = certificatRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<AbonnementDto> CreerAbonnementAsync(CreerAbonnementDto dto, string? utilisateur = null, CancellationToken cancellationToken = default)
    {
        var abonnement = _mapper.Map<Abonnement>(dto);
        abonnement.CreePar = utilisateur;

        // Sauvegarder d'abord l'abonnement pour obtenir son ID
        await _repository.AddAsync(abonnement, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Rattacher les certificats si fournis
        if (dto.CertificateIds != null && dto.CertificateIds.Any())
        {
            foreach (var certificateId in dto.CertificateIds)
            {
                var certificat = await _certificatRepository.GetByIdAsync(certificateId, cancellationToken);
                if (certificat == null)
                {
                    throw new KeyNotFoundException($"Certificat avec l'ID {certificateId} introuvable.");
                }
                certificat.AbonnementId = abonnement.Id;
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        // Recharger l'abonnement avec les certificats
        abonnement = await _repository.GetByIdAsync(abonnement.Id, cancellationToken);
        var result = _mapper.Map<AbonnementDto>(abonnement!);
        result.NombreCertificats = abonnement!.Certificats.Count;
        return result;
    }

    public async Task<IEnumerable<AbonnementDto>> GetAllAbonnementsAsync(CancellationToken cancellationToken = default)
    {
        var abonnements = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<AbonnementDto>>(abonnements);
    }

    public async Task<IEnumerable<AbonnementDto>> GetAbonnementsByExportateurAsync(string exportateur, CancellationToken cancellationToken = default)
    {
        var abonnements = await _repository.GetByExportateurAsync(exportateur, cancellationToken);
        return _mapper.Map<IEnumerable<AbonnementDto>>(abonnements);
    }

    public async Task<IEnumerable<AbonnementDto>> GetAbonnementsByPartenaireAsync(string partenaire, CancellationToken cancellationToken = default)
    {
        var abonnements = await _repository.GetByPartenaireAsync(partenaire, cancellationToken);
        return _mapper.Map<IEnumerable<AbonnementDto>>(abonnements);
    }

    public async Task<AbonnementDto?> GetAbonnementByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var abonnement = await _repository.GetByIdAsync(id, cancellationToken);
        if (abonnement == null)
            return null;

        var dto = _mapper.Map<AbonnementDto>(abonnement);
        dto.NombreCertificats = abonnement.Certificats.Count;
        return dto;
    }

    public async Task<AbonnementDto> ModifierAbonnementAsync(Guid id, ModifierAbonnementDto dto, string? utilisateur = null, CancellationToken cancellationToken = default)
    {
        var abonnement = await _repository.GetByIdAsync(id, cancellationToken);
        if (abonnement == null)
        {
            throw new KeyNotFoundException($"Abonnement avec l'ID {id} introuvable.");
        }

        _mapper.Map(dto, abonnement);
        abonnement.ModifiePar = utilisateur;
        abonnement.ModifierLe = DateTime.UtcNow;

        _repository.Update(abonnement);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<AbonnementDto>(abonnement);
        result.NombreCertificats = abonnement.Certificats.Count;
        return result;
    }

    public async Task SupprimerAbonnementAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var abonnement = await _repository.GetByIdAsync(id, cancellationToken);
        if (abonnement == null)
        {
            throw new KeyNotFoundException($"Abonnement avec l'ID {id} introuvable.");
        }

        // Détacher tous les certificats avant suppression
        foreach (var certificat in abonnement.Certificats)
        {
            certificat.AbonnementId = null;
        }

        _repository.Remove(abonnement);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<AbonnementDto> RattacherCertificatsAsync(Guid abonnementId, List<Guid> certificateIds, CancellationToken cancellationToken = default)
    {
        var abonnement = await _repository.GetByIdAsync(abonnementId, cancellationToken);
        if (abonnement == null)
        {
            throw new KeyNotFoundException($"Abonnement avec l'ID {abonnementId} introuvable.");
        }

        foreach (var certificateId in certificateIds)
        {
            var certificat = await _certificatRepository.GetByIdAsync(certificateId, cancellationToken);
            if (certificat == null)
            {
                throw new KeyNotFoundException($"Certificat avec l'ID {certificateId} introuvable.");
            }
            certificat.AbonnementId = abonnementId;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Recharger l'abonnement avec les certificats
        abonnement = await _repository.GetByIdAsync(abonnementId, cancellationToken);
        var result = _mapper.Map<AbonnementDto>(abonnement!);
        result.NombreCertificats = abonnement!.Certificats.Count;
        return result;
    }

    public async Task<AbonnementDto> DetacherCertificatAsync(Guid abonnementId, Guid certificateId, CancellationToken cancellationToken = default)
    {
        var abonnement = await _repository.GetByIdAsync(abonnementId, cancellationToken);
        if (abonnement == null)
        {
            throw new KeyNotFoundException($"Abonnement avec l'ID {abonnementId} introuvable.");
        }

        var certificat = await _certificatRepository.GetByIdAsync(certificateId, cancellationToken);
        if (certificat == null)
        {
            throw new KeyNotFoundException($"Certificat avec l'ID {certificateId} introuvable.");
        }

        if (certificat.AbonnementId != abonnementId)
        {
            throw new InvalidOperationException($"Le certificat {certificateId} n'est pas rattaché à l'abonnement {abonnementId}.");
        }

        certificat.AbonnementId = null;
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Recharger l'abonnement
        abonnement = await _repository.GetByIdAsync(abonnementId, cancellationToken);
        var result = _mapper.Map<AbonnementDto>(abonnement!);
        result.NombreCertificats = abonnement!.Certificats.Count;
        return result;
    }
}

