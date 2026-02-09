using AutoMapper;
using COService.Application.DTOs;
using COService.Application.Repositories;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des pays
/// </summary>
public class PaysService : IPaysService
{
    private readonly IPaysRepository _repository;
    private readonly IMapper _mapper;

    public PaysService(IPaysRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PaysDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var pays = await _repository.GetByIdAsync(id, cancellationToken);
        return pays == null ? null : _mapper.Map<PaysDto>(pays);
    }

    public async Task<PaysDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var pays = await _repository.GetByCodeAsync(code, cancellationToken);
        return pays == null ? null : _mapper.Map<PaysDto>(pays);
    }

    public async Task<IEnumerable<PaysDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var pays = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<PaysDto>>(pays);
    }

    public async Task<IEnumerable<PaysDto>> GetActifsAsync(CancellationToken cancellationToken = default)
    {
        var pays = await _repository.GetActifsAsync(cancellationToken);
        return _mapper.Map<IEnumerable<PaysDto>>(pays);
    }
}
