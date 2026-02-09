using AutoMapper;
using COService.Application.DTOs;
using COService.Application.Repositories;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des devises
/// </summary>
public class DeviseService : IDeviseService
{
    private readonly IDeviseRepository _repository;
    private readonly IMapper _mapper;

    public DeviseService(IDeviseRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<DeviseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var devise = await _repository.GetByIdAsync(id, cancellationToken);
        return devise == null ? null : _mapper.Map<DeviseDto>(devise);
    }

    public async Task<DeviseDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var devise = await _repository.GetByCodeAsync(code, cancellationToken);
        return devise == null ? null : _mapper.Map<DeviseDto>(devise);
    }

    public async Task<IEnumerable<DeviseDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var devises = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<DeviseDto>>(devises);
    }

    public async Task<IEnumerable<DeviseDto>> GetActifsAsync(CancellationToken cancellationToken = default)
    {
        var devises = await _repository.GetActifsAsync(cancellationToken);
        return _mapper.Map<IEnumerable<DeviseDto>>(devises);
    }
}
