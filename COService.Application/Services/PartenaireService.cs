using AutoMapper;
using COService.Application.DTOs;
using COService.Application.Repositories;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des partenaires (Chambres de Commerce)
/// </summary>
public class PartenaireService : IPartenaireService
{
    private readonly IPartenaireRepository _repository;
    private readonly IMapper _mapper;

    public PartenaireService(IPartenaireRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PartenaireDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var partenaire = await _repository.GetByIdAsync(id, cancellationToken);
        return partenaire == null ? null : MapToDto(partenaire);
    }

    public async Task<PartenaireDto?> GetByCodeAsync(string codePartenaire, CancellationToken cancellationToken = default)
    {
        var partenaire = await _repository.GetByCodeAsync(codePartenaire, cancellationToken);
        return partenaire == null ? null : MapToDto(partenaire);
    }

    public async Task<IEnumerable<PartenaireDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var partenaires = await _repository.GetAllAsync(cancellationToken);
        return partenaires.Select(MapToDto);
    }

    public async Task<IEnumerable<PartenaireDto>> GetActifsAsync(CancellationToken cancellationToken = default)
    {
        var partenaires = await _repository.GetActifsAsync(cancellationToken);
        return partenaires.Select(MapToDto);
    }

    public async Task<IEnumerable<PartenaireDto>> GetByTypeAsync(Guid typePartenaireId, CancellationToken cancellationToken = default)
    {
        var partenaires = await _repository.GetByTypeAsync(typePartenaireId, cancellationToken);
        return partenaires.Select(MapToDto);
    }

    public async Task<IEnumerable<PartenaireDto>> GetByDepartementAsync(Guid departementId, CancellationToken cancellationToken = default)
    {
        var partenaires = await _repository.GetByDepartementAsync(departementId, cancellationToken);
        return partenaires.Select(MapToDto);
    }

    private PartenaireDto MapToDto(Domain.Entities.Partenaire partenaire)
    {
        var dto = _mapper.Map<PartenaireDto>(partenaire);
        dto.TypePartenaireNom = partenaire.TypePartenaire?.Nom;
        dto.DepartementNom = partenaire.Departement?.Nom;
        return dto;
    }
}
