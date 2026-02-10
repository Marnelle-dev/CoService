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
    private readonly IPartenaireRepository _partenaireRepository;
    private readonly IExportateurRepository _exportateurRepository;
    private readonly IPaysRepository _paysRepository;
    private readonly IPortRepository _portRepository;
    private readonly ICertificateTypeRepository _typeRepository;
    private readonly IZoneProductionRepository _zoneProductionRepository;
    private readonly IBureauDedouanementRepository _bureauDedouanementRepository;
    private readonly IModuleRepository _moduleRepository;
    private readonly IDeviseRepository _deviseRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CertificatOrigineService(
        ICertificatOrigineRepository repository,
        ICertificateLineRepository lineRepository,
        IStatutCertificatRepository statutRepository,
        IPartenaireRepository partenaireRepository,
        IExportateurRepository exportateurRepository,
        IPaysRepository paysRepository,
        IPortRepository portRepository,
        ICertificateTypeRepository typeRepository,
        IZoneProductionRepository zoneProductionRepository,
        IBureauDedouanementRepository bureauDedouanementRepository,
        IModuleRepository moduleRepository,
        IDeviseRepository deviseRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _lineRepository = lineRepository;
        _statutRepository = statutRepository;
        _partenaireRepository = partenaireRepository;
        _exportateurRepository = exportateurRepository;
        _paysRepository = paysRepository;
        _portRepository = portRepository;
        _typeRepository = typeRepository;
        _zoneProductionRepository = zoneProductionRepository;
        _bureauDedouanementRepository = bureauDedouanementRepository;
        _moduleRepository = moduleRepository;
        _deviseRepository = deviseRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Valide que toutes les clés étrangères fournies existent dans la base de données
    /// </summary>
    private async Task ValiderClesEtrangeresAsync(CreerCertificatOrigineDto dto, CancellationToken cancellationToken)
    {
        var erreurs = new List<string>();

        // Valider ExportateurId
        if (dto.ExportateurId.HasValue)
        {
            var exportateurExists = await _exportateurRepository.ExistsAsync(dto.ExportateurId.Value, cancellationToken);
            if (!exportateurExists)
            {
                erreurs.Add($"L'exportateur avec l'ID {dto.ExportateurId.Value} n'existe pas.");
            }
        }

        // Valider PartenaireId
        if (dto.PartenaireId.HasValue)
        {
            var partenaireExists = await _partenaireRepository.ExistsAsync(dto.PartenaireId.Value, cancellationToken);
            if (!partenaireExists)
            {
                erreurs.Add($"Le partenaire avec l'ID {dto.PartenaireId.Value} n'existe pas.");
            }
        }

        // Valider PaysDestinationId
        if (dto.PaysDestinationId.HasValue)
        {
            var paysExists = await _paysRepository.ExistsAsync(dto.PaysDestinationId.Value, cancellationToken);
            if (!paysExists)
            {
                erreurs.Add($"Le pays avec l'ID {dto.PaysDestinationId.Value} n'existe pas.");
            }
        }

        // Valider PortSortieId
        if (dto.PortSortieId.HasValue)
        {
            var portExists = await _portRepository.ExistsAsync(dto.PortSortieId.Value, cancellationToken);
            if (!portExists)
            {
                erreurs.Add($"Le port de sortie avec l'ID {dto.PortSortieId.Value} n'existe pas.");
            }
        }

        // Valider PortCongoId
        if (dto.PortCongoId.HasValue)
        {
            var portExists = await _portRepository.ExistsAsync(dto.PortCongoId.Value, cancellationToken);
            if (!portExists)
            {
                erreurs.Add($"Le port Congo avec l'ID {dto.PortCongoId.Value} n'existe pas.");
            }
        }

        // Valider TypeId
        if (dto.TypeId.HasValue)
        {
            var type = await _typeRepository.GetByIdAsync(dto.TypeId.Value, cancellationToken);
            if (type == null)
            {
                erreurs.Add($"Le type de certificat avec l'ID {dto.TypeId.Value} n'existe pas.");
            }
        }

        // Valider ZoneProductionId
        if (dto.ZoneProductionId.HasValue)
        {
            var zoneExists = await _zoneProductionRepository.ExistsAsync(dto.ZoneProductionId.Value, cancellationToken);
            if (!zoneExists)
            {
                erreurs.Add($"La zone de production avec l'ID {dto.ZoneProductionId.Value} n'existe pas.");
            }
        }

        // Valider BureauDedouanementId
        if (dto.BureauDedouanementId.HasValue)
        {
            var bureauExists = await _bureauDedouanementRepository.ExistsAsync(dto.BureauDedouanementId.Value, cancellationToken);
            if (!bureauExists)
            {
                erreurs.Add($"Le bureau de douane avec l'ID {dto.BureauDedouanementId.Value} n'existe pas.");
            }
        }

        // Valider ModuleId
        if (dto.ModuleId.HasValue)
        {
            var moduleExists = await _moduleRepository.ExistsAsync(dto.ModuleId.Value, cancellationToken);
            if (!moduleExists)
            {
                erreurs.Add($"Le module avec l'ID {dto.ModuleId.Value} n'existe pas.");
            }
        }

        // Valider DeviseId
        if (dto.DeviseId.HasValue)
        {
            var deviseExists = await _deviseRepository.ExistsAsync(dto.DeviseId.Value, cancellationToken);
            if (!deviseExists)
            {
                erreurs.Add($"La devise avec l'ID {dto.DeviseId.Value} n'existe pas.");
            }
        }

        // Note: CarnetAdresseId n'est pas validé car le repository n'existe pas encore
        // DocumentsId n'est pas validé car c'est une référence externe au microservice Documents

        if (erreurs.Any())
        {
            throw new InvalidOperationException($"Erreurs de validation des clés étrangères:\n{string.Join("\n", erreurs)}");
        }
    }

    public async Task<CertificatOrigineDto> CreerCertificatAsync(CreerCertificatOrigineDto dto, string? utilisateur = null, CancellationToken cancellationToken = default)
    {
        // Vérifier si le numéro de certificat existe déjà
        if (await _repository.ExistsAsync(dto.CertificateNo, cancellationToken))
        {
            throw new InvalidOperationException($"Un certificat avec le numéro {dto.CertificateNo} existe déjà.");
        }

        // Valider les clés étrangères avant de créer le certificat
        await ValiderClesEtrangeresAsync(dto, cancellationToken);

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

