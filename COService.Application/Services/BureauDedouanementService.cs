using AutoMapper;
using COService.Application.DTOs;
using COService.Application.Repositories;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des bureaux de douane
/// </summary>
public class BureauDedouanementService : IBureauDedouanementService
{
    private readonly IBureauDedouanementRepository _repository;
    private readonly IMapper _mapper;

    public BureauDedouanementService(IBureauDedouanementRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BureauDedouanementDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var bureau = await _repository.GetByIdAsync(id, cancellationToken);
        return bureau == null ? null : _mapper.Map<BureauDedouanementDto>(bureau);
    }

    public async Task<BureauDedouanementDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var bureau = await _repository.GetByCodeAsync(code, cancellationToken);
        return bureau == null ? null : _mapper.Map<BureauDedouanementDto>(bureau);
    }

    public async Task<IEnumerable<BureauDedouanementDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var bureaux = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<BureauDedouanementDto>>(bureaux);
    }

    public async Task<IEnumerable<BureauDedouanementDto>> GetActifsAsync(CancellationToken cancellationToken = default)
    {
        var bureaux = await _repository.GetActifsAsync(cancellationToken);
        return _mapper.Map<IEnumerable<BureauDedouanementDto>>(bureaux);
    }
}
