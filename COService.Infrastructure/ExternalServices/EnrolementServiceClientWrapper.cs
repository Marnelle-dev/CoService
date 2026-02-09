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
        
        // Utiliser Apache APISIX API Gateway au lieu de la découverte de service directe
        // APISIX gère le routage et la découverte de service via Consul
        var apiGatewayUrl = configuration.GetValue<string>("ApiGateway:BaseUrl") 
            ?? throw new InvalidOperationException("ApiGateway:BaseUrl non configuré");
        
        var enrolementConfig = configuration.GetSection("ExternalServices:EnrolementService");
        var enrolementPath = enrolementConfig.GetValue<string>("Path") ?? "/api/enrolement";
        var timeout = enrolementConfig.GetValue<int>("Timeout", 30);
        
        // APISIX route vers /api/enrolement/... (configuré dans APISIX via etcd)
        var baseAddress = $"{apiGatewayUrl.TrimEnd('/')}{enrolementPath}";
        
        _client = RestService.For<IEnrolementServiceClient>(
            new HttpClient
            {
                BaseAddress = new Uri(baseAddress),
                Timeout = TimeSpan.FromSeconds(timeout)
            });

        _logger.LogInformation("Client Enrolement configuré via API Gateway: {BaseAddress}", baseAddress);
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
