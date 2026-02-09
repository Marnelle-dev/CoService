using AutoMapper;
using COService.Application.DTOs;
using COService.Application.Repositories;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des incoterms
/// </summary>
public class IncotermService : IIncotermService
{
    private readonly IIncotermRepository _repository;
    private readonly IMapper _mapper;

    public IncotermService(IIncotermRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IncotermDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var incoterm = await _repository.GetByIdAsync(id, cancellationToken);
        return incoterm == null ? null : MapToDto(incoterm);
    }

    public async Task<IncotermDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var incoterm = await _repository.GetByCodeAsync(code, cancellationToken);
        return incoterm == null ? null : MapToDto(incoterm);
    }

    public async Task<IEnumerable<IncotermDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var incoterms = await _repository.GetAllAsync(cancellationToken);
        return incoterms.Select(MapToDto);
    }

    public async Task<IEnumerable<IncotermDto>> GetActifsAsync(CancellationToken cancellationToken = default)
    {
        var incoterms = await _repository.GetActifsAsync(cancellationToken);
        return incoterms.Select(MapToDto);
    }

    public async Task<IEnumerable<IncotermDto>> GetByModuleAsync(Guid moduleId, CancellationToken cancellationToken = default)
    {
        var incoterms = await _repository.GetByModuleAsync(moduleId, cancellationToken);
        return incoterms.Select(MapToDto);
    }

    private IncotermDto MapToDto(Domain.Entities.Incoterm incoterm)
    {
        var dto = _mapper.Map<IncotermDto>(incoterm);
        dto.ModuleNom = incoterm.Module?.Nom;
        return dto;
    }
}
