using AutoMapper;
using COService.Application.DTOs;
using COService.Application.Repositories;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des modules de transport
/// </summary>
public class ModuleService : IModuleService
{
    private readonly IModuleRepository _repository;
    private readonly IMapper _mapper;

    public ModuleService(IModuleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ModuleDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var module = await _repository.GetByIdAsync(id, cancellationToken);
        return module == null ? null : _mapper.Map<ModuleDto>(module);
    }

    public async Task<ModuleDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var module = await _repository.GetByCodeAsync(code, cancellationToken);
        return module == null ? null : _mapper.Map<ModuleDto>(module);
    }

    public async Task<IEnumerable<ModuleDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var modules = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ModuleDto>>(modules);
    }

    public async Task<IEnumerable<ModuleDto>> GetActifsAsync(CancellationToken cancellationToken = default)
    {
        var modules = await _repository.GetActifsAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ModuleDto>>(modules);
    }
}
