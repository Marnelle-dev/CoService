using AutoMapper;
using COService.Application.DTOs;
using COService.Application.Repositories;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des aéroports
/// </summary>
public class AeroportService : IAeroportService
{
    private readonly IAeroportRepository _repository;
    private readonly IMapper _mapper;

    public AeroportService(IAeroportRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AeroportDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var aeroport = await _repository.GetByIdAsync(id, cancellationToken);
        return aeroport == null ? null : MapToDto(aeroport);
    }

    public async Task<AeroportDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var aeroport = await _repository.GetByCodeAsync(code, cancellationToken);
        return aeroport == null ? null : MapToDto(aeroport);
    }

    public async Task<IEnumerable<AeroportDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var aeroports = await _repository.GetAllAsync(cancellationToken);
        return aeroports.Select(MapToDto);
    }

    public async Task<IEnumerable<AeroportDto>> GetActifsAsync(CancellationToken cancellationToken = default)
    {
        var aeroports = await _repository.GetActifsAsync(cancellationToken);
        return aeroports.Select(MapToDto);
    }

    public async Task<IEnumerable<AeroportDto>> GetByPaysAsync(Guid paysId, CancellationToken cancellationToken = default)
    {
        var aeroports = await _repository.GetByPaysAsync(paysId, cancellationToken);
        return aeroports.Select(MapToDto);
    }

    private AeroportDto MapToDto(Domain.Entities.Aeroport aeroport)
    {
        var dto = _mapper.Map<AeroportDto>(aeroport);
        dto.PaysNom = aeroport.Pays?.Nom;
        return dto;
    }
}
