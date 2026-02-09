using COService.Application.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Refit;

namespace COService.Infrastructure.ExternalServices;

/// <summary>
/// Wrapper pour le client Enrolement via l'API Gateway (Apache APISIX)
/// Note: APISIX gère le service discovery via Consul, on utilise directement l'URL d'APISIX
/// </summary>
public class EnrolementServiceClientWrapper : IEnrolementServiceClient
{
    private readonly ILogger<EnrolementServiceClientWrapper> _logger;
    private readonly IEnrolementServiceClient _client;

    public EnrolementServiceClientWrapper(
        ILogger<EnrolementServiceClientWrapper> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        
        // TODO: API Gateway (Apache APISIX) non opérationnel pour l'instant
        // Pour l'instant, utiliser directement l'URL du service Enrolement
        // Quand l'API Gateway sera opérationnel, décommenter la section ci-dessous
        
        // Option 1: Via API Gateway (quand opérationnel)
        // var apiGatewayUrl = configuration.GetValue<string>("ApiGateway:BaseUrl") 
        //     ?? "http://localhost:9080";
        // var enrolementConfig = configuration.GetSection("ExternalServices:EnrolementService");
        // var enrolementPath = enrolementConfig.GetValue<string>("Path") ?? "/api/enrolement";
        // var baseAddress = $"{apiGatewayUrl.TrimEnd('/')}{enrolementPath}";
        
        // Option 2: Directement vers le service Enrolement (temporaire)
        var enrolementServiceUrl = configuration.GetValue<string>("ExternalServices:EnrolementService:BaseUrl")
            ?? "http://localhost:5000"; // URL directe du service Enrolement
        var timeout = configuration.GetSection("ExternalServices:EnrolementService")
            .GetValue<int>("Timeout", 30);
        
        var baseAddress = enrolementServiceUrl.TrimEnd('/');
        
        _client = RestService.For<IEnrolementServiceClient>(
            new HttpClient
            {
                BaseAddress = new Uri(baseAddress),
                Timeout = TimeSpan.FromSeconds(timeout)
            });

        _logger.LogInformation("Client Enrolement configuré directement (API Gateway désactivé): {BaseAddress}", baseAddress);
    }

    public async Task<PartenaireDto> GetPartenaireAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _client.GetPartenaireAsync(id, cancellationToken);
    }

    public async Task<List<PartenaireDto>> GetAllPartenairesAsync(CancellationToken cancellationToken = default)
    {
        return await _client.GetAllPartenairesAsync(cancellationToken);
    }

    public async Task<PartenaireDto?> GetPartenaireByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _client.GetPartenaireByCodeAsync(code, cancellationToken);
    }

    public async Task<ExportateurDto> GetExportateurAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _client.GetExportateurAsync(id, cancellationToken);
    }

    public async Task<List<ExportateurDto>> GetAllExportateursAsync(CancellationToken cancellationToken = default)
    {
        return await _client.GetAllExportateursAsync(cancellationToken);
    }

    public async Task<ExportateurDto?> GetExportateurByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _client.GetExportateurByCodeAsync(code, cancellationToken);
    }

    public async Task<List<ExportateurDto>> GetExportateursByPartenaireAsync(Guid partenaireId, CancellationToken cancellationToken = default)
    {
        return await _client.GetExportateursByPartenaireAsync(partenaireId, cancellationToken);
    }
}
