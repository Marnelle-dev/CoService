using AutoMapper;
using COService.Application.DTOs;
using COService.Application.Repositories;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des types de partenaires
/// </summary>
public class TypePartenaireService : ITypePartenaireService
{
    private readonly ITypePartenaireRepository _repository;
    private readonly IMapper _mapper;

    public TypePartenaireService(ITypePartenaireRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TypePartenaireDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var typePartenaire = await _repository.GetByIdAsync(id);
        return typePartenaire == null ? null : _mapper.Map<TypePartenaireDto>(typePartenaire);
    }

    public async Task<TypePartenaireDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var typePartenaire = await _repository.GetByCodeAsync(code);
        return typePartenaire == null ? null : _mapper.Map<TypePartenaireDto>(typePartenaire);
    }

    public async Task<IEnumerable<TypePartenaireDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var typesPartenaires = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<TypePartenaireDto>>(typesPartenaires);
    }

    public async Task<IEnumerable<TypePartenaireDto>> GetActifsAsync(CancellationToken cancellationToken = default)
    {
        var typesPartenaires = await _repository.GetActifsAsync();
        return _mapper.Map<IEnumerable<TypePartenaireDto>>(typesPartenaires);
    }
}
