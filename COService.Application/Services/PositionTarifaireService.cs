using AutoMapper;
using COService.Application.DTOs;
using COService.Application.Repositories;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des positions tarifaires
/// </summary>
public class PositionTarifaireService : IPositionTarifaireService
{
    private readonly IPositionTarifaireRepository _repository;
    private readonly IMapper _mapper;

    public PositionTarifaireService(IPositionTarifaireRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PositionTarifaireDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var position = await _repository.GetByIdAsync(id, cancellationToken);
        return position == null ? null : MapToDto(position);
    }

    public async Task<PositionTarifaireDto?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var position = await _repository.GetByCodeAsync(code, cancellationToken);
        return position == null ? null : MapToDto(position);
    }

    public async Task<IEnumerable<PositionTarifaireDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var positions = await _repository.GetAllAsync(cancellationToken);
        return positions.Select(MapToDto);
    }

    public async Task<IEnumerable<PositionTarifaireDto>> GetActifsAsync(CancellationToken cancellationToken = default)
    {
        var positions = await _repository.GetActifsAsync(cancellationToken);
        return positions.Select(MapToDto);
    }

    public async Task<IEnumerable<PositionTarifaireDto>> GetByCategorieAsync(Guid categorieId, CancellationToken cancellationToken = default)
    {
        var positions = await _repository.GetByCategorieAsync(categorieId, cancellationToken);
        return positions.Select(MapToDto);
    }

    private PositionTarifaireDto MapToDto(Domain.Entities.PositionTarifaire position)
    {
        var dto = _mapper.Map<PositionTarifaireDto>(position);
        dto.CategorieNom = position.Categorie?.Description;
        return dto;
    }
}
