using AutoMapper;
using COService.Application.DTOs;
using COService.Application.Repositories;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des exportateurs
/// </summary>
public class ExportateurService : IExportateurService
{
    private readonly IExportateurRepository _repository;
    private readonly IMapper _mapper;

    public ExportateurService(IExportateurRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ExportateurDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var exportateur = await _repository.GetByIdAsync(id, cancellationToken);
        return exportateur == null ? null : MapToDto(exportateur);
    }

    public async Task<ExportateurDto?> GetByCodeAsync(string codeExportateur, CancellationToken cancellationToken = default)
    {
        var exportateur = await _repository.GetByCodeAsync(codeExportateur, cancellationToken);
        return exportateur == null ? null : MapToDto(exportateur);
    }

    public async Task<IEnumerable<ExportateurDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var exportateurs = await _repository.GetAllAsync(cancellationToken);
        return exportateurs.Select(MapToDto);
    }

    public async Task<IEnumerable<ExportateurDto>> GetActifsAsync(CancellationToken cancellationToken = default)
    {
        var exportateurs = await _repository.GetActifsAsync(cancellationToken);
        return exportateurs.Select(MapToDto);
    }

    public async Task<IEnumerable<ExportateurDto>> GetByPartenaireAsync(Guid partenaireId, CancellationToken cancellationToken = default)
    {
        var exportateurs = await _repository.GetByPartenaireAsync(partenaireId, cancellationToken);
        return exportateurs.Select(MapToDto);
    }

    public async Task<IEnumerable<ExportateurDto>> GetByDepartementAsync(Guid departementId, CancellationToken cancellationToken = default)
    {
        var exportateurs = await _repository.GetByDepartementAsync(departementId, cancellationToken);
        return exportateurs.Select(MapToDto);
    }

    public async Task<IEnumerable<ExportateurDto>> GetByTypeAsync(int typeExportateur, CancellationToken cancellationToken = default)
    {
        var exportateurs = await _repository.GetByTypeAsync(typeExportateur, cancellationToken);
        return exportateurs.Select(MapToDto);
    }

    private ExportateurDto MapToDto(Domain.Entities.Exportateur exportateur)
    {
        var dto = _mapper.Map<ExportateurDto>(exportateur);
        dto.PartenaireNom = exportateur.Partenaire?.Nom;
        dto.DepartementNom = exportateur.Departement?.Nom;
        return dto;
    }
}
