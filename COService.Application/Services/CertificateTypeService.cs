using AutoMapper;
using COService.Application.DTOs;
using COService.Application.Repositories;
using COService.Domain.Entities;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des types de certificats
/// </summary>
public class CertificateTypeService : ICertificateTypeService
{
    private readonly ICertificateTypeRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CertificateTypeService(
        ICertificateTypeRepository repository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<CertificateTypeDto> CreerCertificateTypeAsync(CreerCertificateTypeDto dto, string? utilisateur = null, CancellationToken cancellationToken = default)
    {
        // Vérifier si le code existe déjà
        var existe = await _repository.ExistsByCodeAsync(dto.Code, cancellationToken);
        if (existe)
        {
            throw new InvalidOperationException($"Un type de certificat avec le code '{dto.Code}' existe déjà.");
        }

        var certificateType = _mapper.Map<CertificateType>(dto);
        certificateType.CreePar = utilisateur;
        certificateType.CreeLe = DateTime.UtcNow;

        await _repository.AddAsync(certificateType, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Recharger avec les certificats
        certificateType = await _repository.GetByIdAsync(certificateType.Id, cancellationToken);
        var result = _mapper.Map<CertificateTypeDto>(certificateType!);
        result.NombreCertificats = certificateType!.Certificats.Count;
        return result;
    }

    public async Task<IEnumerable<CertificateTypeDto>> GetAllCertificateTypesAsync(CancellationToken cancellationToken = default)
    {
        var certificateTypes = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<CertificateTypeDto>>(certificateTypes);
    }

    public async Task<CertificateTypeDto?> GetCertificateTypeByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var certificateType = await _repository.GetByIdAsync(id, cancellationToken);
        if (certificateType == null)
            return null;

        var dto = _mapper.Map<CertificateTypeDto>(certificateType);
        dto.NombreCertificats = certificateType.Certificats.Count;
        return dto;
    }

    public async Task<CertificateTypeDto?> GetCertificateTypeByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var certificateType = await _repository.GetByCodeAsync(code, cancellationToken);
        if (certificateType == null)
            return null;

        var dto = _mapper.Map<CertificateTypeDto>(certificateType);
        dto.NombreCertificats = certificateType.Certificats.Count;
        return dto;
    }

    public async Task<CertificateTypeDto> ModifierCertificateTypeAsync(Guid id, ModifierCertificateTypeDto dto, string? utilisateur = null, CancellationToken cancellationToken = default)
    {
        var certificateType = await _repository.GetByIdAsync(id, cancellationToken);
        if (certificateType == null)
        {
            throw new KeyNotFoundException($"Type de certificat avec l'ID {id} introuvable.");
        }

        // Vérifier si le code est modifié et s'il existe déjà
        if (!string.IsNullOrEmpty(dto.Code) && dto.Code != certificateType.Code)
        {
            var existe = await _repository.ExistsByCodeAsync(dto.Code, cancellationToken);
            if (existe)
            {
                throw new InvalidOperationException($"Un type de certificat avec le code '{dto.Code}' existe déjà.");
            }
        }

        _mapper.Map(dto, certificateType);
        certificateType.ModifiePar = utilisateur;
        certificateType.ModifierLe = DateTime.UtcNow;

        _repository.Update(certificateType);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Recharger avec les certificats
        certificateType = await _repository.GetByIdAsync(id, cancellationToken);
        var result = _mapper.Map<CertificateTypeDto>(certificateType!);
        result.NombreCertificats = certificateType!.Certificats.Count;
        return result;
    }

    public async Task SupprimerCertificateTypeAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var certificateType = await _repository.GetByIdAsync(id, cancellationToken);
        if (certificateType == null)
        {
            throw new KeyNotFoundException($"Type de certificat avec l'ID {id} introuvable.");
        }

        // Vérifier si des certificats utilisent ce type
        if (certificateType.Certificats.Any())
        {
            throw new InvalidOperationException($"Impossible de supprimer le type de certificat '{certificateType.Code}' car il est utilisé par {certificateType.Certificats.Count} certificat(s).");
        }

        _repository.Remove(certificateType);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

