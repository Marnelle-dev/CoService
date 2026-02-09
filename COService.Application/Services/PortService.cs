using AutoMapper;
using COService.Application.DTOs;
using COService.Application.Repositories;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des ports
/// </summary>
public class PortService : IPortService
{
    private readonly IPortRepository _repository;
    private readonly IMapper _mapper;

    public PortService(IPortRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PortDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var port = await _repository.GetByIdAsync(id, cancellationToken);
        return port == null ? null : MapToDto(port);
    }

    public async Task<PortDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var port = await _repository.GetByCodeAsync(code, cancellationToken);
        return port == null ? null : MapToDto(port);
    }

    public async Task<IEnumerable<PortDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var ports = await _repository.GetAllAsync(cancellationToken);
        return ports.Select(MapToDto);
    }

    public async Task<IEnumerable<PortDto>> GetActifsAsync(CancellationToken cancellationToken = default)
    {
        var ports = await _repository.GetActifsAsync(cancellationToken);
        return ports.Select(MapToDto);
    }

    public async Task<IEnumerable<PortDto>> GetByPaysAsync(Guid paysId, CancellationToken cancellationToken = default)
    {
        var ports = await _repository.GetByPaysAsync(paysId, cancellationToken);
        return ports.Select(MapToDto);
    }

    public async Task<IEnumerable<PortDto>> GetByTypeAsync(string type, CancellationToken cancellationToken = default)
    {
        var ports = await _repository.GetByTypeAsync(type, cancellationToken);
        return ports.Select(MapToDto);
    }

    private PortDto MapToDto(Domain.Entities.Port port)
    {
        var dto = _mapper.Map<PortDto>(port);
        dto.PaysNom = port.Pays?.Nom;
        return dto;
    }
}
