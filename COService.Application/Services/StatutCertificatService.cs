using AutoMapper;
using COService.Application.DTOs;
using COService.Application.Repositories;

namespace COService.Application.Services;

/// <summary>
/// Service pour la gestion des statuts de certificats
/// </summary>
public class StatutCertificatService : IStatutCertificatService
{
    private readonly IStatutCertificatRepository _repository;
    private readonly IMapper _mapper;

    public StatutCertificatService(IStatutCertificatRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StatutCertificatDto>> GetAllStatutsAsync(CancellationToken cancellationToken = default)
    {
        var statuts = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<StatutCertificatDto>>(statuts);
    }

    public async Task<StatutCertificatDto?> GetStatutByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var statut = await _repository.GetByIdAsync(id, cancellationToken);
        return statut == null ? null : _mapper.Map<StatutCertificatDto>(statut);
    }

    public async Task<StatutCertificatDto?> GetStatutByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var statut = await _repository.GetByCodeAsync(code, cancellationToken);
        return statut == null ? null : _mapper.Map<StatutCertificatDto>(statut);
    }
}
