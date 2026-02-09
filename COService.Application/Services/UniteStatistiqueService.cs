using AutoMapper;
using COService.Application.DTOs;
using COService.Application.Repositories;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des unités statistiques
/// </summary>
public class UniteStatistiqueService : IUniteStatistiqueService
{
    private readonly IUniteStatistiqueRepository _repository;
    private readonly IMapper _mapper;

    public UniteStatistiqueService(IUniteStatistiqueRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UniteStatistiqueDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var unite = await _repository.GetByIdAsync(id, cancellationToken);
        return unite == null ? null : _mapper.Map<UniteStatistiqueDto>(unite);
    }

    public async Task<UniteStatistiqueDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var unite = await _repository.GetByCodeAsync(code, cancellationToken);
        return unite == null ? null : _mapper.Map<UniteStatistiqueDto>(unite);
    }

    public async Task<IEnumerable<UniteStatistiqueDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var unites = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<UniteStatistiqueDto>>(unites);
    }

    public async Task<IEnumerable<UniteStatistiqueDto>> GetActifsAsync(CancellationToken cancellationToken = default)
    {
        var unites = await _repository.GetActifsAsync(cancellationToken);
        return _mapper.Map<IEnumerable<UniteStatistiqueDto>>(unites);
    }
}
