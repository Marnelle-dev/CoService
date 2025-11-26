using Consul;
using Microsoft.Extensions.Logging;

namespace COService.Infrastructure.Services;

/// <summary>
/// Implémentation de la découverte de services via Consul
/// </summary>
public class ServiceDiscovery : IServiceDiscovery
{
    private readonly IConsulClient _consulClient;
    private readonly ILogger<ServiceDiscovery> _logger;

    public ServiceDiscovery(IConsulClient consulClient, ILogger<ServiceDiscovery> logger)
    {
        _consulClient = consulClient;
        _logger = logger;
    }

    public async Task<string?> DiscoverServiceAsync(string serviceName)
    {
        try
        {
            var queryOptions = new QueryOptions { WaitTime = TimeSpan.FromSeconds(5) };
            var services = await _consulClient.Health.Service(serviceName, "", true, queryOptions);
            
            if (services.Response == null || !services.Response.Any())
            {
                _logger.LogWarning("Aucun service '{ServiceName}' trouvé dans Consul", serviceName);
                return null;
            }

            // Prendre le premier service disponible (on pourrait implémenter du load balancing ici)
            var service = services.Response.First();
            var address = $"http://{service.Service.Address}:{service.Service.Port}";
            
            _logger.LogInformation(
                "Service '{ServiceName}' découvert à l'adresse {Address}",
                serviceName,
                address);

            return address;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la découverte du service '{ServiceName}'", serviceName);
            return null;
        }
    }

    public async Task<List<string>> DiscoverServiceInstancesAsync(string serviceName)
    {
        try
        {
            var queryOptions = new QueryOptions { WaitTime = TimeSpan.FromSeconds(5) };
            var services = await _consulClient.Health.Service(serviceName, "", true, queryOptions);
            
            if (services.Response == null || !services.Response.Any())
            {
                _logger.LogWarning("Aucune instance du service '{ServiceName}' trouvée dans Consul", serviceName);
                return new List<string>();
            }

            var addresses = services.Response
                .Select(s => $"http://{s.Service.Address}:{s.Service.Port}")
                .ToList();

            _logger.LogInformation(
                "{Count} instance(s) du service '{ServiceName}' trouvée(s)",
                addresses.Count,
                serviceName);

            return addresses;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la découverte des instances du service '{ServiceName}'", serviceName);
            return new List<string>();
        }
    }
}

